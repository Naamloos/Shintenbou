using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Shintenbou
{
    public class Database : DbContext
    {
        public DbSet<TrackingData> TrackingData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=shintenbou.db");
        }
    }

    public class TrackingData
    {

    }
}