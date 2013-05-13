using System.Linq;
using Harvest.OrchardDevToolbelt.Providers.Security;
using Harvest.OrchardDevToolbelt.Services;
using Orchard;
using Orchard.Security;
using Orchard.UI.Navigation;

namespace Harvest.OrchardDevToolbelt.Providers.Menu {
    public class AdminMenu : Component, INavigationProvider {
        private readonly IContactFormService _contactFormService;
        private readonly IAuthorizer _authorizer;

        public AdminMenu(IContactFormService contactFormService, IAuthorizer authorizer) {
            _contactFormService = contactFormService;
            _authorizer = authorizer;
        }

        public string MenuName { get { return "admin"; } }

        public void GetNavigation(NavigationBuilder builder) {

            if (!_authorizer.Authorize(HarvestPermissions.ManageContactFormEntries))
                throw new OrchardSecurityException(T("You don't have permission to manage contact form entries"));

            builder
                .Add(T("Contact Form ({0})", _contactFormService.GetEntries().Count()), "3", item => {
                    item.LinkToFirstChild(true);
                    item.Add(T("Contact Form Entries"), "1", subItem => subItem
                        .LocalNav()
                        .Action("Index", "ContactFormAdmin", new { area = MyModule.Name }));
                })
                .AddImageSet("contactform"); // Includes a stylesheet named "menu-contactform-admin"
        }
    }
}