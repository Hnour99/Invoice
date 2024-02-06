using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.DataAccess.Data.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity GetById(object Id);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void RemoveById(object Id);
        void Remove(TEntity entity);
        IQueryable<TEntity> GetAll();
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> whereCondition, string includeProperties = "");
       
    }
}
