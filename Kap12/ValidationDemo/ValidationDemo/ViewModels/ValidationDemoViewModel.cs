using System.ComponentModel.DataAnnotations;

namespace ValidationDemo.ViewModels;

public class ValidationDemoViewModel : ValidationViewModelBase
{
  public ValidationDemoViewModel()
  {
    SaveCommand = new Command(Save);
  }
  public Command SaveCommand { get; private set; }

  private string _firstName;

  [Required()]
  [StringLength(50, MinimumLength = 2)]
  public string FirstName
  {
    get => _firstName;
    set => SetProperty(ref _firstName, value);
  }

  private string _lastName;

  [Required()]
  [StringLength(50, MinimumLength = 2)]
  public string LastName
  {
    get => _lastName;
    set => SetProperty(ref _lastName, value);
  }

  private string _email;

  [Required()]
  [EmailAddress]
  public string Email
  {
    get => _email;
    set => SetProperty(ref _email, value);
  }

  private int _age;

  [Required()]
  [Range(1, 120)]
  public int Age
  {
    get => _age;
    set => SetProperty(ref _age, value);
  }

  private void Save()
  {
    if (Validate())
    {
      // Save the data
      // For now, just clear the form

      // Clear the form
      FirstName = string.Empty;
      LastName = string.Empty;
      Email = string.Empty;
      Age = 0;

    }
  }
}
