using ContactManager.Core;

namespace ContactManager.Tests.B_AddContact;

public class B_AddContactRepositoryTests : BaseRepositoryTests
{
    [Fact]
    public void AddContact_ContactAdded()
    {
        var contact = new Contact("Elvis");
        repository.Add(contact);
        Assert.Contains(contact, repository.GetAll());
    }

    [Fact]
    public void AddContact_AssignsId()
    {
        var contact1 = new Contact("Elvis");
        var contact2 = new Contact("Presley");

        repository.Add(contact1);
        repository.Add(contact2);

        Assert.Equal(1, contact1.Id);
        Assert.Equal(2, contact2.Id);
    }
}