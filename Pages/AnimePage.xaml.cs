using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Shintenbou.Pages
{
    public class AnimePage : UserControl
    {
        public AnimePage()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
