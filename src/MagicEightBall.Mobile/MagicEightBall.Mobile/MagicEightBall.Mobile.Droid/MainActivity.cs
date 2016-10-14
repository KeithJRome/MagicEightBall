using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Hardware;

namespace MagicEightBall.Mobile.Droid
{
    [Activity(Label = "Magic 8-Ball", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, ISensorEventListener
    {
        SensorListener _sensorEvents;

        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
            _sensorEvents.OnAccuracyChanged(sensor, accuracy);
        }

        public void OnSensorChanged(SensorEvent e)
        {
            _sensorEvents.OnSensorChanged(e);
        }

        protected override void OnResume()
        {
            base.OnResume();
            _sensorEvents.Resume();
        }

        protected override void OnPause()
        {
            _sensorEvents.Pause();
            base.OnPause();
        }

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            _sensorEvents = new SensorListener(this);
            _sensorEvents.ShakeDetected += OnShakeDetected;

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        private void OnShakeDetected(object sender, EventArgs e)
        {
            ((ShakablePage)Xamarin.Forms.Application.Current.MainPage).OnShake();
        }
    }
}

