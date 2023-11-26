using Moq;
using P06Shop.Shared.Cars;
using System.Net.Http.Json;
using System.Net;
using P06Shop.Shared;

namespace BackendTestsProject
{
    public class BrandTest : IDisposable
    {
        private CustomWebApplicationFactory _factory;
        private HttpClient _client;

        public BrandTest ()
        {
            _factory = new CustomWebApplicationFactory ();
            _client = _factory.CreateClient ();
        }

        [Fact]
        public async void postBrand_ProperData ()
        {
            var newBrand = new CarBrand ("imie", "kraj");

            _factory.BrandRepositoryMock.Setup (r => r.AddCarBrand (It.Is<CarBrand> (cb => cb.Name == "imie" && cb.OriginCountry == "kraj"))).Verifiable();

            var response = await _client.PostAsync ("api/CarBrand", JsonContent.Create (newBrand));

            Assert.Equal (HttpStatusCode.Created, response.StatusCode);

            _factory.BrandRepositoryMock.VerifyAll ();
        }

        [Fact]
        public async void postBrand_EmptyName ()
        {
            var newBrand = new CarBrand (null, "kraj");

            _factory.BrandRepositoryMock.Setup (r => r.AddCarBrand (It.Is<CarBrand> (cb => cb.Name == null && cb.OriginCountry == "kraj"))).Verifiable ();

            var response = await _client.PostAsync ("api/CarBrand", JsonContent.Create (newBrand));

            Assert.Equal (HttpStatusCode.BadRequest, response.StatusCode);

            _factory.BrandRepositoryMock.Verify (r => r.AddCarBrand (It.IsAny<CarBrand> ()), Times.Never);
        }

        [Fact]
        public async void postBrand_EmtpyCountryOfOrigin ()
        {
            var newBrand = new CarBrand ("imie", null);

            _factory.BrandRepositoryMock.Setup (r => r.AddCarBrand (It.Is<CarBrand> (cb => cb.Name == "imie" && cb.OriginCountry == null))).Verifiable ();

            var response = await _client.PostAsync ("api/CarBrand", JsonContent.Create (newBrand));

            Assert.Equal (HttpStatusCode.BadRequest, response.StatusCode);

            _factory.BrandRepositoryMock.Verify (r => r.AddCarBrand (It.IsAny<CarBrand> ()), Times.Never);
        }

        [Fact]
        public async void postBrnad_EmptyAllInputs ()
        {
            var newBrand = new CarBrand (null, null);

            _factory.BrandRepositoryMock.Setup (r => r.AddCarBrand (It.Is<CarBrand> (cb => cb.Name == null && cb.OriginCountry == null))).Verifiable ();

            var response = await _client.PostAsync ("api/CarBrand", JsonContent.Create (newBrand));

            Assert.Equal (HttpStatusCode.BadRequest, response.StatusCode);

            _factory.BrandRepositoryMock.Verify (r => r.AddCarBrand (It.IsAny<CarBrand> ()), Times.Never);
        }

        [Fact]
        public async void putBrand_ProperIdProperMod ()
        {
            var brand = new CarBrand ("imie", "kraj");
            var newBrand = new CarBrand ("nowe imie", "nowy kraj");

            _factory.BrandRepositoryMock.Setup (r => r.GetCarBrandById (1)).Returns (brand);
            _factory.BrandRepositoryMock.Setup (r => r.UpdateCarBrand (It.Is<CarBrand> (cb => cb.Name == "nowe imie" 
                                                                                        && cb.OriginCountry == "nowy kraj"))).Verifiable ();

           var response = await _client.PutAsync ("api/CarBrand/1", JsonContent.Create (newBrand));

            Assert.Equal (HttpStatusCode.OK, response.StatusCode);

            _factory.BrandRepositoryMock.VerifyAll ();
        }

        [Fact]
        public async void putBrand_WrongIdProperMod ()
        {
            var newBrand = new CarBrand ("nowe imie", "nowy kraj");

            _factory.BrandRepositoryMock.Setup (r => r.UpdateCarBrand (It.Is<CarBrand> (cb => cb.Name == "nowe imie"
                                                                                        && cb.OriginCountry == "nowy kraj"))).Verifiable ();

            var response = await _client.PutAsync ("api/CarBrand/1", JsonContent.Create (newBrand));

            Assert.Equal (HttpStatusCode.BadRequest, response.StatusCode);

            _factory.BrandRepositoryMock.Verify (r => r.UpdateCarBrand (It.IsAny<CarBrand> ()), Times.Never);
        }

        [Fact]
        public async void putBrand_WrongIdWrongMod ()
        {
            var newBrand = new CarBrand (null, null);

            _factory.BrandRepositoryMock.Setup (r => r.UpdateCarBrand (It.Is<CarBrand> (cb => cb.Name == null
                                                                                        && cb.OriginCountry == null))).Verifiable ();

            var response = await _client.PutAsync ("api/CarBrand/1", JsonContent.Create (newBrand));

            Assert.Equal (HttpStatusCode.BadRequest, response.StatusCode);

            _factory.BrandRepositoryMock.Verify (r => r.UpdateCarBrand (It.IsAny<CarBrand> ()), Times.Never);
        }

        [Fact]
        public async void deleteBrand_ProperId ()
        {
            var brand = new CarBrand ("imie", "kraj");

            _factory.BrandRepositoryMock.Setup (r => r.GetCarBrandById (1)).Returns (brand);
            _factory.BrandRepositoryMock.Setup (r => r.DeleteCarBrand (It.Is<CarBrand> (cb => cb.Name == "imie" && cb.OriginCountry == "kraj"))).Verifiable ();

            var response = await _client.DeleteAsync ("api/CarBrand/1");

            Assert.Equal (HttpStatusCode.NoContent, response.StatusCode);

            _factory.BrandRepositoryMock.VerifyAll ();
        }

        [Fact]
        public async void deleteBrand_WrongId ()
        {
            var response = await _client.DeleteAsync ("api/CarBrand/1");

            Assert.Equal (HttpStatusCode.BadRequest, response.StatusCode);

            _factory.BrandRepositoryMock.Verify (r => r.DeleteCarBrand (It.IsAny<CarBrand> ()), Times.Never);
        }

        public void Dispose ()
        {
            _client.Dispose ();
            _factory.Dispose ();
        }
    }
}