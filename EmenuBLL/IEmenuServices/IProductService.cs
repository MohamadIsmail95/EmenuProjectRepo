using EmenuDAL.Model.ApiResponse;
using EmenuDAL.Model.Binding;
using EmenuDAL.Model.Filter;
using EmenuDAL.Model.Helper;
using EmenuDAL.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace EmenuBLL.IEmenuServices
{
    public  interface IProductService
    {
        Response<List<ProductViewModel>> GetAllProduct(List<Filter> filters, ParameterPagination parameters);
        Response<ProductViewModel> GetProductById(int id);
        Response<bool> AddNewProduct(AddProductBinding prod);
        Response<bool> UpdateProduct(UpdateProductBinding prod);
        Response<bool> RemoveProduct(int id);
    }
}
