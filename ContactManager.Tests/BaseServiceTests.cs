using ContactManager.Core;

namespace ContactManager.Tests;

public class BaseServiceTests
{
    protected readonly InMemoryContactRepository repository = new();
    protected readonly ContactService service;

    public BaseServiceTests()
    {
        service = new(repository);
    }
}