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
    public class AsistanlarController : ControllerBase
    {
        public readonly Context _context;

        public AsistanlarController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        [ResponseCache(Duration = 60)]
        public IActionResult Get()
        {
            List<Asistanlar> data = _context.Asistanlar.ToList();
            return Ok(data);
        }

        [HttpGet("{id}")]
        [ResponseCache(Duration = 60)]
        public IActionResult Get(int id)
        {
            Asistanlar data = _context.Asistanlar.Where(x => x.Id == id).FirstOrDefault();

            if (data == null)
            {
                return NoContent();
            }

            return Ok(data);
        }

        [HttpPost]
        public IActionResult Add(Asistanlar entity)
        {
            _context.Asistanlar.Add(entity);
            _context.SaveChanges();

            return StatusCode(201);
        }

        [HttpPut]
        public IActionResult Update(Asistanlar entity)
        {
            if (!_context.Asistanlar.Any(x=>x.Id == entity.Id))
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
            Asistanlar data = _context.Asistanlar.FirstOrDefault(x => x.Id == id);

            if (data == null)
            {
                return NoContent();
            }

            _context.Asistanlar.Remove(data);
            _context.SaveChanges();

            return Ok();
        }
    }
}
