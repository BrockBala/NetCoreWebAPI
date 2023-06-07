using Microsoft.AspNetCore.Mvc;
using NetCoreWebAPI.Data;
using NetCoreWebAPI.Models;

namespace NetCoreWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ContactsController : Controller
    {
        private readonly ContactApiDBContext contactApiDBContext;

        public ContactsController(ContactApiDBContext contactApiDBContext)
        {
            this.contactApiDBContext = contactApiDBContext;
        }

        [HttpGet]
        public IActionResult GetContacts()
        {
            var baseUri = $"{Request.Scheme}://{Request.Host}:{Request.Host.Port ?? 80}";
            return Ok(contactApiDBContext.Contacts.ToList());
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetContact([FromRoute] Guid id)
        {
            var contact = contactApiDBContext.Contacts.FindAsync(id).Result;

            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContact addContactRequest)
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Name = addContactRequest.Name,
                Address = addContactRequest.Address,
                PhoneNumber = addContactRequest.PhoneNumber,
                Email   = addContactRequest.Email
            };
            await contactApiDBContext.Contacts.AddAsync(contact);
            await contactApiDBContext.SaveChangesAsync();
            return Ok(contact);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContact UpdateContactRequest)
        {
            var contact = await contactApiDBContext.Contacts.FindAsync(id);
            if (contact != null)
            {
                contact.Name = UpdateContactRequest.Name;
                contact.Address  = UpdateContactRequest.Address;
                contact.PhoneNumber = UpdateContactRequest.PhoneNumber;
                contact.Email = UpdateContactRequest.Email;
                await contactApiDBContext.SaveChangesAsync();

                return Ok(contact);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = await contactApiDBContext.Contacts.FindAsync(id);
            if (contact != null)
            {
                contactApiDBContext.Contacts.Remove(contact);
                await contactApiDBContext.SaveChangesAsync();

                return Ok(contact);
            }
            return NotFound();
        }

    }
}
