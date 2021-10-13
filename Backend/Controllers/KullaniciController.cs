using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tasinmaz_v3.Models;
using Microsoft.AspNetCore.Cors;
using tasinmaz_V3.Models;
using System.Security.Cryptography;
using System.Text;

namespace tasinmaz_v3.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class KullaniciController : Controller
    {
        TasinmazDbContext _db;
        public KullaniciController(TasinmazDbContext db)
        {
            _db = db;
        }
        [HttpPost]
        [Route("{listParameters}")]
        public async Task<IActionResult> Get(ListParameters listParameters)
        {
            listParameters.searchKey = listParameters.searchKey.ToLower();
            var users = _db.kullanicilar
            .Where(
                    x => x.email.ToLower().Contains(listParameters.searchKey) ||
                    x.isim.ToLower().Contains(listParameters.searchKey) ||
                    x.soyisim.ToLower().Contains(listParameters.searchKey) ||
                    (x.role == true ? "kullanici" : "admin").Contains(listParameters.searchKey)
            )
            .Select(data => new
            {
                kullaniciID = data.kullaniciID,
                isim = data.isim,
                soyisim = data.soyisim,
                email = data.email,
                password = data.passwordHash,
                role = data.role,
                isActive = data.isActive
            }).Where(x => x.isActive == true).OrderByDescending(x => x.kullaniciID).Skip((listParameters.PageNumber - 1) * 5).Take(5).ToList();
            Log kullaniciLog = new Log();
            kullaniciLog.islemID = 6;
            kullaniciLog.ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            kullaniciLog.tarihSaat = DateTime.Now;
            if (users == null)
            {
                kullaniciLog.durumID = 2;
                kullaniciLog.aciklama = "Databaseden tum kullanicilar istendi fakat hicbir kullanici bulunamadi";
                await _db.loglar.AddAsync(kullaniciLog);
                await _db.SaveChangesAsync();
                return NotFound("Kullanici al: Kullanici yok");
            }
            kullaniciLog.durumID = 1;
            kullaniciLog.aciklama = "Databaseden kullanicilar istendi tüm kullanicilar getirildi";
            await _db.loglar.AddAsync(kullaniciLog);
            await _db.SaveChangesAsync();
            return Ok(users);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = _db.kullanicilar.FirstOrDefault(z => z.kullaniciID == id && z.isActive == true);
            Log kullaniciLog = new Log();
            kullaniciLog.islemID = 6;
            kullaniciLog.ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            kullaniciLog.tarihSaat = DateTime.Now;
            if (user == null)
            {
                kullaniciLog.durumID = 2;
                kullaniciLog.aciklama = "Databaseden " + id + " id'li kullanici istendi fakat bulunamadi";
                await _db.loglar.AddAsync(kullaniciLog);
                await _db.SaveChangesAsync();
                return NotFound("Get by id: User does not exist");
            }
            kullaniciLog.durumID = 1;
            kullaniciLog.aciklama = "Databaseden " + id + " id'li kullanici getirildi";
            await _db.loglar.AddAsync(kullaniciLog);
            await _db.SaveChangesAsync();
            return Ok(user);
        }
        [HttpPost]
        [Route("reg/{user}")]
        public async Task<IActionResult> Post(userUnHashed user)
        {
            string testMyHash = encryptSha256(user.password).ToString();
            
            var existingUser = _db.kullanicilar.
                Where(x => x.email == user.email).
                Where(x => x.isim == user.isim).
                Where(x => x.soyisim == user.soyisim).
                Where(x => x.isActive == user.isActive).
                Where(x => x.passwordHash == testMyHash).
                Where(x => x.role == user.role).OrderBy(ow => ow.kullaniciID).ToList();
            Log kullaniciLog = new Log();
            kullaniciLog.islemID = 3;
            kullaniciLog.ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            kullaniciLog.tarihSaat = DateTime.Now;
            if (existingUser.Count > 0)
            {
                kullaniciLog.durumID = 2;
                kullaniciLog.aciklama = "Databaseye yeni kullanici eklenmeye calisildi fakat kullanici halihazirda vardi";
                await _db.loglar.AddAsync(kullaniciLog);
                await _db.SaveChangesAsync();
                return BadRequest("Kayit: Kullanici zaten kayitli");
            }
            //----------------------------------\\
            string passwordHash = encryptSha256(user.password).ToString();
            
            Kullanici kullaniciHashed = new Kullanici();
            kullaniciHashed.isim = user.isim;
            kullaniciHashed.soyisim = user.soyisim;
            kullaniciHashed.email = user.email;
            kullaniciHashed.isActive = true;
            kullaniciHashed.role = user.role;
            kullaniciHashed.passwordHash = passwordHash;
            await _db.kullanicilar.AddAsync(kullaniciHashed);
            //----------------------------------\\
            kullaniciLog.durumID = 1;
            kullaniciLog.aciklama = "Databaseye " + user.isim + " isimli kullanici kayit edildi";
            await _db.loglar.AddAsync(kullaniciLog);
            await _db.SaveChangesAsync();
            return Ok(user);
        }

        private void createPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            };
        }private object encryptSha256(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var removeThisUser = _db.kullanicilar.FirstOrDefault(z => z.kullaniciID == id);
            Log kullaniciLog = new Log();
            kullaniciLog.islemID = 5;
            kullaniciLog.ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            kullaniciLog.tarihSaat = DateTime.Now;
            if (removeThisUser == null || removeThisUser.isActive == false)
            {
                kullaniciLog.durumID = 2;
                kullaniciLog.aciklama = "Databaseden " + id + " id'li kullanici silinmek istendi fakat kullanici bulunamadi";
                await _db.loglar.AddAsync(kullaniciLog);
                await _db.SaveChangesAsync();
                return NotFound("Delete: User not found");
            }
            //_db.kullanicilar.Remove(removeThisUser);
            removeThisUser.isActive = false;
            kullaniciLog.durumID = 1;
            kullaniciLog.aciklama = "Databaseden " + id + " id'li kullanici silindi";
            await _db.loglar.AddAsync(kullaniciLog);
            await _db.SaveChangesAsync();
            return Ok("User deleted");
        }
        [HttpPut]
        [Route("{updateUser}")]
        public async Task<IActionResult> Put(userUnHashed updateUser)
        {
            var updateThisUser = _db.kullanicilar.FirstOrDefault(x => x.kullaniciID == updateUser.kullaniciID);
            Log kullaniciLog = new Log();
            kullaniciLog.islemID = 4;
            kullaniciLog.ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            kullaniciLog.tarihSaat = DateTime.Now;
            if (updateThisUser == null || updateThisUser.isActive == false)
            {
                kullaniciLog.durumID = 2;
                kullaniciLog.aciklama = "Databasedeki " + updateUser.kullaniciID + " id'li kullanici güncellenmek istendi fakat kullanici bulunamadi";
                await _db.loglar.AddAsync(kullaniciLog);
                await _db.SaveChangesAsync();
                return NotFound("Update: User not found");
            }
            updateThisUser.isim = updateUser.isim;
            updateThisUser.soyisim = updateUser.soyisim;
            updateThisUser.email = updateUser.email;
            updateThisUser.isActive = updateUser.isActive;
            if (updateUser.password != null)
            {
                string hashThis=encryptSha256(updateUser.password).ToString();
                
                updateThisUser.passwordHash = hashThis;
            }
            updateThisUser.role = updateUser.role;
            kullaniciLog.aciklama = "Databasedeki " + updateUser.kullaniciID + " id'li kullanici güncellendi";
            kullaniciLog.durumID = 1;
            await _db.loglar.AddAsync(kullaniciLog);
            await _db.SaveChangesAsync();
            return Ok(updateThisUser);
        }

        [HttpGet]
        public int getKullaniciSayisi()
        {
            return _db.kullanicilar.ToList().Count;
        }
    }

}