using Microsoft.EntityFrameworkCore;
using BookStoreApi.Data;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            // Convert ModelStateDictionary to IDictionary<string, string[]>
            var errors = context.ModelState.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );
            
            var problems = new CustomValidationProblemDetails(errors)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "One or more validation errors occurred.",
                Status = StatusCodes.Status400BadRequest,
                Instance = context.HttpContext.Request.Path,
            };
            
            return new BadRequestObjectResult(problems)
            {
                ContentTypes = { "application/problem+json" }
            };
        };
    });

builder.Services.AddDbContext<BookStoreContext>(opt => 
    opt.UseInMemoryDatabase("BookStoreDb"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Seed initial data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BookStoreContext>();
    SeedData.Initialize(context);
}

app.Run();


public class CustomValidationProblemDetails : ValidationProblemDetails
{
    public CustomValidationProblemDetails(IDictionary<string, string[]> errors)
        : base(errors)
    {
    }
}