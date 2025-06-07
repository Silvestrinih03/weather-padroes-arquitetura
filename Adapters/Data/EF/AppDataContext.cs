using Domain.Models;
using EntityFramework.Exceptions.MySQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Data.EF
{
    [ExcludeFromCodeCoverage]
    public class AppDataContext : DbContext
    {
        public DbSet<Weather> Weather { get; set; }

        private readonly IConfiguration _configuration;

        public AppDataContext(
            DbContextOptions<AppDataContext> options,
            IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public AppDataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var conn = _configuration.GetConnectionString("MySQLConnection")
                           ?? throw new InvalidOperationException(
                                  "Connection string 'MySQLConnection' not found.");

                optionsBuilder
                    .UseMySql(conn, ServerVersion.AutoDetect(conn))
                    .UseExceptionProcessor();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Weather>(entity =>
            {
                entity.ToTable("weather");
                entity.HasKey(e => e.Id);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}