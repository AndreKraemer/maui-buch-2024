namespace DontLetMeExpire.Services;

using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using DontLetMeExpire.Models;

public class SqliteStorageLocationService : IStorageLocationService
{
  private readonly SQLiteAsyncConnection _connection;

  public SqliteStorageLocationService()
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
    _connection.CreateTableAsync<StorageLocation>().Wait();
  }

  // Abrufen aller Speicherorte
  public async Task<IEnumerable<StorageLocation>> GetAsync()
  {
    return await _connection.Table<StorageLocation>().OrderBy(x => x.Name).ToListAsync();
  }

  // Abrufen aller Speicherorte mit Anzahl der Elemente
  public async Task<IEnumerable<StorageLocationWithItemCount>> GetWithItemCountAsync()
  {
    // SQL-Statement zum Abrufen der Speicherorte mit Anzahl der Elemente
    var sql = "SELECT l.Id, l.Name, l.Icon, COUNT(i.Id) AS ItemCount " +
              "FROM StorageLocation l " +
              "LEFT JOIN Item i ON l.Id = i.StorageLocationId " +
              "GROUP BY l.Id, l.Name, l.Icon " +
              "Order BY l.Name";

    // Ausführen des SQL-Statements. Das Ergebnist wird automatisch
    // in die Klasse StorageLocationWithItemCount gemappt
    var locations = await _connection.QueryAsync<StorageLocationWithItemCount>(sql);

    return locations;
  }

  // Abrufen eines Speicherorts anhand der ID
  public Task<StorageLocation?> GetByIdAsync(string id)
  {
    return _connection.Table<StorageLocation>().Where(i => i.Id == id).FirstOrDefaultAsync();
  }

  // Speichern eines Speicherorts
  public async Task SaveAsync(StorageLocation storageLocation)
  {
    // Prüfen, ob die ID leer ist
    // oder ob ein Datensatz mit der ID bereits existiert
    var hasEmptyId = string.IsNullOrEmpty(storageLocation.Id) || storageLocation.Id == Guid.Empty.ToString();
    var isExistingRecord = await _connection.Table<StorageLocation>()
                        .Where(x => x.Id == storageLocation.Id)
                        .CountAsync() == 1;


    // Speichern oder Aktualisieren des Datensatzes
    if (hasEmptyId || !isExistingRecord)
    {
      if (hasEmptyId)
      {
        storageLocation.Id = Guid.NewGuid().ToString();
      }

      await _connection.InsertAsync(storageLocation);
    }
    else
    {
      await _connection.UpdateAsync(storageLocation);
    }
  }

  // Löschen eines Speicherorts
  public async Task DeleteAsync(StorageLocation storageLocation)
  {
    if (string.IsNullOrEmpty(storageLocation.Id))
    {
      var items = await _connection.Table<Item>()
          .Where(x => x.StorageLocationId == storageLocation.Id)
          .ToListAsync();
      foreach (var item in items)
      {
        await _connection.DeleteAsync(item);
      }
    }
    await _connection.DeleteAsync(storageLocation);
  }

  // Löschen aller Speicherorte
  public Task DeleteAllAsync()
  {
    return _connection.DeleteAllAsync<StorageLocation>();
  }
}