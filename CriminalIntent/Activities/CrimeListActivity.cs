using Android.App;
using Android.Support.V4.App;
using CriminalIntent.Fragments;

namespace CriminalIntent.Activities
{
    [Activity(MainLauncher = true)]
    public class CrimeListActivity : SingleFragmentActivity
    {
        protected override Android.Support.V4.App.Fragment CreateFragment()
        {
            return new CrimeListFragment();
        }
    }
}

