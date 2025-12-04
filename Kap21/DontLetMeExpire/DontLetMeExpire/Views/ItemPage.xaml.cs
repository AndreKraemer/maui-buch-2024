
using DontLetMeExpire.ViewModels;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;

namespace DontLetMeExpire.Views;
[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class ItemPage : ContentPage
{
  private readonly ItemViewModel _viewModel;

  public ItemPage(ItemViewModel viewModel)
  {
    InitializeComponent();
    BindingContext = _viewModel = viewModel;



    // Auf das Ereignis reagieren, dass ein Barcode erkannt wurde
    CameraView.BarcodesDetected += CameraView_BarcodeDetected;


    // BarcodeOptionen setzen
    CameraView.Options = new BarcodeReaderOptions
    {
      // Automatisches Drehen des Bildes aus Performancegründen deaktivieren
      AutoRotate = false,
      // Mögliche Formate setzen (13- und 8-stellige GTIN-Barcodes)
      Formats = BarcodeFormat.Ean13,
      // Nur einen Code lesen
      Multiple = false,
      // Intensiver nach Barcodes suchen deaktivieren, da es länger dauert
      TryHarder = false
    };
    SetCamera();
  }

  public string? ItemId { get; set; }

  override protected async void OnNavigatedTo(NavigatedToEventArgs args)
  {
    await _viewModel.InitializeAsync(ItemId);
    base.OnNavigatedTo(args);
  }


  // Automatische Auswahl der Kamera
  private async void SetCamera()
  {
    // Prüfen, ob Kameras im Gerät entdeckt wurden
    var cameras = await CameraView.GetAvailableCameras();
    if (cameras.Count == 0)
    {
      return;
    }

#if WINDOWS || MACCATALYST
    // Auf dem Desktop (Windows und Mac) wählen wir die erste Frontkamera aus
    // Gibt es keine Frontkamera, dann wählen wir die erste verfügbare Kamera aus
    CameraView.SelectedCamera = cameras.FirstOrDefault(x => x.Location != CameraLocation.Front) ?? cameras.First();
#else
    // Auf dem Handy (Android und iOS) wählen wir die erste Rückkamera aus
    // Gibt es keine Rückkamera, dann wählen wir die erste verfügbare Kamera aus
    CameraView.SelectedCamera = cameras.FirstOrDefault(x => x.Location != CameraLocation.Front) ?? cameras.First();
  #endif
 
  }

  // Wird aufgerufen, sobald ein oder mehrere Barcodes erkannt wurden
  private async void CameraView_BarcodeDetected(object sender, BarcodeDetectionEventArgs args)
  {
    foreach (var result in args.Results)
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
      _viewModel.SearchText = result.Value;
      _viewModel.SearchBarcodeCommand.Execute(null);

      // Visuelles Feedback geben, dass ein Barcode erkannt wurde
      CameraView.FadeToAsync(0.5, 500);
      await CameraView.ScaleToAsync(1.1, 500);
      await CameraView.ScaleToAsync(1, 500);
      await CameraView.FadeToAsync(1);

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
    CameraView.BarcodesDetected -= CameraView_BarcodeDetected;
    CameraView.SelectedCamera = null;
  }
}