using SQLite;

namespace DontLetMeExpire.Models;

public class StorageLocation
{
  [PrimaryKey]
  public string Id { get; set; }
  public string Name { get; set; }
  public string Icon { get; set; }
}