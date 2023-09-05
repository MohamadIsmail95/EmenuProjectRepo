using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmenuDAL.Model.ViewModel
{
    public class ProductViewModel
    {
        public int id { get; set; }
        public string arabicName { get; set; }
        public string englishName { get; set; }
        public string arabicDescription { get; set; }
        public string englishDescription { get; set; }

        public List<ProductAttributeInv> productAttributes { get; set; }

    }


    public class NewProductImage
    {
        public int id { get; set; }
        public string productImagePath { get; set; }
        public bool isMainImage { get; set; }
    }

    public class ProductAttributeInv
    {
         public int id { get; set; }
        public int attributeId { get; set; }
        public string attributeName { get; set; }
        public int variantId { get; set; }
        public string variantName { get; set; }
        public List<NewProductImage> images { get; set; }
    }

}
