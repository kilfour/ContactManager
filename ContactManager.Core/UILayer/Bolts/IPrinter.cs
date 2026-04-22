namespace ContactManager.Core.UILayer.Bolts;

public interface IPrinter
{
    void WriteIf(bool condition, string ifTrue, string ifFalse);
    void WriteMessage(string text);
    void WriteSection(string title, List<string> content);
}
