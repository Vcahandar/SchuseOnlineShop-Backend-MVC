using Microsoft.AspNetCore.Mvc;
using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services.Interfaces;
using SchuseOnlineShop.ViewModels.Contact;

namespace SchuseOnlineShop.Controllers
{
    public class ContactController : Controller
    {
        private readonly ICrudService<Contact> _crudService;
        public ContactController(ICrudService<Contact> crudService)
        {
            _crudService = crudService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PostComment(ContactVM model)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", model);
            Contact contact = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Phone = model.Phone,
                Message = model.Message,
            };

            await _crudService.CreateAsync(contact);
            return RedirectToAction("Index");
        }
    }
}
