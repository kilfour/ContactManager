using ContactManager.Core;

namespace ContactManager.Tests.AddContact;

public class AddContactMenuTests
{
    private readonly InMemoryContactRepository repository = new();
    private readonly ContactService service;
    private readonly FakeConsole console = new();
    private readonly Menu menu;

    public AddContactMenuTests()
    {
        service = new ContactService(repository);
        menu = new Menu(console, service);
    }

    [Fact]
    public void Menu_AddContact_Flow()
    {
        console.Input.Enqueue("1");     // pick option
        console.Input.Enqueue("Elvis"); // input name
        console.Input.Enqueue("q");     // exit the loop.
        menu.Run();
        List<string> expected =
            // Initieel menu
            [ "1. Contact Toevoegen"
            , "q. Exit"
            , "Maak uw keuze:"
            // Na keuze '1'
            , "Voer een naam in: "  
            // Na toevoegen          
            , "Contact toegevoegd: Elvis"     
            // Turtles all the way down
            , "1. Contact Toevoegen"
            , "q. Exit"
            , "Maak uw keuze:"
            ];
        Assert.Equal(expected, console.Output);
        var contact = repository.GetAll()[0];
        Assert.Equal(1, contact.Id);
        Assert.Contains("Elvis", contact.Name);
    }
}