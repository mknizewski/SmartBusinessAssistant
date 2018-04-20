using SBA.Client.Wpf.BOL.Infrastucture;
using SBA.Client.Wpf.BOL.Managers;
using System.Windows;

namespace SBA.Client.Wpf
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // TODO: Do usunięcia potem
            IClientSocketManager clientSocketManager = SimpleFactory.Get<ClientSocketManager, IClientSocketManager>();
            string retData = clientSocketManager.ExchangeDataWithCore("teścik");
            MessageBox.Show(retData);
        }
    }
}