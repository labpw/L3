using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P06Shop.Shared.Services.CarService;

namespace MVCClient.Controllers
{
    public class CarBrandController(ICarBrandService carBrandService) : Controller
    {
        // GET: CarBrandController
        public async Task<ActionResult> Index()
        {
            var carBrands = await carBrandService.GetCarBrandsAsync();
            if (carBrands != null)
            {
                return View(carBrands.Data);
            }
            return Problem("No car brands found.");
        }

        // GET: CarBrandController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var carBrand = (await carBrandService.GetCarBrandsAsync())?.Data?.FirstOrDefault(e => e.Id == id) ?? null;
            if(carBrand != null)
            {
                return View(carBrand);
            }
            return Problem("No car brands found.");
        }

        // GET: CarBrandController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CarBrandController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CarBrandController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var carBrand = (await carBrandService.GetCarBrandsAsync())?.Data?.FirstOrDefault(e => e.Id == id) ?? null;
            if (carBrand != null)
            {
                return View(carBrand);
            }
            return Problem("No car brands found.");
        }

        // POST: CarBrandController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return Problem("Invalid model state.");
            }
            try
            {
                var carBrand = (await carBrandService.GetCarBrandsAsync())?.Data?.FirstOrDefault(e => e.Id == id) ?? null;
                if(carBrand != null && collection != null)
                {
                    carBrand.Name = collection["Name"];
                    carBrand.OriginCountry = collection["OriginCountry"];
                    await carBrandService.UpdateCarBrandAsync(carBrand);
                    return RedirectToAction(nameof(Index));
                }
                return Problem("No such a car brand.");
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        // GET: CarBrandController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var carBrand = (await carBrandService.GetCarBrandsAsync())?.Data?.FirstOrDefault(e => e.Id == id) ?? null;
            if (carBrand != null)
            {
                return View(carBrand);
            }
            return Problem("No car brands found.");
        }

        // POST: CarBrandController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            if(!ModelState.IsValid)
            {
                return Problem("Invalid model state.");
            }
            try
            {
                var carBrand = (await carBrandService.GetCarBrandsAsync())?.Data?.FirstOrDefault(e => e.Id == id) ?? null;
                if (carBrand != null && collection != null)
                {
                    await carBrandService.DeleteCarBrandAsync(carBrand.Id);
                    return RedirectToAction(nameof(Index));
                }
                return Problem("No such a car brand.");
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}
