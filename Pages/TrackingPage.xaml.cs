using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using System.Linq;

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
                    Source = new Bitmap(favani[i].ImageUrl)
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
                    Source = new Bitmap(favman[i].ImageUrl)
                };
                child.SetValue(Grid.ColumnProperty, i);
                child.SetValue(Grid.RowProperty, 4);
                Grid.Children.Add(child);
            }
        }
    }
}
