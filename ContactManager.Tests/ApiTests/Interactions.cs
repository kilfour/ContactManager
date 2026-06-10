using ContactManager.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ContactManager.Core.ServiceLayer;

namespace ContactManager.Tests.ApiTests;

public class ContactsControllerTests
{
    private readonly Mock<IContactService> service = new();
    private readonly ContactsController controller;

    public ContactsControllerTests()
    {
        controller = new ContactsController(service.Object);
    }

    [Fact]
    public void Create_calls_service_and_returns_created()
    {
        var request = new CreateContactRequest
        {
            Name = "Ada Lovelace"
        };

        var response = new CreateContactResponse
        {
            Id = 42
        };

        service
            .Setup(s => s.AddContact(request))
            .Returns(response);

        var result = controller.Create(request);

        var created = Assert.IsType<CreatedResult>(result.Result);
        Assert.Equal("/api/contacts/42", created.Location);
        Assert.Same(response, created.Value);

        service.Verify(s => s.AddContact(request), Times.Once);
    }

    [Fact]
    public void Update_calls_service_and_returns_no_content_when_contact_exists()
    {
        var request = new UpdateContactRequest
        {
            Name = "Ada Byron"
        };

        service
            .Setup(s => s.UpdateContact(42, request))
            .Returns(true);

        var result = controller.Update(42, request);

        Assert.IsType<NoContentResult>(result);

        service.Verify(s => s.UpdateContact(42, request), Times.Once);
    }

    [Fact]
    public void Update_returns_not_found_when_contact_does_not_exist()
    {
        var request = new UpdateContactRequest
        {
            Name = "Ada Byron"
        };

        service
            .Setup(s => s.UpdateContact(42, request))
            .Returns(false);

        var result = controller.Update(42, request);

        Assert.IsType<NotFoundResult>(result);

        service.Verify(s => s.UpdateContact(42, request), Times.Once);
    }

    [Fact]
    public void Delete_calls_service_and_returns_no_content_when_contact_exists()
    {
        service
            .Setup(s => s.DeleteContact(42))
            .Returns(true);

        var result = controller.Delete(42);

        Assert.IsType<NoContentResult>(result);

        service.Verify(s => s.DeleteContact(42), Times.Once);
    }

    [Fact]
    public void Delete_returns_not_found_when_contact_does_not_exist()
    {
        service
            .Setup(s => s.DeleteContact(42))
            .Returns(false);

        var result = controller.Delete(42);

        Assert.IsType<NotFoundResult>(result);

        service.Verify(s => s.DeleteContact(42), Times.Once);
    }

    [Fact]
    public void GetAll_calls_service_and_returns_contacts()
    {
        var contacts = new List<GetAllContactResponse>
        {
            new() { Id = 42, Name = "Ada Lovelace" }
        };

        service
            .Setup(s => s.GetAll())
            .Returns(contacts);

        var result = controller.GetAll();

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Same(contacts, ok.Value);

        service.Verify(s => s.GetAll(), Times.Once);
    }

    [Fact]
    public void Search_calls_service_and_returns_matching_contacts()
    {
        var contacts = new List<SearchContactResponse>
        {
            new() { Id = 42, Name = "Ada Lovelace" }
        };

        service
            .Setup(s => s.Search("ada"))
            .Returns(contacts);

        var result = controller.Search("ada");

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Same(contacts, ok.Value);

        service.Verify(s => s.Search("ada"), Times.Once);
    }
}