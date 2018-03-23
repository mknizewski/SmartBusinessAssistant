using Caliburn.Micro;
using SBA.Client.Wpf.ViewModels;
using System.Windows;

namespace SBA.Client.Wpf
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainPanelViewModel>();
        }
    }
}