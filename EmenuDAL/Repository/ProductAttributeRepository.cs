using EmenuDAL.IRepository;
using EmenuDAL.Model.ViewModel;
using EmenuDAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmenuDAL.Model.Binding;
using AutoMapper;
using EmenuDAL.EmenuDbContext;

namespace EmenuDAL.Repository
{
    public class ProductAttributeRepository: RepositoryBase<ProductAttribute, ProductViewModel, int>, IProductAttributeRepository
    {
        private readonly EmenuAppDbContext _context;
        IMapper _mapper;
        public ProductAttributeRepository(EmenuAppDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
