using Infra.MySql.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.MySql
{
    public class DataContext : DbContext
    {
        public DbSet<CompanyModel> Company { get; set; }
        public DbSet<PhotoModel> Photo { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyModel>()
                .HasMany(x => x.Photos)
                .WithOne(x => x.Company);
        }
    }
}