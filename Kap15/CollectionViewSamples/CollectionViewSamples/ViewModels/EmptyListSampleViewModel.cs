using CollectionViewSamples.Models;
using System.Collections.ObjectModel;
using CollectionViewSamples.Services;

namespace CollectionViewSamples.ViewModels;

public class EmptyListSampleViewModel : BaseViewModel<Person>
{

  public ObservableCollection<Person> Persons { get; } = [];
  public EmptyListSampleViewModel(IDataStore<Person> dataStore) : base(dataStore)
  {
    Title = "Eine leere Liste";
  }
}