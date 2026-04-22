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
            var choice = prompter.AskForText("Maak uw keuze: ");
            running = HandleMenuChoice(choice);
        }
        return 0;
    }

    private void ShowMenu()
    {
        printer.WriteSection("Menu",
            [ "1. Contact Toevoegen"
            , "2. Contacten Tonen"
            , "3. Contact Bewerken"
            , "q. Exit"
            ]);
    }

    private bool HandleMenuChoice(string choice)
    {
        switch (choice)
        {
            case "1": HandleAddContact(); break;
            case "2": HandleShowContacts(); break;
            case "3": HandleUpdateContact(); break;
            case "q": return false;
            default: printer.Write("Ongeldige optie."); break;
        }
        return true;
    }

    private void HandleAddContact()
    {
        var name = prompter.AskForTextOnNewLine("Voer een naam in: ");
        service.AddContact(name);
        printer.Write($"Contact toegevoegd: {name}");
    }

    private void HandleUpdateContact()
    {
        if (prompter.AskForNumber("Voer een id in: ", out var id, "Ongeldige id."))
            UpdateContactById(id);
    }

    private void UpdateContactById(int id)
    {
        var name = prompter.AskForTextOnNewLine("Voer een naam in: ");
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
