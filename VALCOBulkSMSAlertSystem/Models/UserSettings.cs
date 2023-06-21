using VALCOBulkSMSAlertSystem.Areas.Identity.Data;

namespace VALCOBulkSMSAlertSystem.Models
{
    public class UserSettings
    {
        public string? Id { get; set; }

        public IEnumerable<VALCOUser>? VALCOUsers { get; set; }
       
    }
}
