using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using tasinmaz_v3.Models;

namespace tasinmaz_v3.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class IlController : Controller
    {
        TasinmazDbContext _db;
        public IlController(TasinmazDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var ils = _db.iller.Select(x => new{
                ilID = x.ilID,
                ilName = x.ilName
            }).ToList();
            Log ilLog = new Log();
            ilLog.islemID = 6;
            ilLog.ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            ilLog.tarihSaat = DateTime.Now;
            if (ils.Count <= 0)
            {
                ilLog.durumID = 2;
                ilLog.aciklama = "Databaseden iller istendi fakat hicbir il bulunamadi";
                await _db.loglar.AddAsync(ilLog);
                await _db.SaveChangesAsync();
                return NotFound("Illeri getir: il bulunamadi");
            }
            ilLog.durumID = 1;
            ilLog.aciklama = "Databaseden ilceye bagli mahalleler istendi tüm mahalleler getirildi";
            await _db.loglar.AddAsync(ilLog);
            await _db.SaveChangesAsync();
            return Ok(ils);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var il = _db.iller.Select(x => new{
                ilID = x.ilID,
                ilName = x.ilName
            }).FirstOrDefault(x => x.ilID == id);
            Log ilLog = new Log();
            ilLog.islemID = 6;
            ilLog.ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            ilLog.tarihSaat = DateTime.Now;
            if (il == null)
            {
                ilLog.durumID = 2;
                ilLog.aciklama = "Databaseden il istendi fakat hicbir il bulunamadi";
                await _db.loglar.AddAsync(ilLog);
                await _db.SaveChangesAsync();
                return NotFound("Illeri getir: il bulunamadi");
            }
            ilLog.durumID = 1;
            ilLog.aciklama = "Databaseden ilceye bagli mahalleler istendi tüm mahalleler getirildi";
            await _db.loglar.AddAsync(ilLog);
            await _db.SaveChangesAsync();
            return Ok(il);
        }
    }
}