using ContactManager.Core.UILayer;

namespace ContactManager.Tests.Tools;

public class FakeConsole : IConsole
{
    public List<string> Output = new();
    public Queue<string> Input = new();

    public void WriteLine(string message) => Output.Add(message);
    public void Write(string message) => Output.Add(message);
    public string ReadLine() => Input.Dequeue();
}
