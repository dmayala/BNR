using Android.Content;
using Android.Support.V4.App;
using Android.Util;
using PhotoGallery.Services;
using Result = Android.App.Result;

namespace PhotoGallery.Fragments
{
    public abstract class VisibleFragment : Fragment
    {
        const string TAG = "VisibleFragment";

        private BroadcastReceiver _onShowNotification = new VisibleFragmentReceiver();

        public override void OnStart()
        {
            base.OnStart();
            var filter = new IntentFilter(PollService.ActionShowNotification);
            Activity.RegisterReceiver(_onShowNotification, filter, PollService.PermPrivate, null);
        }

        public override void OnStop()
        {
            base.OnStop();
            Activity.UnregisterReceiver(_onShowNotification);
        }

        private class VisibleFragmentReceiver : BroadcastReceiver
        {
            public override void OnReceive(Context context, Intent intent)
            {
                Log.Info(TAG, "cancelling notification");
                ResultCode = Result.Canceled;
            }
        }
    }

}