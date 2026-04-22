using ContactManager.Core.DataLayer;
using ContactManager.Core.ServiceLayer;

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