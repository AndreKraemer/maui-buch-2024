using CollectionViewSamples.Models;
using System.Collections.ObjectModel;
using CollectionViewSamples.Services;

namespace CollectionViewSamples.ViewModels
{
  public class SelectSampleViewModel : BaseViewModel<Person>
  {
    private Person _selectedItem;
    private string _selectedText = string.Empty;
    private string _tappedText = string.Empty;

    public ObservableCollection<Person> Persons { get; } = [];
    public Command LoadPersonsCommand { get; }
    public Command<Person> PersonTapped { get; }

    public SelectSampleViewModel(IDataStore<Person> dataStore) : base(dataStore)
    {
      Title = "Einträge selektieren";
      LoadPersonsCommand = new Command(async () => await ExecuteLoadPersonsCommand());

      PersonTapped = new Command<Person>(OnItemTapped);
    }

    async Task ExecuteLoadPersonsCommand()
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
    }

    public override async Task Initialize()
    {
      await ExecuteLoadPersonsCommand();
      await base.Initialize();
    }

    public Person SelectedPerson
    {
      get => _selectedItem;
      set
      {
        SetProperty(ref _selectedItem, value);
        OnItemSelected(value);
      }
    }


    public string SelectedText
    {
      get => _selectedText;
      set => SetProperty(ref _selectedText, value);
    }
    public string TappedText
    {
      get => _tappedText;
      set => SetProperty(ref _tappedText, value);
    }

    void OnItemSelected(Person person)
    {
      SelectedText = person == null ? "Keine aktive Auswahl" : $"Aktive Auswahl ist: {person.Name}";
    }

    void OnItemTapped(Person person)
    {
      TappedText = person == null ? "Es wurde nichts geklickt" : $"Eintrag: {person.Name} wurde geklickt";
    }
  }
}