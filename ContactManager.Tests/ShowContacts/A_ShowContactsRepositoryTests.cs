using ContactManager.Core;

namespace ContactManager.Tests.ShowContacts;

public class ShowContactsRepositoryTests : BaseRepositoryTests
{
    [Fact]
    public void GetAll_Works() // already tested, but hey
    {
        repository.Add(new Contact("Elvis"));
        repository.Add(new Contact("Presley"));
        Assert.Equal(2, repository.GetAll().Count);
    }
}
