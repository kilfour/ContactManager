using ContactManager.Core.Domain;

namespace ContactManager.Tests.US5_SearchContact;

public class A_SearchContactRepositoryTests : BaseRepositoryTests
{
    [Fact]
    public void Search_Works()
    {
        repository.Add(new Contact("Elvis"));
        repository.Add(new Contact("Elmo"));
        repository.Add(new Contact("Different"));

        var contacts = repository.Search("el");

        Assert.Equal(2, contacts.Count);
        Assert.Equal("Elvis", contacts[0].Name);
        Assert.Equal("Elmo", contacts[1].Name);
    }
}
