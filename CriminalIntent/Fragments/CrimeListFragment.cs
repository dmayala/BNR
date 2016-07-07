using System;
using System.Collections.Generic;
using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using CriminalIntent.Activities;
using CriminalIntent.Models;

namespace CriminalIntent.Fragments
{
    public class CrimeListFragment : Fragment
    {
        const string SavedSubtitleVisible = "subtitle";

        private RecyclerView _crimeRecyclerView;
        private CrimeAdapter _adapter;
        private bool _subtitleVisible;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            HasOptionsMenu = true;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.CrimeListFragment, container, false);
            _crimeRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.CrimeRecyclerView);
            _crimeRecyclerView.SetLayoutManager(new LinearLayoutManager(this.Activity));

            if (savedInstanceState != null)
                _subtitleVisible = savedInstanceState.GetBoolean(SavedSubtitleVisible);
            
            UpdateUI();
            return view;
        }

        public override void OnResume()
        {
            base.OnResume();
            UpdateUI();
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutBoolean(SavedSubtitleVisible, _subtitleVisible);
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            base.OnCreateOptionsMenu(menu, inflater);
            inflater.Inflate(Resource.Menu.CrimeListFragment, menu);

            var subtitleItem = menu.FindItem(Resource.Id.ShowSubtitleMenuItem);
            subtitleItem.SetTitle(_subtitleVisible ? 
                                  Resource.String.hide_subtitle : Resource.String.show_subtitle);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.NewCrimeMenuItem:
                    var crime = new Crime();
                    CrimeLab.Get(Activity).AddCrime(crime);

                    var intent = CrimePagerActivity.NewIntent(Activity, crime.Id);
                    StartActivity(intent);
                    return true;
                case Resource.Id.ShowSubtitleMenuItem:
                    _subtitleVisible = !_subtitleVisible;
                    Activity.InvalidateOptionsMenu();
                    UpdateSubtitle();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        private void UpdateSubtitle()
        {
            var crimeLab = CrimeLab.Get(Activity);
            var crimeCount = crimeLab.Crimes.Count;
            var subtitle = _subtitleVisible ?
                Resources.GetQuantityString(Resource.Plurals.subtitle_plural, crimeCount, crimeCount) : null;

            var activity = (AppCompatActivity)Activity;
            activity.SupportActionBar.Subtitle = subtitle;
        }

        private void UpdateUI()
        {
            var crimes = CrimeLab.Get(this.Activity).Crimes;

            if (_adapter == null)
            {
                _adapter = new CrimeAdapter(crimes);
                _crimeRecyclerView.SetAdapter(_adapter);
            }
            else 
            {
                _adapter.NotifyDataSetChanged();
            }

            UpdateSubtitle();
        }

        private class CrimeHolder : RecyclerView.ViewHolder
        {
            private Crime _crime;

            private readonly TextView _titleTextView;
            private readonly TextView _dateTextView;
            private readonly CheckBox _solvedCheckBox;

            public CrimeHolder(View itemView) : base(itemView)
            {
                itemView.Click += OnItemViewClick;
                _titleTextView = itemView.FindViewById<TextView>(Resource.Id.CrimeListItemTitleTextView);
                _dateTextView = itemView.FindViewById<TextView>(Resource.Id.CrimeListItemDateTextView);
                _solvedCheckBox = itemView.FindViewById<CheckBox>(Resource.Id.CrimeListItemSolvedCheckBox);

                _solvedCheckBox.CheckedChange += (sender, e) =>
                {
                    _crime.Solved = e.IsChecked;
                };
            }

            public void BindCrime(Crime crime)
            {
                _crime = crime;
                _titleTextView.Text = _crime.Title;
                _dateTextView.Text = _crime.Date.ToLongDateString();
                _solvedCheckBox.Checked = _crime.Solved;
            }

            private void OnItemViewClick(object sender, EventArgs e)
            {
                var context = this.ItemView.Context;
                var intent = CrimePagerActivity.NewIntent(context, _crime.Id);
                context.StartActivity(intent);
            }
        }

        private class CrimeAdapter : RecyclerView.Adapter
        {
            private readonly List<Crime> _crimes;

            public CrimeAdapter(List<Crime> crimes)
            {
                _crimes = crimes;
            }
                
            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                var layoutInflater = LayoutInflater.From(parent.Context);
                var view = layoutInflater.Inflate(Resource.Layout.CrimeListItem, parent, false);
                return new CrimeHolder(view);
            }

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                Crime crime = _crimes[position];
                var ch = holder as CrimeHolder;
                ch.BindCrime(crime);
            }

            public override int ItemCount
            {
                get { return _crimes.Count; }
            }
        }
    }
}

