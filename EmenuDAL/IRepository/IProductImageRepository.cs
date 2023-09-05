using EmenuDAL.Model;
using EmenuDAL.Model.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmenuDAL.IRepository
{
    public interface IProductImageRepository:IRepositoryBase<ProductImage, AddProductImageBinding, int>
    {

    }
}
