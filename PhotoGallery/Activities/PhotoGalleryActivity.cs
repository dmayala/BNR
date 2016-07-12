using Android.App;
using PhotoGallery.Fragments;

namespace PhotoGallery.Activities
{
    [Activity(MainLauncher = true, Icon = "@drawable/icon")]
    public class PhotoGalleryActivity : SingleFragmentActivity
    {
        protected override Android.Support.V4.App.Fragment CreateFragment()
        {
            return PhotoGalleryFragment.NewInstance();
        }
    }
}

