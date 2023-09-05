using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmenuDAL.Model.Binding
{
    public class UpdateProductBinding
    {
        public int id { get; set; }
        public string arabicName { get; set; }
        public string englishName { get; set; }
        public string arabicDescription { get; set; }
        public string englishDescription { get; set; }
        public List<UpdateAttributeVariant> attributeVariants { get; set; }


    }

    public class UpdateAttributeVariant
    {
        public int id { get; set; }
        public int? attributeId { get; set; }
        public int? variantId { get; set; }
        public List<UpdateProductImageBinding> images { get; set; }

    }

}
