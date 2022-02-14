using Egitim.API.DBEgitim;
using Egitim.API.DBEgitim.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egitim.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class KatilimcilarController : ControllerBase
    {
        public readonly Context _context;

        public KatilimcilarController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        [ResponseCache(Duration = 60)]
        public IActionResult Get()
        {
            List<Katilimciler> data = _context.Katilimciler.ToList();
            return Ok(data);
        }

        [HttpGet("{id}")]
        [ResponseCache(Duration = 60)]
        public IActionResult Get(int id)
        {
            Katilimciler data = _context.Katilimciler.Where(x => x.Id == id).FirstOrDefault();

            if (data == null)
            {
                return NoContent();
            }

            return Ok(data);
        }

        [HttpPost]
        public IActionResult Add(Katilimciler entity)
        {
            _context.Katilimciler.Add(entity);
            _context.SaveChanges();

            return StatusCode(201);
        }

        [HttpPut]
        public IActionResult Update(Katilimciler entity)
        {
            if (!_context.Katilimciler.Any(x => x.Id == entity.Id))
            {
                return BadRequest();
            }

            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Katilimciler data = _context.Katilimciler.FirstOrDefault(x => x.Id == id);

            if (data == null)
            {
                return NoContent();
            }

            _context.Katilimciler.Remove(data);
            _context.SaveChanges();

            return Ok();
        }
    }
}
