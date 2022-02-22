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
    public class ProduceSupplyController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ProduceSupplyController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<ProduceSupplier> GetAll()
        {
            return _context.ProduceSuppliers.ToList();
        }

        [HttpGet("{sid}/{pid}")]
        public IActionResult GetById(long pid, long sid)
        {
            var item = _context.ProduceSuppliers.FirstOrDefault(s => s.SupplierID == sid && s.ProduceID == pid);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] ProduceSupplier ps)
        {            
            var produce = _context.Produces.Where(p => p.ProduceID == ps.ProduceID).FirstOrDefault();
            var supply = _context.Suppliers.Where(s => s.SupplierID == ps.SupplierID).FirstOrDefault();
            var prsy = _context.ProduceSuppliers.Where(t => t.ProduceID == ps.ProduceID && t.SupplierID == ps.SupplierID).FirstOrDefault();

            if (prsy != null || produce == null || supply == null || ps.Qty.ToString() == null || ps.Qty.ToString() == "")
            {
                return BadRequest();
            }
            _context.ProduceSuppliers.Add(ps);            
            _context.SaveChanges();
            return new ObjectResult(ps);
        }

        [HttpPut]
        [Route("edit")] // Custom route
        public IActionResult GetByParams([FromBody] ProduceSupplier ps)
        {
            var item = _context.ProduceSuppliers.Where(p => p.SupplierID == ps.SupplierID && p.ProduceID == ps.ProduceID).FirstOrDefault();
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                item.Qty = ps.Qty;
                _context.SaveChanges();
            }
            return new ObjectResult(item);
        }

        [HttpDelete("{pid}/{sid}")]
        public IActionResult MyDelete(int pid, int sid)
        {
            var item = _context.ProduceSuppliers.Where(s => s.SupplierID == pid && s.ProduceID == sid).FirstOrDefault();
            if (item == null)
            {
                return NotFound();
            }
           
                _context.ProduceSuppliers.Remove(item);
                _context.SaveChanges();
                return new ObjectResult(item);
                                     
        }
    }
}
