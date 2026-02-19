using Microsoft.AspNetCore.Mvc;
using PartyInvites.Models;

namespace PartyInvites.Controllers {
    public class HomeController : Controller {

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }

        public IActionResult Index() {
            _logger.LogInformation("Index page visited");
            return View();
        }

        [HttpGet]
        public ViewResult RsvpForm() {
            _logger.LogInformation("RSVP Form page visited (GET)");
            return View();
        }

        [HttpPost]
        public ViewResult RsvpForm(GuestResponse guestResponse) {
            if (ModelState.IsValid) {
                _logger.LogInformation("RSVP received from {Name} ({Email}), Phone: {Phone}, WillAttend: {WillAttend}",
                    guestResponse.Name, guestResponse.Email, guestResponse.Phone, guestResponse.WillAttend);
                Repository.AddResponse(guestResponse);
                return View("Thanks", guestResponse);
            } else {
                _logger.LogWarning("RSVP form submitted with invalid data");
                return View();
            }
        }

        public ViewResult ListResponses() {
            var responses = Repository.Responses.Where(r => r.WillAttend == true);
            _logger.LogInformation("ListResponses page visited. Total attending: {Count}", responses.Count());
            return View(responses);
        }
    }
}
