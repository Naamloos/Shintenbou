using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Shintenbou.Windows
{
    public class OverlayWindow : Window
    {
        TextBlock OverlayTitle { get; set; }
        TextBlock OverlayDescription { get; set; }
        Image OverlayIcon { get; set; }

        public OverlayWindow(string title, string desc, int num)
        {
            var img = Path.Combine(AppContext.BaseDirectory, "images", $"Img{num}.png");
            Console.WriteLine(img);
            this.InitializeComponent(title, desc, img);
        }

        private void InitializeComponent(string title, string desc, string icon_path)
        {
            AvaloniaXamlLoader.Load(this);
            this.OverlayTitle = this.Find<TextBlock>("OverlayTitle");
            this.OverlayDescription = this.Find<TextBlock>("OverlayDescription");
            this.OverlayIcon = this.Find<Image>("OverlayImage");
            this.OverlayTitle.Text = title;
            this.OverlayDescription.Text = desc;
            this.OverlayIcon.Source = new Bitmap(icon_path);
        }
    }
}
