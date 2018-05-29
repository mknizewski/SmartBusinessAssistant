using SBA.Client.Wpf.BOL.Infrastucture;
using SBA.Client.Wpf.BOL.Managers;
using SBA.Client.Wpf.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SBA.Client.Wpf.ViewModels
{
    public class MessagesViewModel : INotifyPropertyChanged
    {
        private readonly IWebApiManager _webApiManager;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanges([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public MessagesViewModel() =>
            _webApiManager = SimpleFactory.Get<WebApiManager, IWebApiManager>();

        public async void LoadMessages()
        {
            try
            {
                Messages.Clear();

                var content = await _webApiManager.GetMessagesAsync();
                foreach (var item in content)
                    Messages.Add(new MessageModel
                    {
                        Id = item[nameof(MessageModel.Id)],
                        Name = item[nameof(MessageModel.Name)],
                        Email = item[nameof(MessageModel.Email)],
                        MobileNumber = item[nameof(MessageModel.MobileNumber)],
                        Subject = item[nameof(MessageModel.Subject)],
                        Message = item[nameof(MessageModel.Message)]
                    });

                RefreshTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                LoadStatus = "OK";
            }
            catch
            {
                LoadStatus = "Wystąpił błąd podczas pobierania.";
            }
        }

        public ObservableCollection<MessageModel> Messages { get; set; } = new ObservableCollection<MessageModel>();

        private string _refreshTime;
        public string RefreshTime
        {
            get => _refreshTime;
            set
            {
                _refreshTime = value;
                OnPropertyChanges(nameof(RefreshTime));
            }
        }

        private string _loadStatus;
        public string LoadStatus
        {
            get => _loadStatus;
            set
            {
                _loadStatus = value;
                OnPropertyChanges(nameof(LoadStatus));
            }
        }
    }
}