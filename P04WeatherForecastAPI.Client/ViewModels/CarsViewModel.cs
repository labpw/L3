using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using P06Shop.Shared.Cars;
using P06Shop.Shared.Services.CarService;
using P06Shop.Shared.Services.ProductService;
using P06Shop.Shared.Shop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace P04WeatherForecastAPI.Client.ViewModels
{
    public partial class CarsViewModel : ObservableObject
    {
        private readonly ICarService _carService;

        public ObservableCollection<Car> Cars { get; set; }

        private int _currentPage = 1;
        private int _pageSize = 10;
        public int TotalCars { get; set; }

        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                GetCars();
                IsNextPageEnabled = CurrentPage * _pageSize < TotalCars;
                IsPreviousPageEnabled = CurrentPage > 1;
                OnPropertyChanged();
            }
        }

        public CarsViewModel(ICarService carService)
        {
            _carService = carService;
            Cars = new ObservableCollection<Car>();
        }

        public ICommand NextPageCommand => new RelayCommand(() => CurrentPage++);
        public ICommand PreviousPageCommand => new RelayCommand(() => CurrentPage--);

        [ObservableProperty]
        public bool isNextPageEnabled = true;
        [ObservableProperty]
        public bool isPreviousPageEnabled = false;

        public async void GetCars()
        {
            var carsResult = await _carService.GetCarsAsync();
            TotalCars = carsResult.Data.Count();
            IsNextPageEnabled = CurrentPage * _pageSize < TotalCars;
            var allCars = new List<Car>();
            if (carsResult.Success)
            {
                Cars.Clear();
                foreach (var p in carsResult.Data)
                {                 
                    allCars.Add(p);
                }
                for (int i = (CurrentPage - 1) * _pageSize, j = 0; i < allCars.Count && j < _pageSize; i++, j++)
                {
                    Cars.Add(allCars[i]);
                }
            }
           
        }
    }
}
