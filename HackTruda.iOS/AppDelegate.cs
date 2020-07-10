using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Autofac;
using Firebase.CloudMessaging;
using Foundation;
using HackTruda.Containers;
using HackTruda.Definitions.Models;
using HackTruda.DependencyServices.Interfaces;
using HackTruda.iOS.DependencyServices;
using HackTruda.Services.Interfaces;
using Newtonsoft.Json;
using UIKit;
using UserNotifications;

namespace HackTruda.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : Xamarin.Forms.Platform.iOS.FormsApplicationDelegate,
        IUNUserNotificationCenterDelegate, IMessagingDelegate
    {
        private IPlatformPushNotificationService _platformPushNotificationService;
        private IDebuggerService _debuggerService;

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            // Styles block
            //UITabBar.Appearance.SelectedImageTintColor = UIColor.FromRGB(90, 136, 255);

            //UITabBarItem.Appearance.SetTitleTextAttributes(
            //    new UITextAttributes
            //    {
            //        TextColor = UIColor.FromRGB(90, 136, 255),
            //    },
            //    UIControlState.Selected);

            UINavigationBar.Appearance.TintColor = UIColor.Black;

            Rg.Plugins.Popup.Popup.Init();

            Xamarin.Forms.Forms.Init();

            _platformPushNotificationService = new PlatformPushNotificationService();

            IocInitializer.Initialize(
                new PlatformAlertMessageService(),
                _platformPushNotificationService);

            _debuggerService = IocInitializer.Container.Resolve<IDebuggerService>();

            // Init nugets
            XamEffects.iOS.Effects.Init();

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();

            Plugin.Segmented.Control.iOS.SegmentedControlRenderer.Initialize();

            Lottie.Forms.iOS.Renderers.AnimationViewRenderer.Init();

            //Firebase.Core.App.Configure();
            //Firebase.Crashlytics.Crashlytics.Configure();

            Hackiftekhar.IQKeyboardManager.Xamarin.IQKeyboardManager.SharedManager().Enable = true;

            LoadApplication(new App());

            //RegisterForNotifications();

            return base.FinishedLaunching(app, options);
        }

        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            if (Xamarin.Essentials.Platform.OpenUrl(app, url, options))
            {
                return true;
            }

            return base.OpenUrl(app, url, options);
        }

        //[Export("messaging:didReceiveRegistrationToken:")]
        //public void DidReceiveRegistrationToken(Messaging messaging, string fcmToken)
        //{
        //    _debuggerService?.Log($"Received FCM Token: {fcmToken}");
        //}

        //public override async void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        //{
        //    Messaging.SharedInstance.ApnsToken = deviceToken;

        //    _debuggerService?.Log($"Registered for remote notifications with APNS token {deviceToken?.DebugDescription}");
        //    _debuggerService?.Log($"Registered for remote notifications with Messaging.SharedInstance FCM token {Messaging.SharedInstance.FcmToken}");
        //}

        //public void DidRefreshRegistrationToken(Messaging messaging, string fcmToken)
        //{
        //    _debuggerService?.Log($"Refreshed FCM Token: {fcmToken}");
        //}

        //public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        //{
        //    _debuggerService?.Log($"Error registering push notifications: {error?.LocalizedDescription}");
        //}

        //[Export("userNotificationCenter:DidReceiveRemoteNotification:withCompletionHandler:")]
        //public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        //{
        //    completionHandler(UIBackgroundFetchResult.NewData);

        //    _debuggerService?.Log($"Received background remote notification: {userInfo}");

        //    try
        //    {
        //        NSData data = NSJsonSerialization.Serialize(userInfo, 0, out NSError err);

        //        PushNotificationData pushNotificationData = JsonConvert.DeserializeObject<PushNotificationData>(data?.ToString());

        //        if (data == null)
        //        {
        //            return;
        //        }

        //        if (pushNotificationData.IsShow)
        //        {
        //            _platformPushNotificationService.ShowNotification(pushNotificationData);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        _debuggerService?.Log(e);
        //    }
        //}

        //[Export("userNotificationCenter:willPresentNotification:withCompletionHandler:")]
        //public void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        //{
        //    completionHandler(UNNotificationPresentationOptions.Alert |
        //                      UNNotificationPresentationOptions.Sound |
        //                      UNNotificationPresentationOptions.Badge);

        //    _debuggerService?.Log($"Notification to present: {notification}");
        //}

        //private void RegisterForNotifications()
        //{
        //    // Note: this registration logic works only for iOS 10 and above
        //    UNAuthorizationOptions authOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;

        //    UNUserNotificationCenter.Current.RequestAuthorization(authOptions, (granted, error) =>
        //    {
        //        _debuggerService?.Log($"Granted: {granted}, Error: {error?.LocalizedDescription}");
        //    });

        //    UNUserNotificationCenter.Current.Delegate = this;

        //    Messaging.SharedInstance.Delegate = this;

        //    UIApplication.SharedApplication.RegisterForRemoteNotifications();
        //}
    }
}
