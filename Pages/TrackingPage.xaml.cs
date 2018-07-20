using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Interactivity;
using System;
using System.Linq;
using Shintenbou.Rest.Objects;
using Shintenbou.Rest;

namespace Shintenbou.Pages
{
    public class TrackingPage : UserControl
    {
        private Database Db { get; set; }

        Grid Grid { get; set; }

        public TrackingPage()
        {
            Db = new Database();
            Db.Database.EnsureCreated();
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            Grid = this.Find<Grid>("TrackingGrid");
            LoadFavouriteAnime();
            LoadFavouriteManga();
        }
        
        private void LoadFavouriteAnime()
        {
            var favani = Db.FavouriteAnime.ToList();
            for(var i = 0; i < favani.Count(); i++) 
            {
                Grid.ColumnDefinitions.Add(new ColumnDefinition()
                {
                    MinWidth = 150
                });
                var child = new Image()
                {
                    Margin = new Thickness(50, 0, 0, 0),
                    IsVisible = true,
                    Name = $"Img{i}",
                    MinWidth = 150,
                    MinHeight = 250,
                    Source = new Bitmap(favani[i].ImageFile)
                };
                child.SetValue(Grid.ColumnProperty, i);
                child.SetValue(Grid.RowProperty, 2);
                Grid.Children.Add(child);
            }
        }

        private void LoadFavouriteManga()
        {
            var favman = Db.FavouriteManga.ToList();
            for(var i = 0; i < favman.Count(); i++) 
            {
                Grid.ColumnDefinitions.Add(new ColumnDefinition()
                {
                    MinWidth = 150
                });
                var child = new Image()
                {
                    Margin = new Thickness(50, 0, 0, 0),
                    IsVisible = true,
                    Name = $"Img{i}",
                    MinWidth = 150,
                    MinHeight = 250,
                    Source = new Bitmap(favman[i].ImageFile)
                };
                child.SetValue(Grid.ColumnProperty, i);
                child.SetValue(Grid.RowProperty, 4);
                Grid.Children.Add(child);
            }
        }

        private async void OnImportClick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("OnImport");
            //some textbox for username
            var username = "";
            var userdata = await Anilist.GetUserByName(username);
            await Db.FavouriteAnime.AddRangeAsync(userdata?.Favourites?.Animes.Select(x => new FavouritedAnime()
            {
                Description = x?.Description,
                Id = (Db.FavouriteAnime.Count() + 1),
                ImageUrl = x?.CoverImage.Medium,
                ImageFile = null,
                Name = x?.Title.English,
                TimeIndex = 0
            }));
            await Db.FavouriteManga.AddRangeAsync(userdata?.Favourites?.Mangas.Select(x => new FavouritedManga()
            {
                Page = 1,
                Description = x?.Description,
                Id = (Db.FavouriteManga.Count() + 1),
                ImageUrl = x?.CoverImage.Medium,
                ImageFile = null,
                Name = x?.Title.English
            }));

            //Alert the user the task is done here
        }
    }
}
