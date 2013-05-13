using System.Collections.Generic;
using Orchard.Environment.Extensions.Models;
using Orchard.Security.Permissions;

namespace Harvest.OrchardDevToolbelt.Providers.Security {
    public class HarvestPermissions : IPermissionProvider {
        public static readonly Permission ManageContactFormEntries = new Permission {
            Category = "Demo",
            Name = "ManageContactFormEntries",
            Description = "Manage contact form entries"
        };

        public static readonly Permission AccessContactForm = new Permission {
            Category = "Demo",
            Name = "AccessContactForm",
            Description = "Access the contact form on the front end"
        };

        public Feature Feature { get; set; }

        public IEnumerable<Permission> GetPermissions() {
            yield return ManageContactFormEntries;
            yield return AccessContactForm;
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes() {
            yield return new PermissionStereotype {
                Name = "Administrator", 
                Permissions = new[] {
                    ManageContactFormEntries,
                    AccessContactForm,
                }
            };

            yield return new PermissionStereotype {
                Name = "Harvester",
                Permissions = new[] {
                    AccessContactForm,
                }
            };
        }
    }
}