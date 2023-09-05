using EmenuBLL.IEmenuServices;
using EmenuDAL.IRepository;
using EmenuDAL.Model.ApiResponse;
using EmenuDAL.Model.Filter;
using EmenuDAL.Model.Helper;
using EmenuDAL.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmenuBLL.EmenuServices
{
    public class AttributeService : IAttributeService
    {
        private readonly IAttributeRepository _IAttributeRepository;

        public AttributeService(IAttributeRepository iAttributeRepository)
        {
            _IAttributeRepository = iAttributeRepository;
        }

        public Response<List<AttributeViewModel>> GetAllAttributes(List<Filter> filters, ParameterPagination parameters)
        {
            List<AttributeViewModel> result = new List<AttributeViewModel>();
            var attributes = _IAttributeRepository.GetInclude(x => x.Variants);

            result = attributes.Select(x => new AttributeViewModel()
            {
                id= x.Id,
                name= x.Name,
                description= x.Description,
                Variants=x.Variants.Select(y=>new VariantsViewModel()
                {
                    id=y.Id,
                    name=y.Name,
                    description=y.Description,

                }).ToList()

            }).ToList();

            result = FilterandPagination(result,parameters,filters);

            return new Response<List<AttributeViewModel>>(true, result, null, null, (int)ResponseStatus.ApiReturnCode.success, attributes.ToList().Count());


        }

        private List<AttributeViewModel> FilterandPagination(List<AttributeViewModel> filterDataTable, ParameterPagination parameterPagination, List<Filter> filter)
        {
            try
            {


                if (filter != null || filter.Count != 0)
                {
                    foreach (Filter c in filter)
                    {
                        var filterKeyProperty = typeof(AttributeViewModel).GetProperty(c.key);

                        if (filterKeyProperty == null)
                        {
                            return null;
                        }

                        if (filterKeyProperty.PropertyType == typeof(string))
                        {
                            foreach (string s in c.values)
                            {
                                filterDataTable = filterDataTable.Where(x => filterKeyProperty.GetValue(x).ToString().ToLower().StartsWith(s.ToLower())).ToList();

                            }

                        }
                        else
                        {
                            filterDataTable = filterDataTable.Where(x => c.values.Contains(filterKeyProperty.GetValue(x).ToString())).ToList();

                        }

                    }
                }


                if (!string.IsNullOrEmpty(parameterPagination.sortKey))
                {

                    var sortProperty = typeof(AttributeViewModel).GetProperty(parameterPagination.sortKey);
                    if (sortProperty != null && parameterPagination.order == "asc")
                        filterDataTable = filterDataTable.OrderBy(x => sortProperty.GetValue(x)).ToList();

                    else if (sortProperty != null && parameterPagination.order == "desc")
                        filterDataTable = filterDataTable.OrderByDescending(x => sortProperty.GetValue(x)).ToList();

                    filterDataTable = filterDataTable.Skip((parameterPagination.PageNumber - 1) * parameterPagination.PageSize)
                        .Take(parameterPagination.PageSize).ToList();

                    return filterDataTable;
                }


                else
                {

                    filterDataTable = filterDataTable.Skip((parameterPagination.PageNumber - 1) * parameterPagination.PageSize)
                        .Take(parameterPagination.PageSize).ToList();

                    return filterDataTable;
                }
            }

            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
