using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VALCOBulkSMSAlertSystem.Areas.Identity.Data;
using VALCOBulkSMSAlertSystem.Models;
using VALCOBulkSMSAlertSystem.Models.VALCOBulkSMSAlertSystem.Models;

namespace VALCOBulkSMSAlertSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<VALCOUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        public DbSet<VALCOBulkSMSAlertSystem.Models.Messages> Messages { get; set; } = default!;
        public DbSet<VALCOBulkSMSAlertSystem.Models.VALCOBulkSMSAlertSystem.Models.Contacts> Contacts { get; set; } = default!;
    }
}