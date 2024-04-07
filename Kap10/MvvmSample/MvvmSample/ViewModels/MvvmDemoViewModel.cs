namespace MvvmSample.ViewModels;

public class MvvmDemoViewModel : ViewModelBase
{
  private string _name = string.Empty;
  private string _place = string.Empty;
  private bool _acceptTerms;
  private string _signedInfo = string.Empty;
  private string _datePlace = string.Empty;

  public MvvmDemoViewModel()
  {
    SignCommand = new Command(OnSign, CanSign);
  }

  public Command SignCommand { get; }

  public string Name
  {
    get => _name;
    set => SetProperty(ref _name, value, SignCommand.ChangeCanExecute);
  }

  public string Place
  {
    get => _place;
    set => SetProperty(ref _place, value, () => { DatePlace = Place; SignCommand.ChangeCanExecute(); });
  }

  public bool AcceptTerms
  {
    get => _acceptTerms;
    set => SetProperty(ref _acceptTerms, value, SignCommand.ChangeCanExecute);
  }

  public string SignedInfo
  {
    get => _signedInfo;
    set => SetProperty(ref _signedInfo, value);
  }

  public string DatePlace
  {
    get => _datePlace;
    set => SetProperty(ref _datePlace, value);
  }

  private bool CanSign()
  {
    return !string.IsNullOrWhiteSpace(Name) &&
           !string.IsNullOrWhiteSpace(Place) &&
           AcceptTerms;
  }

  private void OnSign()
  {
    var currentTime = DateTime.Now;
    SignedInfo = $"Unterschrieben am {currentTime:d} um {currentTime:t}";
    DatePlace = $"{currentTime:d}, {Place}";
  }
}
