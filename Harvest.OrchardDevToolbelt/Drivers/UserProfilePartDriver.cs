using Harvest.OrchardDevToolbelt.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;

namespace Harvest.OrchardDevToolbelt.Drivers {
    public class UserProfilePartDriver : ContentPartDriver<UserProfilePart> {

        protected override string Prefix {
            get { return "UserProfile"; }
        }

        protected override DriverResult Editor(UserProfilePart part, dynamic shapeHelper) {
            return ContentShape("Parts_UserProfile_Edit", () =>
                shapeHelper.EditorTemplate(
                    TemplateName: "Parts.UserProfile",
                    Model: part,
                    Prefix: Prefix));
        }

        protected override DriverResult Editor(UserProfilePart part, IUpdateModel updater, dynamic shapeHelper) {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }

        protected override void Exporting(UserProfilePart part, ExportContentContext context) {
            context.Element(part.PartDefinition.Name).SetAttributeValue("TwitterAlias", part.TwitterAlias);
        }

        protected override void Importing(UserProfilePart part, ImportContentContext context) {
            context.ImportAttribute(part.PartDefinition.Name, "TwitterAlias", x => part.TwitterAlias = x, () => part.TwitterAlias = "-");
        }
    }
}