using VALCOBulkSMSAlertSystem.Areas.Identity.Data;

namespace VALCOBulkSMSAlertSystem.Models
{
    public class UserSettings
    {
        public string? Id { get; set; }

        public IEnumerable<VALCOUser> VALCOUsers { get; set; } = new List<VALCOUser>();

        //Use the GetCurrentUserMessages method in the
        //Controllers/UserSettingsController.cs file to get the user messages
        
       /*var VALCOUsers.Messages = await GetCurrentUserMessages();*/
    }
}
