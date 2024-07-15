using BarcodeScanning;


namespace DeviceDemo.Views
{
  public partial class BarcodeScannerNativeMauiDemo : ContentPage
  {
    public BarcodeScannerNativeMauiDemo()
    {
      InitializeComponent();
    }

    // Wird jedes Mal aufgerufen, nach dem ein Kamera-Frame geprüft wurde,
    // also auch, wenn kein Barcode erkannt wurde!
    private async void CameraView_OnDetectionFinished(object? sender, OnDetectionFinishedEventArg e)
    {
      // Barcode-Ergebnisse durchgehen
      foreach (BarcodeResult barcodeResult in e.BarcodeResults)
      {
        // Falls der Barcode mit dem zuletzt erkannten Barcode übereinstimmt,
        // dann überspringen wir diesen Schleifendurchlauf
        if (barcodeResult.DisplayValue == ResultLabel.Text)
        {
          continue;
        }
        try
        {
          Vibration.Vibrate(500);

        }
        catch (FeatureNotSupportedException)
        {
          // Do nothing. Happens on Devices without Vibration support
        }
        ResultLabel.Text = barcodeResult.DisplayValue;
      }
    }

    protected override async void OnAppearing()
    {
      // Berechtigung für die Nutzung der Kamera anfordern
      await Methods.AskForRequiredPermissionAsync();
      base.OnAppearing();

      // Kamera aktivieren
      BarcodeScannerView.CameraEnabled = true;
    }

    protected override void OnDisappearing()
    {
      base.OnDisappearing();
      // Kamera deaktivieren
      BarcodeScannerView.CameraEnabled = false;
    }
  }
}