using System.Net;
using System.Net.Http.Json;
using ContactManager.Core.ServiceLayer;

namespace ContactManager.Api.Tests;

public class ContactsPostTests
{
    private readonly CustomWebApplicationFactory factory = new();

    [Fact]
    public async Task Post_contact_creates_a_contact()
    {
        var client = factory.CreateClient();
        var request = new CreateContactRequest { Name = "Ada Lovelace" };

        var response = await client.PostAsJsonAsync("/api/contacts", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var created = await response.Content
            .ReadFromJsonAsync<CreateContactResponse>();

        Assert.NotNull(created);
        Assert.NotEqual(0, created.Id);
        Assert.Equal("Ada Lovelace", created.Name);

        var contacts = await client
            .GetFromJsonAsync<List<SearchContactResponse>>("/api/contacts");

        Assert.Contains(
            contacts!,
            contact => contact.Id == created.Id
                    && contact.Name == "Ada Lovelace");
    }
}