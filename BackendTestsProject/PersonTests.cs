using Moq;
using P06Shop.Shared.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestsProject
{
    public class PersonTest : IDisposable
    {
        private CustomWebApplicationFactory _factory;
        private HttpClient _client;

        public PersonTest ()
        {
            _factory = new CustomWebApplicationFactory ();
            _client = _factory.CreateClient ();
        }

        [Fact]
        public async void postPerson_ProperData ()
        {
            var newPerson = new Person (1, "imie", "000111222");

            _factory.PersonRepositoryMock.Setup (r => r.AddPerson (It.Is<Person> (p => p.Name == "imie" && p.PhoneNumber == "000111222"))).Verifiable ();

            var response = await _client.PostAsync ("api/Person", JsonContent.Create (newPerson));

            Assert.Equal (HttpStatusCode.Created, response.StatusCode);

            _factory.PersonRepositoryMock.VerifyAll ();
        }

        [Fact]
        public async void postPerson_EmptyName ()
        {
            var newPerson = new Person (1, null, "000111222");

            _factory.PersonRepositoryMock.Setup (r => r.AddPerson (It.Is<Person> (p => p.Name == null && p.PhoneNumber == "000111222"))).Verifiable ();

            var response = await _client.PostAsync ("api/Person", JsonContent.Create (newPerson));

            Assert.Equal (HttpStatusCode.BadRequest, response.StatusCode);

            _factory.PersonRepositoryMock.Verify (r => r.AddPerson (It.IsAny<Person> ()), Times.Never);
        }

        [Fact]
        public async void postPerson_EmptyPhone ()
        {
            var newPerson = new Person (1, "imie", null);

            _factory.PersonRepositoryMock.Setup (r => r.AddPerson (It.Is<Person> (p => p.Name == "imie" && p.PhoneNumber == null))).Verifiable ();

            var response = await _client.PostAsync ("api/Person", JsonContent.Create (newPerson));

            Assert.Equal (HttpStatusCode.BadRequest, response.StatusCode);

            _factory.PersonRepositoryMock.Verify (r => r.AddPerson (It.IsAny<Person> ()), Times.Never);
        }

        [Fact]
        public async void postPerson_AllInputsEmpty ()
        {
            var newPerson = new Person (1, null, null);

            _factory.PersonRepositoryMock.Setup (r => r.AddPerson (It.Is<Person> (p => p.Name == null && p.PhoneNumber == null))).Verifiable ();

            var response = await _client.PostAsync ("api/Person", JsonContent.Create (newPerson));

            Assert.Equal (HttpStatusCode.BadRequest, response.StatusCode);

            _factory.PersonRepositoryMock.Verify (r => r.AddPerson (It.IsAny<Person> ()), Times.Never);
        }

        [Fact]
        public async void putPerson_ProperIdProperMod ()
        {
            var person = new Person (1, "imie", "000111222");
            var newPerson = new Person (1, "nowe imie", "000111333");

            _factory.PersonRepositoryMock.Setup (r => r.GetPersonById (1)).Returns (person);
            _factory.PersonRepositoryMock.Setup (r => r.UpdatePerson (It.Is<Person> (p => p.Name == "nowe imie"
                                                                                        && p.PhoneNumber == "000111333"))).Verifiable ();

            var response = await _client.PutAsync ("api/Person/1", JsonContent.Create (newPerson));

            Assert.Equal (HttpStatusCode.OK, response.StatusCode);

            _factory.PersonRepositoryMock.VerifyAll ();
        }

        [Fact]
        public async void putPerson_WrongIdProperMod ()
        {
            var newPerson = new Person (1, "nowe imie", "000111333");

            _factory.PersonRepositoryMock.Setup (r => r.UpdatePerson (It.Is<Person> (p => p.Name == "nowe imie"
                                                                                        && p.PhoneNumber == "000111333"))).Verifiable ();

            var response = await _client.PutAsync ("api/Person/1", JsonContent.Create (newPerson));

            Assert.Equal (HttpStatusCode.BadRequest, response.StatusCode);

            _factory.PersonRepositoryMock.Verify (r => r.UpdatePerson (It.IsAny<Person> ()), Times.Never);
        }

        [Fact]
        public async void putPerson_WrongIdWrongMod ()
        {
            var newPerson = new Person (1, null, null);

            _factory.PersonRepositoryMock.Setup (r => r.UpdatePerson (It.Is<Person> (p => p.Name == null && p.PhoneNumber == null))).Verifiable ();

            var response = await _client.PutAsync ("api/Person/1", JsonContent.Create (newPerson));

            Assert.Equal (HttpStatusCode.BadRequest, response.StatusCode);

            _factory.PersonRepositoryMock.Verify (r => r.UpdatePerson (It.IsAny<Person> ()), Times.Never);
        }

        [Fact]
        public async void deletePerson_ProperId ()
        {
            var person = new Person (1, "imie", "000111222");

            _factory.PersonRepositoryMock.Setup (r => r.GetPersonById (1)).Returns (person);
            _factory.PersonRepositoryMock.Setup (r => r.DeletePerson (It.Is<Person> (p => p.Name == "imie" && p.PhoneNumber == "000111222"))).Verifiable ();

            var response = await _client.DeleteAsync ("api/Person/1");

            Assert.Equal (HttpStatusCode.NoContent, response.StatusCode);

            _factory.PersonRepositoryMock.VerifyAll ();
        }

        [Fact]
        public async void deletePerson_WrongId ()
        {
            var response = await _client.DeleteAsync ("api/Person/1");

            Assert.Equal (HttpStatusCode.BadRequest, response.StatusCode);

            _factory.PersonRepositoryMock.Verify (r => r.DeletePerson (It.IsAny<Person> ()), Times.Never);
        }

        public void Dispose ()
        {
            _client.Dispose ();
            _factory.Dispose ();
        }
    }
}
