using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

namespace HackTruda.Droid
{
    [Activity(
        Label = "HackTruda",
        Icon = "@mipmap/ic_launcher",
        RoundIcon = "@mipmap/icon_round",
        Theme = "@style/Theme.Splash",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
            Finish();
        }
    }
}
