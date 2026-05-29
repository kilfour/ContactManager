using System.Net;
using ContactManager.Core.DataLayer;
using ContactManager.Core.ServiceLayer;
using QuickCheckr;
using QuickFuzzr;
using QuickWebr;

namespace ContactManager.Api.Tests;

public record ContactInfo(int Id);

public class TheWebr
{
    [Fact]
    public void Run() =>
        Webr.Named("Contact Manager")
            .Context(() => new CustomWebApplicationFactory())
            .Client(a => a.CreateClient())
            .Reader(a => a.GetReader())
            .Methods(
                new CreateContact(),
                new UpdateContact(),
                new DeleteContact(),
                new GetContacts())
            .Run(1.Runs(), 50.ExecutionsPerRun());
}

public class CreateContact : ApiMethod<IContactRepository>
{
    public override Specification<IContactRepository> Define() =>
        Create("Create Contact")
            .Always<ContactInfo>()
            .Route("api/contacts")
            .Send(Fuzzr.One<CreateContactRequest>())
            .ResponseIs<CreateContactResponse>(a => a is not null)
            .Store(response => new ContactInfo(response.Id))
            .ReadBack((repo, info) => repo.GetById(info.Id))
            .Expect(("Name", (request, contact) => contact.Name == request.Name));
}

public class UpdateContact : ApiMethod<IContactRepository>
{
    public override Specification<IContactRepository> Define() =>
        Update("Update Contact", HttpMethod.Put)
            .When<ContactInfo>(a => a.Count > 0)
            .Route(info => info.Id, a => $"api/contacts/{a}")
            .Send(Fuzzr.One<UpdateContactRequest>())
            .Store((info, request) => info)
            .ReadBack((repo, info) => repo.GetById(info.Id))
            .FailsWith("NotFound", HttpStatusCode.NotFound, (info, request) => (info with { Id = -1 }, request))
            .Expect(("Name", (request, contact) => contact.Name == request.Name));
}

public class DeleteContact : ApiMethod<IContactRepository>
{
    public override Specification<IContactRepository> Define() =>
        Delete("Delete Contact")
            .When<ContactInfo>(a => a.Count > 0)
            .Route(info => info.Id, a => $"api/contacts/{a}")
            .ReadBack((repo, info) => repo.GetById(info.Id))
            .FailsWith("NotFound", HttpStatusCode.NotFound, info => (info with { Id = -1 }))
            .Expect(("Deleted", contact => contact is null));
}

public class GetContacts : ApiMethod<IContactRepository>
{
    public override Specification<IContactRepository> Define() =>
        Get("Get Contacts")
            .Always<ContactInfo>()
            .Route("api/contacts")
            .ResponseIs<IReadOnlyList<GetAllContactResponse>>()
            .Expect("Contains All Stored Contacts",
                (response, info) => response.Any(contact => contact.Id == info.Id));
}