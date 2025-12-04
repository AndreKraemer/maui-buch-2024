
namespace ElVegetarianoFurio.Maui.AutomaticMigration
{
  public partial class App : Application
  {
    private readonly IServiceProvider _serviceProvider;

    public App(IServiceProvider serviceProvider)
    {
      _serviceProvider = serviceProvider;
      InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
      var window = new Window(new AppShell());
      return window;
    }

    public IServiceProvider ServiceProvider => _serviceProvider;
  }
}
