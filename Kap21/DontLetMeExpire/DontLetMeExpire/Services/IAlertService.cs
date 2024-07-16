namespace DontLetMeExpire.Services;

public interface IAlertService
{
  Task DisplayAlert(string title, string message, string cancel);
  Task<bool> DisplayAlert(string title, string message, string accept, string cancel);
}