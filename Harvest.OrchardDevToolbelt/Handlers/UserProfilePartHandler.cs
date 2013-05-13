using Harvest.OrchardDevToolbelt.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace Harvest.OrchardDevToolbelt.Handlers {
    public class UserProfilePartHandler : ContentHandler {
        public UserProfilePartHandler(IRepository<UserProfilePartRecord> repository) {
            Filters.Add(StorageFilter.For(repository));
            Filters.Add(new ActivatingFilter<UserProfilePart>("User"));
        }
    }
}