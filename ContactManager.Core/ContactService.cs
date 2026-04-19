namespace ContactManager.Core;

public class ContactService(InMemoryContactRepository repository)
{
    private readonly InMemoryContactRepository repository = repository;
    public void AddContact(string naam) => repository.Add(new Contact(naam));
}