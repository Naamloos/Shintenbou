using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;

namespace Shintenbou
{
	public class MainWindow : Window
	{
        private Database database = new Database();
		Button Anime;
		Button Manga;
		Button Music;
		Button Tracking;

		public MainWindow()
		{
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

			// Creating events for controls
			this.Anime.Click += Anime_Click;
			this.Manga.Click += Manga_Click;
			this.Music.Click += Music_Click;
			this.Tracking.Click += Tracking_Click;
		}

		private void Tracking_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			Console.WriteLine("Clicked Tracking Button");
		}

		private void Music_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			Console.WriteLine("Clicked Music Button");
		}

		private void Manga_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			Console.WriteLine("Clicked Manga Button");
		}

		private void Anime_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			Console.WriteLine("Clicked Anime Button");
		}
	}
}