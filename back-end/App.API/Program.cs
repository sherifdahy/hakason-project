using App.API;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddConfiguration(builder.Configuration);


var app = builder.Build();


app.MapOpenApi();
app.MapScalarApiReference(options =>
{
    options.AddPreferredSecuritySchemes("Bearer");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
