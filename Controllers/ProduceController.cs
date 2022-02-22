using backend_assignment.DatabaseHelper;
using backend_assignment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduceController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ProduceController(MyDbContext context)
        {
            _context = context;
        }
        // GetAll() is automatically recognized as
        // http://localhost:<port #>/api/todo
        [HttpGet]
        public IEnumerable<Produce> GetAll()
        {
            return _context.Produces.ToList();
        }
        // GetById() is automatically recognized as
        // http://localhost:<port #>/api/todo/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var item = _context.Produces.FirstOrDefault(p => p.ProduceID == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] Produce prod)
        {
            if (prod.Description == null || prod.Description == "")
            {
                return BadRequest();
            }
            _context.Produces.Add(prod);
            _context.SaveChanges();
            return new ObjectResult(prod);
        }

        [HttpPut]
        [Route("edit")] // Custom route
        public IActionResult GetByParams([FromBody] Produce prod)
        {
            var item = _context.Produces.Where(p => p.ProduceID == prod.ProduceID).FirstOrDefault();
            if (item == null)
            {
                return NotFound();
            }
            else
            {                
                item.Description = prod.Description;
                _context.SaveChanges();
            }
            return new ObjectResult(item);
        }

        [HttpDelete("{id}")]
        public IActionResult MyDelete(int id)
        {
            var item = _context.Produces.Where(p => p.ProduceID == id).FirstOrDefault();            
            if (item == null)
            {
                return NotFound();
            }
            try
            {
                var children = _context.ProduceSuppliers.Where(p => p.ProduceID == id);

                if(children != null)
                {
                    foreach(var c in children)
                    {
                        _context.ProduceSuppliers.Remove(c);
                    }
                }
                _context.Produces.Remove(item);
                _context.SaveChanges();
                return new ObjectResult(item);
            } catch
            {
                return Conflict();
            }
          
        }
    }
}

