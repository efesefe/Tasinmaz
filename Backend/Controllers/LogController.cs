using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using tasinmaz_v3.Models;
using tasinmaz_V3.Models;

namespace tasinmaz_v3.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class LogController : Controller
    {
        TasinmazDbContext _db;
        public LogController(TasinmazDbContext db)
        {
            _db = db;
        }
        [HttpPost]
        [Route("getMeMyLogs/{listParameters}")]
        public IActionResult Get(ListParameters listParameters)
        {
            listParameters.searchKey = listParameters.searchKey.ToLower();
            var logs = _db.loglar
            .Where(
                x => x.aciklama.ToLower().Contains(listParameters.searchKey) ||
                x.ip.ToLower().Contains(listParameters.searchKey) ||
                x.tarihSaat.ToString().ToLower().Contains(listParameters.searchKey) ||
                x.Durumlar.durumAdi.ToLower().Contains(listParameters.searchKey) ||
                x.Islemler.islemAdi.ToLower().Contains(listParameters.searchKey) ||
                x.id.ToString().ToLower().Contains(listParameters.searchKey)
             )
            .Select(t => new
            {
                logID = t.id,
                logIP = t.ip,
                logIslemi = t.Islemler.islemAdi,
                logAciklama = t.aciklama,
                logDurumu = t.Durumlar.durumAdi,
                logZaman = t.tarihSaat
            }).Skip((listParameters.PageNumber - 1) * 5).Take(5).ToList();
            return Ok(logs);
        }
        [HttpGet]
        [Route("{logId}")]
        public IActionResult GetById(int logId)
        {
            var arananLog = _db.loglar.FirstOrDefault(x => x.id == logId);
            if (arananLog == null)
            {
                return NotFound("Get log by id: log not found");
            }
            return Ok(arananLog);
        }
    }
}