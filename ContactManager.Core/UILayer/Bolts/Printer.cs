namespace ContactManager.Core.UILayer.Bolts;

public class Printer(IConsole console) : IPrinter
{
    public void WriteMessage(string text)
    {
        console.WriteLine(text);
    }

    public void WriteIf(bool condition, string ifTrue, string ifFalse)
    {
        if (condition)
        {
            console.WriteLine(ifTrue);
            return;
        }
        console.WriteLine(ifFalse);
    }

    const int Width = 30;

    public void WriteSection(string title, List<string> content)
    {
        DrawTitle(title);
        foreach (var text in content)
        {
            console.WriteLine(" " + text);
        }
        DrawLine();
    }

    private void DrawTitle(string title)
    {
        string label = $" {title} ";
        var middle = label.Length;
        var left = (Width - middle) / 2;
        var right = Width - left - middle;
        console.WriteLine($"{Line(left)}{label}{Line(right)}");
    }

    private void DrawLine() => console.WriteLine(Line());
    private static string Line(int length = Width) => new('-', length);
}
