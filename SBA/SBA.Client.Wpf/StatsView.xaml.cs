using SBA.Client.Wpf.ViewModels;
using System.Windows;
using System.Windows.Media;

namespace SBA.Client.Wpf
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class StatsView : Window
    {
        public StatsView()
        {
            InitializeComponent();

            this.DataContext = new TestPageViewModel();
        }

        private void ShellView_Loaded_1(object sender, RoutedEventArgs e)
        {
            Matrix m = PresentationSource.FromVisual(Application.Current.MainWindow).CompositionTarget.TransformToDevice;
            double dx = m.M11;
            double dy = m.M22;

            ScaleTransform s = (ScaleTransform)mainGrid.LayoutTransform;
            s.ScaleX = 1 / dx;
            s.ScaleY = 1 / dy;
        }
    }
}