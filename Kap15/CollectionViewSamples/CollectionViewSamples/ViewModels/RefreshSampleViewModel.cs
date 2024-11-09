using CollectionViewSamples.Models;
using System.Collections.ObjectModel;
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

    async Task LoadPersons(bool isRefreshing = true)
    {
      try
      {
        await Task.Delay(6000);
        Persons.Clear();
        var persons = await DataStore.GetItemsAsync(true);
        // Add Dummy Item when refreshing
        if (isRefreshing)
        {
          Persons.Add(new Person { Id = "0", Name = $"Refresh Demo ({DateTime.Now:T})", CompanyName = "-" });
        }
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
      await LoadPersons(false);
      await base.Initialize();
    }
  }
}