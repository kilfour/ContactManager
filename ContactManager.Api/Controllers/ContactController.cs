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

    [HttpPut("{id:guid}")]
    public IActionResult Update(int id, UpdateContactRequest request)
        => service.UpdateContact(id, request) ? Ok() : NotFound();


    [HttpDelete("{id:guid}")]
    public IActionResult Delete(int id)
        => service.DeleteContact(id) ? Ok() : NotFound();

    [HttpGet]
    public ActionResult<List<GetAllContactResponse>> GetAll()
        => Ok(service.GetAll());

    [HttpGet("search")]
    public ActionResult<List<SearchContactResponse>> Search([FromQuery] string name)
        => Ok(service.Search(name));
}

