﻿using MahApps.Metro.Controls;
using MahApps.Metro.SimpleChildWindow;
using System.Windows;

namespace SBA.Client.Wpf.Views
{
    public partial class MainView : MetroWindow
    {
        public MainView() => 
            InitializeComponent();

        private void ArticleClick(object sender, RoutedEventArgs e)
        {
            RecommendationView articleView = new RecommendationView();
            this.Close();
            articleView.Show();
        }

        private void BuilderClick(object sender, RoutedEventArgs e)
        {
            BuilderView builderView = new BuilderView();
            this.Close();
            builderView.Show();
        }

        private void FaqClick(object sender, RoutedEventArgs e)
        {
            FaqView faqView = new FaqView();
            this.Close();
            faqView.Show();
        }

        private async void SettingsClick(object sender, RoutedEventArgs e) => 
            await this.ShowChildWindowAsync(new SettingsChildView() { IsModal = true, AllowMove = true }, ChildWindowManager.OverlayFillBehavior.FullWindow);

        private async void AboutProgramClick(object sender, RoutedEventArgs e) => 
            await this.ShowChildWindowAsync(new AboutProgramChildView() { IsModal = true, AllowMove = true }, ChildWindowManager.OverlayFillBehavior.FullWindow);

        private async void AboutUsClick(object sender, RoutedEventArgs e) => 
            await this.ShowChildWindowAsync(new AboutUsChildView() { IsModal = true, AllowMove = true }, ChildWindowManager.OverlayFillBehavior.FullWindow);

        private void ExitClick(object sender, RoutedEventArgs e) => 
            Close();

        private void MessagesClick(object sender, RoutedEventArgs e)
        {
            MessagesView messagesView = new MessagesView();
            this.Close();
            messagesView.Show();
        }
    }
}