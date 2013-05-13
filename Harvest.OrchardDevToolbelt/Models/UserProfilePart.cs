using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;

namespace Harvest.OrchardDevToolbelt.Models {
    public class UserProfilePart : ContentPart<UserProfilePartRecord> {
        public string TwitterAlias {
            get { return Record.TwitterAlias; }
            set { Record.TwitterAlias = value; }
        }
    }

    public class UserProfilePartRecord : ContentPartRecord {
        public virtual string TwitterAlias { get; set; }
    }
}