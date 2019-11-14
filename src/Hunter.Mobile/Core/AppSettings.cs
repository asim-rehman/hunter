using Android.Content;
using Android.Preferences;
using System.Collections.Generic;

namespace Hunter.Mobile.Core
{
    public class AppSettings
    {
        Context context;
        private const string _deviceId = "DeviceId";
        private const string _baseURL = "BaseURL";
        private const string _authtoken = "Auth_Token";
        public AppSettings(Context context)
        {
            this.context = context;
        }

        public string DeviceId
        {
            get
            {
                return GetSharedPreferences.GetString(_deviceId, string.Empty);
            }
        }
        public string BaseURL
        {
            get
            {
                return GetSharedPreferences.GetString(_baseURL, string.Empty);
            }
        }
        public string AuthToken
        {
            get
            {
                return GetSharedPreferences.GetString(_authtoken, string.Empty);
            }
        }

        public bool ConfigurationExists
        {
            get
            {
                return !string.IsNullOrEmpty(GetSharedPreferences.GetString(_deviceId, string.Empty)) && !string.IsNullOrEmpty(GetSharedPreferences.GetString(_baseURL, string.Empty));              
            }
        }

        public void PutSharedPref(IDictionary<string, string> values)
        {
            ISharedPreferencesEditor sharedPreferencesEditor = GetSharedPreferences.Edit();

            foreach (var item in values)
            {
                sharedPreferencesEditor.PutString(item.Key, item.Value);
            }
            sharedPreferencesEditor.Apply();
        }       
        public ISharedPreferences GetSharedPreferences
        {
            get
            {
                return PreferenceManager.GetDefaultSharedPreferences(context);
            }
        }

        public static string TASKS_FILE
        {
            get
            {
                return "Tasks.json";
            }
        }
    }
}