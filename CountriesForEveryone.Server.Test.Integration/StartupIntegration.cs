using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CountriesForEveryone.Server.Test.Integration
{
    public class StartupIntegration
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment CurrentEnvironment { get; }

        public StartupIntegration(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            CurrentEnvironment = environment;
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.None
            });
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Controllers.CountryController),
                               typeof(Service.CountryService),
                               typeof(Core.Entities.Country),
                               typeof(Adapter.Models.CountryDto));

            services.AddControllers();
            services.AddBusinessServices();
            services.AddMockedHttpCommandAdapters();

            // Needed for detecting controllers in tests
            services.AddControllers().AddApplicationPart(typeof(Controllers.CountryController).Assembly);
        }
    }
}
