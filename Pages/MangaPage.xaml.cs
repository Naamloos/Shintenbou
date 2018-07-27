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
    public class MangaPage : UserControl
    {
        private TextBox TextBox { get; set; }
        private Grid Grid { get; set; }
        private Button Button { get; set; }
        private Window Overlay { get; set; }
        private Dictionary<AnilistManga, Button> LoadedMangas { get; set; }

        public MangaPage()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            this.Grid = this.Find<Grid>("MangaGrid");
            this.TextBox = this.Find<TextBox>("MangaSearch");
            this.Button = this.Find<Button>("MangaSearchButton");
            this.TextBox.KeyDown += OnKeyDown;
            this.Button.Click += OnQuerySubmit;
        }

        private async void OnQuerySubmit(object sender, RoutedEventArgs e)
        {
            await ClearImagesAsync();
            var Mangas = await Anilist.GetMangaByNameAsync(this.TextBox.Text);
            if(Mangas != null) await DisplayMangas(Mangas.ToList());
        }

        private async void OnKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                await ClearImagesAsync();
                var Mangas = await Anilist.GetMangaByNameAsync(this.TextBox.Text);
                if(Mangas != null) await DisplayMangas(Mangas.ToList());
            }
        }

        private async Task DisplayMangas(IReadOnlyList<AnilistManga> Mangas)
        {
            LoadedMangas = new Dictionary<AnilistManga, Button>();
            int colcount = 0;
            int rowcount = 1;
            var topmargin = -200;
            using(var http = new HttpClient())
            {
                for(var i = 0; i < Mangas.Count; i++) 
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

        private void OnClick(object sender, RoutedEventArgs e)
        {
            var btn = this.Grid.Children.FirstOrDefault(x => x == e.Source);
            var item = this.LoadedMangas.First(x => x.Value == btn);
            if(Overlay != null) Overlay.Hide();
            var btnname = btn.Name.Replace("Btn", "");
            Overlay = new Windows.OverlayWindow(item.Key.Title.EnglishReadableName, item.Key.Description.Replace("<br>", "\n"), int.Parse(btnname));
            Overlay.Show();
        }

        private Task ClearImagesAsync()
        {
            var items = this.Grid.Children;
            if(items.Count() > 3)
            {
                this.Grid.Children.RemoveRange(3, (items.Count() - 2));
                this.Grid.RowDefinitions.Clear();
                this.Grid.ColumnDefinitions.Clear();
                for(var i = 0; i < 4; i++) this.Grid.ColumnDefinitions.Add(new ColumnDefinition(){MinWidth=20});
                for(var i = 0; i < 3; i++) this.Grid.RowDefinitions.Add(new RowDefinition(){MinHeight=10});
            }
            return Task.CompletedTask;
        }
    }
}
