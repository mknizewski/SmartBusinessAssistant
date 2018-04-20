using MahApps.Metro.SimpleChildWindow;
using System.Windows;

namespace SBA.Client.Wpf.Views
{
    public partial class SettingsChildView : ChildWindow
    {
        public SettingsChildView()
        {
            InitializeComponent();
        }

        private void OkButtonOnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CloseButtonOnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}