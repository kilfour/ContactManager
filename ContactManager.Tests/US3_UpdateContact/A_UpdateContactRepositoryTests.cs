using ContactManager.Core;
using ContactManager.Core.Domain;

namespace ContactManager.Tests.US3_UpdateContact;

public class A_UpdateContactRepositoryTests : BaseRepositoryTests
{
    [Fact]
    public void GetById_Works()
    {
        repository.Add(new Contact("Elvis"));
        repository.Add(new Contact("Presley"));
        var contact = repository.GetById(2);
        Assert.NotNull(contact);
        Assert.Equal("Presley", contact.Name);
    }

    [Fact]
    public void GetById_Returns_Null_If_Not_Found()
    {
        var contact = repository.GetById(2);
        Assert.Null(contact);
    }
}
