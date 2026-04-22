# Refactoring the Menu (after US3)
> For which you need 100% test coverage.

## Before
```csharp
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
        console.WriteLine("Voer een naam in: ");
        var name = console.ReadLine();
        service.AddContact(name);
        console.WriteLine($"Contact toegevoegd: {name}");
    }

    private void HandleUpdateContact()
    {
        console.WriteLine("Voer een id in: ");
        if (int.TryParse(console.ReadLine(), out var id))
        {
            console.WriteLine("Voer een naam in: ");
            var name = console.ReadLine();
            if (service.UpdateContact(id, name))
            {
                console.WriteLine($"Contact '{id}' bijgewerkt.");
            }
            else
            {
                console.WriteLine($"Contact '{id}' niet gevonden.");
            }
        }
        else
        {
            console.WriteLine("Ongeldige id.");
            return;
        }
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
```
## After
```csharp
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
            [ new MenuOption("1","Contact Toevoegen", HandleAddContact)
            , new MenuOption("2","Contacten Tonen",   HandleShowContacts)
            , new MenuOption("3","Contact Bewerken",  HandleUpdateContact)
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

    private bool HandleShowContacts()
    {
        var contactsOverview = service.GetContactsOverview();
        var content = contactsOverview.Count == 0 ? ["Geen contacten gevonden."] : contactsOverview;
        printer.WriteSection("Contacten", content);
        return true;
    }
}
```
## Commit Log
```bash
0288f2c got rid of some conditionals
3c91f73 removed IConsole from Menu
6cf96e9 minor clarifications
a5902b5 build fix
97fea1c use write section for menu
25d36ff printer writeif
045e81a Introducing the printer
a2fa037 moving  the furniture
8063a82 moved some complexity out of menu
b47e308 minor cleanup
```

> [On Github](https://github.com/kilfour/ContactManager/commits/us3/)
