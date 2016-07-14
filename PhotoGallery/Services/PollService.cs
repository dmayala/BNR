using Android.App;
using Android.Content;
using Android.Net;
using PhotoGallery.Utils;
using PhotoGallery.Models;
using System.Collections.Generic;
using Android.Util;
using Android.OS;

namespace PhotoGallery.Services
{
    [Service]
    public class PollService : IntentService
    {
        const string TAG = "PollService";
        const int PollInterval = 1000 * 60;

        public static Intent NewIntent(Context context)
        {
            return new Intent(context, typeof(PollService));
        }

        public PollService() : base(TAG)
        {

        }

        public static void SetServiceAlarm(Context context, bool isOn)
        {
            var i = NewIntent(context);
            var pi = PendingIntent.GetService(context, 0, i, 0);

            var alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);

            if (isOn)
            {
                alarmManager.SetInexactRepeating(AlarmType.ElapsedRealtime, SystemClock.ElapsedRealtime(), PollInterval, pi);
            }
            else
            {
                alarmManager.Cancel(pi);
                pi.Cancel();
            }
        }

        public static bool IsServiceAlarmOn(Context context)
        {
            var i = NewIntent(context);
            var pi = PendingIntent.GetService(context, 0, i, PendingIntentFlags.NoCreate);
            return pi != null;
        }

        protected async override void OnHandleIntent(Intent intent)
        {
            if (!isNetworkAvailableAndConnected()) return;
            var ff = new FlickrFetchr();
            var query = QueryPreferences.GetStoredQuery(this);
            var lastResultId = QueryPreferences.GetLastResultId(this);
            IList<GalleryItem> items;

            if (query == null)
            {
                items = await ff.FetchRecentPhotosAsync();
            }
            else
            {
                items = await ff.SearchPhotosAsync(query);
            }

            if (items.Count == 0) return;

            var resultId = items[0].Id;
            if (resultId.Equals(lastResultId))
            {
                Log.Info(TAG, "Got an old result: " + resultId);
            }
            else
            {
                Log.Info(TAG, "Got a new result: " + resultId);
            }

            QueryPreferences.SetLastResultId(this, resultId);
        }

        private bool isNetworkAvailableAndConnected()
        {
            var cm = (ConnectivityManager)GetSystemService(ConnectivityService);
            bool isNetworkAvailable = cm.ActiveNetworkInfo != null;
            bool isNetworkConnected = isNetworkAvailable && cm.ActiveNetworkInfo.IsConnected;
            return isNetworkConnected;
        }
    }
}