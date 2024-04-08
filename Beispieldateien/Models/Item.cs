// using SQLite;

namespace DontLetMeExpire.Models;

public class Item
{
    public string Id { get; set; }
    public string Name { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string StorageLocationId { get; set; }
    public string Image { get; set; }

    public decimal Amount { get; set; }
}