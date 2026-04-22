using ContactManager.Core.Domain;

namespace ContactManager.Core.DataLayer;

public class FileBasedRepository : IContactRepository
{
    const string FileName = "contacts.txt";
    private readonly string path;
    private int lastId = 0;
    private readonly List<Contact> contacts = [];
    public FileBasedRepository(string path)
    {
        this.path = path;
        LoadFile();
    }

    public void Add(Contact contact)
    {
        contact.Id = ++lastId;
        contacts.Add(contact);
        SaveFile();
    }

    public bool Delete(int id)
    {
        var contact = contacts.FirstOrDefault(c => c.Id == id);
        if (contact is null) return false;
        SaveFile();
        return true;
    }

    public void Commit() => SaveFile();

    public IReadOnlyList<Contact> GetAll() => contacts;

    public Contact? GetById(int id)
        => contacts.FirstOrDefault(c => c.Id == id);

    public IReadOnlyList<Contact> Search(string search)
        => [.. contacts.Where(c => c.Name.Contains(search, StringComparison.CurrentCultureIgnoreCase))];

    private static string ContactToFile(Contact contact)
        => $"|{contact.Id}|{contact.Name}|";

    private static Contact FileToContact(string text)
    {
        var split = text.Split('|');
        return new(split[2]) { Id = int.Parse(split[1]) };
    }

    private void LoadFile()
    {
        if (File.Exists(path))
        {
            var lines = File.ReadAllLines(path);
            lastId = int.Parse(lines.First());
            contacts.AddRange(lines.Skip(1).Select(FileToContact));
        }
    }

    private void SaveFile()
    {
        var lines = new string[] { lastId.ToString() }.Concat(contacts.Select(ContactToFile));
        File.WriteAllLines(path, lines);
    }
}
