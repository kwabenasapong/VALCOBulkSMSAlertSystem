using VALCOBulkSMSAlertSystem.Areas.Identity.Data;

namespace VALCOBulkSMSAlertSystem.Models
{
    public class UserSettings
    {
        public string? Id { get; set; }

        public IEnumerable<VALCOUser> VALCOUsers { get; set; } = new List<VALCOUser>();

        // Add property to get user's messages using Task<List<Messages>> GetUserMessages(string userId)
        // in the Controllers\MessagesController.cs file
        // public IEnumerable<Messages>? UserMessages { get; set; } = new List<Messages>();

    }
}
