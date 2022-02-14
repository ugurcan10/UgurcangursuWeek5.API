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
    public class EgitmenlerController : ControllerBase
    {
        public readonly Context _context;

        public EgitmenlerController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        [ResponseCache(Duration = 60)]
        public IActionResult Get()
        {
            List<Egitmenler> data = _context.Egitmenler.ToList();
            return Ok(data);
        }

        [HttpGet("{id}")]
        [ResponseCache(Duration = 60)]
        public IActionResult Get(int id)
        {
            Egitmenler data = _context.Egitmenler.Where(x => x.Id == id).FirstOrDefault();

            if (data == null)
            {
                return NoContent();
            }

            return Ok(data);
        }

        [HttpPost]
        public IActionResult Add(Egitmenler entity)
        {
            _context.Egitmenler.Add(entity);
            _context.SaveChanges();

            return StatusCode(201);
        }

        [HttpPut]
        public IActionResult Update(Egitmenler entity)
        {
            if (!_context.Egitmenler.Any(x => x.Id == entity.Id))
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
            Egitmenler data = _context.Egitmenler.FirstOrDefault(x => x.Id == id);

            if (data == null)
            {
                return NoContent();
            }

            _context.Egitmenler.Remove(data);
            _context.SaveChanges();

            return Ok();
        }
    }
}
