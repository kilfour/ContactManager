namespace ContactManager.Core;

public interface IConsole
{
    public void WriteLine(string message);
    public void Write(string message);
    public string ReadLine();
}