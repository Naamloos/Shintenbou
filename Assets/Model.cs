using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Shintenbou.Rest.Objects;

namespace Shintenbou
{
    public class Database : DbContext
    {
        public DbSet<AnilistAnime> FavouriteAnime { get; set; }
        //This will need to be changed to AnilistManga
        public DbSet<object> FavouriteManaga { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=shintenbou.db");
        }
    }
}