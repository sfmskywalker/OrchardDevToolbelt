using System.Linq;
using System.Web.Mvc;
using Harvest.OrchardDevToolbelt.Services;
using Orchard.Localization;
using Orchard.UI.Admin;

namespace Harvest.OrchardDevToolbelt.Controllers {
    [Admin]
    public class ContactFormAdminController : Controller {
        private readonly IContactFormService _contactFormService;
        protected Localizer T { get; set; }

        public ContactFormAdminController(IContactFormService contactFormService) {
            _contactFormService = contactFormService;
            T = NullLocalizer.Instance;
        }
        
        public ActionResult Index() {
            var entries = _contactFormService.GetEntries().ToList();
            return View(entries);
        }

        public ActionResult Delete(int id) {
            var entry = _contactFormService.GetEntry(id);
            _contactFormService.DeleteEntry(entry);
            return RedirectToAction("Index");
        }
    }
}