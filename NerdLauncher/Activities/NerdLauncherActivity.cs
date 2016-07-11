using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Fragment = Android.Support.V4.App.Fragment;
using NerdLauncher.Fragments;

namespace NerdLauncher.Activities
{
    [Activity(Label = "NerdLauncher", MainLauncher = true, Icon = "@drawable/icon")]
    public class NerdLauncherActivity : SingleFragmentActivity
    {
        protected override Fragment CreateFragment()
        {
            return NerdLauncherFragment.NewInstance();
        }
    }
}

