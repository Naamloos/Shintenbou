using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Shintenbou.Pages
{
    public class MangaPage : UserControl
    {
        public MangaPage()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
