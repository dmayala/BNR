using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;

namespace CriminalIntent.Receivers
{
    [BroadcastReceiver]
    [IntentFilter(new string[] { "android.intent.action.BOOT_COMPLETED" })]
    public class StartupReceiver : BroadcastReceiver
    {
        const string TAG = "StartupReceiver";

        public override void OnReceive(Context context, Intent intent)
        {
            Log.Info(TAG, "Received broadcast intent: " + intent.Action);
        }
    }
}