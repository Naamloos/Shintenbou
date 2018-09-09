using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using DiscsharpRPC;
using System.IO;
using System.Linq;

namespace Shintenbou.Windows
{
    public class MangaReaderWindow : Window
	{
		Grid list;
		ScrollViewer scroll;
		public MangaReaderWindow(string name)
		{
			this.InitializeComponent(name);
		}

		private void InitializeComponent(string name)
		{
			AvaloniaXamlLoader.Load(this);
			this.list = this.FindControl<Grid>("pagelist");
			this.scroll = this.FindControl<ScrollViewer>("scroller");
			var mgs = Directory.GetFiles("testmanga");

            var client = Application.Current.MainWindow.Resources.FirstOrDefault(x => (string)x.Key == "rpc").Value as RpcClient;
            client.ModifyPresence(x => x.State = $"Reading {name}");

            int i = 0;
			foreach (var m in mgs)
			{
				using (var img = new FileStream(m, FileMode.Open))
				{
					list.RowDefinitions.Add(new RowDefinition(GridLength.Auto));

					var bmp = new Bitmap(img);
					var imag = new Image()
					{
						Source = bmp,
						UseLayoutRounding = true,
						Width = this.Width,
						Height = this.Height
					};

					imag.UseLayoutRounding = true;

					imag.SetValue(Grid.RowProperty, i);
					list.Children.Add(imag);
					var split = new StackPanel();
					split.Height = 20;
					split.SetValue(Grid.RowProperty, i + 1);
					list.Children.Add(split);
				}
				i+=2;
			}

			this.PropertyChanged += MangaReaderWindow_PropertyChanged;
		}

		private void MangaReaderWindow_PropertyChanged(object sender, AvaloniaPropertyChangedEventArgs e)
		{
			if (e.Property == Control.WidthProperty || e.Property == Control.HeightProperty)
			{
				this.list.Width = this.Width;
				foreach (var c in list.Children)
				{
					if (c.GetType() == typeof(Image))
					{
						var cc = (Image)c;
						cc.Width = this.Width;
						cc.Height = this.Height;
						cc.UseLayoutRounding = true;
					}
				}

				if (e.Property == Control.WidthProperty)
					this.MaxHeight = this.Width * 1.5;
			}
		}
	}
}
