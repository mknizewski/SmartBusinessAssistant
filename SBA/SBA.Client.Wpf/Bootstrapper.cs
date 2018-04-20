using Caliburn.Micro;
using SBA.Client.Wpf.ViewModels;
using System.Windows;

namespace SBA.Client.Wpf
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            this.Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            this.DisplayRootViewFor<MainViewModel>();
        }
    }
}