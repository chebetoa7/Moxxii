using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using AndroidX.Core.OS;
using Android.Gms.Common;
using Xamarin.Essentials;
using System.Linq;

namespace App.Droid
{
    [Activity(Label = "App", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private string dbPathConfig;

        [Obsolete]
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            IsPlayServicesAvailable();
            dbPathConfig = Service.FileAccess.GetLocalFilePath("configuser.db3");

            Rg.Plugins.Popup.Popup.Init(this);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            Xamarin.FormsGoogleMaps.Init(this, savedInstanceState);

            bool ActionPush = false;
            if (Intent.Extras != null)
            {
                //ActionPush = true;
                foreach (var key in Intent.Extras.KeySet())
                {
                    var value = Intent.Extras.GetString(key);
                    if (key == "key_1")
                    {
                        if (value?.Length > 0)
                        {
                            if (value == "Solicitud")
                            {
                                ActionPush = true;
                                LoadApplication(new App(dbPathConfig, ActionPush));
                            }
                        }
                    }
                }
            }

            
            LoadApplication(new App(dbPathConfig, ActionPush));
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public void IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            bool isGooglePlayServce = resultCode != ConnectionResult.Success;
            Preferences.Set("isGooglePlayServce", isGooglePlayServce);

        }
    }
}