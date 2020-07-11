using Android.App;
using Android.Content.PM;

namespace HackTruda.Droid
{
    [Activity(
        NoHistory = true,
        LaunchMode = LaunchMode.SingleTop)]
    [IntentFilter(
        new[] { Android.Content.Intent.ActionView },
        Categories = new[] { Android.Content.Intent.CategoryDefault, Android.Content.Intent.CategoryBrowsable },
        DataScheme = "hacktruda")]
    public class WebAuthenticationCallbackActivity : Xamarin.Essentials.WebAuthenticatorCallbackActivity
    {
    }
}
