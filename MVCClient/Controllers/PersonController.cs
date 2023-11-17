using Microsoft.AspNetCore.Mvc;
using P06Shop.API.Services.PersonService;
using P06Shop.Shared.Cars; // Assuming Person class is here
using System.Threading.Tasks;

namespace MVCClient.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonService personService;

        public PersonController(IPersonService personService)
        {
            this.personService = personService;
        }

        // GET: PersonController
        public async Task<ActionResult> Index()
        {
            var response = await personService.GetPeopleAsync();
            if (response.Success)
            {
                return View(response.Data);
            }
            return Problem(response.Message);
        }

        // GET: PersonController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Person person)
        {
            try
            {
                var response = await personService.CreatePersonAsync(person);
                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(person);
            }
            catch
            {
                return View(person);
            }
        }

        // GET: PersonController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var response = await personService.GetPersonByIdAsync(id); 
            if (response.Success)
            {
                return View(response.Data);
            }
            return NotFound();
        }

        // POST: PersonController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Person person)
        {
            try
            {
                var response = await personService.UpdatePersonAsync(person);
                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(person);
            }
            catch
            {
                return View(person);
            }
        }

        // GET: PersonController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var response = await personService.GetPersonByIdAsync(id); 
            if (response.Success)
            {
                return View(response.Data);
            }
            return NotFound();
        }

        // POST: PersonController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var response = await personService.DeletePersonAsync(id);
            if (response.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
