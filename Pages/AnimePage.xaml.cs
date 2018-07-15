using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Input;
using Avalonia.Media.Imaging;
using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var animes = await Anilist.GetAnimeByNameAsync(this.TextBox.Text);
            DisplayAnimes(animes.ToList());
        }

        private async void OnKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                var animes = await Anilist.GetAnimeByNameAsync(this.TextBox.Text);
                DisplayAnimes(animes.ToList());
            }
        }

        private void DisplayAnimes(IReadOnlyList<AnilistAnime> animes)
        {
            int colcount = 0;
            int rowcount = 1;
            for(var i = 0; i < animes.Count(); i++) 
            {
                var child = new Image()
                {
                    Margin = new Thickness(50, 0, 0, 0),
                    IsVisible = true,
                    Name = $"Img{i}",
                    MinWidth = 150,
                    MinHeight = 250,
                    Source = new Bitmap(animes[i].CoverImage.Medium)
                };
                child.SetValue(Grid.ColumnProperty, colcount);
                child.SetValue(Grid.RowProperty, rowcount);
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
            }
        }
    }
}
