using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmenuDAL.Model
{
    public class Variant
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Name between 1 and 50 characters")]
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey("Attribute")]
        public int AttributeId { get; set; }

        public virtual Attribute Attribute { get; set; }
        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; }

    }
}
