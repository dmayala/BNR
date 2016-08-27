using Android.App;
using Android.Content;
using Android.Util;
using PhotoGallery.Utils;
using PhotoGallery.Services;

namespace PhotoGallery.Receivers
{
    [BroadcastReceiver]
    [IntentFilter(new string[] { "android.intent.action.BOOT_COMPLETED" })]
    public class StartupReceiver : BroadcastReceiver
    {
        const string TAG = "StartupReceiver";

        public override void OnReceive(Context context, Intent intent)
        {
            Log.Info(TAG, "Received broadcast intent: " + intent.Action);

            bool isOn = QueryPreferences.IsAlarmOn(context);
            PollService.SetServiceAlarm(context, isOn);
        }
    }
}