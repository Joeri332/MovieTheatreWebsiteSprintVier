using Microsoft.AspNetCore.Mvc;
using MovieTheatreModels.Dto;

namespace MovieTheatreWebsite.Controllers
{ 
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;

        public ContactController(ILogger<ContactController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact([Bind("Name, Email, Subject, Message")] ContactDto contact)
        {
            return View(contact);
        }
    }
}
