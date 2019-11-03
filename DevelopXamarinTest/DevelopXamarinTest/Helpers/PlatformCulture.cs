﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DevelopXamarinTest.Helpers
{
    public class PlatformCulture
    {
        public PlatformCulture(string platformCultureString)
        {
            if (string.IsNullOrEmpty(platformCultureString))
            {
                throw new ArgumentException("Expected culture identifier", "platformCultureString");
            }

            PlatformString = platformCultureString.Replace("_", "-");
            var dashIndex = PlatformString.IndexOf("-", StringComparison.Ordinal);
            if (dashIndex > 0)
            {
                var parts = PlatformString.Split('-');
                LanguageCode = parts[0];
                LocalCode = parts[1];
            }
            else
            {
                LanguageCode = PlatformString;
                LocalCode = "";
            }
        }


        public string PlatformString { get; private set; }
        public string LanguageCode { get; private set; }
        public string LocalCode { get; private set; }

        public override string ToString()
        {
            return PlatformString;
        }

    }
}
