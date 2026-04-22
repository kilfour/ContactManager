using QuickPulse.Show;

namespace ContactManager.Tests.US5_SearchContact;

public class C_SearchContactMenuTests : BaseMenuTests
{
    [Fact]
    public void Menu_Initial_Shows_Update_Contact_Option()
    {
        RunMenuAndQuit();
        Assert.Contains(" 5. Contact Zoeken", console.Output);
    }

    [Fact]
    public void Menu_SearchContact_HappyPath()
    {
        AddContact("Elvis", "Presley", "Elmo");
        PickOption('5');
        EnterText("el");
        RunMenuAndQuit();
        Assert.Contains(" 1. Elvis", console.Output);
        Assert.Contains(" 3. Elmo", console.Output);
    }
}