using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VALCOBulkSMSAlertSystem.Data;
using VALCOBulkSMSAlertSystem.Services;
using VALCOBulkSMSAlertSystem.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using VALCOBulkSMSAlertSystem.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<VALCOUser>(options => 
    options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

// Add Authorization Policy
builder.Services.AddControllers(config =>
{
    var policy = new AuthorizationPolicyBuilder()
                     .RequireAuthenticatedUser()
                     .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

// Register Authorization handlers
builder.Services.AddScoped<IAuthorizationHandler, IsOwnerAuthorizationHandler>();
builder.Services.AddScoped<IAuthorizationHandler, AdministratorsAuthorizationHandler>();

builder.Services.AddTransient<HubtelSmsService>(x => new HubtelSmsService(
    builder.Configuration["HubtelSms:ClientId"],
    builder.Configuration["HubtelSms:ClientSecret"],
    builder.Configuration["HubtelSms:FromName"]));

builder.Services.AddTransient<ContactsService>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// //Add Role-based Authorization to the project for SeedData
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
    // requires using Microsoft.Extensions.Configuration;
    // Set password with the Secret Manager tool.
    // dotnet user-secrets set SeedUserPW <pw>

    var testUserPw = builder.Configuration.GetValue<string>("SeedUserPW");

    await SeedData.Initialize(services, testUserPw);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
/*app.MapControllerRoute(
    name: "SelectedRecipients",
    pattern: "Contacts/SelectedRecipients",
    defaults: new { controller = "Contacts", action = "SelectedRecipients" });*/
app.MapRazorPages();

app.Run();
