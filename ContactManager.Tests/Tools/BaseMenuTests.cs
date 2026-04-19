using ContactManager.Core;

namespace ContactManager.Tests.Tools;

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
}