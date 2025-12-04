namespace CollectionViewSamples
{
  public partial class App : Application
  {
    public App()
    {
      InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
      var window = new Window(new AppShell());
#if WINDOWS
      window.Width = 500;
#endif
      return window;
    }
  }
}
