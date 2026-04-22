using ContactManager.Core.DataLayer;

namespace ContactManager.Tests;

public class BaseRepositoryTests
{
    protected readonly InMemoryContactRepository repository = new();
}
