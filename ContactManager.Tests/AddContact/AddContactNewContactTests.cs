using ContactManager.Core;

namespace ContactManager.Tests.AddContact;

public class AddContactNewContactTests
{
    [Fact]
    public void AddContact_Ctor()
    {
        var contact = new Contact("Elvis");
        Assert.Equal(0, contact.Id);
        Assert.Equal("Elvis", contact.Name);
    }
}