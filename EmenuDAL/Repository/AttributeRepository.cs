using EmenuDAL.IRepository;
using EmenuDAL.Model.ViewModel;
using EmenuDAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EmenuDAL.EmenuDbContext;
using Attribute = EmenuDAL.Model.Attribute;

namespace EmenuDAL.Repository
{
    public class AttributeRepository: RepositoryBase<Attribute, AttributeViewModel, int>, IAttributeRepository
    {
        private readonly EmenuAppDbContext _context;
        IMapper _mapper;
        public AttributeRepository(EmenuAppDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
