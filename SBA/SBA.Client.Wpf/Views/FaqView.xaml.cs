using MahApps.Metro.Controls;
using MahApps.Metro.SimpleChildWindow;
using SBA.Client.Wpf.BOL.Infrastucture;
using SBA.Client.Wpf.Models;
using SBA.Client.Wpf.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SBA.Client.Wpf.Views
{
    public partial class FaqView : MetroWindow
    {
        private FaqViewModel _dataContext;

        public FaqView()
        {
            InitializeComponent();
            InitDataContext();
        }

        public void InitDataContext()
        {
            this.DataContext = _dataContext = SimpleFactory.Get<FaqViewModel>();
            _dataContext.LoadDataAsync();
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

        private async void SettingsClick(object sender, RoutedEventArgs e) => 
            await this.ShowChildWindowAsync(new SettingsChildView() { IsModal = true, AllowMove = true }, ChildWindowManager.OverlayFillBehavior.FullWindow);

        private async void AboutProgramClick(object sender, RoutedEventArgs e) => 
            await this.ShowChildWindowAsync(new AboutProgramChildView() { IsModal = true, AllowMove = true }, ChildWindowManager.OverlayFillBehavior.FullWindow);

        private async void AboutUsClick(object sender, RoutedEventArgs e) => 
            await this.ShowChildWindowAsync(new AboutUsChildView() { IsModal = true, AllowMove = true }, ChildWindowManager.OverlayFillBehavior.FullWindow);

        private void Refresh(object sender, RoutedEventArgs e) => 
            _dataContext.LoadDataAsync();

        private void QuestionEditRow(object sender, DataGridRowEditEndingEventArgs e) =>
            _dataContext.EditQuestionAsync(e.Row.Item as FaqModel.Question);

        private void AnswerEditRow(object sender, DataGridRowEditEndingEventArgs e) =>
            _dataContext.EditAnswerAsync(e.Row.Item as FaqModel.Answer);

        private void DeleteQuestionClick(object sender, RoutedEventArgs e) => 
            _dataContext.DeleteQuestionRowAsync((sender as Button).Tag.ToString());

        private void DeleteAnswerClick(object sender, RoutedEventArgs e) =>
            _dataContext.DeleteAnswerRowAsync((sender as Button).Tag.ToString());

        private void NewFaqSubmitClick(object sender, RoutedEventArgs e) => 
            _dataContext.AddNewFaq();
    }
}