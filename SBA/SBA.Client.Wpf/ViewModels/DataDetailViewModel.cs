using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SBA.Client.Wpf.ViewModels
{
    public class DataDetailViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}