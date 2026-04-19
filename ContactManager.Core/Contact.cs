namespace ContactManager.Core;

public class Contact(string name)
{
    public int Id { get; private set; }
    public string Name { get; private set; } = name;

    public void SetId(int newId)
    {
        Id = newId;
    }
}
