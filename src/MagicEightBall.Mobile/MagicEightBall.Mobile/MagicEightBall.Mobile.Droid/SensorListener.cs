using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Hardware;

namespace MagicEightBall.Mobile.Droid
{
    class SensorListener: Java.Lang.Object, ISensorEventListener
    {
        bool hasUpdated = false;
        DateTime lastUpdate;
        float last_x = 0.0f;
        float last_y = 0.0f;
        float last_z = 0.0f;

        const int ShakeDetectionTimeLapse = 250;
        const double ShakeThreshold = 800;

        Context _context;
        SensorManager _manager;

        public event EventHandler ShakeDetected;

        void RaiseShakeDetected()
        {
            var handler = ShakeDetected;
            if (handler == null)
                return;
            handler(this, EventArgs.Empty);
        }

        public SensorListener(Context context)
        {
            _context = context;

            _manager = _context.GetSystemService(Context.SensorService) as SensorManager;
            Resume();
        }

        public void Pause()
        {
            _manager.UnregisterListener(this);
        }

        public void Resume()
        {
            // Register this as a listener with the underlying service.
            var sensor = _manager.GetDefaultSensor(SensorType.Accelerometer);
            _manager.RegisterListener(this, sensor, SensorDelay.Game);
        }

        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
            throw new NotImplementedException();
        }

        public void OnSensorChanged(SensorEvent e)
        {
            if (e.Sensor.Type == SensorType.Accelerometer)
            {
                float x = e.Values[0];
                float y = e.Values[1];
                float z = e.Values[2];

                DateTime curTime = System.DateTime.Now;
                if (hasUpdated == false)
                {
                    hasUpdated = true;
                    lastUpdate = curTime;
                    last_x = x;
                    last_y = y;
                    last_z = z;
                }
                else
                {
                    if ((curTime - lastUpdate).TotalMilliseconds > ShakeDetectionTimeLapse)
                    {
                        float diffTime = (float)(curTime - lastUpdate).TotalMilliseconds;
                        lastUpdate = curTime;
                        float total = x + y + z - last_x - last_y - last_z;
                        float speed = Math.Abs(total) / diffTime * 10000;

                        if (speed > ShakeThreshold)
                        {
                            RaiseShakeDetected();
                        }

                        last_x = x;
                        last_y = y;
                        last_z = z;
                    }
                }
            }
        }
    }
}