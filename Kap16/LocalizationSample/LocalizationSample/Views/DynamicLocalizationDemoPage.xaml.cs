using LocalizationSample.Resources.Strings;
using System.Globalization;

namespace LocalizationSample.Views;

public partial class DynamicLocalizationDemoPage : ContentPage
{
  public DynamicLocalizationDemoPage()
  {
    InitializeComponent();
  }

  private void Button_Clicked(object sender, EventArgs e)
  {

    // In AppointmentBooking steht der String
    // "Appointment Booking"
    var title = AppResources.AppointmentBooking;

    // In AppointmentSuccessfullyBooked steht folgender String:
    // "Thank you {0} {1}. Your appointment has been successfully booked."
    // Die Werte für {0} und {1} werden durch die Werte der im XAML
    // definierten Textfelder FirstNameEntry.Text und LastNameEntry.Text ersetzt.
    var message = string.Format(AppResources.AppointmentSuccessfullyBooked, FirstNameEntry.Text, LastNameEntry.Text);

    DisplayAlertAsync(title, message, "OK");
  }

  private void English_Clicked(object sender, EventArgs e)
  {
    ChangeCurrentUICulture("en-US");
  }

  private void French_Clicked(object sender, EventArgs e)
  {
    ChangeCurrentUICulture("fr-FR");
  }

  private void German_Clicked(object sender, EventArgs e)
  {
    ChangeCurrentUICulture("de-DE");
  }

  private void ChangeCurrentUICulture(string culture)
  {
    // Setzt die aktuelle UI-Kultur auf die angegebene Kultur
    CultureInfo.CurrentUICulture = new CultureInfo(culture);
    
    // Benachrichtigt die Instanz der TranslateSource für die AppResources
    // Resource, dass sich die Sprache geändert hat
    TranslateSource.For(typeof(AppResources).FullName).NotifyLanguageChanged();
  }
}