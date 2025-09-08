using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineBookstoreMVC.Data;
using OnlineBookstoreMVC.Models;
using OnlineBookstoreMVC.Filters;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// MVC with Logging Filter
builder.Services.AddControllersWithViews(options => options.Filters.Add<LoggingFilter>());

// Session
builder.Services.AddSession();

var app = builder.Build();

// HTTP pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

// Custom routes
app.MapControllerRoute(
    name: "bookdetails",
    pattern: "Books/Details/{id:int}",
    defaults: new { controller = "Books", action = "Details" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Books}/{action=Index}/{id?}");

app.Run();
