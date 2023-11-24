using Microsoft.EntityFrameworkCore;
using P05Shop.API;
using P05Shop.API.Services.CarService;
using P05Shop.API.Services.ProductService;
using P06Shop.API.Services.CarBrandService;
using P06Shop.API.Services.PersonService;
using P06Shop.Shared.Services.CarService;
using P06Shop.Shared.Services.ProductService;

public class Program
{
    private static void Main (string[] args)
    {

        var builder = WebApplication.CreateBuilder (args);

        builder.Services.AddDbContext<DataBaseContext> (options => options.UseNpgsql ("Server=localhost;Username=postgres;Database=postgres"));

        // Add services to the container.

        builder.Services.AddControllers ();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer ();
        builder.Services.AddSwaggerGen ();


        builder.Services.AddScoped<IProductService, ProductService> ();
        builder.Services.AddScoped<ICarService, CarService> ();
        builder.Services.AddScoped<ICarBrandService, CarBrandService> ();
        builder.Services.AddScoped<IPersonService, PersonService> ();

        // addScoped - obiekt jest tworzony za kazdym razem dla nowego zapytania http
        // jedno zaptranie tworzy jeden obiekt 

        // addTransinet obiekt jest tworzony za kazdym razem kiedy odwolujmey sie do konstuktora 
        // nawet wielokrotnie w cyklu jedengo zaptrania 

        //addsingleton - nowa instancja klasy tworzona jest tylko 1 na caly cykl trwania naszej aplikacji 




        var app = builder.Build ();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment ())
        {
            app.UseSwagger ();
            app.UseSwaggerUI ();
        }

        app.UseHttpsRedirection ();

        app.UseAuthorization ();

        app.MapControllers ();

        app.Run ();
    }
}
