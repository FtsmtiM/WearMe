using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace WearMe.Data.Models
{
    public class AppDbContext:IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }

        public DbSet<Cloth> Clothes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Sex> SexGroups { get; set; }
        public DbSet<SexCategory> SexCategories { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //creating many-to-many in Ef core (https://www.learnentityframeworkcore.com/configuration/many-to-many-relationship-configuration)
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SexCategory>()
            .HasKey(sc => new { sc.CategoryId, sc.SexId });

            modelBuilder.Entity<SexCategory>()
            .HasOne(sc => sc.Sex)
            .WithMany(s => s.SexCategories)
            .HasForeignKey(sc => sc.SexId);

            modelBuilder.Entity<SexCategory>()
            .HasOne(sc => sc.Category)
            .WithMany(c => c.SexCategories)
            .HasForeignKey(sc => sc.CategoryId);


         
        }

       
    }
}
