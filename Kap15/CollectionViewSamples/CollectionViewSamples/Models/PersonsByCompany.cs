using System.Collections.Generic;

namespace CollectionViewSamples.Models
{
    public class PersonsByCompany: List<Person>
    {
        public string CompanyName { get; private set; }

        public PersonsByCompany(string companyName, List<Person> items): base(items)
        {
            CompanyName = companyName;
        }
    }
}
