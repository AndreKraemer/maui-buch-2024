using BarcodeScanning;


namespace DeviceDemo.Views
{
  public partial class BarcodeScannerNativeMauiDemo : ContentPage
  {
    public BarcodeScannerNativeMauiDemo()
    {
      InitializeComponent();
    }

    private async void CameraView_OnOnDetectionFinished(object? sender, OnDetectionFinishedEventArg e)
    {
      foreach (BarcodeResult barcodeResult in e.BarcodeResults)
      {
        if(barcodeResult.DisplayValue == ResultLabel.Text)
        {
          return;
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
      await Methods.AskForRequiredPermissionAsync();
      base.OnAppearing();
      BarcodeScannerView.CameraEnabled = true;
      BarcodeScannerView.CameraFacing = CameraFacing.Back;
    }

    protected override void OnDisappearing()
    {
      base.OnDisappearing();
      BarcodeScannerView.CameraEnabled = false;
    }
  }
}