using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Hunter.Mobile.Core;
using Hunter.Mobile.Framework;
using Hunter.Mobile.Framework.Adapters;
using Hunter.Mobile.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Hunter.Mobile
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private const int REQUESTCODE_READ_FILE = 1000;
        View view;
        ViewPagerAdapter viewPagerAdapter;
        ViewPager viewPager;
        TabLayout tabs;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
          SetContentView(Resource.Layout.activity_main);

            viewPagerAdapter = new ViewPagerAdapter(this.SupportFragmentManager, this);
            viewPager = FindViewById<ViewPager>(Resource.Id.view_pager);
            viewPager.Adapter = viewPagerAdapter;
            tabs = FindViewById<TabLayout>(Resource.Id.tabs);
            tabs.SetupWithViewPager(viewPager);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            view = Window.DecorView;
  
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                Intent intent = new Intent(Intent.ActionOpenDocument);
                intent.AddCategory(Intent.CategoryOpenable);
                intent.SetType("*/*");
                StartActivityForResult(intent, REQUESTCODE_READ_FILE);
            }

            return base.OnOptionsItemSelected(item);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == REQUESTCODE_READ_FILE && resultCode == Result.Ok)
            {
                new ParseConfiguration(this, new ProgressBarView(view), data, viewPager, viewPagerAdapter).Run();
            }

        }


        private class ParseConfiguration
        {

            Context context;
            ProgressBarView progressBarView;
            Intent data;
            bool isJsonFile = true;
            ViewPager viewPager;
            ViewPagerAdapter viewPagerAdapter;
            public ParseConfiguration(Context context, ProgressBarView progressBarView, Intent data, ViewPager viewPager,ViewPagerAdapter viewPagerAdapter)
            {
                this.context = context;
                this.progressBarView = progressBarView;
                this.data = data;
                this.viewPager = viewPager;
                this.viewPagerAdapter = viewPagerAdapter;
            }
            public async Task Run()
            {
                await OnPreExecute();
                string file = data.Data.ToString();
                Uri uri = Uri.Parse(file);
                string path = uri.Path.Replace("/document/raw:", string.Empty);
                SettingsModel settingsModel = null;
                //TODO: Check Permissions;
                if (File.Exists(path))
                {
                    string ext = Path.GetExtension(path).Trim();
                    if (ext == ".json")
                    {
                        string json = File.ReadAllText(path);
                        settingsModel = JsonConvert.DeserializeObject<SettingsModel>(json);
                    }
                    else
                    {
                        isJsonFile = false;
                    }

                }
                await OnPostExecute(settingsModel);
            }

            protected async Task OnPostExecute(SettingsModel settingsModel)
            {
                if (settingsModel != null && isJsonFile)
                {
                    await progressBarView.SetProgressMessage("Reading file");
                    if (!string.IsNullOrEmpty(settingsModel.BaseURL) && !string.IsNullOrEmpty(settingsModel.DeviceId))
                    {
                        await progressBarView.SetProgressMessage("Adding content..");
                        AppSettings appSettings = new AppSettings(context);
                        IDictionary<string, string> values = new Dictionary<string, string>();
                        values.Add("DeviceId", settingsModel.DeviceId);
                        values.Add("BaseURL", settingsModel.BaseURL);
                        values.Add("Auth_Token", string.Empty);
                        appSettings.PutSharedPref(values);


                        viewPager.Adapter = viewPagerAdapter;
                    }
                    else
                    {
                        Toast.MakeText(context, "Error converting JSON, is the file valid?", ToastLength.Long).Show();
                    }
                }
                else
                {
                    Toast.MakeText(context, "Not a valid JSON file", ToastLength.Long).Show();
                }


                progressBarView.Hide(true);
            }
            protected async Task OnPreExecute()
            {
                await progressBarView.SetProgressMessage("Reading Configuration");
                progressBarView.Hide(false);
            }

        }
    }

}


