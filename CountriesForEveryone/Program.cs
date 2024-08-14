using AspNetCoreRateLimit;
using CountriesForEveryone.Core.Services;
using CountriesForEveryone.Server.Sanitization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddEndpointsApiExplorer();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddAutoMapper(typeof(CountriesForEveryone.Server.Controllers.CountryController), 
                               typeof(CountriesForEveryone.Service.CountryService), 
                               typeof(CountriesForEveryone.Core.Entities.Country), 
                               typeof(CountriesForEveryone.Adapter.Models.CountryDto));

builder.Services.AddBusinessServices();
builder.Services.AddAuthService();
builder.Services.AddAdapters(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddCountriesForEveryoneContext(builder.Configuration);
builder.Services.AddRateLimitingServices(builder.Configuration);
builder.Services.AddJWTAuthentication(builder.Configuration);
builder.Services.AddSwaggerWithJWT();

var app = builder.Build();

app.InitializeDataBase(builder.Configuration);

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseIpRateLimiting();

app.UseMiddleware<InputSanitizationMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await InitializeDataOnFirstRunOrForced(app.Services);

app.Run();



async Task InitializeDataOnFirstRunOrForced(IServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();

    var dataInitializationService = scope.ServiceProvider.GetRequiredService<IDataInitializationService>();
    var forceDataFetchingUpdate = builder.Configuration.GetValue<bool>("ForceDataFetchingUpdate");

    await dataInitializationService.LoadCountriesData(forceDataFetchingUpdate);
}