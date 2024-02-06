using Invoice.DataAccess.Data.Repository;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Invoice.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Invoice.DataAccess.Data
{
    public class UnitOfWork:IUnitOfWork
    {
        private bool disposed = false;
        readonly InvoiceDbContext _InvoiceDbContext;

        public UnitOfWork(InvoiceDbContext InvoiceDbContext)
        {
            _InvoiceDbContext = InvoiceDbContext;
        }
        public IGenericRepository<Unit> UnitRepo => new GenericRepository<Unit>(_InvoiceDbContext);
        public IGenericRepository<Item> ItemRepo => new GenericRepository<Item>(_InvoiceDbContext);
        public IGenericRepository<Store> StoreRepo => new GenericRepository<Store>(_InvoiceDbContext);
        public IGenericRepository<Inventory> InventoryRepo => new GenericRepository<Inventory>(_InvoiceDbContext);
        public IGenericRepository<Entities.Invoice> InvoiceRepo => new GenericRepository<Entities.Invoice>(_InvoiceDbContext);
        public IGenericRepository<InvoiceDetails> InvoiceDetailsRepo => new GenericRepository<InvoiceDetails>(_InvoiceDbContext);
        public IGenericRepository<User> UserRepo => new GenericRepository<User>(_InvoiceDbContext);
        public IGenericRepository<Role> RoleRepo => new GenericRepository<Role>(_InvoiceDbContext);

        public int SaveChanges()
        {
            return _InvoiceDbContext.SaveChanges();
        }
        public int SaveChanges(string user)
        {
            using (var dbContextTransaction = _InvoiceDbContext.Database.BeginTransaction())
            {
                List<EntityEntry> entityList = _InvoiceDbContext.ChangeTracker.Entries().Where(p =>
                    p.State == Microsoft.EntityFrameworkCore.EntityState.Added ||
                    p.State == Microsoft.EntityFrameworkCore.EntityState.Deleted ||
                    p.State == Microsoft.EntityFrameworkCore.EntityState.Modified
                ).ToList();

                foreach (var itemEntry in entityList)
                {
                    if (itemEntry.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                    {
                        itemEntry.Property("CreatedBy").CurrentValue="22";
                        itemEntry.Property("CreatedDate").CurrentValue = DateTime.Now;
                    }
                    else if (itemEntry.State == Microsoft.EntityFrameworkCore.EntityState.Modified)
                    {
                        itemEntry.Property("LastModifiedBy").CurrentValue = "25";
                        itemEntry.Property("LastModifiedDate").CurrentValue = DateTime.Now;;
                    }
                    else
                    {
                        
                    }
                    
                }
               
            }
            return _InvoiceDbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _InvoiceDbContext.Dispose();
                }
            }
            this.disposed = true;
        }

    }
}
