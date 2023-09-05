using EmenuDAL.Model.Binding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmenuDAL.Model
{
    public  class ProductImage
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ProductAttribute")]
        public int ProductAttId { get; set; }
        public string ProductImagePath { get; set; }
        public bool IsMainImage { get; set; }

        public virtual ProductAttribute ProductAttribute { get; set; }


        public ProductImage() { }
        public ProductImage(int prodAttId,AddProductImageBinding item)
        {
            this.ProductAttId = prodAttId;
            this.ProductImagePath = item.productImagePath;
            this.IsMainImage = item.isMainImage;
        }


        public ProductImage(UpdateProductImageBinding item)
        {
            this.Id = item.id;
            this.ProductAttId = item.productAttId;
            this.ProductImagePath = item.productImagePath;
            this.IsMainImage = item.isMainImage;
        }


    }
}
