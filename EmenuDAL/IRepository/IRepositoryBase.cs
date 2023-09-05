using EmenuDAL.EmenuDbContext;
using EmenuDAL.Model.Filter;
using EmenuDAL.Model.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmenuDAL.IRepository
{
    public interface IRepositoryBase<TEntity, TModel, TKey>
    {
        IEnumerable<TEntity> GetIncludeWhere(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes);
        IEnumerable<TEntity> GetInclude( params Expression<Func<TEntity, object>>[] includes);
        IEnumerable<TEntity> GetAllWithoutCount();
        EmenuAppDbContext GetContext();
        TEntity GetByID(TKey id);
         ICollection<TEntity> GetWhere(Expression<Func<TEntity, bool>> where);

        void Add(TEntity entity);
        void Update(TEntity item);
        void RemoveItem(TEntity entity);

    }
}
