using Avalonia.Interactivity;
using System;
using System.Linq;
using Shintenbou.Rest;

namespace Shintenbou.Pages
{
    public class TrackingPage : TrackingPageBase
    {
        private async void OnImportClick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("OnImport");
            //some textbox for username
            var username = "";
            var userdata = await Anilist.GetUserByNameAsync(username);
            await base.Db.FavouriteAnime.AddRangeAsync(userdata?.Favourites?.Animes.Select(x => new FavouritedAnime()
            {
                Description = x?.Description,
                Id = (base.Db.FavouriteAnime.Count() + 1),
                ImageUrl = x?.CoverImage.Medium,
                //Image = null,
                Name = x?.Title.English,
                TimeIndex = 0
            }));
            await base.Db.FavouriteManga.AddRangeAsync(userdata?.Favourites?.Mangas.Select(x => new FavouritedManga()
            {
                Page = 1,
                Description = x?.Description,
                Id = (base.Db.FavouriteManga.Count() + 1),
                ImageUrl = x?.CoverImage.Medium,
                //Image = null,
                Name = x?.Title.English
            }));

            //Alert the user the task is done here
        }
    }
}
