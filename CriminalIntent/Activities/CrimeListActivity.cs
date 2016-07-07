using Android.App;
using Android.Support.V4.App;
using CriminalIntent.Fragments;
using Fragment = Android.Support.V4.App.Fragment;

namespace CriminalIntent.Activities
{
    [Activity(MainLauncher = true)]
    public class CrimeListActivity : SingleFragmentActivity
    {
        protected override Fragment CreateFragment()
        {
            return new CrimeListFragment();
        }
    }
}

