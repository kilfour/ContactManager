namespace ContactManager.Tests.ShowContacts;

public class C_ShowContactsMenuTests : BaseMenuTests
{
    [Fact]
    public void Menu_Initial_Shows_Add_Contact_Option()
    {
        RunMenuAndQuit();
        Assert.Contains("2. Contacten Tonen", console.Output);
    }

    [Fact]
    public void Menu_Shows_Message_If_No_Contacts()
    {
        PickOption('2');
        RunMenuAndQuit();
        var index = console.Output.IndexOf("Geen contacten gevonden.");
        Assert.True(index > 0);
        string[] expected =
            [ "--------- Contacten ----------"
            , "Geen contacten gevonden."
            , "------------------------------"];
        Assert.Equal(expected, console.Output.Skip(index - 1).Take(3).ToArray());
    }

    [Fact]
    public void Menu_Check_Box_Format()
    {
        PickOption('2');
        RunMenuAndQuit();

        Assert.Contains("Geen contacten gevonden.", console.Output);
    }

    [Fact]
    public void Menu_Shows_Contacts_After_Selection()
    {
        AddContact("Elvis", "Presley");
        PickOption('2');
        RunMenuAndQuit();
        Assert.Contains("1. Elvis", console.Output);
        Assert.Contains("2. Presley", console.Output);
    }
}