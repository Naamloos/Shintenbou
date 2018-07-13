using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;
using System;
using System.Collections.Generic;

namespace Shintenbou
{
	public class MainWindow : Window
	{
        private Database Database { get; set; }

		Button Anime;
		Button Manga;
		Button Music;
		Button Tracking;

		UserControl WelcomePage;
		UserControl AnimePage;
		UserControl MangaPage;
		UserControl MusicPage;
		UserControl TrackingPage;

		public MainWindow()
		{
			this.Database = new Database();
			InitializeComponent();
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
			this.WelcomePage = this.FindControl<UserControl>("WelcomePage");
			this.AnimePage = this.FindControl<UserControl>("AnimePage");
			this.MangaPage = this.FindControl<UserControl>("MangaPage");
			this.MusicPage = this.FindControl<UserControl>("MusicPage");
			this.TrackingPage = this.FindControl<UserControl>("TrackingPage");

			// Creating events for controls
			this.Anime.Click += Anime_Click;
			this.Manga.Click += Manga_Click;
			this.Music.Click += Music_Click;
			this.Tracking.Click += Tracking_Click;
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
			this.AnimePage.IsVisible = false;
			this.MangaPage.IsVisible = false;
			this.MusicPage.IsVisible = false;
			this.TrackingPage.IsVisible = false;
		}
	}
}