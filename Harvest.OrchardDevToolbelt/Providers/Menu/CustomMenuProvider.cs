//using Harvest.OrchardDevToolbelt.Providers.Security;
//using Orchard;
//using Orchard.ContentManagement;
//using Orchard.ContentManagement.Aspects;
//using Orchard.Security;
//using Orchard.UI.Navigation;

//namespace Harvest.OrchardDevToolbelt.Providers.Menu {
//    public class CustomMenuProvider : Component, IMenuProvider {
//        private readonly IAuthorizer _authorizer;
//        public CustomMenuProvider(IAuthorizer authorizer) {
//            _authorizer = authorizer;
//        }

//        public void GetMenu(IContent menu, NavigationBuilder builder) {

//            if (!_authorizer.Authorize(HarvestPermissions.AccessContactForm))
//                return;

//            if (menu.As<ITitleAspect>().Title != "Main Menu")
//                return;

//            builder.Add(T("Contact Us"), "2", item => {
//                item.AddClass("contact-menu-item");
//                item.Action("Index", "ContactForm", new { area = MyModule.Name });

//                for (var i = 0; i < 5; i++) {
//                    item.Add(T("Item {0}", i + 1), string.Format("2.{0}", i), subItem => subItem.Url("http://google.com"));
//                }
//            });
//        }
//    }
//}