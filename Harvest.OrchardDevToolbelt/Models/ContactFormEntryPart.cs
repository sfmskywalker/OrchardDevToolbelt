using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;

namespace Harvest.OrchardDevToolbelt.Models {
    public class ContactFormEntryPart : ContentPart<ContactFormEntryPartRecord> {
        public string SenderName {
            get { return Record.SenderName; }
            set { Record.SenderName = value; }
        }

        public string SenderEmail {
            get { return Record.SenderEmail; }
            set { Record.SenderEmail = value; }
        }
    }

    public class ContactFormEntryPartRecord : ContentPartRecord {
        public virtual string SenderName { get; set; }
        public virtual string SenderEmail { get; set; }
    }
}