using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Invoice.DataAccess.Data.Repository;
using Invoice.DataAccess.Entities;

namespace Invoice.DataAccess.Data
{
    public interface IUnitOfWork:IDisposable
    {
        public IGenericRepository<Unit> UnitRepo { get; }
        public IGenericRepository<Item> ItemRepo { get; }
        public IGenericRepository<Store> StoreRepo { get; }
        public IGenericRepository<Inventory> InventoryRepo { get; }
        public IGenericRepository<Entities.Invoice> InvoiceRepo { get; }
        public IGenericRepository<InvoiceDetails> InvoiceDetailsRepo { get; }
        public IGenericRepository<User> UserRepo { get; }

        public IGenericRepository<Role> RoleRepo { get; }
        void Dispose();
        int SaveChanges();
        int SaveChanges(string user);
    }
}
