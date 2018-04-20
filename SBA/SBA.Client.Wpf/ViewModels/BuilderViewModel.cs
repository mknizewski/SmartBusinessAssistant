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
    public class BuilderViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static int NumberOfResults = 49;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private DispatcherTimer timer;
        private Random random;

        public BuilderViewModel()
        {
            random = new Random(Int32.MaxValue);
            timer = new DispatcherTimer(DispatcherPriority.Background, Application.Current.Dispatcher);
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += TimerTick;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (BuilderCollection.Count >= NumberOfResults)
            {
                Stop();
            }
            var newBuilder = new BuilderModel
            {
                FullPath = BuilderSampleData.FullPaths.ElementAt(random.Next(0, BuilderSampleData.FullPaths.Count)),
                PathQuantity = BuilderSampleData.PathQuantities.ElementAt(random.Next(0, BuilderSampleData.PathQuantities.Count)),
                LocalizationForBuilder = BuilderSampleData.LocalizationsForBuilder.ElementAt(random.Next(0, BuilderSampleData.LocalizationsForBuilder.Count))
            };
            Action action = () => { BuilderCollection.Add(newBuilder); };
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

        public ObservableCollection<BuilderModel> BuilderCollection { get; } = new ObservableCollection<BuilderModel>();
    }
}