using ContactManager.Core.Domain;

namespace ContactManager.Tests.US5_SearchContact;

public class B_SearchContactServiceTests : BaseServiceTests
{
    [Fact]
    public void SearchContact_Finds_The_Right_Ones()
    {
        repository.Add(new Contact("Elvis"));
        repository.Add(new Contact("Elmo"));
        repository.Add(new Contact("Different"));

        var result = service.Search("el");
        Assert.Equal(2, result.Count);
        Assert.Equal(1, result[0].Id);
        Assert.Equal("Elvis", result[0].Name);
        Assert.Equal(2, result[1].Id);
        Assert.Equal("Elmo", result[1].Name);
    }
}