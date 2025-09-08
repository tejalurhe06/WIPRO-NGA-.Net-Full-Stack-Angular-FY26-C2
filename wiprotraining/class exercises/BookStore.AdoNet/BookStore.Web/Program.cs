using BookStore.Web.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
// Register data access services
builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

builder.Services.AddScoped<DisconnectedBookService>();
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(
name: "default",
pattern: "{controller=Books}/{action=Index}/{id?}");
app.Run();