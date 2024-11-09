using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace DontLetMeExpire.ViewModels;

/// <summary>
/// Base class for view models that require validation.
/// </summary>
public abstract class ValidationViewModelBase : ViewModelBase
{
  private readonly Dictionary<string, List<ValidationResult>> _errors = [];
  private readonly ValidationContext _validationContext;

  /// <summary>
  /// Initializes a new instance of the <see cref="ValidationViewModelBase"/> class.
  /// </summary>
  protected ValidationViewModelBase()
  {
    _validationContext = new ValidationContext(this);
  }

  /// <summary>
  /// Gets all error messages.
  /// </summary>
  public IEnumerable<string> ErrorMessages =>
    _errors.Values.SelectMany(errors => errors.Select(error => error.ErrorMessage));

  /// <summary>
  /// Gets a value indicating whether the view model has errors.
  /// </summary>
  public bool HasErrors => _errors.Any();

  /// <summary>
  /// Gets the property error messages as a Dictionary.
  /// </summary>
  public Dictionary<string, IEnumerable<string>> PropertyErrorMessages
  {
    get
    {
      return _errors.ToDictionary(
          pair => pair.Key,
          pair => pair.Value.Select(error => error.ErrorMessage).AsEnumerable()
      );
    }
  }

  /// <summary>
  /// Adds errors to the specified property.
  /// </summary>
  /// <param name="propertyName">The name of the property.</param>
  /// <param name="validationResults">The validation results.</param>
  protected virtual void AddErrors(string propertyName, List<ValidationResult> validationResults)
  {
    if (_errors.ContainsKey(propertyName) &&
      _errors[propertyName].SequenceEqual(validationResults))
    {
      return;
    }

    _errors[propertyName] = validationResults;

    // UI über die Änderung der Fehler informieren
    OnPropertyChanged(nameof(HasErrors));
    OnPropertyChanged(nameof(ErrorMessages));
    OnPropertyChanged(nameof(PropertyErrorMessages));
  }

  /// <summary>
  /// Removes errors from the specified property.
  /// </summary>
  /// <param name="propertyName">The name of the property.</param>
  protected virtual void RemoveErrors(string propertyName)
  {
    if (_errors.ContainsKey(propertyName) && _errors[propertyName].Count > 0)
    {
      _errors.Remove(propertyName);

      // UI über die Änderung der Fehler informieren
      OnPropertyChanged(nameof(HasErrors));
      OnPropertyChanged(nameof(ErrorMessages));
      OnPropertyChanged(nameof(PropertyErrorMessages));
    }
  }

  /// <summary>
  /// Validates all properties of the view model.
  /// </summary>
  /// <returns><c>true</c> if all properties are valid; otherwise, <c>false</c>.</returns>
  protected virtual bool ValidateAllProperties()
  {
    var isValid = true;
    var properties = GetType().GetProperties();
    foreach (var property in properties)
    {
      var value = property.GetValue(this);
      isValid = isValid && ValidateProperty(value, property.Name);
    }
    return isValid;
  }

  /// <summary>
  /// Validates the specified property.
  /// </summary>
  /// <param name="value">The value of the property.</param>
  /// <param name="propertyName">The name of the property.</param>
  /// <returns><c>true</c> if the property is valid; otherwise, <c>false</c>.</returns>
  protected virtual bool ValidateProperty(object? value, [CallerMemberName] string propertyName = null!)
  {
    var validationResults = new List<ValidationResult>();
    _validationContext.MemberName = propertyName;

    var isValid = Validator.TryValidateProperty(value, _validationContext, validationResults);
    if (isValid)
    {
      RemoveErrors(propertyName);
    }
    else
    {
      AddErrors(propertyName, validationResults);
    }
    return isValid;
  }

  /// <summary>
  /// Validates the view model.
  /// </summary>
  /// <returns><c>true</c> if the view model is valid; otherwise, <c>false</c>.</returns>
  public bool Validate()
  {
    var validationResults = new List<ValidationResult>();

    // ValidationContext für das aktuelle Objekt erstellen
    var context = new ValidationContext(this);

    // Validierung über alle Eigenschaften des Objekts durchführen
    bool isValid = Validator.TryValidateObject(this, context, validationResults, true);

    _errors.Clear();
    if (!isValid)
    {

      foreach (var validationResult in validationResults)
      {
        foreach (var memberName in validationResult.MemberNames)
        {
          AddErrors(memberName, [validationResult]);
        }
      }
    }

    // UI über die Änderung der Fehler informieren
    OnPropertyChanged(nameof(HasErrors));
    OnPropertyChanged(nameof(ErrorMessages));
    OnPropertyChanged(nameof(PropertyErrorMessages));
    return isValid;
  }
}
