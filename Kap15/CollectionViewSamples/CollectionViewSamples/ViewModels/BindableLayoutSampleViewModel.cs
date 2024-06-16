using CollectionViewSamples.Models;
using System.Collections.ObjectModel;
using CollectionViewSamples.Services;

namespace CollectionViewSamples.ViewModels
{
  public class BindableLayoutSampleViewModel : BaseViewModel<Person>
  {

    public ObservableCollection<Person> Persons { get; } = [];
    public BindableLayoutSampleViewModel(IDataStore<Person> dataStore) : base(dataStore)
    {
      Title = "Ein einfaches Beispiel mit Bindable Layouts";
    }

    public override async Task Initialize()
    {
      IsBusy = true;
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
      await base.Initialize();
    }
  }
}