using Harvest.OrchardDevToolbelt.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace Harvest.OrchardDevToolbelt.Handlers {
    public class ContactFormEntryPartHandler : ContentHandler {
        public ContactFormEntryPartHandler(IRepository<ContactFormEntryPartRecord> repository) {
            Filters.Add(StorageFilter.For(repository));
        }

        protected override void GetItemMetadata(GetContentItemMetadataContext context) {
            base.GetItemMetadata(context);
        }
    }
}