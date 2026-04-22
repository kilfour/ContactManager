using ContactManager.Core.Domain;

namespace ContactManager.Core.DataLayer;

public class InMemoryContactRepository
{
    private readonly List<Contact> contacts = [];
    private int lastId = 0;
    public void Add(Contact contact)
    {
        contact.Id = ++lastId;
        contacts.Add(contact);
    }
    public IReadOnlyList<Contact> GetAll() => contacts;

    public Contact? GetById(int id)
        => contacts.FirstOrDefault(c => c.Id == id);
}