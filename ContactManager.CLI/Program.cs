using ContactManager.CLI;
using ContactManager.Core.DataLayer;
using ContactManager.Core.ServiceLayer;
using ContactManager.Core.UILayer;

return
    new Menu(
        new SystemConsole(),
        new ContactService(new InMemoryContactRepository()))
    .Run();
