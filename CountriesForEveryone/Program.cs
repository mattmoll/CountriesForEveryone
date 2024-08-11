using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddAutoMapper(typeof(CountriesForEveryone.Server.Controllers.CountryController), 
                               typeof(CountriesForEveryone.Service.CountryService), 
                               typeof(CountriesForEveryone.Core.Entities.Country), 
                               typeof(CountriesForEveryone.Adapter.Models.CountryDto));

builder.Services.AddBusinessServices();
builder.Services.AddAdapters(builder.Configuration);

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

app.Run();
