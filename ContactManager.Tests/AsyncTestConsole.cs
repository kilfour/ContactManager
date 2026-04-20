using System.Collections.Concurrent;
using ContactManager.Core;

namespace ContactManager.Tests;

public class AsyncTestConsole : IConsole
{
    private readonly BlockingCollection<string> input = [];
    private readonly ConcurrentQueue<string> output = new();

    public IReadOnlyCollection<string> Output => output.ToArray();

    public Task PushAsync(string value)
    {
        input.Add(value);
        return Task.CompletedTask;
    }

    public void WriteLine(string message) => output.Enqueue(message);

    public void Write(string message) => output.Enqueue(message);

    public string ReadLine() => input.Take();
}