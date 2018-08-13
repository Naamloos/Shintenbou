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
    public abstract class AnimePageBase : UserControl
    {
        public TextBox TextBox { get; set; }
        public Grid Grid { get; set; }
        public Button Button { get; set; }
        public Window Overlay { get; set; }
        public Dictionary<AnilistAnime, Button> LoadedAnimes { get; set; }

        public async Task DisplayAnimes(IReadOnlyList<AnilistAnime> animes)
        {
            LoadedAnimes = new Dictionary<AnilistAnime, Button>();
            int colcount = 0;
            int rowcount = 1;
            var topmargin = -200;
            using(var http = new HttpClient())
            {
                for(var i = 0; i < animes.Count; i++) 
                {
                    var path = Path.Combine(AppContext.BaseDirectory, "images", $"Img{i}.png");
                    var stream = await http.GetStreamAsync(animes[i].CoverImage.Large);
                    var fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                    await stream.CopyToAsync(fs);
                    fs.Dispose();
                    stream.Dispose();
                    
                    var child = new Image()
                    {
                        Margin = new Thickness(50, topmargin, 0, 0),
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

                    var button = new Button()
                    {
                        Margin = new Thickness(50, topmargin, 0, 0),
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
                    LoadedAnimes.Add(animes[i], button);
                    Grid.Children.AddRange(new List<Control> {child, button});
                    colcount++;
                    if(colcount == 3)
                    {
                        colcount = 0;
                        rowcount++;
                        topmargin = (rowcount <= 1)?  -200 : (rowcount <= 2)?  -140 : 60;
                        Grid.RowDefinitions.Add(new RowDefinition()
                        {
                            MinHeight = 150
                        });
                    }
                }
            }
        }

        public Task ClearImagesAsync()
        {
            var items = this.Grid.Children;
            if (items.Count() > 2)
            {
                Console.WriteLine(string.Join(" | ", items.Skip(2).Select(x => x.Name)));
                this.Grid.Children.RemoveRange(2, (items.Count() - 2));
                this.Grid.RowDefinitions.Clear();
                this.Grid.ColumnDefinitions.Clear();
                for (var i = 0; i < 4; i++) this.Grid.ColumnDefinitions.Add(new ColumnDefinition() { MinWidth = 20 });
                for (var i = 0; i < 3; i++) this.Grid.RowDefinitions.Add(new RowDefinition() { MinHeight = 10 });
            }
            return Task.CompletedTask;
        }

        public void OnClick(object sender, RoutedEventArgs e)
        {
            var btn = this.Grid.Children.FirstOrDefault(x => x == e.Source);
            var item = this.LoadedAnimes.First(x => x.Value == btn);
            if (Overlay != null) Overlay.Hide();
            var btnname = btn.Name.Replace("Btn", "");
            Overlay = new Windows.OverlayWindow(item.Key.Title.EnglishReadableName, item.Key.Description.Replace("<br>", "\n"), int.Parse(btnname));
            Overlay.Show();
        }
    }

    public abstract class MangaPageBase : UserControl
    {
        public TextBox TextBox { get; set; }
        public Grid Grid { get; set; }
        public Button Button { get; set; }
        public Window Overlay { get; set; }
        public Dictionary<AnilistManga, Button> LoadedMangas { get; set; }

        public async Task DisplayMangas(IReadOnlyList<AnilistManga> Mangas)
        {
            LoadedMangas = new Dictionary<AnilistManga, Button>();
            int colcount = 0;
            int rowcount = 1;
            var topmargin = -200;
            using (var http = new HttpClient())
            {
                for (var i = 0; i < Mangas.Count; i++)
                {
                    var path = Path.Combine(AppContext.BaseDirectory, "images", $"MangaImg{i}.png");
                    var stream = await http.GetStreamAsync(Mangas[i].CoverImage.Large);
                    var fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                    await stream.CopyToAsync(fs);
                    fs.Dispose();
                    stream.Dispose();

                    var child = new Image()
                    {
                        Margin = new Thickness(50, topmargin, 0, 0),
                        IsVisible = true,
                        Name = $"MangaImg{i}",
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

                    var button = new Button()
                    {
                        Margin = new Thickness(50, topmargin, 0, 0),
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
                    LoadedMangas.Add(Mangas[i], button);
                    Grid.Children.AddRange(new List<Control> { child, button });
                    colcount++;
                    if (colcount == 3)
                    {
                        colcount = 0;
                        rowcount++;
                        topmargin = (rowcount <= 1) ? -200 : (rowcount <= 2) ? -140 : 60;
                        Grid.RowDefinitions.Add(new RowDefinition()
                        {
                            MinHeight = 150
                        });
                    }
                }
            }
        }
        
        public Task ClearImagesAsync()
        {
            var items = this.Grid.Children;
            if (items.Count() > 2)
            {
                this.Grid.Children.RemoveRange(2, (items.Count() - 2));
                this.Grid.RowDefinitions.Clear();
                this.Grid.ColumnDefinitions.Clear();
                for (var i = 0; i < 4; i++) this.Grid.ColumnDefinitions.Add(new ColumnDefinition() { MinWidth = 20 });
                for (var i = 0; i < 3; i++) this.Grid.RowDefinitions.Add(new RowDefinition() { MinHeight = 10 });
            }
            return Task.CompletedTask;
        }

        public void OnClick(object sender, RoutedEventArgs e)
        {
            var btn = this.Grid.Children.FirstOrDefault(x => x == e.Source);
            var item = this.LoadedMangas.First(x => x.Value == btn);
            if (Overlay != null) Overlay.Hide();
            var btnname = btn.Name.Replace("Btn", "");
            Overlay = new Windows.OverlayWindow(item.Key.Title.EnglishReadableName, item.Key.Description.Replace("<br>", "\n"), int.Parse(btnname));
            Overlay.Show();
        }
    }
}