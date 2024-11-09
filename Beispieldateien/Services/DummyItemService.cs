namespace DontLetMeExpire.Services;

using DontLetMeExpire.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class DummyItemService : IItemService
{
    private readonly List<Item> _items;

    public DummyItemService()
    {
        _items = [.. DummyData.Items];
    }

    public Task<IEnumerable<Item>> GetAsync()
    {
        return Task.FromResult(_items.OrderBy(x => x.ExpirationDate).AsEnumerable());
    }

    public Task<Item?> GetByIdAsync(string id)
    {
        return Task.FromResult(_items.SingleOrDefault(x => x.Id == id));
    }

    public Task DeleteAllAsync()
    {
        _items.Clear();
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Item>> GetByLocationAsync(string locationId)
    {
        return Task.FromResult(_items.OrderBy(x => x.ExpirationDate)
            .Where(x => x.StorageLocationId == locationId).AsEnumerable());
    }

    public Task<IEnumerable<Item>> GetExpiredAsync()
    {
        return Task.FromResult(_items.OrderBy(x => x.ExpirationDate)
            .Where(x => x.ExpirationDate < DateTime.Today).AsEnumerable());
    }

    public Task<IEnumerable<Item>> GetExpiresTodayAsync()
    {
        return Task.FromResult(_items.OrderBy(x => x.Name)
            .Where(x => x.ExpirationDate == DateTime.Today).AsEnumerable());
    }

    public Task<IEnumerable<Item>> GetExpiresSoonAsync(int days = 5)
    {
        var expiryDateUpperLimit = DateTime.Today.AddDays(days);
        return Task.FromResult(_items.OrderBy(x => x.ExpirationDate)
            .Where(x => x.ExpirationDate >= DateTime.Today && x.ExpirationDate <= expiryDateUpperLimit)
            .AsEnumerable());
    }

    public Task SaveAsync(Item item)
    {
        var hasEmptyId = string.IsNullOrEmpty(item.Id) || item.Id == Guid.Empty.ToString();
        var isExistingRecord = _items.Any(x => x.Id == item.Id);

        if (hasEmptyId || !isExistingRecord)
        {
            if (hasEmptyId)
            {
                item.Id = Guid.NewGuid().ToString();
            }

            _items.Add(item);
        }
        else
        {
            var itemToUpdate = _items.Single(x => x.Id == item.Id);
            itemToUpdate.Name = item.Name;
            itemToUpdate.Image = item.Image;
            itemToUpdate.ExpirationDate = item.ExpirationDate;
            itemToUpdate.StorageLocationId = item.StorageLocationId;
        }

        return Task.CompletedTask;
    }

    public Task DeleteAsync(Item item)
    {
        if (!string.IsNullOrEmpty(item?.Id) && _items.Any(x => x.Id == item.Id))
        {
            var itemToDelete = _items.Single(x => x.Id == item.Id);
            _items.Remove(itemToDelete);
        }

        return Task.CompletedTask;
    }
}