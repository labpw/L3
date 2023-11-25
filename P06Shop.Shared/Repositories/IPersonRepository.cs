using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using P06Shop.Shared.Cars;

namespace P06Shop.Shared.Repositories
{
   public interface IPersonRepository
{
    Task<ServiceResponse<List<Person>>> GetPeopleAsync();
    Task<ServiceResponse<Person>> GetPersonByIdAsync(int id);
    Task<ServiceResponse> CreatePersonAsync(Person person);
    Task<ServiceResponse> UpdatePersonAsync(Person person);
    Task<ServiceResponse> DeletePersonAsync(int id);
}
}