using Android.OS;
using Android.Support.V4.App;

namespace CriminalIntent.Activities
{
    public abstract class SingleFragmentActivity : FragmentActivity
    {
        protected abstract Fragment CreateFragment();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FragmentActivity);

            var fragment = SupportFragmentManager.FindFragmentById(Resource.Id.FragmentContainer);
            if (fragment == null)
            {
                fragment = CreateFragment();
                SupportFragmentManager.BeginTransaction()
                                      .Add(Resource.Id.FragmentContainer, fragment)
                                      .Commit();
            }
        }
    }
}

