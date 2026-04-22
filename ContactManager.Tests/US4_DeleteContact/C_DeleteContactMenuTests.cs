namespace ContactManager.Tests.US4_DeleteContact;

public class C_DeleteContactMenuTests : BaseMenuTests
{
    [Fact]
    public void Menu_Initial_Shows_Update_Contact_Option()
    {
        RunMenuAndQuit();
        Assert.Contains(" 4. Contact Verwijderen", console.Output);
    }

    [Fact]
    public void Menu_DeleteContact_HappyPath()
    {
        AddContact("Elvis", "Presley");
        PickOption('4');
        EnterText("2");
        RunMenuAndQuit();
        Assert.Contains("Contact '2' verwijdert.", console.Output);
        Assert.Single(repository.GetAll());
    }
}