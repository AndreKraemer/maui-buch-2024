namespace MvvmSample.Views;

public partial class NoMvvmDemoPage : ContentPage
{
	public NoMvvmDemoPage()
	{
		InitializeComponent();
    SignButton.IsEnabled = false; // Initialer Zustand
  }

  private void NameEntry_TextChanged(object sender, TextChangedEventArgs e)
  {
    // Aktualisiere die Unterschrift live, während der Benutzer tippt
    SignatureNameLabel.Text = e.NewTextValue;
    ValidateForm();
  }

  private void PlaceEntry_TextChanged(object sender, TextChangedEventArgs e)
  {
    // Setze den Ort in DatePlaceLabel, während der Benutzer tippt.
    // Das aktuelle Datum wird hier noch nicht eingefügt, da es erst beim Klicken des Buttons gesetzt werden soll.
    DatePlaceLabel.Text = e.NewTextValue;
    ValidateForm();
  }

  private void AcceptTermsCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
  {
    ValidateForm();
  }


  private void ValidateForm()
  {
    // Überprüfen, ob Name, Ort und AGB akzeptiert wurden
    // und den Button entsprechend aktivieren oder deaktivieren
    SignButton.IsEnabled = !string.IsNullOrWhiteSpace(NameEntry.Text) &&
                           !string.IsNullOrWhiteSpace(PlaceEntry.Text) &&
                           AcceptTermsCheckBox.IsChecked;
  }

  private void OnSignButtonClick(object sender, EventArgs e)
  {
    var currentTime = DateTime.Now;

    // Datum und Ort Formatierung
    DatePlaceLabel.Text = $"{currentTime:d}, {PlaceEntry.Text}";

    // Unterschrift ausgeben
    SignatureNameLabel.Text = $"{NameEntry.Text}";

    // "Unterschrieben am" ausgeben
    SignedInfoLabel.Text = $"Unterschrieben am {currentTime:d} um {currentTime:t}";
  }
}