using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Input;
using Avalonia.Media.Imaging;
using Avalonia.Interactivity;
using Avalonia.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Shintenbou.Rest;
using Shintenbou.Rest.Objects;

namespace Shintenbou.Pages
{
    public class AnimePage : UserControl
    {
        private TextBox TextBox { get; set; }
        private Grid Grid { get; set; }
        private Button Button { get; set; }

        public AnimePage()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            this.Grid = this.Find<Grid>("AnimeGrid");
            this.TextBox = this.Find<TextBox>("AnimeSearch");
            this.Button = this.Find<Button>("AnimeSearchButton");
            this.TextBox.KeyDown += OnKeyDown;
            this.Button.Click += OnQuerySubmit;
        }

        private async void OnQuerySubmit(object sender, RoutedEventArgs e)
        {
            await ClearImagesAsync();
            var animes = await Anilist.GetAnimeByNameAsync(this.TextBox.Text);
            if(animes != null) await DisplayAnimes(animes.ToList());
        }

        private async void OnKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                await ClearImagesAsync();
                var animes = await Anilist.GetAnimeByNameAsync(this.TextBox.Text);
                if(animes != null) await DisplayAnimes(animes.ToList());
            }
        }

        private async Task DisplayAnimes(IReadOnlyList<AnilistAnime> animes)
        {
            int colcount = 0;
            int rowcount = 1;
            using(var http = new HttpClient())
            {
                for(var i = 0; i < animes.Count(); i++) 
                {
                    var path = Path.Combine(AppContext.BaseDirectory, "images", $"Img{i}.png");
                    var fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                    using(var stream = await http.GetStreamAsync(animes[i].CoverImage.Medium))
                    {
                        await stream.CopyToAsync(fs);
                        fs.Dispose();
                        var child = new Image()
                        {
                            Margin = (i > 2) ? new Thickness(50, 50, 0, 0) : new Thickness(50, -200, 0, 0),
                            IsVisible = true,
                            Name = $"Img{i}",
                            Width = 150,
                            Height = 250,
                            MinWidth = 150,
                            MinHeight = 250,
                            Source = new Bitmap(path),
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center
                        };
                        child.SetValue(Grid.ColumnProperty, colcount);
                        child.SetValue(Grid.RowProperty, rowcount);
                        child.ZIndex = 1;
                        colcount++;
                        if(colcount == 3)
                        {
                            colcount = 0;
                            rowcount++;
                            Grid.RowDefinitions.Add(new RowDefinition()
                            {
                                MinHeight = 150
                            });
                        }
                        Grid.Children.Add(child);
                        var button = new Button()
                        {
                            Margin = (i > 2) ? new Thickness(50, 50, 0, 0) : new Thickness(50, -200, 0, 0),
                            IsVisible = true,
                            Opacity = 0,
                            Name = $"Btn{i}",
                            Width = 150,
                            Height = 250,
                            MinWidth = 150,
                            MinHeight = 250,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center
                        };
                        button.SetValue(Grid.ColumnProperty, colcount);
                        button.SetValue(Grid.RowProperty, rowcount);
                        button.ZIndex = 2;
                        button.Click += OnClick;
                        Grid.Children.Add(button);
                    }
                }
            }
        }
        private void OnClick(object sender, RoutedEventArgs e) => Console.WriteLine("Clicked");

        private Task ClearImagesAsync()
        {
            var items = this.Grid.Children.Where(x => x.GetType() == typeof(Image));
            this.Grid.Children.RemoveRange(2, items.Count());
            this.Grid.RowDefinitions.Clear();
            this.Grid.ColumnDefinitions.Clear();
            for(var i = 0; i < 4; i++) this.Grid.ColumnDefinitions.Add(new ColumnDefinition(){MinWidth=20});
            for(var i = 0; i < 3; i++) this.Grid.RowDefinitions.Add(new RowDefinition(){MinHeight=10});
            return Task.CompletedTask;
        }
    }
}
