using ContactManager.Core.Domain;
using ContactManager.Core.ServiceLayer;

namespace ContactManager.Tests.US3_UpdateContact;

public class B_UpdateContactServiceTests : BaseServiceTests
{
    [Fact]
    public void UpdateContact_Changes_Contact_Name()
    {
        var contact = new Contact("Elviz");
        repository.Add(contact);
        service.UpdateContact(1, new UpdateContactRequest { Name = "Elvis" });
        Assert.Equal("Elvis", contact.Name);
    }

    [Fact]
    public void UpdateContact_Returns_True_If_Updated()
    {
        var contact = new Contact("Elviz");
        repository.Add(contact);
        Assert.True(service.UpdateContact(1, new UpdateContactRequest { Name = "Elvis" }));
    }

    [Fact]
    public void UpdateContact_Returns_False_If_Not_Found()
    {
        Assert.False(service.UpdateContact(1, new UpdateContactRequest { Name = "Elvis" }));
    }
}