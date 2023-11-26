using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using P05Shop.API.Repositories;
using P05Shop.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestsProject
{
    public class CustomWebApplicationFactory: WebApplicationFactory<Program>
    {
        public Mock<ICarRepository> CarRepositoryMock { get; }
        public Mock<ICarBrandRepository> BrandRepositoryMock { get; }
        public Mock<IPersonRepository> PersonRepositoryMock { get; }

        public CustomWebApplicationFactory ()
        {
            CarRepositoryMock = new Mock<ICarRepository>();
            BrandRepositoryMock = new Mock<ICarBrandRepository>();
            PersonRepositoryMock = new Mock<IPersonRepository>();
        }

        protected override void ConfigureWebHost (IWebHostBuilder builder)
        {
            base.ConfigureWebHost (builder);

            builder.ConfigureTestServices (services =>
            {
                services.AddSingleton (CarRepositoryMock.Object);
                services.AddSingleton (BrandRepositoryMock.Object);
                services.AddSingleton (PersonRepositoryMock.Object);
            });
        }
    }
}
