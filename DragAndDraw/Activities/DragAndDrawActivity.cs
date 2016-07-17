using Android.App;
using DragAndDraw.Fragments;

namespace DragAndDraw.Activities
{
    [Activity(MainLauncher = true, Icon = "@drawable/icon")]
    public class DragAndDrawActivity : SingleFragmentActivity
    {
        protected override Android.Support.V4.App.Fragment CreateFragment()
        {
            return DragAndDrawFragment.NewInstance();
        }
    }
}