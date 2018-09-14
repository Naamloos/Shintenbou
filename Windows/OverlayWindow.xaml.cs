using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Interactivity;
using Shintenbou.Pages;
using Shintenbou.Rest.Objects;
using System;
using System.IO;
using System.Linq;
using Avalonia;
using DiscsharpRPC;

namespace Shintenbou.Windows
{
    public class OverlayWindow : Window
    {
        Database _model { get; set;  }
        Button _favButton { get; set; }
        TextBlock _overlayTitle { get; set; }
        TextBlock _overlayDescription { get; set; }
        Image _overlayIcon { get; set; }
        object _overlayItem { get; set; }
        Source _source { get; set; }

        public OverlayWindow(object item, string title, string desc, int num, Source source)
        {
            this._model = new Database();
            this._model.Database.EnsureCreated();
            this._overlayItem = item;
            this._source = source;
            string filename = "";
            switch (source)
            {
                case Source.Anime:
                    filename = $"Img{num}.png";
                    break;
                case Source.Manga:
                    filename = $"MangaImg{num}.png";
                    break;
               case Source.FavAnime:
                    filename = $"FavAniImg{num}.png";
                    break;
                case Source.FavManga:
                    filename = $"FavMangaImg{num}.png";
                    break;
            }
            var img = Path.Combine(AppContext.BaseDirectory, "images", filename);
            this.InitializeComponent(title, desc, img);
        }

        private void InitializeComponent(string title, string desc, string icon_path)
        {
            AvaloniaXamlLoader.Load(this);
            this._overlayTitle = this.Find<TextBlock>("OverlayTitle");
            this._overlayDescription = this.Find<TextBlock>("OverlayDescription");
            this._overlayIcon = this.Find<Image>("OverlayImage");
            this._favButton = this.Find<Button>("FavButton");
            this._favButton.Click += OnClick;
            var arr = new[]{Source.FavAnime, Source.FavManga, Source.FavMusic};
            if(arr.Any(x => x == _source) || this._model.FavouriteAnime.Any(x => x.Name == title) || this._model.FavouriteManga.Any(x => x.Name == title)) this._favButton.IsVisible = false;
            this._overlayTitle.Text = title;
            this._overlayDescription.Text = desc;
            this._overlayIcon.Source = new Bitmap(icon_path);
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            switch(this._source)
            {
                case Source.Anime:
                    var aniitem = this._overlayItem as AnilistAnime;
                    this._model.FavouriteAnime?.Add(new FavouritedAnime()
                    {
                        Description = aniitem.Description,
                        Name = aniitem.Title?.EnglishReadableName ?? aniitem.Title.English,
                        //Image = OverlayIcon,
                        ImageUrl = aniitem.CoverImage.Medium
                    });
                    this._model.SaveChanges();
                    break;
                
                case Source.Manga:
                    var manitem = this._overlayItem as AnilistManga;
                    this._model.FavouriteManga.Add(new FavouritedManga()
                    {
                        Description = manitem.Description,
                        Name = manitem.Title?.EnglishReadableName ?? manitem.Title.English,
                        //Image = OverlayIcon.Source as Bitmap,
                        ImageUrl = manitem.CoverImage.Medium,
                        Page = 1
                    });
                    this._model.SaveChanges();
                    break;
            }
        }

        protected override bool HandleClosing()
        {
            var client = Application.Current.MainWindow.Resources.FirstOrDefault(x => (string)x.Key == "rpc").Value as RpcClient;
            switch(this._source)
            {
                case Source.Anime:
                    client.ModifyPresence(x => x.State = "At Anime Screen");
                    break;
                
                case Source.Manga:
                    client.ModifyPresence(x => x.State = "At Manga Screen");
                    break;

                case Source.Music:
                    client.ModifyPresence(x => x.State = "At Music Screen");
                    break;

                default:
                    client.ModifyPresence(x => x.State = "At Tracking Screen");
                    break;
            }
            return base.HandleClosing();
        }
    }
}
