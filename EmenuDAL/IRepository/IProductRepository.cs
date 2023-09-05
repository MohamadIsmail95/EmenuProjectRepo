using EmenuDAL.Model;
using EmenuDAL.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmenuDAL.IRepository
{

    public interface IProductRepository : IRepositoryBase<Product, ProductViewModel, int>
    {

    }
}
