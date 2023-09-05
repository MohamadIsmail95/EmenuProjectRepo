using EmenuDAL.Model.ViewModel;
using EmenuDAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmenuDAL.Model.Binding;

namespace EmenuDAL.IRepository
{
    public interface IProductAttributeRepository : IRepositoryBase<ProductAttribute, ProductViewModel, int>
    {
    }
}
