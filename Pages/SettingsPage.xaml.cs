using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Input;
using Avalonia.Interactivity;
using System.Linq;
using Shintenbou.Assets;
using Avalonia;

namespace Shintenbou.Pages
{
    public class SettingsPage : UserControl
    {
        Settings _settings { get; set; }
        Button _rpcBtn { get; set; }
        Button _savePageStateBtn { get; set; }

        public SettingsPage()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            this.PointerLeave += PointerEvent;
            this.PointerEnter += PointerEvent;
        }
        
        private void PointerEvent(object sender, PointerEventArgs e)
        {
            this._settings = Application.Current.MainWindow.Resources.FirstOrDefault(res => (string)res.Key == "settings").Value as Settings;
            this._rpcBtn = this.Find<Button>("rpcbtn");
            this._rpcBtn.Content = (this._settings.EnableRpc) ? "Enabled" : "Disabled";
            this._rpcBtn.Click += _rpcBtn_Click;
            //this._savePageStateBtn = this.Find<Button>("savepagestatebtn");
            //this._savePageStateBtn.Content = (this._settings.SavePageState) ? "Enabled" : "Disabled";
        }

        private void _rpcBtn_Click(object sender, RoutedEventArgs e)
        {
            this._settings.EnableRpc = !this._settings.EnableRpc;
            this._rpcBtn.Content = (this._settings.EnableRpc) ? "Enabled" : "Disabled";
        }
    }
}