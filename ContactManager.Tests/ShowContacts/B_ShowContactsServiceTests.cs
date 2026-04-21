using ContactManager.Core;

namespace ContactManager.Tests.ShowContacts;

public class ShowContactsServiceTests : BaseServiceTests
{
    [Fact]
    public void ShowContacts_ReturnsStringRepresentatioOfContacts()
    {
        repository.Add(new Contact("Elvis"));
        repository.Add(new Contact("Presley"));
        var result = service.GetContactsOverview();
        Assert.Equal(2, result.Count);
        Assert.Equal("1. Elvis", result[0]);
        Assert.Equal("2. Presley", result[1]);
    }
}