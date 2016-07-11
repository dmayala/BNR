using Android.OS;
using Android.Views;
using Android.Support.V7.Widget;
using Android.Content;
using Android.Util;
using Fragment = Android.Support.V4.App.Fragment;
using System.Linq;
using Android.Content.PM;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace NerdLauncher.Fragments
{
    public class NerdLauncherFragment : Fragment
    {
        const string TAG = "NerdLauncherFragment";

        private RecyclerView _recyclerView;

        public static NerdLauncherFragment NewInstance()
        {
            return new NerdLauncherFragment();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var v = inflater.Inflate(Resource.Layout.NerdLauncherFragment,  container, false);
            _recyclerView = v.FindViewById<RecyclerView>(Resource.Id.NerdLauncherRecyclerViewFragment);
            _recyclerView.SetLayoutManager(new LinearLayoutManager(Activity));

            SetupAdapter();
            return v;
        }

        private void SetupAdapter()
        {
            var startupIntent = new Intent(Intent.ActionMain);
            startupIntent.AddCategory(Intent.CategoryLauncher);

            var pm = Activity.PackageManager;
            var activities = pm.QueryIntentActivities(startupIntent, 0)
                               .OrderBy(a => a.LoadLabel(pm).ToLower())
                               .ToList();

            Log.Info(TAG, $"Found {activities.Count} activities.");
            _recyclerView.SetAdapter(new ActivityAdapter(activities));
        }
    }

    public class ActivityHolder : RecyclerView.ViewHolder
    {
        private Context _context;
        private ResolveInfo _resolveInfo;
        private TextView _nameTextView;

        public ActivityHolder(View itemView) : base(itemView)
        {
            _nameTextView = (TextView)itemView;
            _context = itemView.Context;
            _nameTextView.Click += OnItemViewClick;
        }

        public void BindActivity(ResolveInfo resolveInfo)
        {
            _resolveInfo = resolveInfo;
            var pm = _context.PackageManager;
            var appName = _resolveInfo.LoadLabel(pm);
            _nameTextView.Text = appName;
        }

        private void OnItemViewClick(object sender, EventArgs e)
        {
            var activityInfo = _resolveInfo.ActivityInfo;
            var i = new Intent(Intent.ActionMain)
                .SetClassName(activityInfo.ApplicationInfo.PackageName, activityInfo.Name);
            _context.StartActivity(i);
        }
    }

    public class ActivityAdapter : RecyclerView.Adapter
    {
        private readonly IList<ResolveInfo> _activities;

        public ActivityAdapter(IList<ResolveInfo> activities)
        {
            _activities = activities;
        }

        public override int ItemCount
        {
            get
            {
                return _activities.Count;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var activityHolder = holder as ActivityHolder;
            var resolveInfo = _activities[position];
            activityHolder.BindActivity(resolveInfo);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var layoutInflater = LayoutInflater.From(parent.Context);
            var view = layoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, parent, false);
            return new ActivityHolder(view);
        }
    }
}