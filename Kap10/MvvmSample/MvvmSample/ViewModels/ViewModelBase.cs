using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MvvmSample.ViewModels;

public abstract class ViewModelBase : INotifyPropertyChanged
{
  public event PropertyChangedEventHandler? PropertyChanged;
  protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

  protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
  {
    if (EqualityComparer<T>.Default.Equals(field, value))
    {
      return false;
    }

    field = value;
    OnPropertyChanged(propertyName);
    return true;
  }

  protected bool SetProperty<T>(ref T field, T value, Action callback, [CallerMemberName] string propertyName = "")
  {
    if (EqualityComparer<T>.Default.Equals(field, value))
    {
      return false;
    }

    field = value;
    OnPropertyChanged(propertyName);
    callback.Invoke();
    return true;
  }
}