using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration configuration;
        public DatabaseContext(DbContextOptions<DatabaseContext> options, IConfiguration configuration)
            : base(options)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                Console.WriteLine("Connection string is null or empty.");
            }
            else
            {
                optionsBuilder.UseNpgsql(connectionString);
            }
        }

        // DbSet properties for entities
        public DbSet<User> Users { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}
