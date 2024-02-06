using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Invoice.DataAccess.Data.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
    private readonly InvoiceDbContext _context;
    private readonly DbSet<TEntity> _entity;

    public GenericRepository(InvoiceDbContext context)
    {
        _context = context;
        _entity = _context.Set<TEntity>();
    }

    public void Add(TEntity entity)
    {
        _entity.Add(entity).GetDatabaseValues();
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
        _entity.AddRange(entities);
    }
    public void Update(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }
    public IQueryable<TEntity> GetAll()
    {
        return _entity.AsQueryable();
    }

    public TEntity GetById(object Id)
    {
        return _entity.Find(Id);
    }

    public void Remove(TEntity entity)
    {
        _entity.Remove(entity);
    }
    public void RemoveById(object Id)
    {
        _entity.Remove(GetById(Id));
    }

    public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> whereCondition, string includeProperties = "")
    {
        IQueryable<TEntity> query = _entity;

        if (includeProperties != null)
        {
            foreach (var includeProperty in includeProperties.Split
                         (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }
        return query.Where(whereCondition).SingleOrDefault();
    }

    

}
}
