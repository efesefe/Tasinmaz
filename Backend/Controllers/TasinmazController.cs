using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using tasinmaz_v3.Models;
using tasinmaz_V3.Models;

namespace tasinmaz_v3.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class TasinmazController : Controller
    {
        TasinmazDbContext _db;
        public TasinmazController(TasinmazDbContext db)
        {
            _db = db;
        }
        [HttpPost]
        [Route("pag/{listParameters}")]
        public async Task<IActionResult> Get(ListParameters listParameters)
        {
            listParameters.searchKey = listParameters.searchKey.ToLower();
            var tasinmazs = _db.tasinmazlar
            .Where(
                x => x.Mahalle.mahalleName.ToLower().StartsWith(listParameters.searchKey)||
                x.Mahalle.Ilce.ilceName.ToLower().StartsWith(listParameters.searchKey) ||
                x.Mahalle.Ilce.Il.ilName.ToLower().StartsWith(listParameters.searchKey) ||
                x.adres.ToLower().StartsWith(listParameters.searchKey) ||
                x.nitelik.StartsWith(listParameters.searchKey) ||
                x.Parsel.ToString().StartsWith(listParameters.searchKey) ||
                x.Ada.ToString().StartsWith(listParameters.searchKey)
            )
            .Where(x => x.isActive == true)
            .Select(t => new
            {
                tasinmazId = t.tasinmazID,
                tasinmazIl = t.Mahalle.Ilce.Il.ilName,
                tasinmazIlce = t.Mahalle.Ilce.ilceName,
                tasinmazMahalle = t.Mahalle.mahalleName,
                ada = t.Ada,
                parsel = t.Parsel,
                nitelik = t.nitelik,
                adres = t.adres,
                isActive = t.isActive
            })
            .OrderBy(x => x.tasinmazIl).ThenBy(x => x.tasinmazIlce).ThenBy(x=> x.tasinmazMahalle)
            .Skip((listParameters.PageNumber - 1) * 5).Take(5).ToList();
            Log tasinmazLog = new Log();
            tasinmazLog.islemID = 6;
            tasinmazLog.ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            tasinmazLog.tarihSaat = DateTime.Now;
            if (tasinmazs == null)
            {
                tasinmazLog.durumID = 2;
                tasinmazLog.aciklama = "Databaseden tüm tasinmazlar istendi fakat hicbir tasinmaz bulunamadi";
                await _db.loglar.AddAsync(tasinmazLog);
                await _db.SaveChangesAsync();
                return NotFound("Tasinmaz getir: tasinmaz bulunamadi");
            }
            tasinmazLog.durumID = 1;
            tasinmazLog.aciklama = "Databaseden tasinmazlar getirildi";
            await _db.loglar.AddAsync(tasinmazLog);
            await _db.SaveChangesAsync();
            return Ok(tasinmazs);
        }
        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var idTasinmaz = _db.tasinmazlar.Select(t => new
            {
                tasinmazID = t.tasinmazID,
                ilID = t.Mahalle.Ilce.ilID,
                ilceID = t.Mahalle.ilceID,
                mahalleID = t.Mahalle.mahalleID,
                ada = t.Ada,
                parsel = t.Parsel,
                nitelik = t.nitelik,
                adres = t.adres,
                isActive = t.isActive
            }).FirstOrDefault(x => x.tasinmazID == id && x.isActive == true);
            Log tasinmazLog = new Log();
            tasinmazLog.islemID = 6;
            tasinmazLog.ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            tasinmazLog.tarihSaat = DateTime.Now;
            if (idTasinmaz == null)
            {
                tasinmazLog.durumID = 2;
                tasinmazLog.aciklama = "Databaseden" + id + " id'li tasinmaz istendi istendi fakat tasinmaz bulunamadi";
                await _db.loglar.AddAsync(tasinmazLog);
                await _db.SaveChangesAsync();
                return NotFound("Id ile al: Tasinmaz bulunamadi");
            }
            tasinmazLog.durumID = 1;
            tasinmazLog.aciklama = "Databaseden" + id + " id'li tasinmaz getirildi";
            await _db.loglar.AddAsync(tasinmazLog);
            await _db.SaveChangesAsync();
            return Ok(idTasinmaz);
        }
        [HttpPost]
        [Route("{addTasinmaz}")]
        public async Task<IActionResult> Post(Tasinmaz addTasinmaz)
        {
            Log tasinmazLog = new Log();
            tasinmazLog.islemID = 3;
            tasinmazLog.ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            tasinmazLog.tarihSaat = DateTime.Now;
            var existingTasinmaz = _db.tasinmazlar.
                Where(x => x.Ada == addTasinmaz.Ada).
                Where(x => x.Parsel == addTasinmaz.Parsel).
                Where(x => x.adres == addTasinmaz.adres).
                Where(x => x.isActive == addTasinmaz.isActive).
                Where(x => x.mahalleID == addTasinmaz.mahalleID).
                Where(x => x.nitelik == addTasinmaz.nitelik).ToList();
            if (existingTasinmaz.Count > 0)
            {
                tasinmazLog.durumID = 2;
                tasinmazLog.aciklama = "Databaseye eklenmek istenen tasinmaz halihazirda databasede bulunmakta";
                await _db.loglar.AddAsync(tasinmazLog);
                await _db.SaveChangesAsync();
                return BadRequest("Ekle: Bu tasinmaz halihazirda databasede bulunmakta");
            }
            await _db.tasinmazlar.AddAsync(addTasinmaz);
            tasinmazLog.durumID = 1;
            tasinmazLog.aciklama = "Databaseye yeni tasinmaz eklendi";
            await _db.loglar.AddAsync(tasinmazLog);
            await _db.SaveChangesAsync();
            return Ok(addTasinmaz);
        }
        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            Log tasinmazLog = new Log();
            tasinmazLog.islemID = 5;
            tasinmazLog.ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            tasinmazLog.tarihSaat = DateTime.Now;
            var removeTasinmaz = _db.tasinmazlar.FirstOrDefault(x => x.tasinmazID == id);
            if (removeTasinmaz == null || removeTasinmaz.isActive == false)
            {
                tasinmazLog.durumID = 2;
                tasinmazLog.aciklama = "Databaseden " + id + " id'li tasinmaz silinmek istendi fakat tasinmaz bulunamadi";
                await _db.loglar.AddAsync(tasinmazLog);
                await _db.SaveChangesAsync();
                return NotFound("Sil: Tasinmaz bulunamadi");
            }
            removeTasinmaz.isActive = false;
            tasinmazLog.durumID = 1;
            tasinmazLog.aciklama = "Tasinmaz silindi";
            await _db.loglar.AddAsync(tasinmazLog);
            await _db.SaveChangesAsync();
            return Ok("Tasinmaz silindi");
        }
        [HttpPut]
        [Route("{updateTasinmaz}")]
        public async Task<IActionResult> Put(Tasinmaz updateTasinmaz)
        {
            Log tasinmazLog = new Log();
            tasinmazLog.islemID = 4;
            tasinmazLog.ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            tasinmazLog.tarihSaat = DateTime.Now;
            if (_db.mahalleler.FirstOrDefault(x => x.mahalleID == updateTasinmaz.mahalleID) == null)
            {
                tasinmazLog.durumID = 2;
                tasinmazLog.aciklama = "Guncellenmek istenen tasinmazin mahalle bilgisi gecersiz";
                await _db.loglar.AddAsync(tasinmazLog);
                await _db.SaveChangesAsync();
                return NotFound("Guncelleme: Mahalle yok");
            }
            var updateThisTasinmaz = _db.tasinmazlar.FirstOrDefault(x => x.tasinmazID == updateTasinmaz.tasinmazID);
            if (updateThisTasinmaz == null || updateTasinmaz.isActive == false)
            {
                tasinmazLog.durumID = 2;
                tasinmazLog.aciklama = "Guncellenmek istenen tasinmaz bulunamadi";
                await _db.loglar.AddAsync(tasinmazLog);
                await _db.SaveChangesAsync();
                return NotFound("Guncelleme: Tasinmaz bulunamadi");
            }
            updateThisTasinmaz.Ada = updateTasinmaz.Ada;
            updateThisTasinmaz.adres = updateTasinmaz.adres;
            updateThisTasinmaz.Parsel = updateTasinmaz.Parsel;
            updateThisTasinmaz.isActive = updateTasinmaz.isActive;
            updateThisTasinmaz.nitelik = updateTasinmaz.nitelik;
            updateThisTasinmaz.mahalleID = updateTasinmaz.mahalleID;
            tasinmazLog.durumID = 1;
            tasinmazLog.aciklama = "" + updateTasinmaz.tasinmazID + " Id'li tasinmaz guncellendi";
            await _db.loglar.AddAsync(tasinmazLog);
            await _db.SaveChangesAsync();
            return Ok(updateThisTasinmaz);
        }

        [HttpPost]
        [Route("pagnum/{listParameters}")]
        public IActionResult getSayfaSayisi(ListParameters listParameters)
        {
            listParameters.searchKey = listParameters.searchKey.ToLower();
            int numberOfTasinmazs = _db.tasinmazlar.Where(
                x => x.Mahalle.mahalleName.ToLower().StartsWith(listParameters.searchKey)||
                x.Mahalle.Ilce.ilceName.ToLower().StartsWith(listParameters.searchKey) ||
                x.Mahalle.Ilce.Il.ilName.ToLower().StartsWith(listParameters.searchKey) ||
                x.adres.ToLower().StartsWith(listParameters.searchKey) ||
                x.nitelik.StartsWith(listParameters.searchKey) ||
                x.Parsel.ToString().StartsWith(listParameters.searchKey) ||
                x.Ada.ToString().StartsWith(listParameters.searchKey)
            )
            .Where(x => x.isActive == true).ToList().Count();
            int numberOfPages = numberOfTasinmazs % 5 == 0 ? numberOfTasinmazs / 5 : numberOfTasinmazs / 5 + 1;
            return Ok(numberOfPages);
        }
    }
}