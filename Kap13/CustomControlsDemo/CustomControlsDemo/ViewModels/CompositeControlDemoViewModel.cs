using System.ComponentModel.DataAnnotations;

namespace CustomControlsDemo.ViewModels;

public class CompositeControlDemoViewModel : ValidationViewModelBase
{
  private string _password;
  private string _userName;

  public CompositeControlDemoViewModel()
  {
    LoginCommand = new Command(Login);
  }

  public Command LoginCommand { get; private set; }

  [Required]
  [Length(1, 50)]
  public string Username
  {
    get => _userName;
    set => SetProperty(ref _userName, value);
  }

  [Required]
  [Length(8, 50)]
  [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,50}$", 
    ErrorMessage = "8 - 50 characters, at least one upper case letter, one lower case letter, one number and one special character")]
  public string Password
  {
    get => _password;
    set => SetProperty(ref _password, value);
  }

  private void Login()
  {
    if (!Validate())
    {
      return;
    }
  }

}
