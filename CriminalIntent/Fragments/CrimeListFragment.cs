using System;
using System.Collections.Generic;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using CriminalIntent.Models;

namespace CriminalIntent.Fragments
{
    public class CrimeListFragment : Fragment
    {
        private RecyclerView _crimeRecyclerView;
        private CrimeAdapter _adapter;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.CrimeListFragment, container, false);
            _crimeRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.CrimeRecyclerView);
            _crimeRecyclerView.SetLayoutManager(new LinearLayoutManager(this.Activity));
            UpdateUI();
            return view;
        }

        private void UpdateUI()
        {
            var crimes = CrimeLab.Get(this.Activity).Crimes;
            _adapter = new CrimeAdapter(crimes);
            _crimeRecyclerView.SetAdapter(_adapter);
        }

        private class CrimeHolder : RecyclerView.ViewHolder
        {
            public TextView TitleTextView { get; private set; }

            public CrimeHolder(View itemView) : base(itemView)
            {
                TitleTextView = (TextView) itemView;
            }
        }

        private class CrimeAdapter : RecyclerView.Adapter
        {
            private List<Crime> _crimes;

            public CrimeAdapter(List<Crime> crimes)
            {
                _crimes = crimes;
            }
                
            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                var layoutInflater = LayoutInflater.From(parent.Context);
                var view = layoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, parent, false);
                return new CrimeHolder(view);
            }

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                Crime crime = _crimes[position];
                CrimeHolder ch = holder as CrimeHolder;
                ch.TitleTextView.Text = crime.Title;
            }

            public override int ItemCount
            {
                get { return _crimes.Count; }
            }
        }
    }
}

