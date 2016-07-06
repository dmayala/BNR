using Android.App;
using Android.Support.V4.App;
using CriminalIntent.Fragments;

namespace CriminalIntent.Activities
{
    [Activity]
    public class CrimeActivity : SingleFragmentActivity
    {
        protected override Android.Support.V4.App.Fragment CreateFragment()
        {
            return new CrimeFragment();
        }
    }
}

