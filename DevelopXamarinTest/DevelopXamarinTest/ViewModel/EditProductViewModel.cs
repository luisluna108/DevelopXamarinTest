using DevelopXamarinTest.Common.Models;
using DevelopXamarinTest.Helpers;
using DevelopXamarinTest.Services;
using GalaSoft.MvvmLight.Command;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DevelopXamarinTest.ViewModel
{
    public class EditProductViewModel : BaseViewModel
    {
        #region Attributes
        private Product _product;

        private MediaFile _file;

        private ImageSource _imageSource;

        private ApiServices _apiServices;

        private bool _isRunning;

        private bool _isEnabled;
        #endregion

        #region Properties
        public Product Product
        {
            get { return this._product; }
            set { this.SetValue(ref this._product, value); }
        }

        public bool IsRunning
        {
            get { return _isRunning; }
            set { this.SetValue(ref this._isRunning, value); }
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { this.SetValue(ref this._isEnabled, value); }
        }

        public ImageSource ImageSource
        {
            get { return _imageSource; }
            set { this.SetValue(ref this._imageSource, value); }
        }
        #endregion

        #region Constructors
        public EditProductViewModel(Product product)
        {
            this._product = product;
            this.IsEnabled = true;
            this._apiServices = new ApiServices();
            this.ImageSource = product.ImageFullPath;
        }
        #endregion

        #region Commands
        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }


        public ICommand ChanceImageCommand
        {
            get
            {
                return new RelayCommand(ChanceImage);
            }
        }


        #endregion

        #region Methods


        private async void Save()
        {
            if (string.IsNullOrEmpty(this.Product.Description))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorLbl,
                    Languages.DescriptionErrorLbl,
                    Languages.AcceptBtn);
                return;
            }

            if (this.Product.Price < 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorLbl,
                    Languages.PriceErrorLbl,
                    Languages.AcceptBtn);
                return;
            }
            //UserDialogs.Instance.ShowLoading(Languages.LoadingLbl, MaskType.Black);

            this.IsRunning = true;
            this.IsEnabled = false;

            var connection = await this._apiServices.CheckConnection();

            if (!connection.IsSuccess)
            {
                //UserDialogs.Instance.HideLoading();
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.ErrorLbl, connection.Message, Languages.AcceptBtn);
                return;
            }

            byte[] imageArray = null;
            if (this._file != null)
            {
                imageArray = FileHelper.ReadFully(this._file.GetStream());
                this.Product.ImageArray = imageArray;
            }

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var api = Application.Current.Resources["Api"].ToString();
            var controller = Application.Current.Resources["ControllerProducts"].ToString();
            var response = await this._apiServices.Put(url, api, controller, this.Product, this.Product.ProductId);

            if (!response.IsSuccess)
            {
                //UserDialogs.Instance.HideLoading();
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.ErrorLbl, response.Message, Languages.AcceptBtn);
                return;
            }
            //UserDialogs.Instance.HideLoading();


            var newProduct = (Product)response.Result;

            //if (newProduct.ValidationSet.Length > 0 || )
            //{
            //    await Application.Current.MainPage.DisplayAlert(Languages.ErrorLbl, newProduct.ValidationSet, Languages.AcceptBtn);
            //}

            var productsViewModel = ProductsViewModel.GetInstance();

            var oldProduct = productsViewModel.MyProducts.Where(p => p.ProductId == this.Product.ProductId).FirstOrDefault();
            if (oldProduct != null)
                productsViewModel.MyProducts.Remove(oldProduct);

            productsViewModel.MyProducts.Add(newProduct);
            productsViewModel.RefreshList();

            this.IsRunning = false;
            this.IsEnabled = true;

            await Application.Current.MainPage.DisplayAlert(Languages.SaveLbl, Languages.DescriptionSaveLbl, Languages.AcceptBtn);
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async void ChanceImage()
        {
            await CrossMedia.Current.Initialize();

            var source = await Application.Current.MainPage.DisplayActionSheet(
                Languages.ImageSourceLbl,
                Languages.CancelBtn,
                null,
                Languages.FromGalleryLbl,
                Languages.NewPictureLbl);

            if (source == Languages.CancelBtn)
            {
                this._file = null;
                return;
            }

            if (source == Languages.NewPictureLbl)
            {
                this._file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        PhotoSize = PhotoSize.Small,
                    });
            }
            else
            {
                this._file = await CrossMedia.Current.PickPhotoAsync();
            }

            if (this._file != null)
            {
                this.ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = this._file.GetStream();
                    return stream;
                });
            }
        }
        #endregion
    }
}
