using System;
using System.Windows;

namespace SBA.Client.Wpf.Views
{
    public partial class MainPanelView : Window
    {
        public MainPanelView()
        {
            InitializeComponent();
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

        private void OptionClick(object sender, RoutedEventArgs e)
        {
            if (ArticleRadioButton.IsChecked == true)
                ArticleClick(sender, e);

            if (FaqRadioButton.IsChecked == true)
                FaqClick(sender, e);

            if (BuilderRadioButton.IsChecked == true)
                BuilderClick(sender, e);
        }
    }
}