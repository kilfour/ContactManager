using ContactManager.CLI;
using ContactManager.Core.DataLayer;
using ContactManager.Core.ServiceLayer;
using ContactManager.Core.UILayer;
using ContactManager.Core.UILayer.Bolts;

var console = new SystemConsole();
return
    new Menu(
        new ContactService(new InMemoryContactRepository()),
        new Prompter(console),
        new Printer(console))
    .Run();
