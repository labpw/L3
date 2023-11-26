using Microsoft.AspNetCore.Mvc;
using P06Shop.Shared.Cars;
using P06Shop.Shared.Services.CarService;

namespace P05Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : Controller
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCars()
        {
            var response = await _carService.GetCarsAsync();
            if (response.Success)
            {
                return Ok(response.Data.Select(e => new { e.Id, e.CarBrand, e.PreviousOwner, e.Power, e.Model}));
            }
            return BadRequest(response.Message);
        }

        // POST: api/Car
        [HttpPost]
        public async Task<IActionResult> CreateCar([FromBody] Car car)
        {
            var response = await _carService.CreateCarAsync(car);
            
            if (response.Success)
            {
                //var createdCar = _carService.GetCarsAsync ().Result.Data.FirstOrDefault (e => e.Id == car.Id);

                //if (createdCar != null)
                    return CreatedAtAction(nameof(GetCars), new { id = car.Id },
                        new { car.Id, car.CarBrand, car.PreviousOwner, car.Power, car.Model });
            }
            return BadRequest(response.Message);
        }

        // PUT: api/Car/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar(int id, [FromBody] Car car)
        {
            car.Id = id;
            var response = await _carService.UpdateCarAsync(car);
            if (response.Success)
            {
                return Ok(car);
            }
            return BadRequest(response.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var response = await _carService.DeleteCarAsync(id);
            if (response.Success)
            {
                return NoContent ();
            }
            return BadRequest(response.Message);
        }
    }
}
