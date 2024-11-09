namespace LocalDataSample
{
  public partial class App : Application
  {
    public App()
    {
      InitializeComponent();

      MainPage = new AppShell();
    }
#if WINDOWS
    protected override Window CreateWindow(IActivationState activationState)
    {
      Window window = base.CreateWindow(activationState);

      window.Width = 500;

      return window;
    }
#endif    
  }
}
