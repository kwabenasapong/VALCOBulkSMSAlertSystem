using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VALCOBulkSMSAlertSystem.Models;

namespace VALCOBulkSMSAlertSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<VALCOBulkSMSAlertSystem.Models.Messages> Messages { get; set; } = default!;
    }
}