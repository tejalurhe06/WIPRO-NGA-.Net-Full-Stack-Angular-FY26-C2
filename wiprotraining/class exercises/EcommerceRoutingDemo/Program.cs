var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Custom constraint for filtering products
builder.Services.Configure<RouteOptions>(options =>
{
    options.ConstraintMap.Add("priceRange", typeof(EcommerceRoutingDemo.Constraints.PriceRangeConstraint));
});

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

// Complex route for product details
app.MapControllerRoute(
    name: "productRoute",
    pattern: "Products/{category}/{id:int}",
    defaults: new { controller = "Products", action = "Details" }
);

// Dynamic route for checkout
app.MapControllerRoute(
    name: "checkoutRoute",
    pattern: "Checkout",
    defaults: new { controller = "Checkout", action = "Index" }
);

app.MapControllerRoute(
    name: "filterRoute",
    pattern: "Products/Filter/{category}/{priceRange:priceRange}",
    defaults: new { controller = "Products", action = "Filter" }
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
