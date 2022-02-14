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
    public class OgrencilerController : ControllerBase
    {
        public readonly Context _context;

        public OgrencilerController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Ogrenciler> data = _context.Ogrenciler.ToList();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Ogrenciler data = _context.Ogrenciler.Where(x => x.Id == id).FirstOrDefault();

            if (data == null)
            {
                return NoContent();
            }

            return Ok(data);
        }

        [HttpPost]
        public IActionResult Add(Ogrenciler entity)
        {
            _context.Ogrenciler.Add(entity);
            _context.SaveChanges();

            return StatusCode(201);
        }

        [HttpPut]
        public IActionResult Update(Ogrenciler entity)
        {
            if (!_context.Ogrenciler.Any(x => x.Id == entity.Id))
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
            Ogrenciler data = _context.Ogrenciler.FirstOrDefault(x => x.Id == id);

            if (data == null)
            {
                return NoContent();
            }

            _context.Ogrenciler.Remove(data);
            _context.SaveChanges();

            return Ok();
        }
    }
}
