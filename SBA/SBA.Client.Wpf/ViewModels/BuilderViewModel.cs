using SBA.Client.Wpf.BOL.Infrastucture;
using SBA.Client.Wpf.BOL.Managers;
using SBA.Client.Wpf.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Threading;

namespace SBA.Client.Wpf.ViewModels
{
    public class BuilderViewModel : INotifyPropertyChanged
    {
        private readonly IClientSocketManager _clientSocketManager;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        public BuilderViewModel() =>
            _clientSocketManager = SimpleFactory.Get<ClientSocketManager, IClientSocketManager>();

        public void LoadLogs()
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                IsLoad = true;

                try
                {
                    var logs = _clientSocketManager.GetLogs();
                    LogsCollection.Clear();

                    foreach (var item in logs)
                        LogsCollection.Add(new LogModel
                        {
                            Id = item[nameof(LogModel.Id)],
                            SessionId = item[nameof(LogModel.SessionId)],
                            CurrentTime = item[nameof(LogModel.CurrentTime)],
                            ClientIp = item[nameof(LogModel.ClientIp)],
                            CurrentUrl = item[nameof(LogModel.CurrentUrl)],
                            PreviousUrl = item[nameof(LogModel.PreviousUrl)]
                        });

                    LoadDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    LoadStatus = "OK";
                }
                catch (Exception ex)
                {
                    LoadStatus = "Wystąpił błąd podczas pobierania danych.";
                }

                IsLoad = false;
            }));
        }

        public ObservableCollection<LogModel> LogsCollection { get; } = new ObservableCollection<LogModel>();

        private string _loadDate;
        public string LoadDate
        {
            get => _loadDate;
            set
            {
                _loadDate = value;
                OnPropertyChanged(nameof(LoadDate));
            }
        }

        private bool _isLoad;
        public bool IsLoad
        {
            get => _isLoad;
            set
            {
                _isLoad = value;
                OnPropertyChanged(nameof(IsLoad));
            }
        }

        private string _loadStatus;
        public string LoadStatus
        {
            get => _loadDate;
            set
            {
                _loadDate = value;
                OnPropertyChanged(nameof(LoadStatus));
            }
        }
    }
}