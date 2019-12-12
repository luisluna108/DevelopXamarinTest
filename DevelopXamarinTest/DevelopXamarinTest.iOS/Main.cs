using System;
using System.Collections.Generic;
using System.Linq;
//using Acr.UserDialogs;
using Foundation;
using UIKit;

namespace DevelopXamarinTest.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            //if (UserDialogs.Instance == null)
            //    UserDialogs.Init(this);

            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
