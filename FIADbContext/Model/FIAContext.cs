using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using FIADbContext.Model.DTO;
using FIADbContext.Model.Configure;
using FIADbContext.Connection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FIADbContext.Model
{
    public class FIAContext : IdentityDbContext<UserDbDTO>
    {

        public DbSet<EnterpriseDbDTO> Enterprises { get; set; }
        public DbSet<FinancialResultDbDTO> FinancialResults { get; set; }

        public FIAContext() : base() { }
        public FIAContext(DbContextOptions options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(new ConnectionStringManager().ConnectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new EnterpriseeDbDTOConfiguration());
            modelBuilder.ApplyConfiguration(new FinancialResultDbDTOConfiguration());
        }
    }   
}
