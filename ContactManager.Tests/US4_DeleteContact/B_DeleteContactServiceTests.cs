using ContactManager.Core.Domain;

namespace ContactManager.Tests.US4_DeleteContact;

public class B_DeleteContactServiceTests : BaseServiceTests
{
    [Fact]
    public void DeleteContact_Erm_Deletes_The_Contact()
    {
        var contact = new Contact("Elviz");
        repository.Add(contact);
        service.DeleteContact(1);
        Assert.Empty(repository.GetAll());
    }

    [Fact]
    public void DeleteContact_Returns_True_If_Deleted()
    {
        var contact = new Contact("Elvis");
        repository.Add(contact);
        Assert.True(service.DeleteContact(1));
    }

    [Fact]
    public void DeleteContact_Returns_False_If_Not_Found()
    {
        Assert.False(service.DeleteContact(1));
    }
}