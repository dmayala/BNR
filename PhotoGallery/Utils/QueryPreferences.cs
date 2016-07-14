using Android.Content;
using Android.Preferences;

namespace PhotoGallery.Utils
{
    public class QueryPreferences
    {
        const string PrefSearchQuery = "searchQuery";
        const string PrefLastResultId = "lastResultId";

        public static string GetStoredQuery(Context context)
        {
            return PreferenceManager.GetDefaultSharedPreferences(context)
                .GetString(PrefSearchQuery, null);
        }

        public static void SetStoredQuery(Context context, string query)
        {
            PreferenceManager.GetDefaultSharedPreferences(context)
                .Edit()
                .PutString(PrefSearchQuery, query)
                .Apply();
        }

        public static string GetLastResultId(Context context)
        {
            return PreferenceManager.GetDefaultSharedPreferences(context)
                .GetString(PrefLastResultId, null);
        }

        public static void SetLastResultId(Context context, string lastResultId)
        {
            PreferenceManager.GetDefaultSharedPreferences(context)
                .Edit()
                .PutString(PrefLastResultId, lastResultId)
                .Apply();
        }
    }
}