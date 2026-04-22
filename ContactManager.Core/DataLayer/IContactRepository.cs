using ContactManager.Core.Domain;

namespace ContactManager.Core.DataLayer;

public interface IContactRepository
{
    void Add(Contact contact);
    IReadOnlyList<Contact> GetAll();
    Contact? GetById(int id);
    bool Delete(int id);
    IReadOnlyList<Contact> Search(string search);
    void Commit();
}
