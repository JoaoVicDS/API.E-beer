using APIEbeer.Controllers;
using APIEbeer.Data.Models;
using APIEbeer.Services.Form;
using APIEbeer.Services.Json;
using APIEbeer.Services.Cache;
using APIEbeer.Services.Recommendation;
using APIEbeer.Shared.ViewModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add the FormController to the application part manager
builder.Services
    .AddControllersWithViews()
    .AddApplicationPart(typeof(FormController).Assembly)
    .AddApplicationPart(typeof(RecommendationController).Assembly);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

// Register models as singletons
builder.Services.AddSingleton<QuestionsModel>();
builder.Services.AddSingleton<ResponseOptionsModel>();

// Register services
builder.Services.AddScoped<IJsonService, JsonService>();
builder.Services.AddScoped<IFormService, FormService>();
builder.Services.AddScoped<ICacheService, CacheService>();
builder.Services.AddScoped<IRecommendationService, RecommendationService>();

builder.Services.AddMemoryCache(); // Register memory cache service

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();
app.UseStaticFiles();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
