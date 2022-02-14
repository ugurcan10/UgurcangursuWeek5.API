using Egitim.API.Abstract;
using Egitim.API.DBEgitim;
using Egitim.API.DBEgitim.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egitim.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class EgitimlerController : ControllerBase
    {
        public readonly Context _context;
        private readonly IToken _token;
        private readonly IDistributedCache _cache;

        public EgitimlerController(Context context, IToken token, IDistributedCache cache)
        {
            _context = context;
            _token = token;
            _cache = cache;
        }

        [HttpGet]
        [AllowAnonymous]
        [ResponseCache(Duration = 60)] //Bu attribute sayesinde GetEdu metodundaki response datayı 60 saniyeliğine cache'lemiş olduk. Uygulama çalıştığında bu metoda ilk yapılan istekte veritabanından veriler çekilecek ve response data CacheMiddleware’i tarafından 60 saniye boyunca Response Cache’de tutulacak. Bu 60 saniye boyunca hiçbir request GetEdu metodunu tetiklemeyecek ve response olarak direkt olarak middleware tarafından cache üzerinden elde edilen data gönderilecektir.
        public IActionResult Get()
        {
            List<Egitimler> data = _context.Egitimler.ToList();

            return Ok(data);
        }

        [HttpGet("{id}")]
        [ResponseCache(Duration = 60)] //Bu attribute sayesinde GetEdu metodundaki response datayı 60 saniyeliğine cache'lemiş olduk. Uygulama çalıştığında bu metoda ilk yapılan istekte veritabanından veriler çekilecek ve response data CacheMiddleware’i tarafından 60 saniye boyunca Response Cache’de tutulacak. Bu 60 saniye boyunca hiçbir request GetEdu metodunu tetiklemeyecek ve response olarak direkt olarak middleware tarafından cache üzerinden elde edilen data gönderilecektir.
        public IActionResult Get(int id)
        {
            Egitimler data = _context.Egitimler.Where(x => x.Id == id).FirstOrDefault();

            if (data == null)
            {
                return NoContent();
            }

            return Ok(data);
        }

        [AllowAnonymous]
        public IActionResult Search(string text, string kategori, string siralama)
        {
            List<Egitimler> data = _context.Egitimler.Where(x => x.Kategori == kategori && x.EgitimAdi.Contains(text)).ToList();

            if (siralama == "A-Z")
            {
                data = data.OrderBy(x => x.EgitimAdi).ToList();
            }
            else if(siralama == "Z-A")
            {
                data = data.OrderByDescending(x => x.EgitimAdi).ToList();
            }
            else
            {
                return BadRequest();
            }

            return Ok(data);
        }

        [HttpPost]
        public IActionResult Add(Egitimler entity)
        {
            _context.Egitimler.Add(entity);
            _context.SaveChanges();

            return StatusCode(201);
        }

        [HttpPost("{_EgitimAdi}/{_EgitmenID}/{_Kategori}/{_Tarihi}")]
        public IActionResult Add(string _EgitimAdi, int _EgitmenID, string _Kategori, DateTime _Tarihi)
        {
            Egitimler egitim = new Egitimler();

            egitim.EgitimAdi = _EgitimAdi;
            egitim.Kategori = _Kategori;
            egitim.EgitmenID = _EgitmenID;
            egitim.Tarihi = _Tarihi;

            _context.Egitimler.Add(egitim);
            _context.SaveChanges();

            return StatusCode(201);
        }

        [HttpPut]
        public IActionResult Update(Egitimler entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}/{_EgitimAdi}/{_EgitmenID}/{_Kategori}/{_Tarihi}")]
        public IActionResult Update(int id, string _EgitimAdi, int _EgitmenID, string _Kategori, DateTime _Tarihi)
        {
            Egitimler data = _context.Egitimler.FirstOrDefault(x => x.Id == id);
            data.EgitimAdi = _EgitimAdi;
            data.EgitmenID = _EgitmenID;
            data.Kategori = _Kategori;
            data.Tarihi = _Tarihi;

            _context.Egitimler.Update(data);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Egitimler data = _context.Egitimler.FirstOrDefault(x => x.Id == id);

            _context.Egitimler.Remove(data);
            _context.SaveChanges();

            return Ok();
        }



        [HttpPatch("{id}/{_EgitimAdi}/{_EgitmenID}/{_Kategori}/{_Tarihi}")]
        public IActionResult Update2(int id, string _EgitimAdi, int _EgitmenID, string _Kategori, DateTime _Tarihi)
        {
            Egitimler data = _context.Egitimler.FirstOrDefault(x => x.Id == id);
            data.EgitimAdi = _EgitimAdi;
            data.EgitmenID = _EgitmenID;
            data.Kategori = _Kategori;
            data.Tarihi = _Tarihi;

            _context.Egitimler.Update(data);
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public IActionResult Bad()
        {
            return BadRequest("400 döndürdüm");
        }

        [HttpGet]
        public IActionResult ServerError()
        {
            return StatusCode(500);
        }
    }
}
