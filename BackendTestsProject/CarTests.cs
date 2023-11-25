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
            var newCar = new Car ("cos", 2);

            _factory.CarRepositoryMock.Setup(r => r.AddCar (It.Is<Car>(c => c.Model == "cos" && c.Power == 2))).Verifiable ();

            var response = await _client.PostAsync ("api/Car", JsonContent.Create (newCar));

            Assert.Equal (HttpStatusCode.Created, response.StatusCode);

            _factory.CarRepositoryMock.VerifyAll ();
        }

        [Fact]
        public void postCar_NotExistingBrand ()
        {
            
        }

        [Fact]
        public void postCar_NotExistingOwner ()
        {
            
        }

        [Fact]
        public void postCar_NullModelAttributes ()
        {
            
        }

        [Fact]
        public void putCar_ProperIdProperData ()
        {
            
        }

        [Fact]
        public void putCar_WrongIdProperData ()
        {
            
        }

        [Fact]
        public void putCar_WrongIdWrongData ()
        {
            
        }

        [Fact]
        public void deleteCar_ProperId ()
        {
            
        }

        [Fact]
        public void deleteCar_WrongId ()
        {
            
        }

        public void Dispose ()
        {
            _client.Dispose ();
            _factory.Dispose ();
        }
    }
}
