using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using SecureDatabaseApp.Data;
using SecureDatabaseApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 1. Register DbContext with connection string from Configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Add Data Protection and store keys in the database (for column encryption/decryption)
builder.Services.AddDataProtection()
    .PersistKeysToDbContext<ApplicationDbContext>();

// 3. Register our custom services
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // Enforce HTTPS in production
app.UseAuthorization();
app.MapControllers();

app.Run();