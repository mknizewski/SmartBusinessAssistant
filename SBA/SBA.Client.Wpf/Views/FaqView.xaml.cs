using System;
using System.Windows;

namespace SBA.Client.Wpf.Views
{
    public partial class FaqView : Window
    {
        public FaqView()
        {
            InitializeComponent();
        }

        private void MainPanelClick(object sender, RoutedEventArgs e)
        {
            MainPanelView mainPanelView = new MainPanelView();
            this.Close();
            mainPanelView.Show();
        }

        private void AboutClick(object sender, RoutedEventArgs e)
        {
            AboutView aboutView = new AboutView();
            this.Close();
            aboutView.Show();
        }

        private void SettingsClick(object sender, RoutedEventArgs e)
        {
            SettingsView settingsView = new SettingsView();
            this.Close();
            settingsView.Show();
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void ArticleClick(object sender, RoutedEventArgs e)
        {
            ArticleView articleView = new ArticleView();
            this.Close();
            articleView.Show();
        }

        private void FaqClick(object sender, RoutedEventArgs e)
        {
            FaqView faqView = new FaqView();
            this.Close();
            faqView.Show();
        }

        private void BuilderClick(object sender, RoutedEventArgs e)
        {
            BuilderView builderView = new BuilderView();
            this.Close();
            builderView.Show();
        }
    }
}