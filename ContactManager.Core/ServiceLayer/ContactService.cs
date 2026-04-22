using ContactManager.Core.DataLayer;
using ContactManager.Core.Domain;

namespace ContactManager.Core.ServiceLayer;

public class ContactService(InMemoryContactRepository repository)
{
    public void AddContact(string naam) => repository.Add(new Contact(naam));

    public List<string> GetContactsOverview()
    {
        var result = new List<string>();
        foreach (var contact in repository.GetAll())
        {
            result.Add($"{contact.Id}. {contact.Name}");
        }
        return result;
    }

    public bool UpdateContact(int id, string name)
    {
        var contact = repository.GetById(id);
        if (contact == null) return false;
        contact.Name = name;
        return true;
    }
}