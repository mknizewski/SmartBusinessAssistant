using SBA.Client.Wpf.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Threading;

namespace SBA.Client.Wpf.ViewModels
{
    public class ArticleViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static int NumberOfResults = 49;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private DispatcherTimer timer;
        private Random random;

        public ArticleViewModel()
        {
            random = new Random(Int32.MaxValue);
            timer = new DispatcherTimer(DispatcherPriority.Background, Application.Current.Dispatcher);
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += TimerTick;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (ArticleCollection.Count >= NumberOfResults)
            {
                Stop();
            }
            var newArticle = new ArticleModel
            {
                Title = ArticleSampleData.Titles.ElementAt(random.Next(0, ArticleSampleData.Titles.Count)),
                Category = ArticleSampleData.Categories.ElementAt(random.Next(0, ArticleSampleData.Categories.Count)),
                Content = ArticleSampleData.Contents.ElementAt(random.Next(0, ArticleSampleData.Contents.Count)),
                SourceOfArticle = ArticleSampleData.Sources.ElementAt(random.Next(0, ArticleSampleData.Sources.Count))
            };
            Action action = () => { ArticleCollection.Add(newArticle); };
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(action));
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }

        public ObservableCollection<ArticleModel> ArticleCollection { get; } = new ObservableCollection<ArticleModel>();
    }
}