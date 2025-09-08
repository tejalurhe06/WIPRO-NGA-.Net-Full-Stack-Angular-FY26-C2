using Microsoft.EntityFrameworkCore;
using webapidemo1.Data;
using webapidemo1.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EFCodeFirstContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));  

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<EFCodeFirstContext>();

    var studentAddress = new StudentAddress
    {
        AddressLine1 = "Text1",
        AddressLine2 = "Text2"
    };

    var student = new Student
    {
        FirstName = "Pranaya",
        LastName = "Rout",
        Address = studentAddress
    };

    context.Students.Add(student);
    context.SaveChanges();

    Console.WriteLine("Student Added");
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
