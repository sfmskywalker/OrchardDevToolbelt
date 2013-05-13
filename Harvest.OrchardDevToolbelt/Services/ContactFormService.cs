using System.Collections.Generic;
using System.Linq;
using Harvest.OrchardDevToolbelt.Events;
using Harvest.OrchardDevToolbelt.Models;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Core.Common.Models;
using Orchard.Core.Title.Models;
using Orchard.UI.Notify;

namespace Harvest.OrchardDevToolbelt.Services {
    public interface IContactFormService {
        ContentItem NewEntry(ContactFormEntry entry);
        ContentItem StoreEntry(ContactFormEntry entry);
        IEnumerable<ContentItem> GetEntries();
        ContentItem GetEntry(int id);
        void DeleteEntry(ContentItem entry);
    }

    public class ContactFormService : Component, IContactFormService {
        private readonly IOrchardServices _services;
        private readonly IEnumerable<IContactFormFilter> _filters;
        private readonly IContactFormEventHandler _contactFormEventHandler;

        public ContactFormService(
            IOrchardServices services, 
            IEnumerable<IContactFormFilter> filters, 
            IContactFormEventHandler contactFormEventHandler) {

            _services = services;
            _filters = filters;
            _contactFormEventHandler = contactFormEventHandler;
        }

        public ContentItem NewEntry(ContactFormEntry entry) {
            var contentItem = _services.ContentManager.New("ContactFormEntry");
            var commonPart = contentItem.As<CommonPart>();
            var titlePart = contentItem.As<TitlePart>();
            var bodyPart = contentItem.As<BodyPart>();
            var contactFormEntryPart = contentItem.As<ContactFormEntryPart>();

            commonPart.CreatedUtc = entry.CreatedUtc;
            titlePart.Title = entry.Subject;
            bodyPart.Text = entry.MessageBody;
            contactFormEntryPart.SenderName = entry.Name;
            contactFormEntryPart.SenderEmail = entry.Email;

            return contentItem;
        }

        public ContentItem StoreEntry(ContactFormEntry entry) {

            foreach (var filter in _filters) {
                filter.Process(entry);
            }
            
            var contentItem = NewEntry(entry);

            var entryCreatingContext = new ContactFormCreatingContext {
                ContactFormEntry = contentItem
            };

            _contactFormEventHandler.ContactFormEntryCreating(entryCreatingContext);

            if (entryCreatingContext.Cancel)
                return null;

            _services.ContentManager.Create(contentItem);

            var entryCreatedContext = new ContactFormCreatedContext {
                ContactFormEntry = contentItem
            };

            _contactFormEventHandler.ContactFormEntryCreated(entryCreatedContext);

            _services.Notifier.Information(T("Your message has been received. Thanks for contacting us!"));
            return contentItem;
        }

        public IEnumerable<ContentItem> GetEntries() {
            return _services.ContentManager.Query("ContactFormEntry").List();
        }

        public IEnumerable<ContentItem> GetEntriesByTitle(string title) {
            return _services.ContentManager.Query("ContactFormEntry")
                .ForPart<TitlePart>()
                .Where<TitlePartRecord>(x => x.Title == title)
                .List()
                .Select(x => x.ContentItem);
        }

        public ContentItem GetEntry(int id) {
            return _services.ContentManager.Get(id);
        }

        public void DeleteEntry(ContentItem entry) {
            _services.ContentManager.Remove(entry);
        }
    }

    public interface IContactFormFilter {
        void Process(ContactFormEntry entry);
    }
}