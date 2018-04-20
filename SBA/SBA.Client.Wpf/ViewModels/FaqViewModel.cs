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
    public class FaqViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static int NumberOfResults = 49;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private DispatcherTimer timer;
        private Random random;

        public FaqViewModel()
        {
            random = new Random(Int32.MaxValue);
            timer = new DispatcherTimer(DispatcherPriority.Background, Application.Current.Dispatcher);
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += TimerTick;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (FaqCollection.Count >= NumberOfResults)
            {
                Stop();
            }
            var newFaq = new FaqModel
            {
                Question = FaqSampleData.Questions.ElementAt(random.Next(0, FaqSampleData.Questions.Count)),
                SimilarQuestionQuantity = FaqSampleData.SimilarQuestionQuantities.ElementAt(random.Next(0, FaqSampleData.SimilarQuestionQuantities.Count)),
                Usefulness = FaqSampleData.Usefulnesses.ElementAt(random.Next(0, FaqSampleData.Usefulnesses.Count)),
                LocalizationForFaq = FaqSampleData.LocalizationsForFaq.ElementAt(random.Next(0, FaqSampleData.LocalizationsForFaq.Count))
            };
            Action action = () => { FaqCollection.Add(newFaq); };
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

        public ObservableCollection<FaqModel> FaqCollection { get; } = new ObservableCollection<FaqModel>();
    }
}