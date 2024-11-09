using CollectionViewSamples.Models;

namespace CollectionViewSamples.Services
{
  public class MockDataStore : IDataStore<Person>
  {
    readonly List<Person> _persons;

    public MockDataStore()
    {

      _persons = [
        new Person { Id = Guid.NewGuid().ToString(), Name = "John Smith", CompanyName = "ABC Technologies", JobTitle = "Software Engineer" },
        new Person { Id = Guid.NewGuid().ToString(), Name = "Emily Johnson", CompanyName = "XYZ Solutions", JobTitle = "Software Developer" },
        new Person { Id = Guid.NewGuid().ToString(), Name = "Michael Williams", CompanyName = "PQR Innovations", JobTitle = "Senior Software Engineer" },
        new Person { Id = Guid.NewGuid().ToString(), Name = "Jessica Brown", CompanyName = "ABC Technologies", JobTitle = "Software Developer" },
        new Person { Id = Guid.NewGuid().ToString(), Name = "Daniel Taylor", CompanyName = "XYZ Solutions", JobTitle = "Software Engineer" },
        new Person { Id = Guid.NewGuid().ToString(), Name = "Olivia Davis", CompanyName = "PQR Innovations", JobTitle = "Junior Software Developer" },
        new Person { Id = Guid.NewGuid().ToString(), Name = "Matthew Wilson", CompanyName = "ABC Technologies", JobTitle = "Software Engineer" },
        new Person { Id = Guid.NewGuid().ToString(), Name = "Sophia Martinez", CompanyName = "XYZ Solutions", JobTitle = "Software Developer" },
        new Person { Id = Guid.NewGuid().ToString(), Name = "Andrew Anderson", CompanyName = "PQR Innovations", JobTitle = "Senior Software Engineer" },
        new Person { Id = Guid.NewGuid().ToString(), Name = "Emma Thomas", CompanyName = "ABC Technologies", JobTitle = "Software Developer" }
      ];
    }

    public async Task<bool> AddItemAsync(Person item)
    {
      _persons.Add(item);

      return await Task.FromResult(true);
    }

    public async Task<bool> UpdateItemAsync(Person person)
    {
      var existingPerson = _persons.FirstOrDefault(p => p.Id == person.Id);
      _persons.Remove(existingPerson);
      _persons.Add(person);

      return await Task.FromResult(true);
    }

    public async Task<bool> DeleteItemAsync(string id)
    {
      var existingPerson = _persons.FirstOrDefault(p => p.Id == id);
      _persons.Remove(existingPerson);
      return await Task.FromResult(true);
    }

    public async Task<Person> GetItemAsync(string id)
    {
      return await Task.FromResult(_persons.FirstOrDefault(p => p.Id == id));
    }

    public async Task<IEnumerable<Person>> GetItemsAsync(bool forceRefresh = false)
    {
      return await Task.FromResult(_persons);
    }
  }
}