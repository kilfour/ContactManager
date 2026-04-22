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
        Assert.Equal("1. Elvis", result[0]);
        Assert.Equal("2. Elmo", result[1]);
    }
}