using Android.Telephony;

namespace Hunter.Mobile.Models
{
    public class NetworkInfo
    {
        TelephonyManager telephonyManager;
        public NetworkInfo(TelephonyManager telephonyManager)
        {
            this.telephonyManager = telephonyManager;
        }

        public string CarrierName
        {
            get
            {
                return telephonyManager.SimCarrierIdName;
            }
        }
        public int CarrierId
        {
            get
            {
                return telephonyManager.SimCarrierId;
            }
        }
        public string SimSerialNumber
        {
            get
            {
                return telephonyManager.SimSerialNumber;
            }
        }
        public string MEID
        {
            get
            {
                return telephonyManager.Meid;
            }
        }
        public string IMEI
        {
            get
            {
                return telephonyManager.Imei;
            }
        }
        public string SubscriberId
        {
            get
            {
                return telephonyManager.SubscriberId;
            }
        }
    }
}