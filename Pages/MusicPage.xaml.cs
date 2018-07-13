using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Shintenbou.Pages
{
    public class MusicPage : UserControl
    {
        public MusicPage()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
