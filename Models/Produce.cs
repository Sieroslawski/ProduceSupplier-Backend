using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend_assignment.Models
{
    public class Produce
    {
        [Key]
        public int ProduceID { get; set; }
        public string Description { get; set; }

        // Navigation properties.
        // Child.        
        public virtual ICollection<ProduceSupplier>
            ProduceSuppliers
        { get; set; }
    }
}
