using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Input;
using Avalonia.Interactivity;
using System;
using System.IO;
using Shintenbou.Pages;
using DiscordRPC;
using Shintenbou.MainControl;
using Newtonsoft.Json;

namespace Shintenbou
{
    public class MainWindow : Window
	{
        DiscordRpcClient RpcClient { get; set; }
        Settings Settings { get; set; }

		Button Anime;
		Button Manga;
		Button Music;
		Button Tracking;

		WelcomePage WelcomePage;
		UserControl AnimePage;
		UserControl MangaPage;
		UserControl MusicPage;
		UserControl TrackingPage;

		public MainWindow()
		{
            var path = Path.Combine(AppContext.BaseDirectory,"images");
            if(!Directory.Exists(path)) Directory.CreateDirectory(path);
			InitializeComponent();
		}

        protected override void HandleApplicationExiting()
        {
            RpcClient.Dispose();
            var path = Path.Combine(AppContext.BaseDirectory,"images");
            if(!Directory.Exists(path)) return;
            var files = Directory.GetFiles(path);
            foreach(var file in files) File.Delete(file);
        }

		private void InitializeComponent()
		{
            AvaloniaXamlLoader.Load(this);
			// Setting references to controls
			this.Anime = this.FindControl<Button>("Anime");
			this.Manga = this.FindControl<Button>("Manga");
			this.Music = this.FindControl<Button>("Music");
			this.Tracking = this.FindControl<Button>("Tracking");

			// Setting references to pages
			this.WelcomePage = this.FindControl<WelcomePage>("WelcomePage");
			this.AnimePage = this.FindControl<UserControl>("AnimePage");
			this.MangaPage = this.FindControl<UserControl>("MangaPage");
			this.MusicPage = this.FindControl<UserControl>("MusicPage");
			this.TrackingPage = this.FindControl<UserControl>("TrackingPage");

			// Creating events for controls
			this.Anime.Click += Anime_Click;
			this.Manga.Click += Manga_Click;
			this.Music.Click += Music_Click;
			this.Tracking.Click += Tracking_Click;
			this.KeyDown += MainWindow_KeyDown;

            var path = Path.Combine(AppContext.BaseDirectory, "settings.json");
            Settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(path));

            if (!Settings.EnableRpc) return;
            RpcClient = new DiscordRpcClient("487373829673451530", false, -1);
            RpcClient.OnPresenceUpdate += (sender, e) =>
            {
                Console.WriteLine($"Update: {e.Presence}");
            };
            RpcClient.Initialize();
            RpcClient.SetPresence(new RichPresence()
            {
                Details = "Using Shintenbou",
                State = "At Main Screen",
                Assets = new Assets()
                {
                    LargeImageKey = "placeholder",
                    LargeImageText = "Place holder",
                },
                Timestamps = new Timestamps()
                {
                    Start = DateTime.Now
                }
            });
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
		{
            switch(e.Key)
            {
                case Key.F2:
                    var manga = new Windows.MangaReaderWindow();
				    manga.Show();
                    break;
                    
               case Key.F4:
                    var iw = new Windows.ImportWindow();
				    iw.Show();
                    break;
                    
                case Key.Escape:
                    App.Current.Exit();
                    break;
            }
		}

        private void Tracking_Click(object sender, RoutedEventArgs e)
        {
            this.Title = "Shintenbou: Tracking";
            HideAllPages();
            this.TrackingPage.IsVisible = true;

            if (!Settings.EnableRpc) return;
            RpcClient.SetPresence(new RichPresence()
            {
                Details = "Using Shintenbou",
                State = "At Tracking Page",
                Assets = new Assets()
                {
                    LargeImageKey = "placeholder",
                    LargeImageText = "Place holder",
                }
            });
        }

		private void Music_Click(object sender, RoutedEventArgs e)
		{
            this.Title = "Shintenbou: Music";
			HideAllPages();
			this.MusicPage.IsVisible = true;

            if (!Settings.EnableRpc) return;
            RpcClient.SetPresence(new RichPresence()
            {
                Details = "Using Shintenbou",
                State = "At Music Page",
                Assets = new Assets()
                {
                    LargeImageKey = "placeholder",
                    LargeImageText = "Place holder",
                }
            });
        }

		private void Manga_Click(object sender, RoutedEventArgs e)
        {
            this.Title = "Shintenbou: Manga";
			HideAllPages();
			this.MangaPage.IsVisible = true;

            if (!Settings.EnableRpc) return;
            RpcClient.SetPresence(new RichPresence()
            {
                Details = "Using Shintenbou",
                State = "At Manga Page",
                Assets = new Assets()
                {
                    LargeImageKey = "placeholder",
                    LargeImageText = "Place holder",
                }
            });
        }

		private void Anime_Click(object sender, RoutedEventArgs e)
        {
            this.Title = "Shintenbou: Anime";
			HideAllPages();
			this.AnimePage.IsVisible = true;

            if (!Settings.EnableRpc) return;
            RpcClient.SetPresence(new RichPresence()
            {
                Details = "Using Shintenbou",
                State = "At Anime Page",
                Assets = new Assets()
                {
                    LargeImageKey = "placeholder",
                    LargeImageText = "Place holder",
                }
            });
        }

		private void HideAllPages()
		{
			this.WelcomePage.IsVisible = false;
			this.AnimePage.IsVisible = false;
			this.MangaPage.IsVisible = false;
			this.MusicPage.IsVisible = false;
			this.TrackingPage.IsVisible = false;
		}
	}
}