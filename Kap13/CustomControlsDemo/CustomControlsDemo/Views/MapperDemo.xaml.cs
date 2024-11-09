namespace CustomControlsDemo.Views;

public partial class MapperDemoPage : ContentPage
{
  public MapperDemoPage()
  {
    InitializeComponent();
  }

  private async void OnProgressPlusClicked(object sender, EventArgs e)
  {
    if (progressBar.Progress < 1)
    {
      // progressBar.Progress += 0.1;
      await progressBar.ProgressTo(progressBar.Progress + 0.1, 750, Easing.SinOut);
    }
  }

  private async void OnProgressMinusClicked(object sender, EventArgs e)
  {
    if(progressBar.Progress > 0)
    {
      // progressBar.Progress -= 0.1;
      await progressBar.ProgressTo(progressBar.Progress - 0.1, 500, Easing.SinOut);
    }
  }
}