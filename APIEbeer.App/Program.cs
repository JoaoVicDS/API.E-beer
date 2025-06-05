using APIEbeer.Controllers;
using APIEbeer.Services.Json;
using APIEbeer.Services.Form;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add the JsonController to the application part manager
builder.Services
    .AddControllersWithViews()
    .AddApplicationPart(typeof(JsonController).Assembly);

// Register services
builder.Services.AddScoped<IJsonService, JsonService>();
builder.Services.AddScoped<IFormService, FormService>();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
