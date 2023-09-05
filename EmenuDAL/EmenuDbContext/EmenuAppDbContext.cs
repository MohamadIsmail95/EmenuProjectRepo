using EmenuDAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmenuDAL.EmenuDbContext
{
    public class EmenuAppDbContext: DbContext
    {
        public EmenuAppDbContext(DbContextOptions<EmenuAppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>().HasIndex(a => a.ArabicName).IsUnique();
            builder.Entity<EmenuDAL.Model.Attribute>().HasIndex(a => a.Name).IsUnique();
            builder.Entity<Variant>().HasIndex(a => a.Name).IsUnique();

        }

        //Database models

        public DbSet<Product> Products { get; set; }
        public DbSet<EmenuDAL.Model.Attribute> Attributes { get; set; }
        public DbSet<Variant> Variants { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

    }
}
