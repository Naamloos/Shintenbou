using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Input;
using Avalonia.Interactivity;
using System.Linq;
using Shintenbou.Assets;
using Avalonia;
using DiscsharpRPC;
using System.Diagnostics;

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
            if(this._settings != null)
            {
                this.PointerLeave -= PointerEvent;
                this.PointerEnter -= PointerEvent;
                this.GotFocus += SettingsPage_GotFocus;
                return;
            }
            this._settings = Application.Current.MainWindow.Resources.FirstOrDefault(res => (string)res.Key == "settings").Value as Settings;
            UpdateButtons();
        }

        private void SettingsPage_GotFocus(object sender, RoutedEventArgs e) => UpdateButtons();

        private void UpdateButtons()
        {
            this._rpcBtn = this.Find<Button>("rpcbtn");
            this._rpcBtn.Content = (this._settings.EnableRpc) ? "Enabled" : "Disabled";
            this._rpcBtn.Click += _rpcBtn_Click;
            this._savePageStateBtn = this.Find<Button>("savepagestatebtn");
            this._savePageStateBtn.Content = (this._settings.SavePageState) ? "Enabled" : "Disabled";
            this._savePageStateBtn.Click += _savePageStateBtn_Click;
        }
        
        private void _rpcBtn_Click(object sender, RoutedEventArgs e)
        {
            this._settings.EnableRpc = !this._settings.EnableRpc;
            UpdateButtons();
            var client = Application.Current.MainWindow.Resources.FirstOrDefault(res => (string)res.Key == "rpc").Value as RpcClient;
            switch (this._settings.EnableRpc)
            {
                case true:
                    client.Connect();
                    client.ModifyPresence(x =>
                    {
                        x.Assets = y =>
                        {
                            y.LargeImage = "placeholder";
                            y.LargeImageText = "Place Holder";
                        };
                        x.State = "At Settings Screen";
                        x.Details = "Using Shintenbou";
                        x.StartTimestamp = Process.GetCurrentProcess().StartTime;
                    });
                    break;

                case false:
                    client.Clear();
                    client.Disconnect();
                    break;
            }
        }

        private void _savePageStateBtn_Click(object sender, RoutedEventArgs e)
        {
            this._settings.SavePageState = !this._settings.SavePageState;
            UpdateButtons();
        }
    }
}