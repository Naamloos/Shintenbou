using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Input;
using Avalonia.Interactivity;
using System;
using System.IO;
using Shintenbou.Pages;
using MuscordRpc;
using Shintenbou.MainControl;
using Newtonsoft.Json;
using System.Timers;
using System.Diagnostics;

namespace Shintenbou
{
    public class MainWindow : Window
	{
        DateTime _startTime { get; } = Process.GetCurrentProcess().StartTime;
        Timer _timer { get; set; }
        RpcClient _rpcClient { get; set; }
        Settings _settings { get; set; }

		Button _anime;
		Button _manga;
		Button _music;
		Button _tracking;

		WelcomePage _welcomePage;
		UserControl _animePage;
		UserControl _mangaPage;
		UserControl _musicPage;
		UserControl _trackingPage;

		public MainWindow()
		{
            var path = Path.Combine(AppContext.BaseDirectory,"images");
            if(!Directory.Exists(path)) Directory.CreateDirectory(path);
			InitializeComponent();
		}

        protected override void HandleApplicationExiting()
        {
            _rpcClient.Disconnect();
            var path = Path.Combine(AppContext.BaseDirectory,"images");
            if(!Directory.Exists(path)) return;
            var files = Directory.GetFiles(path);
            foreach(var file in files) File.Delete(file);
        }

		private void InitializeComponent()
		{
            AvaloniaXamlLoader.Load(this);
			// Setting references to controls
			this._anime = this.FindControl<Button>("Anime");
			this._manga = this.FindControl<Button>("Manga");
			this._music = this.FindControl<Button>("Music");
			this._tracking = this.FindControl<Button>("Tracking");

			// Setting references to pages
			this._welcomePage = this.FindControl<WelcomePage>("WelcomePage");
			this._animePage = this.FindControl<UserControl>("AnimePage");
			this._mangaPage = this.FindControl<UserControl>("MangaPage");
			this._musicPage = this.FindControl<UserControl>("MusicPage");
			this._trackingPage = this.FindControl<UserControl>("TrackingPage");

			// Creating events for controls
			this._anime.Click += Anime_Click;
			this._manga.Click += Manga_Click;
			this._music.Click += Music_Click;
			this._tracking.Click += Tracking_Click;
			this.KeyDown += MainWindow_KeyDown;

            var path = Path.Combine(AppContext.BaseDirectory, "settings.json");
            _settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(path));

            if (!_settings.EnableRpc) return;
            _rpcClient = new RpcClient(487373829673451530);//("487373829673451530", false, -1);
            _rpcClient.Connect();
            _timer = new Timer(5000);
           // _timer.Elapsed += _timer_Elapsed;
            _rpcClient.ModfiyPresence(x =>
            {
                x.Details = "Using Shintenbou";
                x.State = "At Main Screen";
                x.LargeImage = "placeholder";
                x.LargeImageText = "Place holder";
                x.StartTimestamp = _startTime;
            });
        }

        //private void _timer_Elapsed(object sender, ElapsedEventArgs e) => _rpcClient.Connect

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
            this._trackingPage.IsVisible = true;

            if (!_settings.EnableRpc) return;
            _rpcClient.ModfiyPresence(x =>
            {
                x.Details = "Using Shintenbou";
                x.State = "At Tracking Screen";
                x.LargeImage = "placeholder";
                x.LargeImageText = "Place holder";
                x.StartTimestamp = _startTime;
            });
        }

		private void Music_Click(object sender, RoutedEventArgs e)
		{
            this.Title = "Shintenbou: Music";
			HideAllPages();
			this._musicPage.IsVisible = true;

            if (!_settings.EnableRpc) return;
            _rpcClient.ModfiyPresence(x =>
            {
                x.Details = "Using Shintenbou";
                x.State = "At Music Screen";
                x.LargeImage = "placeholder";
                x.LargeImageText = "Place holder";
                x.StartTimestamp = _startTime;
            });
        }

		private void Manga_Click(object sender, RoutedEventArgs e)
        {
            this.Title = "Shintenbou: Manga";
			HideAllPages();
			this._mangaPage.IsVisible = true;

            if (!_settings.EnableRpc) return;
            _rpcClient.ModfiyPresence(x =>
            {
                x.Details = "Using Shintenbou";
                x.State = "At Manga Screen";
                x.LargeImage = "placeholder";
                x.LargeImageText = "Place holder";
                x.StartTimestamp = _startTime;
            });
        }

		private void Anime_Click(object sender, RoutedEventArgs e)
        {
            this.Title = "Shintenbou: Anime";
			HideAllPages();
			this._animePage.IsVisible = true;

            if (!_settings.EnableRpc) return;
            _rpcClient.ModfiyPresence(x =>
            {
                x.Details = "Using Shintenbou";
                x.State = "At Anime Screen";
                x.LargeImage = "placeholder";
                x.LargeImageText = "Place holder";
                x.StartTimestamp = _startTime;
            });
        }

		private void HideAllPages()
		{
			this._welcomePage.IsVisible = false;
			this._animePage.IsVisible = false;
			this._mangaPage.IsVisible = false;
			this._musicPage.IsVisible = false;
			this._trackingPage.IsVisible = false;
		}
	}
}