using Android.Content;
using Android.Net;
using PhotoGallery.Fragments;
using Android.App;
using Android.Content.PM;

namespace PhotoGallery.Activities
{
    [Activity(ConfigurationChanges = ConfigChanges.KeyboardHidden | ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class PhotoPageActivity : SingleFragmentActivity
    {
        public static Intent NewIntent(Context context, Uri photoPageUri)
        {
            var i = new Intent(context, typeof(PhotoPageActivity));
            i.SetData(photoPageUri);
            return i;
        }

        protected override Android.Support.V4.App.Fragment CreateFragment()
        {
            return PhotoPageFragment.NewInstance(Intent.Data);
        }
    }
}