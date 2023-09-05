using EmenuDAL.IRepository;
using EmenuDAL.Model.Binding;
using EmenuDAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EmenuDAL.EmenuDbContext;

namespace EmenuDAL.Repository
{
    public class ProductImageRepository : RepositoryBase<ProductImage, AddProductImageBinding, int>, IProductImageRepository
    {
        private readonly EmenuAppDbContext _context;
        IMapper _mapper;
        public ProductImageRepository(EmenuAppDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
