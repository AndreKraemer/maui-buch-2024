using DontLetMeExpire.Models;

namespace DontLetMeExpire.Services;

public class DummyStorageLocationService : IStorageLocationService
{
    private readonly IItemService _itemService;
    private readonly List<StorageLocation> _storageLocations = [];

    public DummyStorageLocationService(IItemService itemService)
    {
        _itemService = itemService;
    }

    public Task<IEnumerable<StorageLocation>> GetAsync()
    {
        var locations = _storageLocations.OrderBy(x => x.Name).ToList();
        return Task.FromResult(locations.AsEnumerable());
    }

    public async Task<IEnumerable<StorageLocationWithItemCount>> GetWithItemCountAsync()
    {
        var locations = _storageLocations.OrderBy(x => x.Name).ToList();
        var locationWithItemCount = new List<StorageLocationWithItemCount>();

        foreach (var location in locations)
        {
            var items = await _itemService.GetByLocationAsync(location.Id);
            locationWithItemCount.Add(new StorageLocationWithItemCount
            {
                Id = location.Id,
                Name = location.Name,
                Icon = location.Icon,
                ItemCount = items.Count()
            });
        }

        return locationWithItemCount;
    }

    public Task<StorageLocation?> GetByIdAsync(string id)
    {
        var location = _storageLocations.SingleOrDefault(x => x.Id == id);
        return Task.FromResult(location);
    }

    public Task SaveAsync(StorageLocation storageLocation)
    {
        var hasEmptyId = string.IsNullOrEmpty(storageLocation.Id) || storageLocation.Id == Guid.Empty.ToString();
        var isExistingRecord = _storageLocations.Any(x => x.Id == storageLocation.Id);

        if (hasEmptyId || !isExistingRecord)
        {
            if (hasEmptyId)
            {
                storageLocation.Id = Guid.NewGuid().ToString();
            }

            _storageLocations.Add(storageLocation);
        }
        else
        {
            var locationToUpdate = _storageLocations.Single(x => x.Id == storageLocation.Id);
            locationToUpdate.Name = storageLocation.Name;
            locationToUpdate.Icon = storageLocation.Icon;
        }

        return Task.CompletedTask;
    }

    public async Task DeleteAsync(StorageLocation storageLocation)
    {
        if (!string.IsNullOrEmpty(storageLocation.Id) && _storageLocations.Any(x => x.Id == storageLocation.Id))
        {
            var items = await _itemService.GetByLocationAsync(storageLocation.Id);
            foreach (var item in items)
            {
                await _itemService.DeleteAsync(item);
            }

            var locationToDelete = _storageLocations.Single(x => x.Id == storageLocation.Id);
            _storageLocations.Remove(locationToDelete);
        }
    }

    public async Task DeleteAllAsync()
    {
        await _itemService.DeleteAllAsync();
        _storageLocations.Clear();
    }
}