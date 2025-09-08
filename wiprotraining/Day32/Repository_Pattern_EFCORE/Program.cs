using Microsoft.EntityFrameworkCore;
using RepositoryPatternEFCore.Models;
using RepositoryPatternEFCore.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext with connection string
builder.Services.AddDbContext<EmpContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Register repository
builder.Services.AddScoped<IEmpRepository, EmpRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Emp}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
