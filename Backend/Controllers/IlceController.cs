using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using tasinmaz_v3.Models;

namespace tasinmaz_v3.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class IlceController : Controller
    {
        TasinmazDbContext _db;
        public IlceController(TasinmazDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        [Route("{ilId}")]
        public async Task<IActionResult> Get(int ilId)
        {
            var ilinIlceleri = _db.ilceler.Select(y=> new{
                ilceID = y.ilceID,
                ilID = y.ilID,
                ilceName = y.ilceName
            }).Where(x => x.ilID == ilId).ToList();
            Log ilceLog = new Log();
            ilceLog.islemID = 6;
            ilceLog.ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            ilceLog.tarihSaat = DateTime.Now;
            var ilceleriArananIl = _db.iller.FirstOrDefault(x => x.ilID == ilId);
            if (ilinIlceleri.Count <= 0)
            {
                ilceLog.durumID = 2;
                ilceLog.aciklama = "Databaseden " + ilceleriArananIl.ilName + " ilinin ilceler istendi fakat hicbir ilce bulunamadi";
                await _db.loglar.AddAsync(ilceLog);
                await _db.SaveChangesAsync();
                return NotFound("Get ilces: There are no ilce for this il");
            }
            ilceLog.durumID = 1;
            ilceLog.aciklama = "Databaseden " + ilceleriArananIl.ilName + " ilinin ilceler getirildi";
            await _db.loglar.AddAsync(ilceLog);
            await _db.SaveChangesAsync();
            return Ok(ilinIlceleri);
        }
    }
}