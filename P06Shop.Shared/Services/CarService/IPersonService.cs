using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using P06Shop.Shared.Cars;
using P06Shop.Shared;

namespace P06Shop.API.Services.PersonService
{
    public interface IPersonService
    {
        Task<ServiceResponse<List<Person>>> GetPeopleAsync();
        Task<ServiceResponse> DeletePersonAsync(int personId);
        Task<ServiceResponse> UpdatePersonAsync(Person person);
        Task<ServiceResponse> CreatePersonAsync(Person person);
    }
}
