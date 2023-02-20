
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi_EFCore;
using WebApi_EFCore.Data.SeedData;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("WebApiDB_SqlServer");
builder.Services.AddDbContext<EmployeeDbContext>(x => x.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


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