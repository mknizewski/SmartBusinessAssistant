using MahApps.Metro.Controls;
using SBA.Client.Wpf.BOL.Infrastucture;
using SBA.Client.Wpf.ViewModels;

namespace SBA.Client.Wpf.Views
{
    public partial class DataDetailView : MetroWindow
    {
        private DataDetailViewModel _dataContext;

        public DataDetailView(string dataId)
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            DataContext = _dataContext = SimpleFactory.Get<DataDetailViewModel>();
        }
    }
}