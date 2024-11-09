using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
namespace MvvmSample.ViewModels;

public partial class MvvmToolkitDemoViewModel : ObservableObject
{

  [ObservableProperty()]
  [NotifyCanExecuteChangedFor(nameof(SignCommand))]
  private string _name = string.Empty;

  [ObservableProperty()]
  [NotifyCanExecuteChangedFor(nameof(SignCommand))]
  private string _place = string.Empty;

  [ObservableProperty()]
  [NotifyCanExecuteChangedFor(nameof(SignCommand))]
  private bool _acceptTerms;

  [ObservableProperty()]
  private string _signedInfo = string.Empty;

  [ObservableProperty()]
  private string _datePlace = string.Empty;

  partial void OnPlaceChanged(string value)
  {
    DatePlace = value;
  }

  [RelayCommand(CanExecute = nameof(CanSign))]
  private void OnSign()
  {
    var currentTime = DateTime.Now;
    SignedInfo = $"Unterschrieben am {currentTime:d} um {currentTime:t}";
    DatePlace = $"{currentTime:d}, {Place}";
  }

  private bool CanSign()
  {
    return !string.IsNullOrWhiteSpace(Name) &&
           !string.IsNullOrWhiteSpace(Place) &&
           AcceptTerms;
  }
}
