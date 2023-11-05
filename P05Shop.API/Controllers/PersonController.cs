using Microsoft.AspNetCore.Mvc;
using P06Shop.Shared.Cars;
using P06Shop.Shared;
using System;
using System.Threading.Tasks;
using P06Shop.API.Services.PersonService;

namespace P05Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPeople()
        {
            var response = await _personService.GetPeopleAsync();
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.Message);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody] Person person)
        {
            var response = await _personService.CreatePersonAsync(person);
            if (response.Success)
            {
                return CreatedAtAction(nameof(GetPeople), new { id = person.Id }, person);
            }
            return BadRequest(response.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson(int id, [FromBody] Person person)
        {
            person.Id = id;
            var response = await _personService.UpdatePersonAsync(person);
            if (response.Success)
            {
                return Ok(person);
            }
            return BadRequest(response.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var response = await _personService.DeletePersonAsync(id);
            if (response.Success)
            {
                return NoContent();
            }
            return BadRequest(response.Message);
        }
    }
}
