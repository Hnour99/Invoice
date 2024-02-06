using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Invoice.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Invoice.DataAccess.Data
{
    public class InvoiceDbContext: DbContext
    {
        public InvoiceDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<Entities.Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceDetails> InvoiceDetailss { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        #region Secuirty
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region DeleteBehavior
            modelBuilder.Entity<Store>().HasMany(e => e.Items).WithOne(e => e.Store).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Store>().HasMany(e => e.Inventories).WithOne(e => e.Store).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Store>().HasMany(e => e.Invoices).WithOne(e => e.Store).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Item>().HasMany(e => e.Inventories).WithOne(e => e.Item).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Item>().HasMany(e => e.InvoiceDetailss).WithOne(e => e.Item).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Role>().HasMany(e => e.Users).WithOne(e => e.Role).OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Seeds
            modelBuilder.Entity<Store>().HasData(
                new Store() {Id=1, Code= 1 , Name ="Store_1" },
                new Store() { Id = 2, Code = 2, Name ="Store_2" }
            );
            modelBuilder.Entity<Unit>().HasData(
                new Unit() { Id = 1, Code = 1, Name = "Unit_1" },
                new Unit() { Id = 2, Code = 2, Name = "Unit_2" }
            );
            modelBuilder.Entity<Item>().HasData(
                new Item() { Id = 1, Code = 1, Name = "Item_1" ,StoreId = 1},
                new Item() { Id = 2, Code = 2, Name = "Item_2" , StoreId = 1 },
                new Item() { Id = 3, Code = 3, Name = "Item_3", StoreId = 2 }
            );
           
            modelBuilder.Entity<Role>().HasData(
                new Role() { Id = 1, Code = 1, Name = "Admin"},
                new Role() { Id = 2, Code = 2, Name = "User" }

            );
            modelBuilder.Entity<User>().HasData(
                new User() { Id = 1, Code = 1, UserName = "Admin",Password= "Admin", RoleId = 1},
                new User() { Id = 2, Code = 2, UserName = "User", Password = "User", RoleId = 2 }
            );
            #endregion


        }
    }
}
