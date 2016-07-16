using Android.App;
using Android.Content;
using Android.Support.V4.App;
using Android.Util;
using PhotoGallery.Services;

namespace PhotoGallery.Receivers
{
    [BroadcastReceiver(Exported = false)]
    [IntentFilter(new string[] { "la.daya.photogallery.SHOW_NOTIFICATION" }, Priority = -999)]
    public class NotificationReceiver : BroadcastReceiver
    {
        const string TAG = "NotificationReceiver";

        public override void OnReceive(Context context, Intent intent)
        {
            Log.Info(TAG, "received result: " + ResultCode);
            if (ResultCode != Result.Ok)
            {
                // A foreground activity cancelled the broadcast
                return;
            }

            var requestCode = intent.GetIntExtra(PollService.RequestCode, 0);
            var notification = (Notification)intent.GetParcelableExtra(PollService.Notification);

            var notificationManager = NotificationManagerCompat.From(context);
            notificationManager.Notify(requestCode, notification);
        }
    }
}