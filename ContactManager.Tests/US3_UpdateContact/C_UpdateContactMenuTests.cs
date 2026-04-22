namespace ContactManager.Tests.US3_UpdateContact;

public class C_UpdateContactMenuTests : BaseMenuTests
{
    [Fact]
    public void Menu_Initial_Shows_Update_Contact_Option()
    {
        RunMenuAndQuit();
        Assert.Contains(" 3. Contact Bewerken", console.Output);
    }

    [Fact]
    public void Menu_UpdateContact_HappyPath()
    {
        AddContact("Elvis", "Presley");
        PickOption('3');
        EnterText("2");
        EnterText("Lisa Marie");
        RunMenuAndQuit();
        Assert.Contains("Contact '2' bijgewerkt.", console.Output);
        Assert.Equal("Lisa Marie", repository.GetById(2)!.Name);
    }
}