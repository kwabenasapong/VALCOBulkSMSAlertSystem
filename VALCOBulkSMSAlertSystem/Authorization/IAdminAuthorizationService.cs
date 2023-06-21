using Microsoft.AspNetCore.Mvc;

namespace VALCOBulkSMSAlertSystem.Authorization
{
    public interface IAdminAuthorizationService
    {
        IActionResult AuthorizeAdminsOnly();
    }

}
