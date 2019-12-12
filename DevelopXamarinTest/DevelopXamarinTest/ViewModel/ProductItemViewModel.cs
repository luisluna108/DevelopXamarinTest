using DevelopXamarinTest.Common.Models;
using DevelopXamarinTest.Helpers;
using DevelopXamarinTest.Services;
using DevelopXamarinTest.View;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DevelopXamarinTest.ViewModel
{
    public class ProductItemViewModel : Product
    {
        #region Attributes
        private ApiServices _apiServices;
        #endregion

        #region Constructor
        public ProductItemViewModel()
        {
            this._apiServices = new ApiServices();
        }
        #endregion

   

        #region Command

        public ICommand EditProductCommand
        {
            get
            {
                return new RelayCommand(EditProduct);
            }
        }

        public ICommand DeleteProductCommand
        {
            get
            {
                return new RelayCommand(DeleteProduct);
            }
        }

        private async void EditProduct()
        {
            MainViewModel.GetInstance().EditProduct = new EditProductViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new EditProductPage());
        }

        private async void DeleteProduct()
        {

            var anwser = await Application.Current.MainPage.DisplayAlert(Languages.ConfirmLbl, Languages.DeleteConfirmLbl, Languages.YesLbl, Languages.NoLbl);
            if (!anwser)
            {
                return;
            }

            var connection = await this._apiServices.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.ErrorLbl, connection.Message, Languages.AcceptBtn);
                return;
            }

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var api = Application.Current.Resources["Api"].ToString();
            var controller = Application.Current.Resources["ControllerProducts"].ToString();
            var response = await this._apiServices.Delete(url, api, controller, this.ProductId);
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.ErrorLbl, response.Message, Languages.AcceptBtn);
                return;
            }

            var productsViewModel = ProductsViewModel.GetInstance();
            var deleteProduct = productsViewModel.Products.Where(p => p.ProductId == this.ProductId).FirstOrDefault();
            if (deleteProduct != null)
            {
                productsViewModel.Products.Remove(deleteProduct);
            }

            await Application.Current.MainPage.DisplayAlert(Languages.SaveLbl, Languages.DescriptionSaveLbl, Languages.AcceptBtn);

        }
        #endregion
    }
}
