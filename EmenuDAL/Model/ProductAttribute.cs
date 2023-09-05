using EmenuDAL.Model.Binding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmenuDAL.Model
{
    public class ProductAttribute
    {
        [Key]
        public int Id { get;set; }
        [ForeignKey("Product")]
        public int ProductId { get;set; }
        [ForeignKey("Attribute")]
        public int ? AttributId { get;set; }
        [ForeignKey("Variant")]
        public int ? VarId { get;set; }



        public virtual Product Product { get;set; }
        public virtual Attribute Attribute { get;set; }
        public virtual Variant Variant { get;set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; }

        public ProductAttribute() { }

        public ProductAttribute(int ProductId, AttributeVariant item)
        {
           this.ProductId = ProductId;
           this.AttributId = item.AttributId;
           this.VarId=item.VarId;
        }

        public ProductAttribute(int ProductId, UpdateAttributeVariant item)
        {
            this.Id = item.id;
            this.ProductId = ProductId;
            this.AttributId = item.attributeId;
            this.VarId = item.variantId;
        }

    }
}
