using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using ShopForHome.API.Data;
using ShopForHome.API.Filters; // Add this using directive
using Microsoft.Extensions.FileProviders;
using ShopForHome.API.Services;
using ShopForHome.API.Interfaces;


var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("https://localhost:7000", "http://localhost:7001");
// Add services to the container.

// 1. Register the filter with the Dependency Injection container
builder.Services.AddScoped<ExceptionFilter>();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IAdminCouponService, AdminCouponService>();
builder.Services.AddScoped<IAdminOrderService, AdminOrderService>();
builder.Services.AddScoped<IAdminProductService, AdminProductService>();
builder.Services.AddScoped<IAdminUserService, AdminUserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICartService,CartService>();
builder.Services.AddScoped<ICategoryService,CategoryService>();
builder.Services.AddScoped<ICouponService,CouponService>();
builder.Services.AddScoped<ImageService, ImageService>();
builder.Services.AddScoped<IOrderService,OrderService>();
builder.Services.AddScoped<IProductService,ProductService>();
builder.Services.AddScoped<IProfileService,ProfileService>();
builder.Services.AddScoped<IReportsService,ReportsService>();
builder.Services.AddScoped<ISearchService,SearchService>();
builder.Services.AddScoped<IWishlistService,WishlistService>();



// 2. Add services needed for API Controllers WITH the filter
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>();
});

// Add DbContext
builder.Services.AddDbContext<ShopForHomeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ShopForHomeConnection")));

// Add JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// Add HttpContextAccessor (for accessing User in controllers)
builder.Services.AddHttpContextAccessor();

// Add CORS for Angular app
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("https://localhost:4200", "http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
                  
        });

        
});


// Configure Swagger/OpenAPI with JWT support
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShopForHome API", Version = "v1" });
    
    // Add JWT Auth to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "ShopForHomeImages")),
    RequestPath = "/Images",
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
        ctx.Context.Response.Headers.Append("Cross-Origin-Resource-Policy", "cross-origin");
    }
});

app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");
// Add static file serving for images


app.UseCors("AllowAngularApp");


app.UseAuthentication(); // MUST come before UseAuthorization
app.UseAuthorization();

app.MapControllers();

app.Run();