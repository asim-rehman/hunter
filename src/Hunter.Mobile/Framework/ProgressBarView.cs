using Android.Views;
using Android.Widget;
using System.Threading.Tasks;

namespace Hunter.Mobile.Framework
{
    public class ProgressBarView
    {


        private ProgressBar progressBar = null;
        private TextView progressStatus = null;
        private FrameLayout frameLayout = null;


        public ProgressBarView(View view)
        {
            progressBar = (ProgressBar)view.FindViewById(Resource.Id.progressBar1);
            progressStatus = (TextView)view.FindViewById(Resource.Id.tvProgressStatus);
            frameLayout = (FrameLayout)view.FindViewById(Resource.Id.include_progressbar);
        }
        public void Hide(bool hide)
        {
            if(hide)
            {
                frameLayout.Visibility = ViewStates.Gone;
            }
            else
            {
                frameLayout.Visibility = ViewStates.Visible;
            }
        }
        public async Task SetProgressMessage(string text)
        {
            progressStatus.Text = text;
        }


    }
}