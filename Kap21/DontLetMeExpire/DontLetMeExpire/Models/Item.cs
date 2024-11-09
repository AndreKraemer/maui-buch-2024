using SQLite;

namespace DontLetMeExpire.Models;

public class Item
{
  [PrimaryKey]
  public string Id { get; set; }
  public string Name { get; set; }
  [Indexed]
  public DateTime ExpirationDate { get; set; }
  [Indexed]
  public string StorageLocationId { get; set; }
  public string Image { get; set; }
  public decimal Amount { get; set; }
}