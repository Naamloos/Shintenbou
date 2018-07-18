using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;
using System;

namespace Shintenbou.Windows
{
    public class AlertboxWindow : Window
    {
        Button okaybtn;
        TextBlock textBlock;
        public AlertboxWindow()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            this.okaybtn = this.Find<Button>("AlertButton");
            this.textBlock = this.Find<TextBlock>("AlertTextBlock");
        }
    }
}
