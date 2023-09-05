using EmenuDAL.Model.ApiResponse;
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
    public interface IAttributeService
    {
        Response<List<AttributeViewModel>> GetAllAttributes(List<Filter> filters, ParameterPagination parameters);

    }
}
