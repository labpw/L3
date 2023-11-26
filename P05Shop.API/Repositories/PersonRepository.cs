using P05Shop.API.Repositories.Interfaces;
using P06Shop.Shared.Cars;

namespace P05Shop.API.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private DataBaseContext dataBaseContext;

        public PersonRepository (DataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }
        public async void AddPerson (Person person)
        {
            await dataBaseContext.People.AddAsync (person);
            await dataBaseContext.SaveChangesAsync ();
        }

        public async void DeletePerson (Person person)
        {
            dataBaseContext.People.Remove (person);
            await dataBaseContext.SaveChangesAsync ();
        }

        public List<Person> GetAllPerson ()
        {
            List<Person> result = dataBaseContext.People.ToList ();
            return result;
        }

        public Person? GetPersonById (int personId)
        {
            Person result = dataBaseContext.People.FirstOrDefault (cb => cb.Id == personId);
            return result; 
        }

        public async void UpdatePerson (Person updatedPerson)
        {
            dataBaseContext.People.Update (updatedPerson);
            await dataBaseContext.SaveChangesAsync ();
        }
    }
}
