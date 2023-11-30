using Microsoft.EntityFrameworkCore;
using StatisticsWebApp.Models;

namespace StatisticsWebApp
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Statistic> Statistics { get; set; }
    }
}
