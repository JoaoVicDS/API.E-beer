using APIEbeer.Controllers;
using APIEbeer.Data.Models;
using APIEbeer.Services.Cache;
using APIEbeer.Services.Form;
using APIEbeer.Services.Json;
using APIEbeer.Services.Recommendation;

namespace APIEbeer.App
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options => { options.JsonSerializerOptions.PropertyNameCaseInsensitive = true; })
                .AddApplicationPart(typeof(FormController).Assembly)
                .AddApplicationPart(typeof(RecommendationController).Assembly);

            services.AddSingleton<QuestionsModel>();
            services.AddSingleton<ResponseOptionsModel>();
            
            services.AddScoped<IJsonService, JsonService>();
            services.AddScoped<IFormService, FormService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IRecommendationService, RecommendationService>();

            services.AddMemoryCache();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
