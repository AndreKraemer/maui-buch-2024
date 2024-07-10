using Camera.MAUI;
using Camera.MAUI.ZXing;
using Camera.MAUI.ZXingHelper;
using System.Threading.Tasks;


namespace DeviceDemo.Views;

public partial class ZxingCameraMauiDemoPage : ContentPage
{
  public ZxingCameraMauiDemoPage()
  {
    InitializeComponent();

    //CameraView.ZoomFactor = 3;
    CameraView.BarCodeOptions = new BarcodeDecodeOptions
    {
      AutoRotate = true,
      PossibleFormats = [BarcodeFormat.EAN_13, BarcodeFormat.EAN_8, BarcodeFormat.QR_CODE],
      TryHarder = true,
      TryInverted = true
    };
    CameraView.BarCodeDecoder = new ZXingBarcodeDecoder();
    CameraView.BarCodeDetectionMaxThreads = 5;
    CameraView.ControlBarcodeResultDuplicate = true;
    CameraView.BarCodeDetectionEnabled = true;
    CameraView.BarCodeDetectionEnabled = true;
  }

  private void CameraView_CamerasLoaded(object? sender, EventArgs e)
  {
    SetCamera();
  }

  private void SetCamera()
  {
    if (CameraView.NumCamerasDetected == 0)
    {
      return;
    }

#if WINDOWS || MACCATALYST
    CameraView.Camera = CameraView.Cameras.FirstOrDefault();
#else
    CameraView.Camera = CameraView.Cameras.FirstOrDefault(x => x.Position != CameraPosition.Front);
#endif
   CameraView.StartCameraAsync();
  }

  private void CameraView_BarcodeDetected(object sender, BarcodeEventArgs args)
  {
    foreach (var result in args.Result)
    {
      MainThread.InvokeOnMainThreadAsync(async () =>
      {

        try
        {
          Vibration.Vibrate(500);

        }
        catch (FeatureNotSupportedException)
        {
          // Do nothing. Happens on Devices without Vibration support
        }

        ResultLabel.Text = result.Text;

        CameraView.FadeTo(0.5, 500);
        await CameraView.ScaleTo(1.1, 500);
        await CameraView.ScaleTo(1, 500);
        CameraView.FadeTo(1);

      });
    }
  }

  override protected void OnAppearing()
  {
    base.OnAppearing();
    CameraView.CamerasLoaded += CameraView_CamerasLoaded;
    CameraView.BarcodeDetected += CameraView_BarcodeDetected;
    SetCamera();
  }

  protected override async void OnDisappearing()
  {
    base.OnDisappearing();
    CameraView.BarcodeDetected -= CameraView_BarcodeDetected;
    CameraView.CamerasLoaded -= CameraView_CamerasLoaded;
    await CameraView.StopCameraAsync();
    CameraView.Camera = null;

  }
}