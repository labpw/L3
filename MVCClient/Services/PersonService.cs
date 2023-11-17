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

namespace MVCClient.Services
{
    public class PersonService : IPersonService
    {
        private readonly AppSettings appSettings;
        private readonly HttpClient httpClient;

        public PersonService(HttpClient httpClient, IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
            this.httpClient = httpClient;
        }

        public async Task<ServiceResponse> CreatePersonAsync(Person person)
        {
            var json = JsonConvert.SerializeObject(person);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(appSettings.PeopleEndpoint.Create, content);
            
            return new ServiceResponse
            {
                Message = "Create person successfully.",
                Success = response.IsSuccessStatusCode
            };
        }

        public async Task<ServiceResponse> DeletePersonAsync(int personId)
        {
            var response = await httpClient.DeleteAsync(
                $"{appSettings.PeopleEndpoint.Delete}/{personId}"
            );
            var json = await response.Content.ReadAsStringAsync();
            return new ServiceResponse
            {
                Message = "Delete person successfully.",
                Success = response.IsSuccessStatusCode
            };
        }

        public async Task<ServiceResponse<List<Person>>> GetPeopleAsync()
        {
            var response = await httpClient.GetAsync(appSettings.PeopleEndpoint.GetAll);
            var json = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<List<Person>>(json);
            return new ServiceResponse<List<Person>>
            {
                Data = content,
                Message = "People retrieved successfully.",
                Success = true
            };
        }

        public async Task<ServiceResponse<Person>> GetPersonByIdAsync(int id)
        {
            try
            {
                var personUrl = $"{appSettings.PeopleEndpoint.GetById}/{id}";
                var response = await httpClient.GetAsync(personUrl);

                if (!response.IsSuccessStatusCode)
                {
                    // Handle the case where the person is not found or other HTTP errors
                    return new ServiceResponse<Person>
                    {
                        Data = null,
                        Message = "Person not found.",
                        Success = false
                    };
                }

                var json = await response.Content.ReadAsStringAsync();
                var person = JsonConvert.DeserializeObject<Person>(json);

                return new ServiceResponse<Person>
                {
                    Data = person,
                    Message = "Person retrieved successfully.",
                    Success = true
                };
            }
            catch (Exception ex)
            {
                // Log the exception here
                return new ServiceResponse<Person>
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false
                };
            }
        }

        public async Task<ServiceResponse> UpdatePersonAsync(Person person)
        {
            var json = JsonConvert.SerializeObject(person);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync(
                $"{appSettings.PeopleEndpoint.Update}/{person.Id}",
                content
            );
            return new ServiceResponse
            {
                Message = "Update person successfully.",
                Success = response.IsSuccessStatusCode
            };
        }
    }
}
