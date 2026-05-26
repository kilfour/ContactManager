using ContactManager.Core.DataLayer;
using ContactManager.Core.Domain;

namespace ContactManager.Core.ServiceLayer;

public interface IContactService
{
    CreateContactResponse AddContact(CreateContactRequest createContactRequest);
    // bool DeleteContact(int id);
    // List<string> GetContactsOverview();
    // List<string> Search(string search);
    // bool UpdateContact(int id, string name);
}

public class ContactService(IContactRepository repository) : IContactService
{
    public CreateContactResponse AddContact(CreateContactRequest createContactRequest)
    {
        var contact = new Contact(createContactRequest.Name);
        repository.Add(contact);
        return new CreateContactResponse { Id = contact.Id, Name = contact.Name };
    }

    public List<string> GetContactsOverview()
    {
        var result = new List<string>();
        foreach (var contact in repository.GetAll())
        {
            result.Add(FormatContact(contact));
        }
        return result;
    }

    public bool UpdateContact(int id, string name)
    {
        var contact = repository.GetById(id);
        if (contact == null) return false;
        contact.Name = name;
        repository.Commit();
        return true;
    }

    public bool DeleteContact(int id)
        => repository.Delete(id);

    public List<string> Search(string search)
    {
        var result = new List<string>();
        foreach (var contact in repository.Search(search))
        {
            result.Add(FormatContact(contact));
        }
        return result;
    }

    private static string FormatContact(Contact contact)
    {
        return $"{contact.Id}. {contact.Name}";
    }
}