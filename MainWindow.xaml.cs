using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Input;
using Avalonia.Interactivity;
using System;
using System.IO;
using System.Threading.Tasks;
using Shintenbou.Pages;

using System.Reflection;

namespace Shintenbou
{
    public class MainWindow : Window
	{
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
            Console.WriteLine(string.Join(" || ", Assembly.GetExecutingAssembly().GetManifestResourceNames()));
            var path = Path.Combine(AppContext.BaseDirectory,"images");
            if(!Directory.Exists(path)) Directory.CreateDirectory(path);
			InitializeComponent();
		}

        protected override void HandleApplicationExiting()
        {
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
		}

		private void MainWindow_KeyDown(object sender, KeyEventArgs e)
		{
            switch(e.Key)
            {
                case Key.F2:
                    var manga = new Windows.MangaReaderWindow();
				    manga.Show();
                    break;

                case Key.F3:
                    var ab = new Windows.AlertWindow("Test Alert");
				    ab.Show();
                    break;

               case Key.F4:
                    var iw = new Windows.ImportWindow();
				    iw.Show();
                    break;

               case Key.Q:
                    Console.WriteLine($"{this.Width}x{this.Height}");
                    break;

                case Key.Escape:
                    App.Current.Exit();
                    break;
            }
		}

		private void Tracking_Click(object sender, RoutedEventArgs e)
		{
			Console.WriteLine("Clicked Tracking Button");
			HideAllPages();
			this.TrackingPage.IsVisible = true;
		}

		private void Music_Click(object sender, RoutedEventArgs e)
		{
			Console.WriteLine("Clicked Music Button");
			HideAllPages();
			this.MusicPage.IsVisible = true;
		}

		private void Manga_Click(object sender, RoutedEventArgs e)
		{
			Console.WriteLine("Clicked Manga Button");
			HideAllPages();
			this.MangaPage.IsVisible = true;
		}

		private void Anime_Click(object sender, RoutedEventArgs e)
		{
			Console.WriteLine("Clicked Anime Button");
			HideAllPages();
			this.AnimePage.IsVisible = true;
		}

		private void HideAllPages()
		{
			this.WelcomePage.IsVisible = false;
			//this.AnimePage.IsVisible = false;
			this.MangaPage.IsVisible = false;
			this.MusicPage.IsVisible = false;
			this.TrackingPage.IsVisible = false;
		}
        
        public Task ClearImagesAsync(Grid grid)
        {
            var items = grid.Children;
            if(items.Count > 2)
            {
                grid.Children.RemoveRange(2, (items.Count - 2));
                grid.RowDefinitions.Clear();
                grid.ColumnDefinitions.Clear();
                for(var i = 0; i < 4; i++) grid.ColumnDefinitions.Add(new ColumnDefinition(){MinWidth=20});
                for(var i = 0; i < 3; i++) grid.RowDefinitions.Add(new RowDefinition(){MinHeight=10});
            }
            return Task.CompletedTask;
        }
	}
}