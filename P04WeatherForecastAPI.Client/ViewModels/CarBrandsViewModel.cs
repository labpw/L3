using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using P06Shop.Shared.Cars;
using P06Shop.Shared.Services.CarService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace P04WeatherForecastAPI.Client.ViewModels
{
    public partial class CarBrandsViewModel : ObservableObject
    {
        private readonly ICarBrandService _carBrandService;

        public ObservableCollection<CarBrand> CarBrands { get; set; }

        private List<CarBrand> _toRemove;
        private int _currentPage = 1;
        private int _pageSize = 10;
        public int TotalCars { get; set; }

        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                GetCarBrands();
                IsNextPageEnabled = CurrentPage * _pageSize < TotalCars;
                IsPreviousPageEnabled = CurrentPage > 1;
                OnPropertyChanged();
            }
        }

        public CarBrandsViewModel(ICarBrandService carBrandService)
        {
            _carBrandService = carBrandService;
            CarBrands = new ObservableCollection<CarBrand>();
        }

        public ICommand NextPageCommand => new RelayCommand(() => CurrentPage++);
        public ICommand PreviousPageCommand => new RelayCommand(() => CurrentPage--);

        [ObservableProperty]
        public bool isNextPageEnabled = true;
        [ObservableProperty]
        public bool isPreviousPageEnabled = false;

        public async void GetCarBrands()
        {
            var carBrandsResult = await _carBrandService.GetCarBrandsAsync();
            TotalCars = carBrandsResult.Data.Count();
            IsNextPageEnabled = CurrentPage * _pageSize < TotalCars;
            var allCarsBrands = new List<CarBrand>();
            _toRemove = new List<CarBrand>();
            if (carBrandsResult.Success)
            {
                CarBrands.Clear();
                foreach (var p in carBrandsResult.Data)
                {
                    allCarsBrands.Add(p);
                }
                for (int i = (CurrentPage - 1) * _pageSize, j = 0; i < allCarsBrands.Count && j < _pageSize; i++, j++)
                {
                    CarBrands.Add(allCarsBrands[i]);
                }
            }

        }

        [RelayCommand]
        public async Task Save()
        {
            var carBrands = (await _carBrandService.GetCarBrandsAsync()).Data;          
            foreach (CarBrand carBrand in CarBrands)
            {
                if (carBrand.Name == string.Empty || carBrand.OriginCountry == string.Empty)
                {
                    continue;
                }

                var serverSideCarBrand = carBrands.FirstOrDefault(e => carBrand.Id == e.Id);
                if (serverSideCarBrand == null)
                {
                    await _carBrandService.CreateCarBrandAsync(carBrand);
                }
                else if(serverSideCarBrand.Name != carBrand.Name || serverSideCarBrand.OriginCountry != carBrand.OriginCountry)
                {
                    await _carBrandService.UpdateCarBrandAsync(carBrand);
                }
            }
            foreach (CarBrand carBrand in _toRemove)
            {
                await _carBrandService.DeleteCarBrandAsync(carBrand.Id);
            }
        }

        [RelayCommand]
        public void Create()
        {
            CarBrands.Insert(0, new CarBrand());
        }

        [RelayCommand]
        public void Delete(CarBrand carBrand)
        {
            _toRemove.Add(carBrand);
            CarBrands.Remove(carBrand);
        }
    }
}
