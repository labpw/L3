using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P06Shop.Shared.Services.CarService;

namespace MVCClient.Controllers
{
    public class CarController(ICarService carService) : Controller
    {
        // GET: CarController
        public async Task<ActionResult> Index()
        {
            var cars = await carService.GetCarsAsync();
            if(cars != null)
            {
                return View(cars.Data);
            }
            return Problem("No cars found.");
        }
    }
}
