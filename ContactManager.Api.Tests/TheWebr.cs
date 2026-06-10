using System.Net;
using ContactManager.Core.DataLayer;
using ContactManager.Core.ServiceLayer;
using QuickCheckr;
using QuickFuzzr;
using QuickWebr;


namespace ContactManager.Api.Tests;

public record ContactInfo(int Id, string Name);

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
                new GetContacts(),
                new SearchContacts())
        //.Autopsy(2061875907, 20.ExecutionsPerRun());
        .Run(10.Runs(), 20.ExecutionsPerRun());
}

public class CreateContact : ApiMethod<IContactRepository>
{
    public override Specification<IContactRepository> Define() =>
        Create("Create Contact")
            .Always<ContactInfo>()
            .Route("api/contacts")
            .Send(Fuzzr.One<CreateContactRequest>())
            .ResponseIs<CreateContactResponse>(a => a is not null)
            .Store(response => new ContactInfo(response.Id, response.Name))
            .ReadBack((repo, info) => repo.GetById(info.Id))
            .Expect(("Name", (request, contact) => contact.Name == request.Name));
}

public class UpdateContact : ApiMethod<IContactRepository>
{
    public override Specification<IContactRepository> Define() =>
        Update("Update Contact", HttpMethod.Put)
            .When<ContactInfo>(a => a.Count > 0)
            .Route(info => info.Id, a => $"api/contacts/{a}")
            .Send(from req in Fuzzr.One<UpdateContactRequest>() select req)
            .Store((info, request) => info with { Name = request.Name })
            .ReadBack((repo, info) => repo.GetById(info.Id))
            .FailsWith("Not Found", HttpStatusCode.NotFound, (info, request) => (info with { Id = -1 }, request))
            .Expect(("Name", (request, contact) => contact.Name == request.Name));
}

public class DeleteContact : ApiMethod<IContactRepository>
{
    public override Specification<IContactRepository> Define() =>
        Delete("Delete Contact")
            .When<ContactInfo>(a => a.Count > 0)
            .Route(info => info.Id, a => $"api/contacts/{a}")
            .ReadBack((repo, info) => repo.GetById(info.Id))
            .FailsWith("Not Found", HttpStatusCode.NotFound, info => (info with { Id = -1 }))
            .Expect(("Deleted", contact => contact is null));
}

public class GetContacts : ApiMethod<IContactRepository>
{
    public override Specification<IContactRepository> Define() =>
        Get("Get Contacts")
            .Always<ContactInfo>()
            .Route("api/contacts")
            .Send()
            .ResponseIs<IReadOnlyList<GetAllContactResponse>>()
            .ExpectAll("Contains All Stored Contacts",
                (response, info) => response.Any(contact => contact.Id == info.Id));
}

public class SearchContacts : ApiMethod<IContactRepository>
{
    public override Specification<IContactRepository> Define() =>
        Get("Search Contacts")
            .When<ContactInfo>(a => a.Count > 0)
            .Route("api/contacts/search")
            .SendQuery(info => ("name", Fuzzr.Constant(info.Name)))
            .ResponseIs<IReadOnlyList<SearchContactResponse>>()
            .When("Empty List", info => ("name", "nope-not-here"), (response, info) => !response.Any())
            .Expect("Contains Stored Contact",
                (response, info) => response.Any(contact => contact.Id == info.Id));
}