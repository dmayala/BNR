using Android.OS;
using Android.Support.V4.App;
using Android.Views;

namespace DragAndDraw.Fragments
{
    public class DragAndDrawFragment : Fragment
    {
        public static DragAndDrawFragment NewInstance()
        {
            return new DragAndDrawFragment();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var v = inflater.Inflate(Resource.Layout.DragAndDrawFragment, container, false);
            return v;
        }
    }
}