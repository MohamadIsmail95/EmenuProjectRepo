using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmenuDAL.Model.Binding
{
    public class AddProdAttBinding
    {
        public int id { get; set; }
        public int productId { get; set; }
        public int? attributId { get; set; }
        public int? varId { get; set; }
    }
}
