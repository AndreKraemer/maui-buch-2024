using DontLetMeExpire.Views;

namespace DontLetMeExpire
{
  public partial class AppShell : Shell
  {
    public AppShell()
    {
      InitializeComponent();
      Routing.RegisterRoute(nameof(ItemPage), typeof(ItemPage));
      Routing.RegisterRoute(nameof(ItemsPage), typeof(ItemsPage));
    }
  }
}
