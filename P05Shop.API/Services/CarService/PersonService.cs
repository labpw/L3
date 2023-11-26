using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P06Shop.Shared.Cars;
using P06Shop.Shared;
using P05Shop.API;
using Microsoft.EntityFrameworkCore;

namespace P06Shop.API.Services.PersonService
{
    public class PersonService : IPersonService
    {
        private readonly DataBaseContext dataBaseContext;

        public PersonService(DataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }

        public async Task<ServiceResponse<List<Person>>> GetPeopleAsync()
        {
            var response = new ServiceResponse<List<Person>>();
            try
            {
                response.Data = await dataBaseContext.People.ToListAsync();
                response.Success = true;
                response.Message = "People retrieved successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error while retrieving people: " + ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse> DeletePersonAsync(int personId)
        {
            var response = new ServiceResponse();
            try
            {
                var personToRemove = dataBaseContext.People.FirstOrDefault(p => p.Id == personId);
                if (personToRemove != null)
                {
                    dataBaseContext.People.Remove(personToRemove);
                    response.Success = true;
                    response.Message = "Person deleted successfully.";
                    await dataBaseContext.SaveChangesAsync();
                }
                else
                {
                    response.Success = false;
                    response.Message = "Person not found for deletion.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error while deleting person: " + ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse> UpdatePersonAsync(Person person)
        {
            var response = new ServiceResponse();
            try
            {
                var existingPerson = await dataBaseContext.People.FirstOrDefaultAsync(
                    p => p.Id == person.Id
                );
                if (existingPerson != null)
                {
                    existingPerson.Name = person.Name;
                    existingPerson.PhoneNumber = person.PhoneNumber;
                    dataBaseContext.People.Update(existingPerson);
                    response.Success = true;
                    response.Message = "Person updated successfully.";
                    await dataBaseContext.SaveChangesAsync();
                }
                else
                {
                    response.Success = false;
                    response.Message = "Person not found for updating.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error while updating person: " + ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse> CreatePersonAsync(Person person)
        {
            var response = new ServiceResponse();
            try
            {
                await dataBaseContext.People.AddAsync(person);
                response.Success = true;
                response.Message = "Person created successfully.";
                await dataBaseContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error while creating person: " + ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<Person>> GetPersonByIdAsync(int id)
        {
            var response = new ServiceResponse<Person>();
            try
            {
                var person = await dataBaseContext.People.FirstOrDefaultAsync(p => p.Id == id);
                if (person != null)
                {
                    response.Data = person;
                    response.Success = true;
                    response.Message = "Person retrieved successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Person not found.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error while retrieving person: " + ex.Message;
            }
            return response;
        }
    }
}
