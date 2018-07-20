using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;
using System;

namespace Shintenbou.Windows
{
    public class AlertWindow : Window
    {
        Button okaybtn;
        TextBlock textBlock;
        public AlertWindow(string alert_text)
        {
            this.InitializeComponent(alert_text);
        }

        private void InitializeComponent(string alert_text)
        {
            AvaloniaXamlLoader.Load(this);
            this.okaybtn = this.Find<Button>("AlertButton");
            this.textBlock = this.Find<TextBlock>("AlertText");
            this.textBlock.Text = alert_text;
        }
    }
}
