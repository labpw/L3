using Microsoft.AspNetCore.Mvc;
using P06Shop.Shared.Cars;
using P06Shop.Shared;
using System;
using System.Threading.Tasks;

namespace P05Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarBrandController : ControllerBase
    {
        private readonly ICarBrandService _carBrandService;

        public CarBrandController(ICarBrandService carBrandService)
        {
            _carBrandService = carBrandService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCarBrands()
        {
            var response = await _carBrandService.GetCarBrandsAsync();
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.Message);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCarBrand([FromBody] CarBrand carBrand)
        {
            var response = await _carBrandService.CreateCarBrandAsync(carBrand);
            if (response.Success)
            {
                return CreatedAtAction(nameof(GetCarBrands), new { id = carBrand.Id }, carBrand);
            }
            return BadRequest(response.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCarBrand(int id, [FromBody] CarBrand carBrand)
        {
            carBrand.Id = id;
            var response = await _carBrandService.UpdateCarBrandAsync(carBrand);
            if (response.Success)
            {
                return Ok(carBrand);
            }
            return BadRequest(response.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarBrand(int id)
        {
            var response = await _carBrandService.DeleteCarBrandAsync(id);
            if (response.Success)
            {
                return NoContent();
            }
            return BadRequest(response.Message);
        }
    }
}
