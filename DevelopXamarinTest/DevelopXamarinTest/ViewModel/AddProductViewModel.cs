﻿using DevelopXamarinTest.Common.Models;
using DevelopXamarinTest.Helpers;
using DevelopXamarinTest.Services;
using GalaSoft.MvvmLight.Command;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DevelopXamarinTest.ViewModel
{
    public class AddProductViewModel : BaseViewModel
    {
        #region Attritutes
        private MediaFile file;

        private ImageSource _imageSource;

        private ApiServices _apiServices;

        private bool _isRunning;

        private bool _isEnabled;
        #endregion

        #region Properties
        public string Description { get; set; }

        public string Price { get; set; }

        public string Remarks { get; set; }

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
        public AddProductViewModel()
        {
            this.IsEnabled = true;
            this._apiServices = new ApiServices();
            this.ImageSource = "noproduct";
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
            if (string.IsNullOrEmpty(this.Description))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorLbl,
                    Languages.DescriptionErrorLbl,
                    Languages.AcceptBtn);
                return;
            }

            if (string.IsNullOrEmpty(this.Price))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorLbl,
                    Languages.PriceErrorLbl,
                    Languages.AcceptBtn);
                return;
            }

            var price = decimal.Parse(this.Price);

            if (price < 0)
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
            if (this.file != null)
                imageArray = FileHelper.ReadFully(this.file.GetStream());

            var product = new Product()
            {
                Description = this.Description,
                Price = price,
                Remarks = this.Remarks,
                ImageArray = imageArray
            };

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var api = Application.Current.Resources["Api"].ToString();
            var controller = Application.Current.Resources["ControllerProducts"].ToString();
            var response = await this._apiServices.Post(url, api, controller, product);

            if (!response.IsSuccess)
            {
                //UserDialogs.Instance.HideLoading();
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.ErrorLbl, response.Message, Languages.AcceptBtn);
                return;
            }
            //UserDialogs.Instance.HideLoading();
            this.IsRunning = false;
            this.IsEnabled = true;

            var newProduct = (Product)response.Result;

            //if (newProduct.ValidationSet.Length > 0 || )
            //{
            //    await Application.Current.MainPage.DisplayAlert(Languages.ErrorLbl, newProduct.ValidationSet, Languages.AcceptBtn);
            //}

            var productsViewModel = ProductsViewModel.GetInstance();

            productsViewModel.MyProducts.Add(newProduct);
            productsViewModel.RefreshList();

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
                this.file = null;
                return;
            }

            if (source == Languages.NewPictureLbl)
            {
                this.file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        PhotoSize = PhotoSize.Small,
                    });
            }
            else
            {
                this.file = await CrossMedia.Current.PickPhotoAsync();
            }

            if (this.file != null)
            {
                this.ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = this.file.GetStream();
                    return stream;
                });
            }
        }
        #endregion
    }
}
