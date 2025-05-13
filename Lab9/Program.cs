using Lab9;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("ZooDb"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/animals", async (AppDbContext db) =>
    await db.Animals.ToListAsync());

app.MapGet("/animals/{id}", async (int id, AppDbContext db) =>
    await db.Animals.FindAsync(id) is Animal animal
        ? Results.Ok(animal)
        : Results.NotFound());

app.MapPost("/animals", async (Animal animal, AppDbContext db) =>
{
    db.Animals.Add(animal);
    await db.SaveChangesAsync();
    return Results.Created($"/animals/{animal.Id}", animal);
});

app.MapPut("/animals/{id}", async (int id, Animal input, AppDbContext db) =>
{
    var animal = await db.Animals.FindAsync(id);
    if (animal is null) return Results.NotFound();

    animal.Name = input.Name;
    animal.Species = input.Species;
    animal.Diet = input.Diet;
    animal.Habitat = input.Habitat;
    animal.ArrivalDate = input.ArrivalDate;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/animals/{id}", async (int id, AppDbContext db) =>
{
    if (await db.Animals.FindAsync(id) is Animal animal)
    {
        db.Animals.Remove(animal);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

app.Run();
