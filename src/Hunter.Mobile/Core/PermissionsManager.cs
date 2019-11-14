using Android;
using Android.Content;
using Android.Content.PM;
using Android.Support.V4.Content;
using Xamarin.Essentials;

namespace Hunter.Mobile.Core
{
    public class PermissionsManager
    {
        Context context;
        public PermissionsManager (Context context)
        {
            this.context = context;
        }
        public bool HasLocationPermission
        {
            get
            {
                return ContextCompat.CheckSelfPermission(context, Manifest.Permission.AccessFineLocation) == Permission.Granted;
            }
        }
        public bool HasCameraPermission
        {
            get
            {
                return ContextCompat.CheckSelfPermission(context, Manifest.Permission.Camera) == Permission.Granted;
            }
        }
        public bool IsInternetEnabled
        {
            get
            {
                var network = Connectivity.NetworkAccess;
                return network == NetworkAccess.Internet;
            }
        }

    }
}