using CollectionViewSamples.Models;
using System.Collections.ObjectModel;
using CollectionViewSamples.Services;

namespace CollectionViewSamples.ViewModels
{
  public class MenuSampleViewModel : BaseViewModel<Person>
  {
    public ObservableCollection<Person> Persons { get; } = [];

    public Command<Person> DeletePersonCommand { get; }
    public Command<Person> FavoritePersonCommand { get; }
    public Command<Person> ArchivePersonCommand { get; }

    public MenuSampleViewModel(IDataStore<Person> dataStore) : base(dataStore)
    {
      Title = "Kontextmenüs";
      DeletePersonCommand = new Command<Person>(OnPersonDeleted);
      FavoritePersonCommand = new Command<Person>(OnPersonFavorited);
      ArchivePersonCommand = new Command<Person>(OnPersonArchived);
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


    async void OnPersonDeleted(Person person)
    {
      if (person != null)
      {
        if (await DataStore.DeleteItemAsync(person.Id))
        {
          Persons.Remove(person);
        };
      }
    }

    private void OnPersonFavorited(Person person)
    {
      Application.Current.MainPage.DisplayAlert("Hinweis", $"Person {person.Name} favorisiert", "OK");
    }

    private void OnPersonArchived(Person person)
    {
      Application.Current.MainPage.DisplayAlert("Hinweis", $"Person {person.Name} archiviert", "OK");
    }

  }
}