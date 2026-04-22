using ContactManager.Core.Domain;

namespace ContactManager.Tests.US1_AddContact;

public class A_AddContactNewContactTests
{
    [Fact]
    public void AddContact_Ctor()
    {
        var contact = new Contact("Elvis");
        Assert.Equal(0, contact.Id);
        Assert.Equal("Elvis", contact.Name);
    }
}