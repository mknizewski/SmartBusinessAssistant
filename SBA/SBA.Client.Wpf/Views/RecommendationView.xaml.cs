using MahApps.Metro.Controls;
using MahApps.Metro.SimpleChildWindow;
using SBA.Client.Wpf.BOL.Infrastucture;
using SBA.Client.Wpf.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SBA.Client.Wpf.Views
{
    public partial class RecommendationView : MetroWindow
    {
        private RecommendationViewModel _dataContext;

        public RecommendationView()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            DataContext = _dataContext = SimpleFactory.Get<RecommendationViewModel>();
            _dataContext.LoadRecommendations();
        }

        private async void SettingsClick(object sender, RoutedEventArgs e) => 
            await this.ShowChildWindowAsync(new SettingsChildView() { IsModal = true, AllowMove = true }, ChildWindowManager.OverlayFillBehavior.FullWindow);

        private async void AboutProgramClick(object sender, RoutedEventArgs e) => 
            await this.ShowChildWindowAsync(new AboutProgramChildView() { IsModal = true, AllowMove = true }, ChildWindowManager.OverlayFillBehavior.FullWindow);

        private async void AboutUsClick(object sender, RoutedEventArgs e) => 
            await this.ShowChildWindowAsync(new AboutUsChildView() { IsModal = true, AllowMove = true }, ChildWindowManager.OverlayFillBehavior.FullWindow);

        private void ExitClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is TabItem)
            {
                MainView mainView = new MainView();
                this.Close();
                mainView.Show();
            }
        }

        private void TileClick(object sender, RoutedEventArgs e)
        {
            string id = (sender as Tile).Tag.ToString();
            var detailView = new DataDetailView(id);

            detailView.Show();
        }

        private void RefreshClick(object sender, RoutedEventArgs e) => 
            _dataContext.LoadRecommendations();
    }
}