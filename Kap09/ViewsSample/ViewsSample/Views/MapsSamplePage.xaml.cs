using Microsoft.Maui.Controls.Maps;

namespace ViewsSample.Views;

public partial class MapsSamplePage : ContentPage
{
	public MapsSamplePage()
	{
		InitializeComponent();
	}

  protected override void OnNavigatedTo(NavigatedToEventArgs args)
  {
    base.OnNavigatedTo(args);
    Pin qualityBytesPin = new Pin
    {
      Location = new Location(50.5014483, 7.308297599999),
      Label = "Quality Bytes GmbH",
      Address = "Bad Breisig",
      Type = PinType.Place
    };

    map.Pins.Add(qualityBytesPin);

  }
}