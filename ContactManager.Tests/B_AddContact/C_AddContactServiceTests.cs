namespace ContactManager.Tests.B_AddContact;

public class C_AddContactServiceTests : BaseServiceTests
{
    [Fact]
    public void AddContact_ContactAdded()
    {
        service.AddContact("Elvis");
        var contact = repository.GetAll()[0];
        Assert.Equal(1, contact.Id);
        Assert.Contains("Elvis", contact.Name);
    }
}