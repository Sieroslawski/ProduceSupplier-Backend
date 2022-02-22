using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend_assignment.Models
{
    public class ProduceSupplier
    {
        [Key, Column(Order = 0)]
        public int ProduceID { get; set; }
        [Key, Column(Order = 1)]
        public int SupplierID { get; set; }
        public int Qty { get; set; }

        // Navigation properties.
        // Parents.
        public virtual Produce Produce { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
