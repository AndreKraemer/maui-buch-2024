using Camera.MAUI;
using Camera.MAUI.ZXing;
using Camera.MAUI.ZXingHelper;

namespace DeviceDemo.Views;

public partial class ZxingCameraMauiDemoPage : ContentPage
{
  public ZxingCameraMauiDemoPage()
  {
    InitializeComponent();
    // Auf das Ereignis, dass die Kameras geladen wurden, reagieren
    cameraView.CamerasLoaded += CameraView_CamerasLoaded;

    // Auf das Ereignis reagieren, dass ein Barcode erkannt wurde
    cameraView.BarcodeDetected += CameraView_BarcodeDetected;

    // BarcodeDecoder auf ZXingBarcodeDecoder setzen
    cameraView.BarCodeDecoder = new ZXingBarcodeDecoder();

    // BarcodeOptionen setzen
    cameraView.BarCodeOptions = new BarcodeDecodeOptions
    {
      // Automatisches Drehen des Bildes
      AutoRotate = true,
      // Mögliche Formate setzen (13- und 8-stellige GTIN-Barcodes, sowie QRCodes)
      PossibleFormats = { BarcodeFormat.EAN_13, BarcodeFormat.EAN_8, BarcodeFormat.QR_CODE },
      // Nur einen Code lesen
      ReadMultipleCodes = false,
      // Intensiver nach Barcodes suchen, auch wenn es länger dauert
      TryHarder = false,
      // Codes auch in einer invertierten Variante des Bildes suchen
      TryInverted = true
    };
    // Barcode suche aktivieren
    cameraView.BarCodeDetectionEnabled = true;
    // Mehrfach erkannte Barcodes ignorieren
    cameraView.ControlBarcodeResultDuplicate = true;
  }

  // Ereignisbehandlung, wenn die Kameras geladen wurden
  private async void CameraView_CamerasLoaded(object? sender, EventArgs e)
  {
    SetCamera();
  }

  // Automatische Auswahl der Kamera
  private void SetCamera()
  {
    // Prüfen, ob Kameras im Gerät entdeckt wurden
    if (cameraView.NumCamerasDetected == 0)
    {
      return;
    }

#if WINDOWS || MACCATALYST
    // Auf dem Desktop (Windows und Mac) wählen wir die erste Frontkamera aus
    // Gibt es keine Frontkamera, dann wählen wir die erste verfügbare Kamera aus
    cameraView.Camera = cameraView.Cameras.FirstOrDefault(x => x.Position == CameraPosition.Front) ?? cameraView.Cameras.FirstOrDefault();
#else
    // Auf dem Handy (Android und iOS) wählen wir die erste Rückkamera aus
    // Gibt es keine Rückkamera, dann wählen wir die erste verfügbare Kamera aus
    cameraView.Camera = cameraView.Cameras.FirstOrDefault(x => x.Position != CameraPosition.Front) ?? cameraView.Cameras.FirstOrDefault();;
#endif
  }

  // Wird aufgerufen, sobald ein oder mehrere Barcodes erkannt wurden
  private async void CameraView_BarcodeDetected(object sender, BarcodeEventArgs args)
  {
    foreach (var result in args.Result)
    {
      // Wechsel in den UI Thread. Ansonsten stürzt die App ab!
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

  private async void ScanButton_Clicked(object sender, EventArgs e)
  {
    // Falles noch keine aktive Kamera gibt, versuchen eine 
    // zu aktivieren
    if (cameraView.Camera == null)
    {
      SetCamera();
    }
    // Kamera starten
    await cameraView.StartCameraAsync();
  }

  protected override async void OnDisappearing()
  {
    base.OnDisappearing();
    // Kamera stoppen, wenn die Seite nicht mehr sichtbar ist
    // Wird dieser Aufruf vergessen, reduziert sich die Performance der App
    // spürbar!
    await cameraView.StopCameraAsync();
    cameraView.Camera = null;

  }
}