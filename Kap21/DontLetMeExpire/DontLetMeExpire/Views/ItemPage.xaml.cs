
using System.ComponentModel;
using DontLetMeExpire.ViewModels;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;

namespace DontLetMeExpire.Views;
[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class ItemPage : ContentPage
{
  private readonly ItemViewModel _viewModel;
  private CameraBarcodeReaderView? _cameraView;
  private bool _isHandlingBarcode;

  public ItemPage(ItemViewModel viewModel)
  {
    InitializeComponent();
    BindingContext = _viewModel = viewModel;

    // Den Scanner ein- bzw. ausschalten, sobald der Scan-Button gedrückt wird
    _viewModel.PropertyChanged += ViewModel_PropertyChanged;
  }

  public string? ItemId { get; set; }

  override protected async void OnNavigatedTo(NavigatedToEventArgs args)
  {
    await _viewModel.InitializeAsync(ItemId);
    base.OnNavigatedTo(args);
  }

  private async void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
  {
    if (e.PropertyName != nameof(ItemViewModel.ShowBarcodeScanner))
    {
      return;
    }

    if (_viewModel.ShowBarcodeScanner)
    {
      await StartScannerAsync();
    }
    else
    {
      StopScanner();
    }
  }

  // Erzeugt die Kameraansicht erst, wenn der Scanner tatsächlich benötigt wird.
  // So wird die Kamera-Berechtigung zum richtigen Zeitpunkt angefragt, und die
  // Kamera läuft nicht unnötig im Hintergrund.
  private async Task StartScannerAsync()
  {
    if (_cameraView is not null)
    {
      return;
    }

    // Kamera-Berechtigung anfragen
    var status = await Permissions.RequestAsync<Permissions.Camera>();
    if (status != PermissionStatus.Granted || !BarcodeScanning.IsSupported)
    {
      _viewModel.ShowBarcodeScanner = false;
      return;
    }

    _cameraView = new CameraBarcodeReaderView
    {
#if WINDOWS || MACCATALYST
      // Auf dem Desktop (Windows und Mac) verwenden wir die Frontkamera
      CameraLocation = CameraLocation.Front,
#else
      // Auf dem Handy (Android und iOS) verwenden wir die Rückkamera
      CameraLocation = CameraLocation.Rear,
#endif
      // BarcodeOptionen setzen
      Options = new BarcodeReaderOptions
      {
        // Bild automatisch drehen, damit Barcodes auch im Hochformat erkannt werden
        AutoRotate = true,
        // Mögliche Formate setzen (13- und 8-stellige GTIN-Barcodes)
        Formats = BarcodeFormat.Ean13 | BarcodeFormat.Ean8,
        // Nur einen Code lesen
        Multiple = false,
        // Intensiver nach Barcodes suchen deaktivieren, da es länger dauert
        TryHarder = false,
        // Eine Auflösung nahe 1280x720 wählen. Höhere Auflösungen verlangsamen die
        // Erkennung deutlich, da jedes Kamerabild vollständig analysiert wird.
        CameraResolutionSelector = resolutions => resolutions
          .OrderBy(r => Math.Abs((r.Width * r.Height) - (1280 * 720)))
          .ThenBy(r => Math.Abs(r.Width - 1280) + Math.Abs(r.Height - 720))
          .First()
      },
      IsDetecting = true
    };

    // Auf das Ereignis reagieren, dass ein Barcode erkannt wurde
    _cameraView.BarcodesDetected += CameraView_BarcodeDetected;
    ScannerHost.Content = _cameraView;
  }

  // Entfernt die Kameraansicht wieder. Dadurch wird auch die Kamera beendet.
  private void StopScanner()
  {
    if (_cameraView is null)
    {
      return;
    }

    _cameraView.IsDetecting = false;
    _cameraView.BarcodesDetected -= CameraView_BarcodeDetected;
    ScannerHost.Content = null;
    _cameraView.Handler?.DisconnectHandler();
    _cameraView = null;
  }

  // Wird aufgerufen, sobald ein oder mehrere Barcodes erkannt wurden
  private void CameraView_BarcodeDetected(object? sender, BarcodeDetectionEventArgs args)
  {
    var result = args.Results.FirstOrDefault();
    if (result is null)
    {
      return;
    }

    // ZXing.Net.Maui meldet erkannte Barcodes auf einem Hintergrund-Thread.
    // Alle Zugriffe auf die Oberfläche müssen daher auf dem UI-Thread erfolgen.
    Dispatcher.Dispatch(async () =>
    {
      // Mehrfachmeldungen desselben Barcodes ignorieren
      if (_isHandlingBarcode || _cameraView is null)
      {
        return;
      }
      _isHandlingBarcode = true;
      _cameraView.IsDetecting = false;

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
      _ = ScannerHost.FadeToAsync(0.5, 500);
      await ScannerHost.ScaleToAsync(1.1, 500);
      await ScannerHost.ScaleToAsync(1, 500);
      await ScannerHost.FadeToAsync(1);

      // Barcode-Suche ausblenden (entfernt auch die Kameraansicht)
      _viewModel.ShowBarcodeScanner = false;
      _isHandlingBarcode = false;
    });
  }

  protected override void OnDisappearing()
  {
    base.OnDisappearing();
    // Kamera stoppen, wenn die Seite nicht mehr sichtbar ist
    // Wird dieser Aufruf vergessen, reduziert sich die Performance der App
    // spürbar!
    _viewModel.ShowBarcodeScanner = false;
  }
}
