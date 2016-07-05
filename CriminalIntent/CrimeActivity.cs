using Android.App;
using Android.OS;
using Android.Support.V4.App;

namespace CriminalIntent
{
    [Activity(MainLauncher = true)]
    public class CrimeActivity : FragmentActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Crime);

            var fragment = SupportFragmentManager.FindFragmentById(Resource.Id.FragmentContainer);

            if (fragment == null)
            {
                fragment = new CrimeFragment();
                SupportFragmentManager.BeginTransaction()
                                      .Add(Resource.Id.FragmentContainer, fragment)
                                      .Commit();
            }
        }
    }
}

