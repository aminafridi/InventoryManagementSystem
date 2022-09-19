using IMS.CoreBusiness;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Plugins.EFCore
{
    public class IMSContext : DbContext
    {
        public IMSContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
        public DbSet<ProductTransaction> ProductTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //build relationships
            modelBuilder.Entity<ProductInventory>()
                .HasKey(pi => new { pi.ProductId, pi.InventoryId });

            modelBuilder.Entity<ProductInventory>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.ProductInventories)
                .HasForeignKey(pi => pi.ProductId);

            modelBuilder.Entity<ProductInventory>()
                .HasOne(pi => pi.Inventory)
                .WithMany(i => i.ProductInventories)
                .HasForeignKey(pi => pi.InventoryId);

            //seeding data
            //modelbuilder.entity<inventory>().hasdata(
            //    new inventory { inventoryid = 1, inventoryname = "gas engine", price = 1000, quantity = 1 },
            //    new inventory { inventoryid = 2, inventoryname = "body", price = 400, quantity = 1 },
            //    new inventory { inventoryid = 3, inventoryname = "wheel", quantity = 4, price = 100 },
            //    new inventory { inventoryid = 4, inventoryname = "seat", price = 50, quantity = 5 },
            //    new inventory { inventoryid = 5, inventoryname = "electric engine", price = 8000, quantity = 2 },
            //    new inventory { inventoryid = 6, inventoryname = "battery", price = 400, quantity = 5 }
            //);

            //modelbuilder.entity<product>().hasdata(
            //    new product { productid = 1, productname = "gas car", price = 20000, quantity = 1 },
            //    new product { productid = 2, productname = "electric car", price = 15000, quantity = 1 }
            //);

            //modelbuilder.entity<productinventory>().hasdata(
            //    new productinventory { productid = 1, inventoryid = 1, inventoryquantity = 1 }, // engine
            //    new productinventory { productid = 1, inventoryid = 2, inventoryquantity = 1 }, // body
            //    new productinventory { productid = 1, inventoryid = 3, inventoryquantity = 4 }, //4 wheels
            //    new productinventory { productid = 1, inventoryid = 4, inventoryquantity = 5 } //5 seats
            //);

            //modelbuilder.entity<productinventory>().hasdata(
            //    new productinventory { productid = 2, inventoryid = 5, inventoryquantity = 1 }, // engine
            //    new productinventory { productid = 2, inventoryid = 2, inventoryquantity = 1 }, // body
            //    new productinventory { productid = 2, inventoryid = 3, inventoryquantity = 4 }, //4 wheels
            //    new productinventory { productid = 2, inventoryid = 4, inventoryquantity = 5 }, //5 seats
            //    new productinventory { productid = 2, inventoryid = 6, inventoryquantity = 1 } // battery
            //);
        }
    }
}

