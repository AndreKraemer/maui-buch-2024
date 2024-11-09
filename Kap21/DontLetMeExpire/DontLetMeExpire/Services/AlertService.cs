namespace DontLetMeExpire.Services;

public class AlertService : IAlertService
{
  public Task DisplayAlert(string title, string message, string cancel)
  {
    return MainThread.InvokeOnMainThreadAsync(() =>
        Application.Current.MainPage.DisplayAlert(title, message, cancel));
  }

  public Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
  {
    return MainThread.InvokeOnMainThreadAsync(() => 
        Application.Current.MainPage.DisplayAlert(title, message, accept, cancel));
  }
}