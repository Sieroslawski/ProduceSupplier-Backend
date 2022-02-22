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
    public class SupplyController : ControllerBase
    {
        private readonly MyDbContext _context;

        public SupplyController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Supplier> GetAll()
        {
            return _context.Suppliers.ToList();
        }
        
        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var item = _context.Suppliers.FirstOrDefault(s => s.SupplierID == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] Supplier supply)
        {
            if (supply.SupplierName == null || supply.SupplierName == "")
            {
                return BadRequest();
            }
            _context.Suppliers.Add(supply);
            _context.SaveChanges();
            return new ObjectResult(supply);
        }

        [HttpPut]
        [Route("edit")] // Custom route
        public IActionResult GetByParams([FromBody] Supplier supply)
        {
            var item = _context.Suppliers.Where(s => s.SupplierID == supply.SupplierID).FirstOrDefault();
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                item.SupplierName = supply.SupplierName;
                _context.SaveChanges();
            }
            return new ObjectResult(item);
        }

        [HttpDelete("{id}")]
        public IActionResult MyDelete(int id)
        {
            var item = _context.Suppliers.Where(s => s.SupplierID == id).FirstOrDefault();
            if (item == null)
            {
                return NotFound();
            }
            try
            {
                _context.Suppliers.Remove(item);
                _context.SaveChanges();
                return new ObjectResult(item);
            }
            catch
            {
                return Conflict();
            }
        }
    }
}
