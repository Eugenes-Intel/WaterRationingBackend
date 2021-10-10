using Microsoft.EntityFrameworkCore;
using System;
using WaterRationingBackend.Entities;

namespace WaterRationingBackend.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }

        public DbSet<Suburb> Suburbs { get; set; }

        public DbSet<UsageHistory> UsageHistories { get; set; }

    }
}
