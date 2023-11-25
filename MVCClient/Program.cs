using MVCClient.Repositories;
using MVCClient.Services;
using P04WeatherForecastAPI.Client.Configuration;
using P06Shop.API.Services.PersonService;
using P06Shop.Shared.Repositories;
using P06Shop.Shared.Services.CarService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var appSettings = builder.Configuration.GetSection(nameof(AppSettings));
var appSettingsSection = appSettings.Get<AppSettings>();

var carsURIBuilder = new UriBuilder(appSettingsSection.BaseAPIUrl)
{
    Path = appSettingsSection.CarsEndpoint.Base_url,
};
var carBrandsURIBuilder = new UriBuilder(appSettingsSection.BaseAPIUrl)
{
    Path = appSettingsSection.CarBrandsEndpoint.Base_url,
};
var peopleURIBuilder = new UriBuilder(appSettingsSection.BaseAPIUrl)
{
    Path = appSettingsSection.PeopleEndpoint.Base_url,
};
//Microsoft.Extensions.Http

builder.Services.Configure<AppSettings>(appSettings);
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddSingleton<ICarRepository, CarRepository>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddSingleton<IPersonRepository, PersonRepository>();
builder.Services.AddSingleton<ICarBrandRepository, CarBrandRepository>();
builder.Services.AddScoped<ICarBrandService, CarBrandService>();
builder.Services.AddHttpClient<ICarRepository, CarRepository>(client => client.BaseAddress = carsURIBuilder.Uri);
builder.Services.AddHttpClient<IPersonRepository, PersonRepository>(client => client.BaseAddress = peopleURIBuilder.Uri);
builder.Services.AddHttpClient<ICarBrandRepository, CarBrandRepository>(client => client.BaseAddress = carBrandsURIBuilder.Uri);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
