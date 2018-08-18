//using Avalonia.Controls;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;

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
            optionsBuilder.UseSqlite("Data Source=Shintenbou.db");
        }
    }

    public class FavouritedAnime
    {
        /// <summary>
        /// Item ID for EF Core
        /// </summary>
        public int Id { get; internal set; }

        /// <summary>
        /// Name of the Anime
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Anime's description
        /// </summary>
        public string Description { get; internal set; }

        /// <summary>
        /// The cover url
        /// </summary>
        public string ImageUrl { get; internal set; }

        /// <summary>
        /// Get the Image
        /// </summary>
        //public Stream Image { get; internal set; }

        /// <summary>
        /// The time at which the user stopped/paused at
        /// </summary>
        public float? TimeIndex { get; internal set; }
    }

    public class FavouritedManga
    {
        /// <summary>
        /// Item ID for EF Core
        /// </summary>
        public int Id { get; internal set; }

        /// <summary>
        /// Name of the Manga
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Manga's description
        /// </summary>
        public string Description { get; internal set; }

        /// <summary>
        /// The cover url
        /// </summary>
        public string ImageUrl { get; internal set; }        
        
        /// <summary>
        /// Get the Image
        /// </summary>
        //public Stream Image { get; internal set; }

        /// <summary>
        /// The page the user was last on
        /// </summary>
        public int? Page { get; internal set; }
    }
}