using System.Web.Mvc;
using Harvest.OrchardDevToolbelt.Models;
using Harvest.OrchardDevToolbelt.Providers.Security;
using Harvest.OrchardDevToolbelt.Services;
using Harvest.OrchardDevToolbelt.ViewModels;
using Orchard.DisplayManagement;
using Orchard.Localization;
using Orchard.Mvc;
using Orchard.Security;
using Orchard.Services;
using Orchard.Themes;

namespace Harvest.OrchardDevToolbelt.Controllers {
    [Themed]
    public class ContactFormController : Controller {
        private readonly IContactFormService _contactFormService;
        private readonly IClock _clock;
        private readonly IAuthorizer _authorizer;
        protected Localizer T { get; set; }
        protected dynamic New { get; set; }

        public ContactFormController(
            IShapeFactory shapeFactory, 
            IContactFormService contactFormService, IClock clock, IAuthorizer authorizer) {

            T = NullLocalizer.Instance;
            New = shapeFactory;
            _contactFormService = contactFormService;
            _clock = clock;
            _authorizer = authorizer;
        }

        public ActionResult Index() {

            if(!_authorizer.Authorize(HarvestPermissions.AccessContactForm))
                throw new OrchardSecurityException(T("You don't have access to the contact form"));

            return View();
        }

        public ActionResult Index2() {
            return new ShapeResult(this, New.Index2()) {
                ViewName = "ViewResults/AsideSecond"
            };
        }

        [HttpPost]
        public ActionResult Index(ContactFormViewModel contactForm) {

            if (!ModelState.IsValid)
                return View(contactForm);

            _contactFormService.StoreEntry(new ContactFormEntry {
                Name = contactForm.Name,
                Email = contactForm.Email,
                Subject = contactForm.Subject,
                MessageBody = contactForm.MessageBody,
                CreatedUtc = _clock.UtcNow
            });

            return RedirectToAction("Confirmation");
        }

        public ActionResult Confirmation() {
            return View();
        }
    }
}