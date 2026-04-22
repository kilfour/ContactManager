namespace ContactManager.Core.UILayer.Bolts;

public class Prompter(IConsole console)
{
    public string AskForTextOnNewLine(string question)
    {
        console.WriteLine(question);
        return console.ReadLine();
    }

    public string AskForText(string question)
    {
        console.Write(question);
        return console.ReadLine();
    }

    public bool AskForNumber(string question, out int number, string errorMessage)
    {
        console.WriteLine(question);
        if (int.TryParse(console.ReadLine(), out var result))
        {
            number = result;
            return true;
        }
        number = 0;
        console.WriteLine(errorMessage);
        return false;
    }


}
