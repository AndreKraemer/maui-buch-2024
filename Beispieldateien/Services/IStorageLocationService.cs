using DontLetMeExpire.Models;

namespace DontLetMeExpire.Services;

public interface IStorageLocationService
{
    Task<IEnumerable<StorageLocation>> GetAsync();
    Task<IEnumerable<StorageLocationWithItemCount>> GetWithItemCountAsync();
    Task<StorageLocation?> GetByIdAsync(string id);
    Task SaveAsync(StorageLocation storageLocation);
    Task DeleteAsync(StorageLocation storageLocation);
    Task DeleteAllAsync();
}