namespace ElVegetarianoFurio.Maui.ManualMigration
{
  public partial class App : Application
  {
    private readonly IServiceProvider _serviceProvider;

    public App(IServiceProvider serviceProvider)
    {
      _serviceProvider = serviceProvider;
      InitializeComponent();
      MainPage = new AppShell();
    }

    public IServiceProvider ServiceProvider => _serviceProvider;
  }
}
