using AutoMapper;
using EmenuDAL.EmenuDbContext;
using EmenuDAL.IRepository;
using EmenuDAL.Model;
using EmenuDAL.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmenuDAL.Repository
{
    public class ProductRepository: RepositoryBase<Product, ProductViewModel, int>, IProductRepository
    {
        private readonly EmenuAppDbContext _context;
        IMapper _mapper;
        public ProductRepository(EmenuAppDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }

}
