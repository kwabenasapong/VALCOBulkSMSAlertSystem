using VALCOBulkSMSAlertSystem.Authorization;
using VALCOBulkSMSAlertSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static VALCOBulkSMSAlertSystem.Authorization.Operations;
using VALCOBulkSMSAlertSystem.Areas.Identity.Data;

// dotnet aspnet-codegenerator razorpage -m Contact -dc ApplicationDbContext -udl -outDir Pages\Contacts --referenceScriptLibraries

namespace VALCOBulkSMSAlertSystem.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw="")
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // For sample purposes seed both with the same password.
                // Password is set with the following:
                // dotnet user-secrets set SeedUserPW <pw>
                // The admin user can do anything

                var adminID = await EnsureUser(serviceProvider, testUserPw, "admin@admin.com");
                await EnsureRole(serviceProvider, adminID, Constants.AdministratorsRole);

                /*// allowed user can create and edit contacts that they create
                var managerID = await EnsureUser(serviceProvider, testUserPw, "manager@contoso.com");
                await EnsureRole(serviceProvider, managerID, Constants.ContactManagersRole);*/

                SeedDB(context, adminID);
            }
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                            string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<VALCOUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new VALCOUser
                {
                    UserName = UserName,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, testUserPw);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                              string uid, string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            IdentityResult IR;
            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<VALCOUser>>();

            if (userManager == null)
            {
                throw new Exception("userManager is null");
            }

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }

        public static void SeedDB(ApplicationDbContext context, string adminID)
        {
            if (context.Messages.Any())
            {
                return;   // DB has been seeded
            }

            context.Messages.AddRange(
                new Messages
                {
                    Id = 1,
                    Title = "Hello World",
                    Content = "This is a test",
                    Sender = "admin@admin.com",
                    Recipient = "0548257283",
                    Date = DateTime.Now.ToString(),
                    UserID = adminID
                }
             );
            context.SaveChanges();
        }

    }
}
