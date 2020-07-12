using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Plugin.Permissions;
using HackTruda.Containers;
using HackTruda.Droid.DependencyServices;
using Plugin.CurrentActivity;
using Android.Content;
using Android;

namespace HackTruda.Droid
{
    [Activity(
        Label = "МигРу",
        Icon = "@mipmap/ic_launcher",
        RoundIcon = "@mipmap/ic_launcher",
        Theme = "@style/MainTheme",
        MainLauncher = false,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static readonly string NotificationChannelId = CrossCurrentActivity.Current.AppContext.PackageName;

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
            }
        }

        protected override void OnResume()
        {
            base.OnResume();

            Xamarin.Essentials.Platform.OnResume();
        }

        protected override void OnStart()
        {
            base.OnStart();

            //this.ApplicationContext.ApplicationInfo.MetaData
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);

            Xamarin.Forms.Forms.Init(this, savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            IocInitializer.Initialize(
                new PlatformAlertMessageService(),
                new PlatformPushNotificationService());

            // Init nugets
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            XamEffects.Droid.Effects.Init();

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);

            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            Plugin.Fingerprint.CrossFingerprint.SetCurrentActivityResolver(() => CrossCurrentActivity.Current.Activity);

            Fabric.Fabric.With(this, new Crashlytics.Crashlytics());
            Crashlytics.Crashlytics.HandleManagedExceptions();

            Xamarin.FormsMaps.Init(this, savedInstanceState);

            PanCardView.Droid.CardsViewRenderer.Preserve();

            LoadApplication(new App());

            CreateNotificationChannel();
        }

        private void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                return;
            }

            NotificationManager notificationManager = CrossCurrentActivity.Current.AppContext.GetSystemService(Context.NotificationService) as NotificationManager;

            var channel = new NotificationChannel(
                NotificationChannelId,
                $"{ApplicationContext.PackageName} notification channel",
                NotificationImportance.Default)
            {
                Description = "Local and remote push notifications appears in this channel",
            };

            notificationManager?.CreateNotificationChannel(channel);
        }
    }
}