using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using VALCOBulkSMSAlertSystem.Models;

namespace VALCOBulkSMSAlertSystem.Areas.Identity.Data;

// Add profile data for application users by adding properties to the VALCOUser class
public class VALCOUser : IdentityUser
{
    public IEnumerable<Messages>? UserMessages { get; set; }
}

