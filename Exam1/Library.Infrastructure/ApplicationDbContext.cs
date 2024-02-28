using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly string connectionString;
        private readonly string migrationAssembly;

        public DbSet<Book> Books { get; set; }
        public ApplicationDbContext(string connectionString, string migrationAssembly)
        {
            this.connectionString = connectionString;
            this.migrationAssembly = migrationAssembly;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString, m => m.MigrationsAssembly(migrationAssembly));
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}
