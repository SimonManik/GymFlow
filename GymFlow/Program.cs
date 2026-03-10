using GymFlow.Data;
using GymFlow.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add support for Razor Pages (required for Identity Login/Register UI)
builder.Services.AddRazorPages();

// Register Database Context first
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Identity (Users/Roles)
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>() 
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddHttpClient<IWgerService, WgerService>(client =>
{
    client.BaseAddress = new Uri("https://wger.de/api/v2/");
});


var app = builder.Build();

// admin
using (var scope = app.Services.CreateScope())
{
var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

if (!await roleManager.RoleExistsAsync("Admin"))
{
await roleManager.CreateAsync(new IdentityRole("Admin"));
}

var user = await userManager.FindByEmailAsync("manik.simon@seznam.cz"); 
if (user != null && !await userManager.IsInRoleAsync(user, "Admin"))
{
await userManager.AddToRoleAsync(user, "Admin");
}
}



// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Map routes for Identity Razor Pages
app.MapRazorPages();

app.Run();