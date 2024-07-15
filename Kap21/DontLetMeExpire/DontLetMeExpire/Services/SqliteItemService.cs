namespace DontLetMeExpire.Services;

using SQLite;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class SqliteItemService : IItemService
{
  private readonly SQLiteAsyncConnection _connection;

  public SqliteItemService()
  {
    // Name der Datenbankdatei
    var databaseFilename = $"dontletmeexpire.db3";

    // Flags für die Datenbankverbindung
    var flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;

    // Pfad zur Datenbankdatei
    var databasePath =
        Path.Combine(FileSystem.AppDataDirectory, databaseFilename);

    // Verbindung zur Datenbank herstellen und Tabelle erstellen
    _connection = new SQLiteAsyncConnection(databasePath);
    _connection.CreateTableAsync<Item>().Wait();
  }

  // Abrufen aller Elemente
  public async Task<IEnumerable<Item>> GetAsync()
  {
    return await _connection.Table<Item>().OrderBy(x => x.ExpirationDate).ToListAsync();
  }

  // Abrufen eines Elements anhand der ID
  public async Task<Item?> GetByIdAsync(string id)
  {
    return await _connection.FindAsync<Item>(id);
  }

  // Löschen aller Elemente
  public Task DeleteAllAsync()
  {
    return _connection.DeleteAllAsync<Item>();
  }

  // Abrufen von Elementen anhand des Speicherorts
  public async Task<IEnumerable<Item>> GetByLocationAsync(string locationId)
  {
    return await _connection.Table<Item>().Where(x => x.StorageLocationId == locationId)
        .OrderBy(x => x.ExpirationDate).ToListAsync();
  }

  // Abrufen von Elementen, die abgelaufen sind
  public async Task<IEnumerable<Item>> GetExpiredAsync()
  {
    return await _connection.Table<Item>().Where(x => x.ExpirationDate < DateTime.Today)
        .OrderBy(x => x.ExpirationDate)
        .ToListAsync();
  }

  // Abrufen von Elementen, die heute ablaufen
  public async Task<IEnumerable<Item>> GetExpiresTodayAsync()
  {
    return await _connection.Table<Item>().Where(x => x.ExpirationDate == DateTime.Today)
        .OrderBy(x => x.Name)
        .ToListAsync();
  }

  // Abrufen von Elementen, die in den nächsten Tagen ablaufen
  public async Task<IEnumerable<Item>> GetExpiresSoonAsync(int days = 5)
  {
    var expiryDateUpperLimit = DateTime.Today.AddDays(days);
    return await _connection.Table<Item>()
        .Where(x => x.ExpirationDate >= DateTime.Today && x.ExpirationDate <= expiryDateUpperLimit)
        .OrderBy(x => x.ExpirationDate)
        .ToListAsync();
  }

  // Hinzufügen oder Aktualisieren eines Elements
  public async Task SaveAsync(Item item)
  {
    var hasEmptyId = string.IsNullOrEmpty(item.Id) 
         || item.Id == Guid.Empty.ToString();
    
    var isExistingRecord = await _connection.Table<Item>()
        .Where(x => x.Id == item.Id)
        .CountAsync() == 1;

    // Prüfen, ob es sich um einen neuen Eintrag handelt
    if (hasEmptyId || !isExistingRecord)
    {
      if (hasEmptyId)
      {
        item.Id = Guid.NewGuid().ToString();
      }
      // Falls es ein neuer Eintrag ist,
      // wird dieser zur Datenbank hinzugefügt
      await _connection.InsertAsync(item);
    }
    else
    {
      // Falls es sich um einen bestehenden Eintrag handelt,
      // wird dieser in der Datenbank aktualisiert
      await _connection.UpdateAsync(item);
    }
  }

  public async Task DeleteAsync(Item item)
  {
    if (!string.IsNullOrEmpty(item?.Id))
    {
      await _connection.DeleteAsync(item);
    }
  }
}