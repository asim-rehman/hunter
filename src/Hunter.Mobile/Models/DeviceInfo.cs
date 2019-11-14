using Android.App;
using Android.Content;
using Android.OS;
using Android.Telephony;
using System;

namespace Hunter.Mobile.Models
{
    public class DeviceInfo
    {
        Context context;
        public DeviceInfo (Context context)
        {
            this.context = context;
        }

        public string Id { get; set; } = Build.Id;
        public string Manufacturer { get; set; } = Build.Manufacturer;
        public string Model { get; set; } = Build.Model;
        public string Device { get; set; } = Build.Device;
        public string Display { get; set; } = Build.Display;
        public string Serial { get; set; } = Build.Serial;
        public string Version { get; set; } = Build.VERSION.Release;
        public string Codename { get; set; } = Build.VERSION.Codename;
        public string SDK { get; set; } = Build.VERSION.SdkInt.ToString();
        public string SecurityPatch { get; set; } = Build.VERSION.SecurityPatch;
        public string Hardware { get; set; } = Build.Hardware;
        public string Radio { get; set; } = Build.Radio;
        public string Bootloader { get; set; } = Build.Bootloader;
        public string Board { get; set; } = Build.Board;
        public string User { get; set; } = Build.User;
        public long Time { get; set; } = Build.Time;
        public DateTime CurrentDateTime = DateTime.UtcNow;
        public NetworkInfo NetworkInfo
        {
            get
            {
                try
                {
                    TelephonyManager mTelephonyMgr;
                    mTelephonyMgr = (TelephonyManager)context.GetSystemService(Service.TelephonyService);
                    return new NetworkInfo(mTelephonyMgr);
                }
                catch (Exception)
                {
                }
                return null;
            }

        }

    }
}