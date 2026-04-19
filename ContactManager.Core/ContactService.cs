namespace ContactManager.Core;

public class ContactService(InMemoryContactRepository repository)
{
    public void AddContact(string naam) => repository.Add(new Contact(naam));
}