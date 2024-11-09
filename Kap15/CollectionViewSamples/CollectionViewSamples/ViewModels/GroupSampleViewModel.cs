using CollectionViewSamples.Models;
using System.Collections.ObjectModel;
using CollectionViewSamples.Services;

namespace CollectionViewSamples.ViewModels;

public class GroupSampleViewModel : BaseViewModel<Person>
{
  private ObservableCollection<PersonsByCompany> _items = [];

  public ObservableCollection<PersonsByCompany> Items
  {
    set => SetProperty(ref _items, value);
    get => _items;
  }
  public GroupSampleViewModel(IDataStore<Person> dataStore) : base(dataStore)
  {
    Title = "Gruppierte Einträge";
  }

  public override async Task Initialize()
  {
    IsBusy = true;

    try
    {
      // Abrufen aller Personen
      var persons = await DataStore.GetItemsAsync(true);

      // Gruppieren der Personen nach Firma
      var group = persons.GroupBy(p => p.CompanyName)
                        .Select(group => new PersonsByCompany(group.Key, [.. group]))
                        .ToList();

      // Das dynamische ändern der Items-Collection führt unter Windows
      // zu einem Fehler. Daher wird die Collection komplett ersetzt.
      // siehe auch: https://github.com/dotnet/maui/issues/18481
      //foreach (var item in group)
      //{
      //  Items.Add(item);
      //}

      Items = new ObservableCollection<PersonsByCompany>(group);
    }
    finally
    {
      IsBusy = false;
    }
    await base.Initialize();
  }
}