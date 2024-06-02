using System.Runtime.CompilerServices;

namespace CustomControlsDemo.Controls;

public partial class EntryWithLabel : ContentView
{
  public static readonly BindableProperty LabelTextProperty = BindableProperty.Create(nameof(LabelText), typeof(string), typeof(EntryWithLabel), default(string));
  public static readonly BindableProperty EntryTextProperty = BindableProperty.Create(nameof(EntryText), typeof(string), typeof(EntryWithLabel), default(string), BindingMode.TwoWay);
  public static readonly BindableProperty ValidationErrorsProperty = BindableProperty.Create(nameof(ValidationErrors), typeof(IEnumerable<string>), typeof(EntryWithLabel), null);

  private Label errorLabel;

  protected override void OnApplyTemplate()
  {
    base.OnApplyTemplate();
    errorLabel = (Label)GetTemplateChild("errorLabel");
  }

  public EntryWithLabel()
  {
    InitializeComponent();
  }

  public string LabelText
  {
    get => (string)GetValue(LabelTextProperty);
    set => SetValue(LabelTextProperty, value);
  }

  public string EntryText
  {
    get => (string)GetValue(EntryTextProperty);
    set => SetValue(EntryTextProperty, value);
  }

  public IEnumerable<string>? ValidationErrors
  {
    get => (IEnumerable<string>?)GetValue(ValidationErrorsProperty);
    set => SetValue(ValidationErrorsProperty, value);
  }

  protected override async void OnPropertyChanged([CallerMemberName] string propertyName = null)
  {
    base.OnPropertyChanged(propertyName);
    if (propertyName == ValidationErrorsProperty.PropertyName)
    {
      if (errorLabel != null)
      {
        bool hasErrors = ValidationErrors != null && ValidationErrors.Any();

        if (hasErrors)
        {
          errorLabel.Text = string.Join(", ", ValidationErrors);
          errorLabel.IsVisible = true;
          await errorLabel.FadeTo(1, 1000);
        }
        else
        {
          await errorLabel.FadeTo(0, 1000);
          errorLabel.Text = string.Empty;
          errorLabel.IsVisible = false;
        }
      }
    }
  }
}