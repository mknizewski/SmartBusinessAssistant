using MahApps.Metro.Controls;
using SBA.Client.Wpf.BOL.Infrastucture;
using SBA.Client.Wpf.ViewModels;

namespace SBA.Client.Wpf.Views
{
    public partial class DataDetailView : MetroWindow
    {
        private DataDetailViewModel _dataContext;
        private RecommendationView _recommendationView;
        private string _tag;

        public DataDetailView(string tagId, RecommendationView recommendationView)
        {
            InitializeComponent();
            Init(tagId, recommendationView);
        }

        private void Init(string tagId, RecommendationView recommendationView)
        {
            DataContext = _dataContext = SimpleFactory.Get<DataDetailViewModel>();

            _recommendationView = recommendationView;
            _tag = tagId;
            _dataContext.GetData(tagId);
        }

        private void LikeBtn(object sender, System.Windows.RoutedEventArgs e)
        {
            // TODO
        }

        private void DislikeBtn(object sender, System.Windows.RoutedEventArgs e)
        {
            // TODO
        }

        private void SetFavoritesBtn(object sender, System.Windows.RoutedEventArgs e) => 
            _dataContext.SetFavorites(_tag);

        private void ShareBtn(object sender, System.Windows.RoutedEventArgs e) => 
            _dataContext.ShareArticleToWeb(_tag);

        private void EditBtn(object sender, System.Windows.RoutedEventArgs e) => 
            _recommendationView.SetSharedTab(_dataContext.GetDetails(_tag));
    }
}