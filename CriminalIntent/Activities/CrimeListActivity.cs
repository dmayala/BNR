using Android.App;
using CriminalIntent.Core.Models;
using CriminalIntent.Fragments;
using Fragment = Android.Support.V4.App.Fragment;

namespace CriminalIntent.Activities
{
    [Activity(MainLauncher = true)]
    public class CrimeListActivity : SingleFragmentActivity, 
                                     CrimeListFragment.ICallbacks, CrimeFragment.ICallbacks
    {
        protected override Fragment CreateFragment()
        {
            return new CrimeListFragment();
        }

        protected override int GetLayoutResId()
        {
            return Resource.Layout.MasterDetailActivity;
        }

        public void OnCrimeSelected(Crime crime)
        {
            if (FindViewById(Resource.Id.DetailFragmentContainer) == null)
            {
                var intent = CrimePagerActivity.NewIntent(this, crime.Id);
                StartActivity(intent);
            }
            else 
            {
                var newDetail = CrimeFragment.NewInstance(crime.Id);

                SupportFragmentManager.BeginTransaction()
                                      .Replace(Resource.Id.DetailFragmentContainer, newDetail)
                                      .Commit();
            }
        }

        public void OnCrimeChecked(Crime crime)
        {
            if (FindViewById(Resource.Id.DetailFragmentContainer) != null)
            {
                var crimeFragment = (CrimeFragment)SupportFragmentManager
                    .FindFragmentById(Resource.Id.DetailFragmentContainer);
                crimeFragment.UpdateUI();
            }
        }

        public void OnCrimeUpdated(Crime crime)
        {
            var listFragment = (CrimeListFragment) SupportFragmentManager
                .FindFragmentById(Resource.Id.FragmentContainer);
            listFragment.UpdateUI();
        }
    }
}

