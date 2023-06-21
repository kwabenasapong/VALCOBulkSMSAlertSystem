using Microsoft.AspNetCore.Mvc;
using static VALCOBulkSMSAlertSystem.Authorization.Operations;

namespace VALCOBulkSMSAlertSystem.Authorization
{
    public class AuthorizationService : IAdminAuthorizationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizationService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult? AuthorizeAdminsOnly()
        {
            if (!_httpContextAccessor.HttpContext.User.IsInRole(Constants.AdministratorsRole))
            {
                return new RedirectToActionResult("AccessDenied", "Account", null);
            }

            return null;
        }
    }

}
