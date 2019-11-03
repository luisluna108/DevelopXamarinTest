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
    }
}
