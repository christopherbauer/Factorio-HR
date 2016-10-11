using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Factorio_HR.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetValue<T>(ref T property, T value, [CallerMemberName] string propertyName = null) where T : class
        {
            if (property != value)
            {
                OnPropertyChanged(propertyName);
                property = value;
            }
        }
    }
}