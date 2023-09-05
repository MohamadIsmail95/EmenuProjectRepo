using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmenuDAL.Model.Binding
{
    public class AddProductBinding
    {
        public string arabicName { get; set; }
        public string englishName { get; set; }
        public string arabicDescription { get; set; }
        public string englishDescription { get; set; }
        public List<AttributeVariant> attributeVariants { get; set; }

    }

    public class AttributeVariant
    {
        public int ? AttributId { get; set; }
        public int ? VarId { get; set; }
        public List<AddProductImageBinding> images { get; set; }

    }
}
