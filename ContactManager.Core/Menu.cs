namespace ContactManager.Core;

public class Menu(IConsole console, ContactService service)
{
    public int Run()
    {
        var running = true;
        while (running)
        {
            ShowMenu();
            running = HandleChoice(console.ReadLine());
        }
        return 0;
    }

    private void ShowMenu()
    {
        console.WriteLine("1. Contact Toevoegen");
        console.WriteLine("q. Exit");
        console.Write("Maak uw keuze:");
    }

    private bool HandleChoice(string choice)
    {
        switch (choice)
        {
            case "1": HandleAddContact(); break;
            case "q": return false;
            default: console.WriteLine("Ongeldige optie."); break;
        }
        return true;
    }

    private void HandleAddContact()
    {
        console.WriteLine("Voer een naam in: ");
        var name = console.ReadLine();
        service.AddContact(name);
        console.WriteLine($"Contact toegevoegd: {name}");
    }
}
