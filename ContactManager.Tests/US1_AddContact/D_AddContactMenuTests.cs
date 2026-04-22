namespace ContactManager.Tests.US1_AddContact;

public class D_AddContactMenuTests : BaseMenuTests
{
    [Fact]
    public void Menu_Initial_Shows_Add_Contact_Option()
    {
        RunMenuAndQuit();
        Assert.Contains(" 1. Contact Toevoegen", console.Output);
    }

    [Fact]
    public void Menu_Asks_For_Name()
    {
        PickOption('1');
        EnterText("Elvis");
        RunMenuAndQuit();
        Assert.Contains("Voer een naam in: ", console.Output);
    }

    [Fact]
    public void Menu_Shows_Confirmation_After_Add()
    {
        PickOption('1');
        EnterText("Elvis");
        RunMenuAndQuit();
        Assert.Contains("Contact toegevoegd: Elvis", console.Output);
    }

    [Fact]
    public void Repository_Contains_Contact_After_Add()
    {
        PickOption('1');
        EnterText("Elvis");
        RunMenuAndQuit();
        var contact = GetFirstContact();
        Assert.Equal(1, contact.Id);
        Assert.Contains("Elvis", contact.Name);
    }
}