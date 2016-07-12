using Android.OS;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using PhotoGallery.Utils;
using System;
using System.Threading.Tasks;
using Fragment = Android.Support.V4.App.Fragment;

namespace PhotoGallery.Fragments
{
    public class PhotoGalleryFragment : Fragment
    {
        const string TAG = "PhotoGalleryFragment";

        private RecyclerView _photoRecyclerView;

        public static PhotoGalleryFragment NewInstance()
        {
            return new PhotoGalleryFragment();
        }

        public override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RetainInstance = true;
            await FetchItemsAsync();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var v = inflater.Inflate(Resource.Layout.PhotoGalleryFragment, container, false);
            _photoRecyclerView = v.FindViewById<RecyclerView>(Resource.Id.PhotoGalleryRecyclerViewFragment);
            _photoRecyclerView.SetLayoutManager(new GridLayoutManager(Activity, 3));

            return v;
        }

        private async Task FetchItemsAsync()
        {
            try
            {
                var ff = new FlickrFetchr();
                var result = await ff.GetStringAsync("http://www.bignerdranch.com");
                Log.Info(TAG, $"Fetched contents of URL: {result}");
            }
            catch (Exception ex)
            {
                Log.Error(TAG, $"Failed to fetch URL: {ex.Message}");
            }
        }
    }
}