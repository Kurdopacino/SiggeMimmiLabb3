using Microsoft.EntityFrameworkCore;
using SiggeMimmiLabb3.Models;

using Microsoft.EntityFrameworkCore;
using SiggeMimmiLabb3.Models;

namespace SiggeMimmiLabb3.Data
{
    public class WeatherDbContext : DbContext
    {
        public DbSet<WeatherData> WeatherData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Använd SQLite och definiera filnamnet för databasen
            optionsBuilder.UseSqlite("Data Source=WeatherData.db");
        }
    }
}

