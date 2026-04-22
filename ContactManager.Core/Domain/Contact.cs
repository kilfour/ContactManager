namespace ContactManager.Core.Domain;

public class Contact(string name)
{
    public int Id { get; set; }
    public string Name { get; set; } = name;
}
