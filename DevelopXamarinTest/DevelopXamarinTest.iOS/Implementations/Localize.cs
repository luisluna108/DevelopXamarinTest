using System.Globalization;
using System.Threading;
using DevelopXamarinTest.Helpers;
using DevelopXamarinTest.Interfaces;
using Foundation;

[assembly: Xamarin.Forms.Dependency(typeof(DevelopXamarinTest.iOS.Implementations.Localize))]

namespace DevelopXamarinTest.iOS.Implementations
{
    public class Localize : ILocalize
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            var netLanguage = "en";
            if (NSLocale.PreferredLanguages.Length > 0)
            {
                var pref = NSLocale.PreferredLanguages[0];
                netLanguage = iOSToDotnetLanguage(pref);
            }

            CultureInfo ci = null;

            try
            {
                ci = new CultureInfo(netLanguage);
            }
            catch (CultureNotFoundException e)
            {
                try
                {
                    var fallback = ToDotnetFallbackLanguage(new PlatformCulture(netLanguage));
                }
                catch (CultureNotFoundException e2)
                {
                    ci = new CultureInfo("en");
                }

            }
            return ci;
        }

        public void SetLocale(CultureInfo ci)
        {
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }

        string iOSToDotnetLanguage(string iOSLanguage)
        {
            var netLanguage = iOSLanguage;
            switch (iOSLanguage)
            {
                case "ms-MY":
                case "ms-SG":
                    netLanguage = "ms";
                    break;
                case "gsw-CH":
                    netLanguage = "de-CH";
                    break;
            }
            return netLanguage;
        }

        string ToDotnetFallbackLanguage(PlatformCulture platCulture)
        {
            var netLanguage = platCulture.LanguageCode;
            switch (platCulture.LanguageCode)
            {
                case "pt":
                    netLanguage = "pt-PT";
                    break;
            }

            return netLanguage;
        }
    }
}