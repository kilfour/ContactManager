using ContactManager.Core.DataLayer;
using ContactManager.Core.Domain;
using ContactManager.Core.ServiceLayer;
using ContactManager.Core.UILayer;
using ContactManager.Tests.Tools;

namespace ContactManager.Tests;

public abstract class BaseMenuTests
{
    protected readonly FakeConsole console = new();
    protected readonly InMemoryContactRepository repository = new();
    protected readonly ContactService service;
    protected readonly Menu menu;

    public BaseMenuTests()
    {
        service = new ContactService(repository);
        menu = new Menu(console, service);
    }

    protected void RunMenuAndQuit()
    {
        console.Input.Enqueue("q"); // exit the loop.
        menu.Run();
    }

    protected void PickOption(char input)
    {
        console.Input.Enqueue(input.ToString());
    }

    protected void EnterText(string input)
    {
        console.Input.Enqueue(input);
    }

    protected void AddContact(params string[] contactNames)
    {
        foreach (var contactName in contactNames)
            repository.Add(new Contact(contactName));
    }

    protected Contact GetFirstContact()
        => repository.GetAll()[0];

    protected Contact GetSecondContact()
        => repository.GetAll()[1];
}