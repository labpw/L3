using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using P04WeatherForecastAPI.Client.Configuration;
using P06Shop.API.Services.PersonService;
using P06Shop.Shared;
using P06Shop.Shared.Cars;
using P06Shop.Shared.Repositories;

namespace MVCClient.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            this._personRepository = personRepository;
        }
    

        public async Task<ServiceResponse> CreatePersonAsync(Person person)
        {
           return await _personRepository.CreatePersonAsync(person);
        }

        public async Task<ServiceResponse> DeletePersonAsync(int personId)
        {
            return await _personRepository.DeletePersonAsync(personId);
        }

        public async Task<ServiceResponse<List<Person>>> GetPeopleAsync()
        {
            return await _personRepository.GetPeopleAsync();
        }

        public async Task<ServiceResponse<Person>> GetPersonByIdAsync(int id)
        {
            return await _personRepository.GetPersonByIdAsync(id);
        }

        public async Task<ServiceResponse> UpdatePersonAsync(Person person)
        {
            return await _personRepository.UpdatePersonAsync(person);
        }
    }
}
