using ContactManager.Core.UILayer.Bolts;
using Spectre.Console;

namespace ContactManager.CLI;

public class SpectrePrinter : IPrinter
{
    private readonly IAnsiConsole console = AnsiConsole.Console;

    public void WriteMessage(string text)
    {
        console.MarkupLine(Escape(text));
    }

    public void WriteIf(bool condition, string ifTrue, string ifFalse)
    {
        if (condition)
        {
            console.MarkupLine($"[green]{Escape(ifTrue)}[/]");
            return;
        }

        console.MarkupLine($"[red]{Escape(ifFalse)}[/]");
    }

    public void WriteSection(string title, List<string> content)
    {
        var body = new Rows(content.Select(a => new Markup(Escape(a))).ToArray());

        var panel =
            new Panel(body)
                .Header($"[yellow]{Escape(title)}[/]")
                .Border(BoxBorder.Rounded)
                .Padding(1, 0);

        console.Write(panel);
        console.WriteLine();
    }

    private static string Escape(string text) =>
        text.Replace("[", "[[").Replace("]", "]]");
}