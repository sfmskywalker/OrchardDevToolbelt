using Harvest.OrchardDevToolbelt.Events;
using Orchard;
using Orchard.UI.Notify;

namespace Harvest.OrchardDevToolbelt.Handlers {
    public class DefaultContactFormEventHandler : Component, IContactFormEventHandler {
        private readonly INotifier _notifier;

        public DefaultContactFormEventHandler(INotifier notifier) {
            _notifier = notifier;
        }

        public void ContactFormEntryCreating(ContactFormCreatingContext context) {
        }

        public void ContactFormEntryCreated(ContactFormCreatedContext context) {
            _notifier.Information(T("A new form entry has been created"));
        }
    }
}