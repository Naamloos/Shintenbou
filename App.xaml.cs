using Avalonia;
using Avalonia.Markup.Xaml;

namespace Shintenbou
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
   }
}