using System.ComponentModel.DataAnnotations;

namespace Harvest.OrchardDevToolbelt.ViewModels {
    public class ContactFormViewModel {
        [Required] public string Name { get; set; }
        [Required] public string Email { get; set; } 
        [Required] public string Subject { get; set; }
        [Required] public string MessageBody { get; set; } 
    }
}