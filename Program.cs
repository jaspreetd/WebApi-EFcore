
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi_EFCore;
using WebApi_EFCore.Data.SeedData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//IF SQL SERVER
var connectionString = builder.Configuration.GetConnectionString("WebApiDB_SqlServer");
builder.Services.AddDbContext<EmployeeDbContext>(x => x.UseSqlServer(connectionString));
//ELSE
//builder.Services.AddDbContext<EmployeeDbContext>(options =>
//     options.UseSqlite(builder.Configuration.GetConnectionString("WebApiDB_SqlLite")));
//builder.Services.AddScoped<IEmployeeRepo, EmployeeRepo>();
//SeedData.Initialize(new EmployeeDbContext());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
       new WeatherForecast
       (
           DateTime.Now.AddDays(index),
           Random.Shared.Next(-20, 55),
           summaries[Random.Shared.Next(summaries.Length)]
       ))
        .ToArray();
        
    return forecast;
})
.WithName("GetWeatherForecast");

//app.MapGet("/employees", ([FromServices] IEmployeeRepo empRepo) =>
//{

//    var emps= empRepo.GetEmployees().ToArray();

//    return emps;
//})
//.WithName("GetEmployees");

app.MapGet("/employees", ([FromServices] EmployeeDbContext db) =>
{
    return db.Employee.ToList();
})
.WithName("GetEmployees");

app.Run();



internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}