using MahApps.Metro.SimpleChildWindow;
using System.Windows;

namespace SBA.Client.Wpf.Views
{
    public partial class AboutUsChildView : ChildWindow
    {
        public AboutUsChildView()
        {
            InitializeComponent();
        }

        private void OkButtonOnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}