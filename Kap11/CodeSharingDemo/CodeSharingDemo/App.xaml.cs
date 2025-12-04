namespace CodeSharingDemo;

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

    public static IServiceProvider Services
    {
        get
        {
#if __ANDROID__
            return MauiApplication.Current.Services;
#elif __IOS__
            return AppDelegate.Current.Services;
#elif WINDOWS
            return MauiWinUIApplication.Current.Services;
#else
            throw new PlatformNotSupportedException();
#endif
        }
    }
}
