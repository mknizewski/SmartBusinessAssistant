using SBA.Client.Wpf.BOL.Infrastucture;
using SBA.Client.Wpf.BOL.Managers;
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
        private readonly IClientSocketManager _clientSocketManager;

        public FaqViewModel() =>
            _clientSocketManager = SimpleFactory.Get<ClientSocketManager, IClientSocketManager>();

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public void LoadDataAsync()
        {
            LoadQuestionsAsync();
            LoadAnwersAsync();
            New = new FaqModel.New();
        }

        public void LoadQuestionsAsync()
        {
            Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Background,
                new Action(() =>
                {
                    try
                    {
                        IsLoading = true;

                        var questions = _clientSocketManager.GetFaqsQuestions();
                        FaqCollectionQuestions.Clear();

                        foreach (var item in questions)
                            FaqCollectionQuestions.Add(new FaqModel.Question
                            {
                                Id = item[nameof(FaqModel.Question.Id)],
                                QuestionName = item[nameof(FaqModel.Question.QuestionName)],
                                AnswerId = item[nameof(FaqModel.Question.AnswerId)],
                                InsertTime = item[nameof(FaqModel.Question.InsertTime)]
                            });

                        RefreshTime = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");
                        LoadStatus = "OK";
                        IsLoading = false;
                    }
                    catch
                    {
                        LoadStatus = "Wystąpił błąd podczas pobierania danych.";
                        IsLoading = false;
                    }
                }));
        }

        public void LoadAnwersAsync()
        {
            Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Background,
                new Action(() =>
                {
                    try
                    {
                        IsLoading = true;

                        var answers = _clientSocketManager.GetFaqsAnswers();
                        FaqCollectionAnswers.Clear();
                        AnswersIds.Clear();

                        foreach (var item in answers)
                        {
                            FaqCollectionAnswers.Add(new FaqModel.Answer
                            {
                                Id = item[nameof(FaqModel.Answer.Id)],
                                AnswerName = item[nameof(FaqModel.Answer.AnswerName)]
                            });

                            AnswersIds.Add(item[nameof(FaqModel.Answer.Id)]);
                        }

                        RefreshTime = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");
                        LoadStatus = "OK";
                        IsLoading = false;
                    }
                    catch
                    {
                        LoadStatus = "Wystąpił błąd podczas pobierania danych.";
                        IsLoading = false;
                    }
                }));
        }

        public void EditQuestionAsync(FaqModel.Question question)
        {
            Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Background,
                new Action(() =>
                {
                    _clientSocketManager.EditQuestion(
                        question.Id,
                        question.AnswerId,
                        question.QuestionName);
                }));
        }

        public void EditAnswerAsync(FaqModel.Answer answer)
        {
            Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Background,
                new Action(() => 
                {
                    _clientSocketManager.EditAnswer(
                        answer.Id,
                        answer.AnswerName);
                }));
        }

        public void DeleteQuestionRowAsync(string id)
        {
            if (!IsUserConfirmAction())
                return;

            Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Background,
                new Action(() =>
                {
                    _clientSocketManager.DeleteQuestion(id);
                    FaqCollectionQuestions.Remove(FaqCollectionQuestions.FirstOrDefault(x => x.Id == id));
                }));
        }

        public void DeleteAnswerRowAsync(string id)
        {
            if (!IsUserConfirmAction())
                return;

            Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Background, 
                new Action(() =>
                {
                    if (!IsUserCanDeleteAnswer(id))
                    {
                        MessageBox.Show(
                            "Nie możesz usunąć tej odpowiedzi, dopóki są do niej przypisane pytania!",
                            "Błąd",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);

                        return;
                    }

                    _clientSocketManager.DeleteAnswer(id);
                    FaqCollectionAnswers.Remove(FaqCollectionAnswers.FirstOrDefault(x => x.Id == id));
                }));
        }

        public void AddNewFaq()
        {
            try
            {
                IsLoading = true;

                if (string.IsNullOrEmpty(_new.Question))
                    throw new Exception("Pole Pytanie nie jest wypełnione.");

                if (string.IsNullOrEmpty(_new.AnswerId))
                    throw new Exception("Proszę wybrać odpowiedź.");

                _clientSocketManager.AddQuestion(_new.Question, _new.AnswerId);
                New = new FaqModel.New();

                MessageBox.Show("Poprawnie dodano nowe pytanie");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            IsLoading = false;
        }

        private bool IsUserCanDeleteAnswer(string id) => 
            !FaqCollectionQuestions.Any(x => x.AnswerId == id);

        public bool IsUserConfirmAction() =>
            MessageBox.Show(
                "Czy na pewno?",
                "Potwierdź akcję",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes;

        public ObservableCollection<FaqModel.Question> FaqCollectionQuestions { get; } = new ObservableCollection<FaqModel.Question>();
        public ObservableCollection<FaqModel.Answer> FaqCollectionAnswers { get; } = new ObservableCollection<FaqModel.Answer>();
        public ObservableCollection<string> AnswersIds { get; } = new ObservableCollection<string>();

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

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        private FaqModel.New _new;
        public FaqModel.New New
        {
            get => _new;
            set
            {
                _new = value;
                OnPropertyChanged(nameof(New));
            }
        }
    }
}