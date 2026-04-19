namespace ContactManager.Core;

public class InMemoryContactRepository
{
    private readonly List<Contact> contacts = [];
    private int lastId = 0;
    public void Add(Contact contact)
    {
        contact.SetId(++lastId);
        contacts.Add(contact);
    }
    public IReadOnlyList<Contact> GetAll() => contacts;
}