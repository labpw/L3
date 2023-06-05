using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using P04WeatherForecastAPI.Client.Configuration;
using P04WeatherForecastAPI.Client.Services.ProductServices;
using P04WeatherForecastAPI.Client.Services.WeatherServices;
using P04WeatherForecastAPI.Client.ViewModels;
using P06Shop.Shared.Services.ProductService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace P04WeatherForecastAPI.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        IServiceProvider _serviceProvider;
        IConfiguration _configuration;
        public App()
        {
            //wczytanie appsettings.json do konfiguracji 
            var builder = new ConfigurationBuilder()
              .AddUserSecrets<App>()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json");
            _configuration = builder.Build();



            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();

        }

        private void ConfigureServices(IServiceCollection services)
        {
            ConfigureAppSettings(services);

            // konfiguracja serwisów 
            ConfigureAppServices(services);

            // konfiguracja viewModeli 
            ConfigureViewModels(services);

            // konfiguracja okienek 
            ConfigureWindows(services);

            // konfiguracja HttpClient
            ConfigureHttpClients(services);
        }

        private void ConfigureAppSettings(IServiceCollection services)
        {
            var appSettings = _configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettings);
        }

        private void ConfigureAppServices(IServiceCollection services)
        {
            services.AddSingleton<IAccuWeatherService, AccuWeatherService>();
            services.AddSingleton<IFavoriteCityService, FavoriteCityService>();
            services.AddSingleton<IProductService, ProductService>();
        }

        private void ConfigureViewModels(IServiceCollection services)
        {
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<FavoriteCityViewModel>();
            services.AddSingleton<ProductsViewModel>();
            // services.AddSingleton<BaseViewModel,MainViewModelV3>();
        }

        private void ConfigureWindows(IServiceCollection services)
        {
            services.AddTransient<MainWindow>();
            services.AddTransient<FavoriteCitiesView>();
            services.AddTransient<ShopProductsView>();
        }

        private void ConfigureHttpClients(IServiceCollection services)
        {
            var appSettingsSection = _configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();

            var uriBuilder = new UriBuilder(appSettingsSection.BaseAPIUrl)
            {
                Path = appSettingsSection.BaseProductEndpoint.Base_url
            };

            services.AddHttpClient<IProductService, ProductService>(client => client.BaseAddress = uriBuilder.Uri);
          
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }

    }
}
