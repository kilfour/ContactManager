using ContactManager.Core.UILayer.Bolts;
using Spectre.Console;

namespace ContactManager.CLI;

public class SpectrePrinter : IPrinter
{
    private readonly IAnsiConsole console = AnsiConsole.Console;

    public void WriteMessage(string text)
    {
        console.MarkupLine($"[blue]{text}[/]");
    }

    public void WriteIf(bool condition, string ifTrue, string ifFalse)
    {
        if (condition)
        {
            console.MarkupLine($"[green]{ifTrue}[/]");
            return;
        }

        console.MarkupLine($"[red]{ifFalse}[/]");
    }

    public void WriteSection(string title, List<string> content)
    {
        var body = new Rows([.. content.Select(a => new Markup(a))]);

        var panel =
            new Panel(body)
                .Header($"[yellow]{title}[/]")
                .Border(BoxBorder.Rounded)
                .Padding(1, 0);

        console.Write(panel);
        console.WriteLine();
    }
}