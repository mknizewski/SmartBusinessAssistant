using MahApps.Metro.Controls;
using MahApps.Metro.SimpleChildWindow;
using SBA.Client.Wpf.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SBA.Client.Wpf.Views
{
    public partial class FaqView : MetroWindow
    {
        public FaqView()
        {
            InitializeComponent();
            var viewModel = new FaqViewModel();
            this.DataContext = viewModel;
            this.Loaded += (sender, args) => viewModel.Start();
            this.Closed += (sender, args) => viewModel.Stop();
        }

        private void ReturnToHomePage(object sender, MouseButtonEventArgs e)
        {
            if (sender is TabItem)
            {
                MainView mainView = new MainView();
                this.Close();
                mainView.Show();
            }
        }

        private async void SettingsClick(object sender, RoutedEventArgs e)
        {
            await this.ShowChildWindowAsync(new SettingsChildView() { IsModal = true, AllowMove = true }, ChildWindowManager.OverlayFillBehavior.FullWindow);
        }

        private async void AboutProgramClick(object sender, RoutedEventArgs e)
        {
            await this.ShowChildWindowAsync(new AboutProgramChildView() { IsModal = true, AllowMove = true }, ChildWindowManager.OverlayFillBehavior.FullWindow);
        }

        private async void AboutUsClick(object sender, RoutedEventArgs e)
        {
            await this.ShowChildWindowAsync(new AboutUsChildView() { IsModal = true, AllowMove = true }, ChildWindowManager.OverlayFillBehavior.FullWindow);
        }
    }
}