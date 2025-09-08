var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RouteOptions>(options =>
{
    options.ConstraintMap.Add("myguid", typeof(AdvancedRoutingDemo.Constraints.GuidRouteConstraint));
});

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

// Complex Routing Example
app.MapControllerRoute(
    name: "productRoute",
    pattern: "Products/{category}/{id:int}",
    defaults: new { controller = "Products", action = "Details" }
);

app.MapControllerRoute(
    name: "userOrdersRoute",
    pattern: "Users/{username}/Orders",
    defaults: new { controller = "Users", action = "Orders" }
);

app.MapControllerRoute(
    name: "guidRoute",
    pattern: "Orders/{orderId:myguid}",
    defaults: new { controller = "Orders", action = "Details" }
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
