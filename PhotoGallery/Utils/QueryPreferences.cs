using Android.Content;
using Android.Preferences;

namespace PhotoGallery.Utils
{
    public class QueryPreferences
    {
        const string PrefSearchQuery = "searchQuery";

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
    }
}