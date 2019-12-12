using DevelopXamarinTest.Common.Models;
using DevelopXamarinTest.Helpers;
using DevelopXamarinTest.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace DevelopXamarinTest.ViewModel
{
    public class ProductsViewModel : BaseViewModel
    {

        #region Attributes
        private ApiServices _apiServices;

        private ObservableCollection<ProductItemViewModel> _products;

        private bool _isRefreshing;
        #endregion

        #region Properties

        public List<Product> MyProducts { get; set; }

        public ObservableCollection<ProductItemViewModel> Products
        {
            get { return _products; }
            set { SetValue(ref _products, value); }
        }

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetValue(ref _isRefreshing, value); }
        }
        #endregion

        #region Constructors
        public ProductsViewModel()
        {
            instance = this;
            this._apiServices = new ApiServices();
            this.LoadProducts();
        }
        #endregion

        #region Singleton
        private static ProductsViewModel instance;

        public static ProductsViewModel GetInstance()
        {
            if (instance == null)
            {
                return new ProductsViewModel();
            }

            return instance;
        }
        #endregion

        #region Methods
        private async void LoadProducts()
        {
            //UserDialogs.Init(() => Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity);

            //UserDialogs.Instance.ShowLoading(Languages.LoadingLbl, MaskType.Black);
            IsRefreshing = true;

            var connection = await this._apiServices.CheckConnection();

            if (!connection.IsSuccess)
            {
                //UserDialogs.Instance.HideLoading();
                IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.ErrorLbl, connection.Message, Languages.AcceptBtn);
                return;
            }

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var api = Application.Current.Resources["Api"].ToString();
            var controller = Application.Current.Resources["ControllerProducts"].ToString();
            var response = await this._apiServices.GetList<Product>(url, api, controller);
            if (!response.IsSuccess)
            {
                //UserDialogs.Instance.HideLoading();
                IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.ErrorLbl, response.Message, Languages.AcceptBtn);
                return;
            }

            this.MyProducts = (List<Product>)response.Result;
            this.RefreshList();
           

            //UserDialogs.Instance.HideLoading();
            IsRefreshing = false;
        }

        public void RefreshList()
        {
            var myListProductItemViewModel = this.MyProducts.Select(p => new ProductItemViewModel
            {
                Description = p.Description,
                ImageArray = p.ImageArray,
                ImagePath = p.ImagePath,
                IsAvailable = p.IsAvailable,
                Price = p.Price,
                ProductId = p.ProductId,
                PublishdOn = p.PublishdOn,
                Remarks = p.Remarks,
                ValidationSet = p.ValidationSet
            });

            this.Products = new ObservableCollection<ProductItemViewModel>(
                myListProductItemViewModel.OrderBy(p => p.Description));

            IsRefreshing = false;
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get { return new RelayCommand(LoadProducts); }
        }
        #endregion

    }
}
