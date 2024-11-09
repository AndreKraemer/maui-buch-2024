using Camera.MAUI.ZXingHelper;
using Camera.MAUI;
using DontLetMeExpire.ViewModels;
using Camera.MAUI.ZXing;

namespace DontLetMeExpire.Views;
[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class ItemPage : ContentPage
{
  private readonly ItemViewModel _viewModel;

  public ItemPage(ItemViewModel viewModel)
  {
    InitializeComponent();
    BindingContext = _viewModel = viewModel;

    // Auf das Ereignis, dass die Kameras geladen wurden, reagieren
    CameraView.CamerasLoaded += CameraView_CamerasLoaded;

    // Auf das Ereignis reagieren, dass ein Barcode erkannt wurde
    CameraView.BarcodeDetected += CameraView_BarcodeDetected;

    // BarcodeDecoder auf ZXingBarcodeDecoder setzen
    CameraView.BarCodeDecoder = new ZXingBarcodeDecoder();

    // BarcodeOptionen setzen
    CameraView.BarCodeOptions = new BarcodeDecodeOptions
    {
      // Automatisches Drehen des Bildes aus Performancegründen deaktivieren
      AutoRotate = false,
      // Mögliche Formate setzen (13- und 8-stellige GTIN-Barcodes)
      PossibleFormats = { BarcodeFormat.EAN_13, BarcodeFormat.EAN_8},
      // Nur einen Code lesen
      ReadMultipleCodes = false,
      // Intensiver nach Barcodes suchen deaktivieren, da es länger dauert
      TryHarder = false
    };
    // Mehrfach erkannte Barcodes ignorieren
    CameraView.ControlBarcodeResultDuplicate = true;

  }

  public string? ItemId { get; set; }

  override protected async void OnNavigatedTo(NavigatedToEventArgs args)
  {
    await _viewModel.InitializeAsync(ItemId);
    base.OnNavigatedTo(args);
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
    if (CameraView.NumCamerasDetected == 0)
    {
      return;
    }

  #if WINDOWS || MACCATALYST
    // Auf dem Desktop (Windows und Mac) wählen wir die erste Frontkamera aus
    // Gibt es keine Frontkamera, dann wählen wir die erste verfügbare Kamera aus
    CameraView.Camera = CameraView.Cameras.FirstOrDefault(x => x.Position == CameraPosition.Front) ?? CameraView.Cameras.First();
  #else
    // Auf dem Handy (Android und iOS) wählen wir die erste Rückkamera aus
    // Gibt es keine Rückkamera, dann wählen wir die erste verfügbare Kamera aus
    CameraView.Camera = CameraView.Cameras.FirstOrDefault(x => x.Position != CameraPosition.Front) ?? CameraView.Cameras.First();
  #endif
    CameraView.ZoomFactor = Math.Min(3, CameraView.Camera.MaxZoomFactor);
  }

  // Wird aufgerufen, sobald ein oder mehrere Barcodes erkannt wurden
  private async void CameraView_BarcodeDetected(object sender, BarcodeEventArgs args)
  {
    foreach (var result in args.Result)
    {
      // Haptisches Feedback geben, dass ein Barcode erkannt wurde
      try
      {
        Vibration.Vibrate(500);

      }
      catch (FeatureNotSupportedException)
      {
        // FeatureNotSupportedException wird auf Endgeräten ohne 
        // Vibrationsfunktion geworfen. Die Exception ignorieren wir
        // an dieser Stelle.
      }

      // Barcode Suche im ViewModel ausführen.
      _viewModel.SearchText = result.Text;
      _viewModel.SearchBarcodeCommand.Execute(null);

      // Visuelles Feedback geben, dass ein Barcode erkannt wurde
      CameraView.FadeTo(0.5, 500);
      await CameraView.ScaleTo(1.1, 500);
      await CameraView.ScaleTo(1, 500);
      await CameraView.FadeTo(1);

      // Barcode-Suche ausblenden
      _viewModel.ShowBarcodeScanner = false;
    }
  }

  protected override async void OnDisappearing()
  {
    base.OnDisappearing();
    // Kamera stoppen, wenn die Seite nicht mehr sichtbar ist
    // Wird dieser Aufruf vergessen, reduziert sich die Performance der App
    // spürbar!
    CameraView.BarcodeDetected -= CameraView_BarcodeDetected;
    CameraView.CamerasLoaded -= CameraView_CamerasLoaded;
    await CameraView.StopCameraAsync();
    CameraView.Camera = null;
  }
}