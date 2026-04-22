using ContactManager.Core.ServiceLayer;
using ContactManager.Core.UILayer.Bolts;

namespace ContactManager.Core.UILayer;

public class Menu
{
    private readonly MenuOption[] menuOptions;
    private readonly ContactService service;
    private readonly Prompter prompter;
    private readonly Printer printer;

    public Menu(ContactService service, Prompter prompter, Printer printer)
    {
        this.service = service;
        this.prompter = prompter;
        this.printer = printer;
        menuOptions =
            [ new MenuOption("1","Contact Toevoegen",   HandleAddContact)
            , new MenuOption("2","Contacten Tonen",     HandleShowContacts)
            , new MenuOption("3","Contact Bewerken",    HandleUpdateContact)
            , new MenuOption("4","Contact Verwijderen", HandleDeleteContact)
            , new MenuOption("5","Contact Zoeken",      HandleSearchContact)
            , new MenuOption("q","Exit", () => false)
            ];
    }

    public int Run()
    {
        var running = true;
        while (running)
        {
            ShowMenu();
            running = HandleMenuChoice();
        }
        return 0;
    }

    private void ShowMenu() =>
        printer.WriteSection("Menu", [.. menuOptions.Select(a => $"{a.Key}. {a.Label}")]);

    private bool HandleMenuChoice()
    {
        var choice = prompter.AskForText("Maak uw keuze: ");
        var option = menuOptions.FirstOrDefault(a => a.Key == choice);
        if (option is null)
        {
            printer.WriteMessage("Ongeldige optie.");
            return true;
        }
        return option.Handler();
    }

    private bool HandleAddContact()
    {
        var name = prompter.AskForTextOnNewLine("Voer een naam in: ");
        service.AddContact(name);
        printer.WriteMessage($"Contact toegevoegd: {name}");
        return true;
    }

    private bool HandleShowContacts()
    {
        var contactsOverview = service.GetContactsOverview();
        var content = contactsOverview.Count == 0 ? ["Geen contacten gevonden."] : contactsOverview;
        printer.WriteSection("Contacten", content);
        return true;
    }

    private bool HandleUpdateContact()
    {
        if (prompter.AskForNumber("Voer een id in: ", out var id, "Ongeldige id."))
            UpdateContactById(id);
        return true;
    }

    private void UpdateContactById(int id)
    {
        var name = prompter.AskForTextOnNewLine("Voer een naam in: ");
        printer.WriteIf(service.UpdateContact(id, name),
            $"Contact '{id}' bijgewerkt.",
            $"Contact '{id}' niet gevonden.");
    }

    private bool HandleDeleteContact()
    {
        if (prompter.AskForNumber("Voer een id in: ", out var id, "Ongeldige id."))
            printer.WriteIf(service.DeleteContact(id),
                $"Contact '{id}' verwijdert.",
                $"Contact '{id}' niet gevonden.");
        return true;
    }

    private bool HandleSearchContact()
    {
        var search = prompter.AskForTextOnNewLine("Voer een zoekterm in: ");
        var contactsOverview = service.Search(search);
        var content = contactsOverview.Count == 0 ? ["Geen contacten gevonden."] : contactsOverview;
        printer.WriteSection("Contacten", content);
        return true;
    }
}
