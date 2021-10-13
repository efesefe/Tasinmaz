using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using tasinmaz_v3.Models;

namespace tasinmaz_v3.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class MahalleController : Controller
    {
        TasinmazDbContext _db;
        public MahalleController(TasinmazDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        [Route("{ilceId}")]
        public async Task<IActionResult> Get(int ilceId)
        {
            var ilceninMahalleleri = _db.mahalleler.Select(t => new{
                mahalleID =t.mahalleID,
                ilceID = t.ilceID,
                mahalleName = t.mahalleName
            }).Where(x => x.ilceID == ilceId).ToList();
            Log tasinmazLog = new Log();
            tasinmazLog.islemID = 6;
            tasinmazLog.ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            tasinmazLog.tarihSaat = DateTime.Now;
            if (ilceninMahalleleri.Count <= 0)
            {
                tasinmazLog.durumID = 2;
                tasinmazLog.aciklama = "Databaseden ilceye bagli tüm mahalleler istendi fakat hicbir mahalle bulunamadi";
                await _db.loglar.AddAsync(tasinmazLog);
                await _db.SaveChangesAsync();
                return NotFound("Mahalle al: Mahalle bulunamadi");
            }
            tasinmazLog.durumID = 1;
            tasinmazLog.aciklama = "Databaseden ilceye bagli mahalleler istendi tüm mahalleler getirildi";
            await _db.loglar.AddAsync(tasinmazLog);
            await _db.SaveChangesAsync();
            return Ok(ilceninMahalleleri);
        }
    }
}