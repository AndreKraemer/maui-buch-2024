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
    cameraView.CamerasLoaded += CameraView_CamerasLoaded;
    cameraView.BarcodeDetected += CameraView_BarcodeDetected;
    cameraView.BarCodeDecoder = new ZXingBarcodeDecoder();

    cameraView.BarCodeOptions = new BarcodeDecodeOptions
    {
      AutoRotate = true,
      PossibleFormats = { BarcodeFormat.EAN_13, BarcodeFormat.EAN_8, BarcodeFormat.QR_CODE },
      ReadMultipleCodes = false,
      TryHarder = false,
      TryInverted = true
    };
    cameraView.BarCodeDetectionEnabled = true;
    cameraView.ControlBarcodeResultDuplicate = true;
  }

  private async void CameraView_CamerasLoaded(object? sender, EventArgs e)
  {
    SetCamera();
  }

  private void SetCamera()
  {
    if (cameraView.NumCamerasDetected == 0)
    {
      return;
    }

#if WINDOWS || MACCATALYST
    cameraView.Camera = cameraView.Cameras.FirstOrDefault(x => x.Position == CameraPosition.Front) ?? cameraView.Cameras.FirstOrDefault();
#else
    cameraView.Camera = cameraView.Cameras.FirstOrDefault(x => x.Position != CameraPosition.Front) ?? cameraView.Cameras.FirstOrDefault();;
#endif
  }


  private async void CameraView_BarcodeDetected(object sender, BarcodeEventArgs args)
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

        cameraView.FadeTo(0.5, 500);
        await cameraView.ScaleTo(1.1, 500);
        await cameraView.ScaleTo(1, 500);
        cameraView.FadeTo(1);

      });
    }
  }

  override protected void OnAppearing()
  {
    base.OnAppearing();
  }


  private async void Button_Clicked(object sender, EventArgs e)
  {
    if(cameraView.Camera == null)
    {
      SetCamera();
    }
    await cameraView.StartCameraAsync();
  }

  protected override async void OnDisappearing()
  {
    //  base.OnDisappearing();
    //  cameraView.BarcodeDetected -= CameraView_BarcodeDetected;
    //  cameraView.CamerasLoaded -= CameraView_CamerasLoaded;
    await cameraView.StopCameraAsync();
    cameraView.Camera = null;

  }
}