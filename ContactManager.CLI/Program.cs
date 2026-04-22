using ContactManager.CLI;
using ContactManager.Core.DataLayer;
using ContactManager.Core.ServiceLayer;
using ContactManager.Core.UILayer;
using ContactManager.Core.UILayer.Bolts;


var path = Path.Combine(AppContext.BaseDirectory, "../../../../Data/Contacts.txt");
var console = new SystemConsole();
return
    new Menu(
        new ContactService(new FileBasedRepository(path)),
        new Prompter(console),
        new Printer(console))
    .Run();
