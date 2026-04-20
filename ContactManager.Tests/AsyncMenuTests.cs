using ContactManager.Core;
using ContactManager.Tests.Tools;

namespace ContactManager.Tests;

public class AsyncMenuTests
{
    [Fact]
    public async Task Run_Adds_two_contacts_and_exits()
    {
        var console = new AsyncTestConsole();
        var repository = new InMemoryContactRepository();
        var service = new ContactService(repository);
        var menu = new Menu(console, service);

        var runTask = Task.Run(() => menu.Run());

        await console.PushAsync("1");
        await console.PushAsync("Mark");
        await console.PushAsync("1");
        await console.PushAsync("John");
        await console.PushAsync("q");


        var completed = await WaitAsync(runTask, TimeSpan.FromSeconds(2));
        Assert.True(completed, "Menu.Run() did not complete in time.");
        var exitCode = await runTask;
        Assert.Equal(0, exitCode);

        var contacts = repository.GetAll();
        Assert.Equal(2, contacts.Count);
        Assert.Contains(contacts, c => c.Name == "Mark");
        Assert.Contains(contacts, c => c.Name == "John");

        Assert.Contains(console.Output, x => x.Contains("Contact toegevoegd: Mark"));
        Assert.Contains(console.Output, x => x.Contains("Contact toegevoegd: John"));
    }

    [Fact]
    public async Task Run_Invalid_option_shows_message_and_keeps_running_until_quit()
    {
        // Arrange
        var console = new AsyncTestConsole();
        var repository = new InMemoryContactRepository();
        var service = new ContactService(repository);
        var menu = new Menu(console, service);

        // Act
        var runTask = Task.Run(() => menu.Run());

        await console.PushAsync("pizza");
        await console.PushAsync("q");

        var completed = await WaitAsync(runTask, TimeSpan.FromSeconds(2));

        // Assert
        Assert.True(completed, "Menu.Run() did not complete in time.");
        Assert.Equal(0, runTask.Result);
        Assert.Empty(repository.GetAll());

        Assert.Contains(console.Output, x => x.Contains("Ongeldige optie."));
    }

    private static async Task<bool> WaitAsync(Task task, TimeSpan timeout)
    {
        var finished = await Task.WhenAny(task, Task.Delay(timeout));
        return finished == task;
    }

    private static async Task<bool> WaitAsync<T>(Task<T> task, TimeSpan timeout)
    {
        var finished = await Task.WhenAny(task, Task.Delay(timeout));
        return finished == task;
    }
}

