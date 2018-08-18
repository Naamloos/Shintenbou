using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Interactivity;
using Shintenbou;
using Shintenbou.Pages;
using Shintenbou.Rest.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Linq;

namespace Shintenbou.Windows
{
    public class OverlayWindow : Window
    {
        Button FavButton { get; set; }
        TextBlock OverlayTitle { get; set; }
        TextBlock OverlayDescription { get; set; }
        Image OverlayIcon { get; set; }
        Object OverlayItem { get; set; }
        Source Source { get; set; }

        public OverlayWindow(object item, string title, string desc, int num, Source source)
        {
            this.OverlayItem = item;
            this.Source = source;
            string filename = "";
            switch(source)
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
            this.OverlayTitle = this.Find<TextBlock>("OverlayTitle");
            this.OverlayDescription = this.Find<TextBlock>("OverlayDescription");
            this.OverlayIcon = this.Find<Image>("OverlayImage");
            this.FavButton = this.Find<Button>("FavButton");
            this.FavButton.Click += OnClick;
            var db = new Database();
            db.Database.EnsureCreated();
            var arr = new[]{Source.FavAnime, Source.FavManga, Source.FavMusic};
            if(arr.Any(x => x == Source) || db.FavouriteAnime.Any(x => x.Name == title) || db.FavouriteManga.Any(x => x.Name == title)) this.FavButton.IsVisible = false;
            db.Dispose();
            this.OverlayTitle.Text = title;
            this.OverlayDescription.Text = desc;
            this.OverlayIcon.Source = new Bitmap(icon_path);
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            var db = new Database();
            db.Database.EnsureCreated();
            switch(this.Source)
            {
                case Source.Anime:
                    var aniitem = this.OverlayItem as AnilistAnime;
                    db.FavouriteAnime?.Add(new FavouritedAnime()
                    {
                        Description = aniitem.Description,
                        Name = aniitem.Title?.EnglishReadableName ?? aniitem.Title.English,
                        //Image = OverlayIcon,
                        ImageUrl = aniitem.CoverImage.Medium
                    });
                    db.SaveChanges();
                    break;
                
                case Source.Manga:
                    var manitem = this.OverlayItem as AnilistManga;
                    db.FavouriteManga.Add(new FavouritedManga()
                    {
                        Description = manitem.Description,
                        Name = manitem.Title?.EnglishReadableName ?? manitem.Title.English,
                        //Image = OverlayIcon.Source as Bitmap,
                        ImageUrl = manitem.CoverImage.Medium,
                        Page = 1
                    });
                    db.SaveChanges();
                    break;
            }
        }
    }
}
