using Harvest.OrchardDevToolbelt.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;

namespace Harvest.OrchardDevToolbelt.Drivers {
    public class ContactFormEntryPartDriver : ContentPartDriver<ContactFormEntryPart> {

        protected override string Prefix {
            get { return "ContactFormEntry"; }
        }

        protected override DriverResult Editor(ContactFormEntryPart part, dynamic shapeHelper) {
            return ContentShape("Parts_ContactFormEntry_Edit", () =>
                shapeHelper.EditorTemplate(
                    TemplateName: "Parts.ContactFormEntry",
                    Model: part,
                    Prefix: Prefix));
        }

        protected override DriverResult Editor(ContactFormEntryPart part, IUpdateModel updater, dynamic shapeHelper) {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }
        
        protected override void Exporting(ContactFormEntryPart part, ExportContentContext context) {
            context.Element(part.PartDefinition.Name).SetAttributeValue("SenderName", part.SenderName);
            context.Element(part.PartDefinition.Name).SetAttributeValue("SenderEmail", part.SenderEmail);
        }

        protected override void Importing(ContactFormEntryPart part, ImportContentContext context) {
            context.ImportAttribute(part.PartDefinition.Name, "SenderName", x => part.SenderName = x, () => part.SenderName = "-");
            context.ImportAttribute(part.PartDefinition.Name, "SenderEmail", x => part.SenderEmail = x, () => part.SenderEmail = "-");
        }
    }
}