using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmenuDAL.Model.Binding
{
    public class UpdateProductImageBinding
    {
        public int id { get;set; }
        public int productAttId { get; set; }
        public string productImagePath { get; set; }
        public bool isMainImage { get; set; }
    }
}
