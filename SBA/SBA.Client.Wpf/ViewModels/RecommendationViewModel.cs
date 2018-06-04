﻿using SBA.Client.Wpf.BOL.Infrastucture;
using SBA.Client.Wpf.BOL.Managers;
using SBA.Client.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace SBA.Client.Wpf.ViewModels
{
    public class RecommendationViewModel : INotifyPropertyChanged
    {
        private readonly IClientSocketManager _clientSocketManager;
        private readonly IWebApiManager _webApiManager;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public RecommendationViewModel()
        {
            _clientSocketManager = SimpleFactory.Get<ClientSocketManager, IClientSocketManager>();
            _webApiManager = SimpleFactory.Get<WebApiManager, IWebApiManager>();
        }

        public void LoadRecommendations()
        {
            try
            {
                Tiles.Clear();

                var recommendData = _clientSocketManager.GetRecommendData();
                foreach (var item in recommendData)
                {
                    string type = item["Type"];
                    if (type == "ArticleCse")
                        Tiles.Add(RecommendationModel.Tile.GetArticle(item["Id"], item["Title"]));
                    else if (type == "EventCse")
                        Tiles.Add(RecommendationModel.Tile.GetEvent(item["Id"], item["Title"]));
                    else if (type == "OrganizationCse")
                        Tiles.Add(RecommendationModel.Tile.GetOrganization(item["Id"], item["Title"]));
                    else if (type == "PersonCse")
                        Tiles.Add(RecommendationModel.Tile.GetPerson(item["Id"], item["Title"]));
                    else if (type == "VideoCse")
                        Tiles.Add(RecommendationModel.Tile.GetVideo(item["Id"], item["Title"]));
                }

                RefreshTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                LoadStatus = "OK";
            }
            catch
            {
                LoadStatus = "Nie możemy załadować rekomendacji.";
            }
        }

        public void LoadFavorites()
        {
            try
            {
                FavoriteTiles.Clear();

                var favoriteData = _clientSocketManager.GetFavorites();
                foreach (var item in favoriteData)
                {
                    string type = item["Type"];
                    if (type == "ArticleCse")
                        FavoriteTiles.Add(RecommendationModel.Tile.GetArticle(item["Id"], item["Title"]));
                    else if (type == "EventCse")
                        FavoriteTiles.Add(RecommendationModel.Tile.GetEvent(item["Id"], item["Title"]));
                    else if (type == "OrganizationCse")
                        FavoriteTiles.Add(RecommendationModel.Tile.GetOrganization(item["Id"], item["Title"]));
                    else if (type == "PersonCse")
                        FavoriteTiles.Add(RecommendationModel.Tile.GetPerson(item["Id"], item["Title"]));
                    else if (type == "VideoCse")
                        FavoriteTiles.Add(RecommendationModel.Tile.GetVideo(item["Id"], item["Title"]));
                }

                RefreshTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                LoadStatus = "OK";
            }
            catch
            {
                LoadStatus = "Nie możemy załadować rekomendacji.";
            }
        }

        public void RecommendOnDemand()
        {
            Task.Run(() => _clientSocketManager.RecommendOnDemand(_keywords));
            MessageBox.Show("Żądanie zostało wysłane. Prosimy o odświeżenie treści za jakiś czas.");
        }

        public void ShareOnWeb()
        {
            _webApiManager.SaveArticleToWebAsync(new Dictionary<string, string>
            {
                { "Title", _title },
                { "Description", _description },
                { "Content", _articleBody }
            });

            Title = string.Empty;
            Description = string.Empty;
            ArticleBody = string.Empty;

            MessageBox.Show("Wysłano żądanie do stworzenia nowego artykułu.");
        }

        public ObservableCollection<RecommendationModel.Tile> Tiles { get; } = new ObservableCollection<RecommendationModel.Tile>();
        public ObservableCollection<RecommendationModel.Tile> FavoriteTiles { get; } = new ObservableCollection<RecommendationModel.Tile>();

        private string _refreshTime;
        public string RefreshTime
        {
            get => _refreshTime;
            set
            {
                _refreshTime = value;
                OnPropertyChanged(nameof(RefreshTime));
            }
        }

        private string _loadStatus;
        public string LoadStatus
        {
            get => _loadStatus;
            set
            {
                _loadStatus = value;
                OnPropertyChanged(nameof(LoadStatus));
            }
        }

        private string _keywords;
        public string Keywords
        {
            get => _keywords;
            set
            {
                _keywords = value;
                OnPropertyChanged(nameof(Keywords));
            }
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

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private string _articleBody;
        public string ArticleBody
        {
            get => _articleBody;
            set
            {
                _articleBody = value;
                OnPropertyChanged(nameof(ArticleBody));
            }
        }
    }
}