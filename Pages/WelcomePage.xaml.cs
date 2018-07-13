using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using System;
using System.IO;
using System.Reflection;

namespace Shintenbou.Pages
{
	public class WelcomePage : UserControl
	{
		Image LogoImage;
		public WelcomePage()
		{
			this.InitializeComponent();
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
			this.LogoImage = this.FindControl<Image>("Logo");

			var asm = Assembly.GetExecutingAssembly();
			var res = "Shintenbou.Assets.logo.png";
			using (var str = asm.GetManifestResourceStream(res))
			{
				this.LogoImage.Source = new Bitmap(str);
			}
		}
	}
}
