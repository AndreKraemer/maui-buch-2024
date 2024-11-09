namespace CustomControlsDemo.Views;

public partial class DrawnControlsDemoPage : ContentPage
{
	public DrawnControlsDemoPage()
	{
		InitializeComponent();
	}

  private void OnProgressPlusClicked(object sender, EventArgs e)
  {
    if (circularProgressBar.Progress < 1)
    {
      circularProgressBar.Progress += 0.1;
    }
  }

  private void OnProgressMinusClicked(object sender, EventArgs e)
  {
    if (circularProgressBar.Progress > 0)
    {
      circularProgressBar.Progress -= 0.1;
    }
  }
}