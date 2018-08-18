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
    public class MangaPage : MangaPageBase
    {

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
            if(Mangas != null) await DisplayMangasAsync(Mangas.ToList());
        }

        private async void OnKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                await ClearImagesAsync();
                var Mangas = await Anilist.GetMangaByNameAsync(this.TextBox.Text);
                if(Mangas != null) await DisplayMangasAsync(Mangas.ToList());
            }
        }
    }
}
