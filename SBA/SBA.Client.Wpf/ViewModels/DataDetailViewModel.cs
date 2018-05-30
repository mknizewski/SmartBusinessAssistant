using SBA.Client.Wpf.BOL.Infrastucture;
using SBA.Client.Wpf.BOL.Managers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace SBA.Client.Wpf.ViewModels
{
    public class DataDetailViewModel : INotifyPropertyChanged
    {
        private readonly IClientSocketManager _clientSocketManager;
        private readonly IWebApiManager _webApiManager;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public DataDetailViewModel()
        {
            _clientSocketManager = SimpleFactory.Get<ClientSocketManager, IClientSocketManager>();
            _webApiManager = SimpleFactory.Get<WebApiManager, IWebApiManager>();
        }

        public void GetData(string dataTag)
        {
            var splitedTag = dataTag.Split(',');
            string type = splitedTag[1];
            var dataDetails = _clientSocketManager.GetDataDetails(dataTag);

            if (type == "Article")
            {
                string details = string.IsNullOrEmpty(dataDetails["Description"]) ? dataDetails["ArticleBody"] : dataDetails["Description"];
                Title = dataDetails["Title"];
                Body = $@"<p>{details}</p>
                            Więcej info na: <a href='{dataDetails["Link"]}'>{dataDetails["DisplayLink"]}</a>";
            }
            else if (type == "Event")
            {
                Title = dataDetails["Title"];
                Body = $@"<p>{dataDetails["Snippet"]}</p>
                          Zobacz wydarzenie na: <a href='{dataDetails["Link"]}'>{dataDetails["DisplayLink"]}</a>";
            }
            else if (type == "Organization")
            {
                Title = dataDetails["Title"];
                Body = $@"<p>{dataDetails["Snippet"]}</p>
                          Zobacz organizację na: <a href='{dataDetails["Link"]}'>{dataDetails["DisplayLink"]}</a>";
            }
            else if (type == "Person")
            {
                Title = dataDetails["Title"];
                Body = $@"<p>{dataDetails["Snippet"]}</p>
                          Więcej informacji na: <a href='{dataDetails["Link"]}'>{dataDetails["DisplayLink"]}</a>";
            }
            else if (type == "Video")
            {
                Title = dataDetails["Title"];
                Body = $@"<p>{dataDetails["Description"]}</p>
                          Obejrzyj wideo na: <a href='{dataDetails["Link"]}'>{dataDetails["DisplayLink"]}<a>";
            }
        }

        public void SetFavorites(string tagId)
        {
            if (_clientSocketManager.SetToFavorites(tagId))
                MessageBox.Show("Poprawnie dodano do ulubionych!");
            else
                MessageBox.Show("Wybrana pozycja już jest w ulubionych.");
        }

        public void ShareArticleToWeb(string dataTag)
        {
            var splitedTag = dataTag.Split(',');
            string type = splitedTag[1];
            var dataDetails = _clientSocketManager.GetDataDetails(dataTag);

            _webApiManager.SaveArticleToWebAsync(new Dictionary<string, string>
            {
                { "Title", _title },
                { "Description", dataDetails["Snippet"] },
                { "Content", _body }
            });

            MessageBox.Show("Wysłano żądanie udostęnienia artykułu.");
        }

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        private string _body;
        public string Body
        {
            get => _body;
            set
            {
                _body = value;
                OnPropertyChanged(nameof(Body));
            }
        }
    }
}