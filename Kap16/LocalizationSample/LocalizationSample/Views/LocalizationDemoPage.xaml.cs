using LocalizationSample.Resources.Strings;

namespace LocalizationSample.Views;

public partial class LocalizationDemoPage : ContentPage
{
  public LocalizationDemoPage()
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
    
  DisplayAlert(title, message, "OK");
}
}