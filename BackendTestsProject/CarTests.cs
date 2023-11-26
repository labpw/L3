using Moq;
using P06Shop.Shared.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestsProject
{
    public class CarTest : IDisposable
    {
        private CustomWebApplicationFactory _factory;
        private HttpClient _client;

        public CarTest ()
        {
            _factory = new CustomWebApplicationFactory ();
            _client = _factory.CreateClient ();
        }

        [Fact]
        public async void postCar_ProperData ()
        {
            var brand = new CarBrand ("nazwa", "kraj");
            var person = new Person (1, "imie", "000111222");
            var newCar = new Car (brand, person, "cos", 2);

            _factory.BrandRepositoryMock.Setup (r => r.GetAllCarBrads()).Returns(new List<CarBrand> { brand });
            _factory.PersonRepositoryMock.Setup (r => r.GetAllPerson()).Returns(new List<Person> { person });
            _factory.CarRepositoryMock.Setup(r => r.AddCar (It.Is<Car>(c => c.CarBrand.Equals(brand) && c.PreviousOwner.Equals(person) 
                                                                        && c.Model == "cos" && c.Power == 2))).Verifiable ();

            var response = await _client.PostAsync ("api/Car", JsonContent.Create (newCar));

            Assert.Equal (HttpStatusCode.Created, response.StatusCode);

            _factory.CarRepositoryMock.VerifyAll ();
        }

        [Fact]
        public async void postCar_NotExistingOwner ()
        {
            var brand = new CarBrand ("nazwa", "kraj");
            var newCar = new Car (brand, null, "cos", 2);

            _factory.BrandRepositoryMock.Setup (r => r.GetAllCarBrads ()).Returns (new List<CarBrand> { brand });
            _factory.PersonRepositoryMock.Setup (r => r.GetAllPerson ()).Returns (new List<Person> { });
            _factory.CarRepositoryMock.Setup (r => r.AddCar (It.Is<Car> (c => c.CarBrand.Equals (brand) && c.Model == "cos" && c.Power == 2))).Verifiable ();

            var response = await _client.PostAsync ("api/Car", JsonContent.Create (newCar));

            Assert.Equal (HttpStatusCode.BadRequest, response.StatusCode);

            _factory.CarRepositoryMock.Verify (r => r.AddCar (It.IsAny<Car> ()), Times.Never);
        }

        [Fact]
        public async void postCar_NotExistingBrand ()
        {
            var person = new Person (1, "imie", "000111222");
            var newCar = new Car (null, person, "cos", 2);

            _factory.BrandRepositoryMock.Setup (r => r.GetAllCarBrads ()).Returns (new List<CarBrand> { });
            _factory.PersonRepositoryMock.Setup (r => r.GetAllPerson ()).Returns (new List<Person> { person });
            _factory.CarRepositoryMock.Setup (r => r.AddCar (It.Is<Car> (c => c.PreviousOwner.Equals (person) && c.Model == "cos" && c.Power == 2))).Verifiable ();

            var response = await _client.PostAsync ("api/Car", JsonContent.Create (newCar));

            Assert.Equal (HttpStatusCode.BadRequest, response.StatusCode);

            _factory.CarRepositoryMock.Verify (r => r.AddCar (It.IsAny<Car> ()), Times.Never);
        }

        [Fact]
        public async void postCar_NullModelAttributes ()
        {
            var newCar = new Car (null, null, "cos", 2);

            _factory.BrandRepositoryMock.Setup (r => r.GetAllCarBrads ()).Returns (new List<CarBrand> { });
            _factory.PersonRepositoryMock.Setup (r => r.GetAllPerson ()).Returns (new List<Person> { });
            _factory.CarRepositoryMock.Setup (r => r.AddCar (It.Is<Car> (c => c.Model == "cos" && c.Power == 2))).Verifiable ();

            var response = await _client.PostAsync ("api/Car", JsonContent.Create (newCar));

            Assert.Equal (HttpStatusCode.BadRequest, response.StatusCode);

            _factory.CarRepositoryMock.Verify (r => r.AddCar (It.IsAny<Car> ()), Times.Never);
        }

        [Fact]
        public async void putCar_ProperIdProperData ()
        {
            var brand = new CarBrand ("nazwa", "kraj");
            var person = new Person (1, "imie", "000111222");
            var car = new Car (brand, person, "cos", 2);
            var newCar = new Car (brand, person, "new cos", 5);

            _factory.BrandRepositoryMock.Setup (r => r.GetAllCarBrads ()).Returns (new List<CarBrand> { brand });
            _factory.PersonRepositoryMock.Setup (r => r.GetAllPerson ()).Returns (new List<Person> { person });

            _factory.CarRepositoryMock.Setup (r => r.GetCarById (1)).Returns (car);
            _factory.CarRepositoryMock.Setup (r => r.UpdateCar (It.Is<Car> (c => c.CarBrand.Equals (brand) && c.PreviousOwner.Equals (person)
                                                                        && c.Model == "new cos" && c.Power == 5))).Verifiable ();

            var response = await _client.PutAsync ("api/Car/1", JsonContent.Create (newCar));

            Assert.Equal (HttpStatusCode.OK, response.StatusCode);

            _factory.CarRepositoryMock.VerifyAll ();
        }

        [Fact]
        public async void putCar_WrongIdProperData ()
        {
            var brand = new CarBrand ("nazwa", "kraj");
            var person = new Person (1, "imie", "000111222");
            var newCar = new Car (brand, person, "new cos", 5);

            _factory.BrandRepositoryMock.Setup (r => r.GetAllCarBrads ()).Returns (new List<CarBrand> { brand });
            _factory.PersonRepositoryMock.Setup (r => r.GetAllPerson ()).Returns (new List<Person> { person });

            _factory.CarRepositoryMock.Setup (r => r.UpdateCar (It.Is<Car> (c => c.CarBrand.Equals (brand) && c.PreviousOwner.Equals (person)
                                                                        && c.Model == "new cos" && c.Power == 5))).Verifiable ();

            var response = await _client.PutAsync ("api/Car/1", JsonContent.Create (newCar));

            Assert.Equal (HttpStatusCode.BadRequest, response.StatusCode);

            _factory.CarRepositoryMock.Verify (r => r.UpdateCar(It.IsAny<Car>()), Times.Never);
        }

        [Fact]
        public async void putCar_WrongIdWrongData ()
        {
            var newCar = new Car (null, null, "new cos", 5);

            _factory.BrandRepositoryMock.Setup (r => r.GetAllCarBrads ()).Returns (new List<CarBrand> { });
            _factory.PersonRepositoryMock.Setup (r => r.GetAllPerson ()).Returns (new List<Person> { });

            _factory.CarRepositoryMock.Setup (r => r.UpdateCar (It.Is<Car> (c => c.Model == "new cos" && c.Power == 5))).Verifiable ();

            var response = await _client.PutAsync ("api/Car/1", JsonContent.Create (newCar));

            Assert.Equal (HttpStatusCode.BadRequest, response.StatusCode);

            _factory.CarRepositoryMock.Verify (r => r.UpdateCar (It.IsAny<Car> ()), Times.Never);
        }

        [Fact]
        public async void deleteCar_ProperId ()
        {
            var brand = new CarBrand ("nazwa", "kraj");
            var person = new Person (1, "imie", "000111222");
            var car = new Car (brand, person, "cos", 2);

            _factory.BrandRepositoryMock.Setup (r => r.GetAllCarBrads ()).Returns (new List<CarBrand> { brand });
            _factory.PersonRepositoryMock.Setup (r => r.GetAllPerson ()).Returns (new List<Person> { person });

            _factory.CarRepositoryMock.Setup (r => r.GetCarById (1)).Returns (car);
            _factory.CarRepositoryMock.Setup (r => r.DeleteCar (It.Is<Car> (c => c.CarBrand.Equals (brand) && c.PreviousOwner.Equals (person)
                                                                        && c.Model == "cos" && c.Power == 2))).Verifiable ();

            var response = await _client.DeleteAsync ("api/Car/1");

            Assert.Equal (HttpStatusCode.NoContent, response.StatusCode);

            _factory.CarRepositoryMock.VerifyAll ();
        }

        [Fact]
        public async void deleteCar_WrongId ()
        {
            var response = await _client.DeleteAsync ("api/Car/1");

            Assert.Equal (HttpStatusCode.BadRequest, response.StatusCode);

            _factory.CarRepositoryMock.Verify (r => r.DeleteCar (It.IsAny<Car> ()), Times.Never);
        }

        public void Dispose ()
        {
            _client.Dispose ();
            _factory.Dispose ();
        }
    }
}
