using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Hunter.Mobile.Core;
using Hunter.Mobile.Framework;
using Hunter.Mobile.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fragment = Android.Support.V4.App.Fragment;

namespace Hunter.Mobile
{
    public class ConfigurationFragment : Fragment
    {

        Context context;
        TextView tvHeader;
        TextView tvDeviceId;
        TextView tvBaseURL;
        RelativeLayout rlLoginForm;
        EditText etUsername;
        EditText etPassword;
        Button btnLogin;
        ProgressBarView progressBarView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = View.Inflate(this.Context, Resource.Layout.fragment_configuration, null);
            tvHeader = (TextView)view.FindViewById(Resource.Id.tvConfigurationHeader);
            tvDeviceId = (TextView)view.FindViewById(Resource.Id.tvConfigDeviceId);
            tvBaseURL = (TextView)view.FindViewById(Resource.Id.tvConfigBaseURL);
            rlLoginForm = (RelativeLayout)view.FindViewById(Resource.Id.rlLoginForm);
            etUsername = (EditText)view.FindViewById(Resource.Id.etUsername);
            etPassword = (EditText)view.FindViewById(Resource.Id.etPassword);
            btnLogin = (Button)view.FindViewById(Resource.Id.btnLogin);
            progressBarView = new ProgressBarView(view);
            this.context = Context;
            btnLogin.Click += BtnLogin_Click;
            return view;
        }

        private void BtnLogin_Click(object sender, System.EventArgs e)
        {

            LoginModel loginModel = new LoginModel();
            loginModel.Username = etUsername.Text;
            loginModel.Password = etPassword.Text;
            if (loginModel.IsValid)
            {
                Login(loginModel);
            }
            else
            {
                Toast.MakeText(context, "Form is not valid", ToastLength.Short).Show();
            }
        }


        public override void OnStart()
        {
            base.OnStart();
            SetValues();
        }

        public async void SetValues()
        {
            progressBarView.Hide(false);
            await progressBarView.SetProgressMessage("Read Configuration");
            AppSettings appSettings = new AppSettings(context);
            if (appSettings.ConfigurationExists)
            {
                tvHeader.SetText("Configuration Details", TextView.BufferType.Normal);
                tvDeviceId.SetText("DeviceId: " + appSettings.DeviceId, TextView.BufferType.Normal);
                tvBaseURL.SetText("BaseURL: " + appSettings.BaseURL, TextView.BufferType.Normal);

                if (string.IsNullOrEmpty(appSettings.AuthToken))
                {
                    rlLoginForm.Visibility = ViewStates.Visible;
                }
                else
                {
                    //await GetTasks(new HunterBLL(context));  
                    Intent intent = new Intent(context, typeof(TasksService));
                    context.StartService(intent);
                }
            }
            else
            {
                tvHeader.SetText("No Configuration File Found", TextView.BufferType.Normal);
            }
            progressBarView.Hide(true);
        }

        private async Task Login(LoginModel loginModel)
        {
            await progressBarView.SetProgressMessage("Connecting...");
            progressBarView.Hide(false);


            HunterBLL hunterBLL = new HunterBLL(context);
            var result = await hunterBLL.Login(loginModel);

            if (result.Status == "success")
            {
                await progressBarView.SetProgressMessage("Status Success");
                if (result.Entity != null && !string.IsNullOrEmpty(result.Entity.Token))
                {
                    await progressBarView.SetProgressMessage("Saving Auth Token");
                    AppSettings appSettings = new AppSettings(context);
                    IDictionary<string, string> values = new Dictionary<string, string>();
                    values.Add("Auth_Token", result.Entity.Token);
                    appSettings.PutSharedPref(values);
                    Toast.MakeText(context, "Logged In", ToastLength.Short).Show();
                }
            }
            else
            {
                Toast.MakeText(context, result.Status, ToastLength.Short).Show();
            }

            progressBarView.Hide(true);
        }

        private async Task<Tasks[]> GetTasks(HunterBLL hunterBLL)
        {
            await progressBarView.SetProgressMessage("Getting Tasks");
            return hunterBLL.GetTasksFromFile();
        }

    }
}