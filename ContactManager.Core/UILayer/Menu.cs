using ContactManager.Core.ServiceLayer;
using ContactManager.Core.UILayer.Bolts;

namespace ContactManager.Core.UILayer;

public class Menu(IConsole console, ContactService service)
{
    private readonly Prompter prompter = new(console);
    private readonly Printer printer = new(console);

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
        console.WriteLine("2. Contacten Tonen");
        console.WriteLine("3. Contact Bewerken");
        console.WriteLine("q. Exit");
        console.Write("Maak uw keuze:");
    }

    private bool HandleChoice(string choice)
    {
        switch (choice)
        {
            case "1": HandleAddContact(); break;
            case "2": HandleShowContacts(); break;
            case "3": HandleUpdateContact(); break;
            case "q": return false;
            default: console.WriteLine("Ongeldige optie."); break;
        }
        return true;
    }

    private void HandleAddContact()
    {
        var name = prompter.AskForText("Voer een naam in: ");
        service.AddContact(name);
        console.WriteLine($"Contact toegevoegd: {name}");
    }

    private void HandleUpdateContact()
    {
        if (prompter.AskForNumber("Voer een id in: ", out var id, "Ongeldige id."))
            UpdateThisContact(id);
    }

    private void UpdateThisContact(int id)
    {
        var name = prompter.AskForText("Voer een naam in: ");
        if (service.UpdateContact(id, name))
        {
            console.WriteLine($"Contact '{id}' bijgewerkt.");
            return;
        }
        console.WriteLine($"Contact '{id}' niet gevonden.");
    }

    private void HandleShowContacts()
    {
        var contacts = service.GetContactsOverview();
        DrawTitle("Contacten");
        if (contacts.Count == 0)
            console.WriteLine("Geen contacten gevonden.");
        foreach (var contact in contacts)
        {
            console.WriteLine(contact);
        }
        DrawLine();
    }

    const int Width = 30;

    private void DrawTitle(string title)
    {
        string label = $" {title} ";
        var middle = label.Length;
        var left = (Width - middle) / 2;
        var right = Width - left - middle;
        console.WriteLine($"{Line(left)}{label}{Line(right)}");
    }

    private void DrawLine() => console.WriteLine(Line());
    private static string Line(int length = Width) => new('-', length);

}
