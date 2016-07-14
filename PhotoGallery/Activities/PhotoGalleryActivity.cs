using Android.App;
using Android.Content;
using PhotoGallery.Fragments;

namespace PhotoGallery.Activities
{
    [Activity(MainLauncher = true, Icon = "@drawable/icon")]
    public class PhotoGalleryActivity : SingleFragmentActivity
    {
        public static Intent NewIntent(Context context)
        {
            return new Intent(context, typeof(PhotoGalleryActivity));
        }

        protected override Android.Support.V4.App.Fragment CreateFragment()
        {
            return PhotoGalleryFragment.NewInstance();
        }
    }
}

