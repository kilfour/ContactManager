using ContactManager.Core;

namespace ContactManager.Tests.AddContact;

public class AddContactServiceTests
{
    private readonly InMemoryContactRepository repository = new();
    private readonly ContactService service;

    public AddContactServiceTests()
    {
        service = new(repository);
    }

    [Fact]
    public void AddContact_ContactAdded()
    {
        service.AddContact("Elvis");
        var contact = repository.GetAll()[0];
        Assert.Equal(1, contact.Id);
        Assert.Contains("Elvis", contact.Name);
    }
}