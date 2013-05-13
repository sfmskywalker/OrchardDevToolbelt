using System.Linq;
using Harvest.OrchardDevToolbelt.Events;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Core.Common.Models;
using Orchard.UI.Notify;

namespace Harvest.OrchardDevToolbelt.Handlers {
    public class SpamProtectedContactFormEventHandler : Component, IContactFormEventHandler {
        private readonly INotifier _notifier;

        public SpamProtectedContactFormEventHandler(INotifier notifier) {
            _notifier = notifier;
        }

        public void ContactFormEntryCreating(ContactFormCreatingContext context) {
            var text = context.ContactFormEntry.As<BodyPart>().Text;
            var spamTerms = new[] { "viagra", "opportunity", "win!" };

            if (!spamTerms.Any(text.Contains))
                return;

            context.Cancel = true;
            _notifier.Warning(T("Your message is rated as spam and therefore not delivered."));
        }

        public void ContactFormEntryCreated(ContactFormCreatedContext context) {
            _notifier.Information(T("Message accepted"));
        }
    }
}