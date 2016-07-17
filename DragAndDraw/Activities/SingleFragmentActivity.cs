using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.App;

namespace DragAndDraw.Activities
{
    public abstract class SingleFragmentActivity : AppCompatActivity
    {
        protected abstract Fragment CreateFragment();

        protected virtual int GetLayoutResId()
        {
            return Resource.Layout.FragmentActivity;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(GetLayoutResId());

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

