namespace DontLetMeExpire.Services;

public interface IAlertService
{
  Task DisplayAlertAsync(string title, string message, string cancel);
  Task<bool> DisplayAlertAsync(string title, string message, string accept, string cancel);
}