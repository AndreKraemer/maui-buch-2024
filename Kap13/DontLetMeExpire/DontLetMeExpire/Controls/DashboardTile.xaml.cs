using System.Text.Json;
using System.Windows.Input;

namespace DontLetMeExpire.Controls;

public partial class DashboardTile : ContentView
{

  public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string),
       typeof(DashboardTile));

  public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string),
      typeof(DashboardTile));

  public static readonly BindableProperty CountProperty = BindableProperty.Create(nameof(Count), typeof(int),
      typeof(DashboardTile), default(int));


  public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor),
      typeof(Color), typeof(DashboardTile), Colors.Black);

  public static readonly BindableProperty CountColorProperty = BindableProperty.Create(nameof(CountColor),
      typeof(Color), typeof(DashboardTile), Colors.Black);

  public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor),
      typeof(Color), typeof(DashboardTile), Colors.Black);

  public DashboardTile()
	{
		InitializeComponent();
	}

  public Color TextColor
  {
    get => (Color)GetValue(TextColorProperty);
    set => SetValue(TextColorProperty, value);
  }

  public Color CountColor
  {
    get => (Color)GetValue(CountColorProperty);
    set => SetValue(CountColorProperty, value);
  }

  public Color BorderColor
  {
    get => (Color)GetValue(BorderColorProperty);
    set => SetValue(BorderColorProperty, value);
  }

  public string Icon
  {
    get => (string)GetValue(IconProperty);
    set => SetValue(IconProperty, value);
  }

  public string Text
  {
    get => (string)GetValue(TextProperty);
    set => SetValue(TextProperty, value);
  }

  public int Count
  {
    get => (int)GetValue(CountProperty);
    set => SetValue(CountProperty, value);
  }
}