using Microsoft.EntityFrameworkCore;
using MovieCatalogApi.Data;
using MovieCatalogApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("MovieCatalogDb"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Seed database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    
    // Add sample directors
    context.Directors.AddRange(
        new Director { Id = 1, Name = "Christopher Nolan" },
        new Director { Id = 2, Name = "Quentin Tarantino" }
    );
    
    // Add sample movies
    context.Movies.AddRange(
        new Movie { Id = 1, Title = "Inception", ReleaseYear = 2010, DirectorId = 1 },
        new Movie { Id = 2, Title = "Pulp Fiction", ReleaseYear = 1994, DirectorId = 2 },
        new Movie { Id = 3, Title = "The Dark Knight", ReleaseYear = 2008, DirectorId = 1 },
        new Movie { Id = 4, Title = "Django Unchained", ReleaseYear = 2012, DirectorId = 2 }
    );
    
    context.SaveChanges();
}

app.Run();