using P06Shop.Shared.Cars;

namespace P05Shop.API.Repositories.Interfaces
{
    public interface IPersonRepository
    {
        void AddPerson (Person person);
        void DeletePerson (Person person);
        List<Person> GetAllPerson ();
        Person? GetPersonById (int personId);
        void UpdatePerson (Person updatedPerson);
    }
}
