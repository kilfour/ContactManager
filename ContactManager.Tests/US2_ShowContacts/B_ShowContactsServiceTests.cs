using ContactManager.Core.Domain;

namespace ContactManager.Tests.US2_ShowContacts;

public class B_ShowContactsServiceTests : BaseServiceTests
{
    [Fact]
    public void ShowContacts_ReturnsStringRepresentatioOfContacts()
    {
        repository.Add(new Contact("Elvis"));
        repository.Add(new Contact("Presley"));
        var result = service.GetAll();
        Assert.Equal(2, result.Count);
        Assert.Equal(1, result[0].Id);
        Assert.Equal("Elvis", result[0].Name);
        Assert.Equal(2, result[1].Id);
        Assert.Equal("Presley", result[1].Name);
    }
}