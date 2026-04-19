using ContactManager.Core;

namespace ContactManager.Tests;

public class MenuTests
{
    private ContactService service = new(new InMemoryContactRepository());
    private FakeConsole console = new();

    private Menu menu;

    public MenuTests()
    {
        menu = new Menu(console, service);
    }

    [Fact]
    public void Menu_Shows_Quit_Option_And_Q_Exits()
    {
        console.Input.Enqueue("q");
        Assert.Equal(0, menu.Run());
        Assert.Contains("q. Exit", console.Output);
    }

    [Fact]
    public void Menu_Invalid_Option()
    {
        console.Input.Enqueue("z");
        console.Input.Enqueue("q");
        Assert.Equal(0, menu.Run());
        Assert.Contains("Ongeldige optie.", console.Output);
    }
}

