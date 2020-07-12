using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

namespace HackTruda.Droid
{
    [Activity(
        Label = "МигРу",
        Icon = "@mipmap/ic_launcher",
        RoundIcon = "@mipmap/ic_launcher",
        Theme = "@style/Theme.Splash",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : Activity
    {
        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var intent = new Intent(this, typeof(MainActivity));

            StartActivity(intent);
            Finish();
        }
    }
}
