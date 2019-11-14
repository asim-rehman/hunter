using Android.Content;
using Android.Support.V4.App;
using Java.Lang;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentManager = Android.Support.V4.App.FragmentManager;

namespace Hunter.Mobile.Framework.Adapters
{
    public class ViewPagerAdapter : FragmentPagerAdapter
    {
        private int[] title = new int[] {Resource.String.tab_text_2};
        Context context;

        public ViewPagerAdapter(FragmentManager fm, Context context) : base(fm)
        {
            this.context = context;
        }

        public override Fragment GetItem(int position)
        {
            switch (position)
            {
                case 0:
                    ConfigurationFragment serverFragment = new ConfigurationFragment();
                    return serverFragment;
            }
            return null;
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            string name = Java.Lang.String.Format(context.GetString(title[position]));
            return new Java.Lang.String(name);
        }

        public override int Count 
        {
            get
            {
                return 1;
            }
        }
    }
}