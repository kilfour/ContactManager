using ContactManager.Core.ServiceLayer;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.Api.Controllers;

[ApiController]
[Route("api/contacts")]
public class ContactsController(ContactService service) : ControllerBase
{
    [HttpPost]
    public ActionResult<CreateContactResponse> Create(CreateContactRequest request)
    {
        var contact = service.AddContact(request);
        return Created($"/api/contacts/{contact.Id}", contact);
    }

    [HttpGet]
    public ActionResult<List<GetAllContactResponse>> GetAll()
    {
        return Ok(service.GetAll());
    }
}

