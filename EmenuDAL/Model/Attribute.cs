using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmenuDAL.Model
{
    public class Attribute
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name between 3 and 50 characters")]
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Variant> Variants { get; set; }
        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; }


    }
}
