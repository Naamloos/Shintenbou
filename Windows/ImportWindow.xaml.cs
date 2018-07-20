using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Shintenbou.Windows
{
    public class ImportWindow : Window
    {
        public ImportWindow()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
