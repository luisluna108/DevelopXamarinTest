using DevelopXamarinTest.Interfaces;
using DevelopXamarinTest.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DevelopXamarinTest.Helpers
{
    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string ErrorLbl
        {
            get { return Resource.ErrorLbl; }
        }
        public static string AcceptBtn
        {
            get { return Resource.AcceptBtn; }
        }
        public static string TurnOnInternetLbl
        {
            get { return Resource.TurnOnInternetLbl; }
        }
        public static string NoInternetLbl
        {
            get { return Resource.NoInternetLbl; }
        }
        public static string ProductsLbl
        {
            get { return Resource.ProductsLbl; }
        }
        public static string AddProductLbl
        {
            get { return Resource.AddProductLbl; }
        }
        public static string DescriptionLbl
        {
            get { return Resource.DescriptionLbl; }
        }
        public static string DescriptionPlaceholderTxt
        {
            get { return Resource.DescriptionPlaceholderTxt; }
        }
        public static string PriceLbl
        {
            get { return Resource.PriceLbl; }
        }
        public static string PricePlaceholderTxt
        {
            get { return Resource.PricePlaceholderTxt; }
        }
        public static string RemarksLbl
        {
            get { return Resource.RemarksLbl; }
        }
        public static string SaveBtn
        {
            get { return Resource.SaveBtn; }
        }
        public static string ChanceImageLbl
        {
            get { return Resource.ChanceImageLbl; }
        }
        public static string DescriptionErrorLbl
        {
            get { return Resource.DescriptionErrorLbl; }
        }
        public static string PriceErrorLbl
        {
            get { return Resource.PriceErrorLbl; }
        }
        public static string DescriptionSaveLbl
        {
            get { return Resource.DescriptionSaveLbl; }
        }
        public static string SaveLbl
        {
            get { return Resource.SaveLbl; }
        }
        public static string ImageSourceLbl
        {
            get { return Resource.ImageSourceLbl; }
        }
        public static string FromGalleryLbl
        {
            get { return Resource.FromGalleryLbl; }
        }
        public static string NewPictureLbl
        {
            get { return Resource.NewPictureLbl; }
        }
        public static string CancelBtn
        {
            get { return Resource.CancelBtn; }
        }
        public static string EditLbl
        {
            get { return Resource.EditLbl; }
        }
        public static string DeleteLbl
        {
            get { return Resource.DeleteLbl; }
        }
        public static string DeleteConfirmLbl
        {
            get { return Resource.DeleteConfirmLbl; }
        }
        public static string YesLbl
        {
            get { return Resource.YesLbl; }
        }
        public static string NoLbl
        {
            get { return Resource.NoLbl; }
        }
        public static string ConfirmLbl
        {
            get { return Resource.ConfirmLbl; }
        }

        public static string LoadingLbl
        {
            get { return Resource.LoadingLbl; }
        }

        public static string EditProductLbl
        {
            get { return Resource.EditProductLbl; }
        }

        public static string IsAvailableLbl
        {
            get { return Resource.IsAvailableLbl; }
        }
    }
}
