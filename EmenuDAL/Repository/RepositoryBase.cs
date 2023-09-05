using AutoMapper;
using EmenuDAL.EmenuDbContext;
using EmenuDAL.IRepository;
using EmenuDAL.Model;
using EmenuDAL.Model.Filter;
using EmenuDAL.Model.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmenuDAL.Repository
{
    public class RepositoryBase<TEntity, TModel, TKey> : IRepositoryBase<TEntity, TModel, TKey> where TEntity : class
    {
        private readonly EmenuAppDbContext _context;
        IMapper _mapper;
        DbSet<TEntity> dataTable
        {
            get
            {
                return _context.Set<TEntity>();
            }
        }


        public RepositoryBase(EmenuAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    
        public IEnumerable<TEntity> GetIncludeWhere(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes)
        {
            var data = includes.Aggregate(dataTable.AsQueryable(),
                          (current, include) => current.Include(include)).Where(where).ToList();
            return data;

        }
        public IEnumerable<TEntity> GetInclude(params Expression<Func<TEntity, object>>[] includes)
        {
            var data = includes.Aggregate(dataTable.AsQueryable(),
                          (current, include) => current.Include(include)).ToList();
            return data;

        }
   
        public EmenuAppDbContext GetContext()
        {
            return _context;
        }

        public virtual IEnumerable<TEntity> GetAllWithoutCount()
        {
            var res = dataTable.ToList();
            return res;
        }
        public virtual void Add(TEntity entity)
        {
            dataTable.Add(entity);
            _context.SaveChanges();
        }

        public virtual void Update(TEntity item)
        {

            _context.Entry(item).State = EntityState.Modified;

            _context.SaveChanges();

        }

        public virtual void RemoveItem(TEntity item)
        {
            dataTable.Remove(item);
            _context.SaveChanges();
        }

        public virtual TEntity GetByID(TKey id)
        {

            return dataTable.Find(id);

        }

        public ICollection<TEntity>GetWhere(Expression<Func<TEntity, bool>> where)
        {
            return  dataTable.AsNoTracking().Where(where).ToList();
        }

        
    }
}
