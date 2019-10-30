using DevelopXamarinTest.Common.Models;
using DevelopXamarinTest.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DevelopXamarinTest.ViewModel
{
    public class ProductsViewModel : BaseViewModel
    {
        private ApiServices _apiServices;

        private ObservableCollection<Product> _products;

        private bool _isRefreshing;

        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set { SetValue(ref _products, value); }
        }

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetValue(ref _isRefreshing, value); }
        }

        public ProductsViewModel()
        {
            this._apiServices = new ApiServices();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {

            IsRefreshing = true;

            var connection = await this._apiServices.CheckConnection();

            if (!connection.IsSuccess)
            {
                IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Acept");
                return;
            }

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var api = Application.Current.Resources["Api"].ToString();
            var controller = Application.Current.Resources["ControllerProducts"].ToString();
            var response = await this._apiServices.GetList<Product>(url, api, controller);
            if (!response.IsSuccess)
            {
                IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Acept");
                return;
            }

            var list = (List<Product>)response.Result;
            this.Products = new ObservableCollection<Product>(list);
            IsRefreshing = false;
        }

        public ICommand RefreshCommand
        {
            get { return new RelayCommand(LoadProducts); }
        }
    }
}
