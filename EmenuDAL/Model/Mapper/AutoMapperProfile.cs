using AutoMapper;
using EmenuDAL.Model.Binding;
using EmenuDAL.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmenuDAL.Model.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<Product, AddProductBinding>().ReverseMap();
            CreateMap<Product, UpdateProductBinding>().ReverseMap();
            CreateMap<ProductAttribute, AttributeVariant>().ReverseMap();
            CreateMap<ProductImage, NewProductImage>().ForMember(x => x.id, c => c.MapFrom(c => c.Id))
             .ForMember(x => x.productImagePath, c => c.MapFrom(c => c.ProductImagePath))
             .ForMember(x => x.isMainImage, c => c.MapFrom(c => c.IsMainImage)).ReverseMap();
        }
    }
}
