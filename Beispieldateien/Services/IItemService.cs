using DontLetMeExpire.Models;

namespace DontLetMeExpire.Services;

public interface IItemService
{
    Task<IEnumerable<Item>> GetAsync();
    Task<Item?> GetByIdAsync(string id);
    Task SaveAsync(Item item);
    Task DeleteAsync(Item item);
    Task DeleteAllAsync();
    Task<IEnumerable<Item>> GetByLocationAsync(string locationId);
    Task<IEnumerable<Item>> GetExpiredAsync();
    Task<IEnumerable<Item>> GetExpiresTodayAsync();
    Task<IEnumerable<Item>> GetExpiresSoonAsync(int days = 5);
}