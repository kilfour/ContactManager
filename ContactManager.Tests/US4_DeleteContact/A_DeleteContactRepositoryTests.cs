using ContactManager.Core.Domain;

namespace ContactManager.Tests.US4_DeleteContact;

public class A_DeleteContactRepositoryTests : BaseRepositoryTests
{
    [Fact]
    public void Delete_Works()
    {
        repository.Add(new Contact("Elvis"));
        var contact = repository.Delete(1);
        Assert.Empty(repository.GetAll());
    }

    [Fact]
    public void Delete_Returns_False_If_Not_Found()
    {
        Assert.False(repository.Delete(2));
    }
}
