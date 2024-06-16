using CollectionViewSamples.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using CollectionViewSamples.Services;

namespace CollectionViewSamples.ViewModels
{
  public class RefreshSampleViewModel : BaseViewModel<Person>
  {

    public ObservableCollection<Person> Persons { get; } = [];
    public Command LoadPersonsCommand { get; }

    public RefreshSampleViewModel(IDataStore<Person> dataStore) : base(dataStore)
    {
      Title = "Daten aktualisieren";
      LoadPersonsCommand = new Command(async () => await LoadPersons());
    }

    async Task LoadPersons()
    {
      try
      {
        Persons.Clear();
        var persons = await DataStore.GetItemsAsync(true);
        foreach (var person in persons)
        {
          Persons.Add(person);
        }
      }
      finally
      {
        IsBusy = false;
      }
    }
    public override async Task Initialize()
    {
      IsBusy = true;
      await LoadPersons();
      await base.Initialize();
    }
  }
}