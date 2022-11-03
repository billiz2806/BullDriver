using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

namespace BullDriver.Droid
{
    [Activity(Label = "BullDriver", Icon = "@mipmap/iconBullDriver",
        Theme = "@style/nuevotema",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize)]
    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            // Create your application here
        }
    }
}