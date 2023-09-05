using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmenuDAL.Model.ViewModel
{
    public class AttributeViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; } 
        public List<VariantsViewModel> Variants { get; set; }

    }

    public class VariantsViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

    }
}
