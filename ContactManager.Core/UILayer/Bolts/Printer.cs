namespace ContactManager.Core.UILayer.Bolts;

public class Printer(IConsole console)
{
    const int Width = 30;

    public void Section(string title, List<string> content)
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
