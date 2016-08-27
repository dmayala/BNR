using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Fragment = Android.Support.V4.App.Fragment;
using NerdLauncher.Fragments;

namespace NerdLauncher.Activities
{
    [Activity(Label = "NerdLauncher", MainLauncher = true, Icon = "@drawable/icon")]
    [IntentFilter(new[] { "android.intent.action.MAIN" }, Categories = new[] { "android.intent.category.HOME", "android.intent.category.DEFAULT" })]
    public class NerdLauncherActivity : SingleFragmentActivity
    {
        protected override Fragment CreateFragment()
        {
            return NerdLauncherFragment.NewInstance();
        }
    }
}

