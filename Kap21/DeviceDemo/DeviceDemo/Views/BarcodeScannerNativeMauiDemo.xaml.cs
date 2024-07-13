using BarcodeScanning;


namespace DeviceDemo.Views
{
  public partial class BarcodeScannerNativeMauiDemo : ContentPage
  {
    public BarcodeScannerNativeMauiDemo()
    {
      InitializeComponent();
    }

    private void CameraView_OnOnDetectionFinished(object? sender, OnDetectionFinishedEventArg e)
    {
      foreach (BarcodeResult barcodeResult in e.BarcodeResults)
      {
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