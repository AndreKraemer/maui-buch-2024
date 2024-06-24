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
    DisplayAlert(AppResources.AppointmentBooking, string.Format(AppResources.AppointmentSuccessfullyBooked, FirstNameEntry.Text, LastNameEntry.Text), "OK");
  }
}