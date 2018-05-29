using MahApps.Metro.Controls;
using MahApps.Metro.SimpleChildWindow;
using SBA.Client.Wpf.BOL.Infrastucture;
using SBA.Client.Wpf.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SBA.Client.Wpf.Views
{
    public partial class MessagesView : MetroWindow
    {
        private MessagesViewModel _dataContext;

        public MessagesView()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            DataContext = _dataContext = SimpleFactory.Get<MessagesViewModel>();
            _dataContext.LoadMessages();
        }

        private async void SettingsClick(object sender, RoutedEventArgs e) =>
            await this.ShowChildWindowAsync(new SettingsChildView() { IsModal = true, AllowMove = true }, ChildWindowManager.OverlayFillBehavior.FullWindow);

        private async void AboutProgramClick(object sender, RoutedEventArgs e) =>
            await this.ShowChildWindowAsync(new AboutProgramChildView() { IsModal = true, AllowMove = true }, ChildWindowManager.OverlayFillBehavior.FullWindow);

        private async void AboutUsClick(object sender, RoutedEventArgs e) =>
            await this.ShowChildWindowAsync(new AboutUsChildView() { IsModal = true, AllowMove = true }, ChildWindowManager.OverlayFillBehavior.FullWindow);

        private void DeleteMessage(object sender, RoutedEventArgs e)
        {

        }

        private void ReturnToHomePage(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is TabItem)
            {
                MainView mainView = new MainView();
                this.Close();
                mainView.Show();
            }
        }

        private void Refresh(object sender, RoutedEventArgs e) =>
            _dataContext.LoadMessages();

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
