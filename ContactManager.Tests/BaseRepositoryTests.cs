using ContactManager.Core;

namespace ContactManager.Tests;

public class BaseRepositoryTests
{
    protected readonly InMemoryContactRepository repository = new();
}
