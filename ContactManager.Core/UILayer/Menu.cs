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
        printer.WriteIf(service.UpdateContact(id, name),
            $"Contact '{id}' bijgewerkt.",
            $"Contact '{id}' niet gevonden.");
    }

    private void HandleShowContacts()
    {
        var contactsOverview = service.GetContactsOverview();
        var content = contactsOverview.Count == 0 ? ["Geen contacten gevonden."] : contactsOverview;
        printer.WriteSection("Contacten", content);
    }
}
