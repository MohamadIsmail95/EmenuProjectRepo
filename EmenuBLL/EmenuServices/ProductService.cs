using AutoMapper;
using EmenuBLL.IEmenuServices;
using EmenuDAL.EmenuDbContext;
using EmenuDAL.IRepository;
using EmenuDAL.Model;
using EmenuDAL.Model.ApiResponse;
using EmenuDAL.Model.Binding;
using EmenuDAL.Model.Filter;
using EmenuDAL.Model.Helper;
using EmenuDAL.Model.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static System.Net.Mime.MediaTypeNames;

namespace EmenuBLL.EmenuServices
{
    public class ProductService: IProductService
    {

        private readonly IProductRepository _productRepository;
        private readonly IProductAttributeRepository _productAttributeRepository;
        private readonly IProductImageRepository _productImageRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IProductAttributeRepository productAttributeRepository,
          IProductImageRepository productImageRepository,  IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _productAttributeRepository = productAttributeRepository;
            _productImageRepository = productImageRepository;
        }

        public Response<bool> AddNewProduct(AddProductBinding prod)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                try
                {
                    if(CheckUniq(0,prod.arabicName)==false)
                    {
                        return new Response<bool>(false, false, null,"This Product is dublicated", (int)ResponseStatus.ApiReturnCode.fail);

                    }

                    Product product = _mapper.Map<Product>(prod);
                    _productRepository.Add(product);

                    foreach(var s in prod.attributeVariants)
                    {
                        ProductAttribute pa = _mapper.Map<ProductAttribute>(s);
                        pa.ProductId = product.Id;
                         _productAttributeRepository.Add(pa);

                        foreach(var t in s.images)
                        {
                            ProductImage productImage = new ProductImage(pa.Id,t);
                            _productImageRepository.Add(productImage);
                        }
                    }
                    transaction.Complete();
                    return new Response<bool>(true, true, null,null, (int)ResponseStatus.ApiReturnCode.success);


                }
                catch (Exception ex)
                {
                    return new Response<bool>(false, false, null,ex.Message, (int)ResponseStatus.ApiReturnCode.fail);

                }
            }
        }

        public Response<List<ProductViewModel>> GetAllProduct(List<Filter> filters, ParameterPagination parameters)
        {
            try
            {

                List<ProductViewModel> result = new List<ProductViewModel>();
                var dataSource = _productRepository.GetContext().Products.Include(x => x.ProductAttributes).
                    ThenInclude(y => y.Attribute).ThenInclude(z => z.Variants).AsQueryable();

                result = dataSource.Select(x => new ProductViewModel()
                {
                    id = x.Id,
                    arabicName = x.ArabicName,
                    englishName=x.EnglishName,
                    arabicDescription = x.ArabicDescription,
                    englishDescription=x.EnglishDescription,
                    productAttributes = x.ProductAttributes.Select(y => new ProductAttributeInv()
                    {
                        id = y.Id,
                        attributeId = (int)y.AttributId,
                        attributeName = y.Attribute.Name,
                        variantId = (int)y.VarId,
                        variantName = y.Variant.Name,
                    }).ToList()
                }).ToList();
                var count = result.Count();
                AddProduct(result);
                result = FilterandPagination(result, parameters, filters);

                if(result==null)
                {
                   return new Response<List<ProductViewModel>>(true, null, null, "This Filter Key is invalid", (int)ResponseStatus.ApiReturnCode.fail, 0);

                }
               return new Response<List<ProductViewModel>>(true, result, null, null, (int)ResponseStatus.ApiReturnCode.success, count);

            }
            catch (Exception ex)
            {
              return new Response<List<ProductViewModel>>(false, null, null, ex.Message, (int)ResponseStatus.ApiReturnCode.fail);

            }
        }

        public Response<ProductViewModel> GetProductById(int id)
        {
            try
            {

                List<ProductViewModel> result = new List<ProductViewModel>();
                var dataSource = _productRepository.GetContext().Products.Where(x=>x.Id==id).Include(x => x.ProductAttributes).
                    ThenInclude(y => y.Attribute).ThenInclude(z => z.Variants).AsQueryable();
                if(dataSource.FirstOrDefault()==null)
                {
                    return new Response<ProductViewModel>(false, null, null,"This Product Id is invalid.", (int)ResponseStatus.ApiReturnCode.fail);
                }
                result = dataSource.Select(x => new ProductViewModel()
                {
                    id = x.Id,
                    arabicName = x.ArabicName,
                    englishName = x.EnglishName,
                    arabicDescription = x.ArabicDescription,
                    englishDescription = x.EnglishDescription,
                    productAttributes = x.ProductAttributes.Select(y => new ProductAttributeInv()
                    {
                        id = y.Id,
                        attributeId = (int)y.AttributId,
                        attributeName = y.Attribute.Name,
                        variantId = (int)y.VarId,
                        variantName = y.Variant.Name,
                    }).ToList()
                }).ToList();
                AddProduct(result);
                
               return new Response<ProductViewModel>(true, result.FirstOrDefault(), null, null, (int)ResponseStatus.ApiReturnCode.success);

            }
            catch (Exception ex)
            {
              return new Response<ProductViewModel>(false, null, null, ex.Message, (int)ResponseStatus.ApiReturnCode.fail);

            }
            
        }

        public Response<bool> RemoveProduct(int id)
        {
            var Prod=_productRepository.GetByID(id);
            if(Prod==null)
            {
                return new Response<bool>(true, false, null, $"This Product id: {id} is invalid", (int)ResponseStatus.ApiReturnCode.fail);
            }

            _productRepository.RemoveItem(Prod);
            return new Response<bool>(true, true, null,null, (int)ResponseStatus.ApiReturnCode.success);

        }

        public Response<bool> UpdateProduct(UpdateProductBinding prod)
        {
            if (CheckUniq(prod.id, prod.arabicName) == false)
            {
                return new Response<bool>(false, false, null, "This Product is dublicated", (int)ResponseStatus.ApiReturnCode.fail);

            }
            var prd =  _productRepository.GetWhere(x=>x.Id==prod.id);
            if (prod == null)
            {
                return new Response<bool>(true, false, null, $"This Product id: {prod.id} is invalid", (int)ResponseStatus.ApiReturnCode.fail);

            }
            using (TransactionScope transaction = new TransactionScope())
            {
                try
                {
                    Product product = _mapper.Map<Product>(prod);
                    _productRepository.Update(product);

                    foreach (var s in prod.attributeVariants)
                    {
                        ProductAttribute pa = new ProductAttribute(prod.id, s);
                        _productAttributeRepository.Update(pa);

                        foreach (var t in s.images)
                        {
                            ProductImage productImage = new ProductImage(t);
                            _productImageRepository.Update(productImage);
                        }
                    }
                    transaction.Complete();
                    return new Response<bool>(true, true, null, null, (int)ResponseStatus.ApiReturnCode.success);


                }
                catch(Exception ex)
                {
                    return new Response<bool>(false, false, null, ex.Message, (int)ResponseStatus.ApiReturnCode.fail);

                }
            }


        }
        private List<ProductViewModel> FilterandPagination(List<ProductViewModel> filterDataTable, ParameterPagination parameterPagination, List<Filter> filter)
        {
            try
            {


                if (filter != null || filter.Count != 0)
                {
                    foreach (Filter c in filter)
                    {
                        var filterKeyProperty = typeof(ProductViewModel).GetProperty(c.key);

                        if(filterKeyProperty==null)
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

                    var sortProperty = typeof(ProductViewModel).GetProperty(parameterPagination.sortKey);
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

            catch(Exception ex)
            {
                return null;
            }
        }

        private  List<NewProductImage> GetProductImage(int prodAtt)
        {
            return _mapper.Map<List<NewProductImage>>(_productImageRepository.GetWhere(x => x.ProductAttId == prodAtt).ToList());
            
        }

        private void AddProduct(List<ProductViewModel> model)
        {
            foreach (var s in model)
            {
                foreach (var t in s.productAttributes)
                {
                    t.images = GetProductImage(t.id);
                }
            }
        }

        private bool CheckUniq(int id,string name)
        {
            var prod=_productRepository.GetWhere(x=>x.ArabicName == name && x.Id!=id).FirstOrDefault();
            if(prod==null)
            {
                return true;
            }
            return false;
        }
    }
}
