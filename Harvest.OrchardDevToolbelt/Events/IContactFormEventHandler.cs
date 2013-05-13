using Orchard.ContentManagement;
using Orchard.Events;

namespace Harvest.OrchardDevToolbelt.Events {
    public interface IContactFormEventHandler : IEventHandler {
        void ContactFormEntryCreating(ContactFormCreatingContext context);
        void ContactFormEntryCreated(ContactFormCreatedContext context);
    }

    public class ContactFormContext {
        public ContentItem ContactFormEntry { get; set; }
    }

    public class ContactFormCreatedContext : ContactFormContext {}

    public class ContactFormCreatingContext : ContactFormContext {
        public bool Cancel { get; set; }
    }
}