using System.Diagnostics;
using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace DNSPromotionManager.Models
{
    public class DNSContext : DbContext
    {
        public DbSet<Kind> Kinds { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Branch> Branchs { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Characteristic> Characteristics { get; set; }
        public DbSet<CharacteristicValue> CharacteristicValues { get; set; }
        public DbSet<BranchPromotion> BranchPromotions { get; set; }
        public DbSet<Residue> Residues { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }
        public DbSet<ProductCharacteristic> ProductCharacteristics { get; set; }

        public DNSContext(DbContextOptions<DNSContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductPrice>()
                .Property(c => c.Value)
                .HasColumnType("decimal(15, 2)");

            var types = modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys());

            foreach (var relationship in types)
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}

