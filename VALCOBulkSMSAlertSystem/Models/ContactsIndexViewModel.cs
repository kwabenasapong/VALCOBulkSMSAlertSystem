using VALCOBulkSMSAlertSystem.Models.VALCOBulkSMSAlertSystem.Models;

namespace VALCOBulkSMSAlertSystem.Models
{
    public class ContactsIndexViewModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public List<Contacts>? ContactsList { get; set; }
        public List<string>? SelectedContacts { get; set; }

        // Add property for uploading contacts from an excel file using Task<IActionResult> UploadFile(IFormFile file)
        // in the Controllers/ContactsController.cs file
        public IFormFile? File { get; set; }

    }

}
