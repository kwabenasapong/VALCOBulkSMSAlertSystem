using VALCOBulkSMSAlertSystem.Models.VALCOBulkSMSAlertSystem.Models;

namespace VALCOBulkSMSAlertSystem.Models
{
    public class ContactsIndexViewModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public List<Contacts> ContactsList { get; set; }
        public List<string> SelectedContacts { get; set; }
    }

}
