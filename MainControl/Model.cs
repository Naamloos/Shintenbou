using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Shintenbou
{
    public class Database : DbContext
    {
        /// <summary>
        /// Contains all the user's favourited anime
        /// </summary>
        public DbSet<FavouritedAnime> FavouriteAnime { get; set; }
        
        /// <summary>
        /// Contains all the user's favourited manga
        /// </summary>
        public DbSet<FavouritedManga> FavouriteManga { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=shintenbou.db");
        }
    }

    public class FavouritedAnime
    {
        /// <summary>
        /// Item ID for EF Core
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Name of the Anime
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Anime's description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// The cover url
        /// </summary>
        public string ImageUrl { get; private set; }

        /// <summary>
        /// The time at which the user stopped/paused at
        /// </summary>
        public float TimeIndex { get; private set; }
    }

    public class FavouritedManga
    {
        /// <summary>
        /// Item ID for EF Core
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Name of the Manga
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Manga's description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// The cover url
        /// </summary>
        public string ImageUrl { get; private set; }

        /// <summary>
        /// The page the user was last on
        /// </summary>
        public int Page { get; private set; }
    }
}