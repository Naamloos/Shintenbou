using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Media;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Shintenbou;

namespace Shintenbou.Pages
{
    public class TrackingPage : UserControl
    {
        Database Db { get; set; }
        Grid Grid { get; set; }
        public TrackingPage()
        {
            Db = new Database();
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            Grid = this.Find<Grid>("TrackingGrid");
        }
        
        //ef core is being weird and wont see that the tables exist so I've put this in comments to allow the app to run
        /*
        private void LoadFavoriteAnime()
        {
            for(var i = 0; i < Db.FavouriteAnime.Count(); i++) 
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
                    Source = new Bitmap(Db.FavouriteAnime[i].CoverImage.Medium)
                };
                child.SetValue(Grid.ColumnProperty, i);
                child.SetValue(Grid.RowProperty, 1);
                Grid.Children.Add(child);
            }
        }

        private void LoadFavoriteManga()
        {
            for(var i = 0; i < Db.FavouriteManga.Count(); i++) 
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
                    Source = new Bitmap(Db.FavouriteManga[i].CoverImage.Medium)
                };
                child.SetValue(Grid.ColumnProperty, i);
                child.SetValue(Grid.RowProperty, 4);
                Grid.Children.Add(child);
            }
        }
        */
    }
}
