namespace DontLetMeExpire.Services;

public class AlertService : IAlertService
{
  public Task DisplayAlertAsync(string title, string message, string cancel)
  {
    return MainThread.InvokeOnMainThreadAsync(() =>
        Application.Current.MainPage.DisplayAlertAsync(title, message, cancel));
  }

  public Task<bool> DisplayAlertAsync(string title, string message, string accept, string cancel)
  {
    return MainThread.InvokeOnMainThreadAsync(() => 
        Application.Current.MainPage.DisplayAlertAsync(title, message, accept, cancel));
  }
}