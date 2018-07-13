using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;

namespace Shintenbou
{
	public class MainWindow : Window
	{
        private Database database { get; set; }
        private List<string> StackNames { get; set; }
		Button Anime;
		Button Manga;
		Button Music;
		Button Tracking;

		public MainWindow()
		{
            database = new Database();
            StackNames = new List<string>()
            {
                "AnimeStack",
                "TrackingStack",
                "MangaStack",
                "MusicStack"
            };
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
            ChangeStackTo("TrackingStack");
		}

		private void Music_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			Console.WriteLine("Clicked Music Button");
            ChangeStackTo("MusicStack");
		}

		private void Manga_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			Console.WriteLine("Clicked Manga Button");
            ChangeStackTo("MangaStack");
		}

		private void Anime_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			Console.WriteLine("Clicked Anime Button");
            ChangeStackTo("AnimeStack");
		}

        //I'm lazy
        public void ChangeStackTo(string stack_name)
        {
            foreach(var stack in StackNames) this.Find<StackPanel>(stack).IsVisible = false;
            this.Find<StackPanel>(stack_name).IsVisible = true;
        }
	}
}