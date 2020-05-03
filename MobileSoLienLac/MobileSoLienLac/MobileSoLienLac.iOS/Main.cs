using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using UserNotifications;

namespace MobileSoLienLac.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");

            // Request Permissions
            //if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            //{
            //    // Request Permissions
            //    UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound, (granted, error) =>
            //    {
            //        // Deal with the problem
            //    });
            //}
            //else if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            //{
            //    var notificationSettings = UIUserNotificationSettings.GetSettingsForTypes(
            //        UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, null);
            //    app.RegisterUserNotificationSettings(notificationSettings);
            //}
        }
    }
}
