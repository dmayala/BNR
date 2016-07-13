using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using PhotoGallery.Models;
using PhotoGallery.Utils;
using Square.Picasso;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fragment = Android.Support.V4.App.Fragment;
using SearchView = Android.Support.V7.Widget.SearchView;

namespace PhotoGallery.Fragments
{
    public class PhotoGalleryFragment : Fragment, SearchView.IOnClickListener
    {
        const string TAG = "PhotoGalleryFragment";

        private RecyclerView _photoRecyclerView;
        private SearchView _searchView;
        private IList<GalleryItem> _items = new List<GalleryItem>();

        public static PhotoGalleryFragment NewInstance()
        {
            return new PhotoGalleryFragment();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RetainInstance = true;
            HasOptionsMenu = true;

            UpdateItems();
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            base.OnCreateOptionsMenu(menu, inflater);
            inflater.Inflate(Resource.Menu.PhotoGalleryFragment, menu);

            var searchItem = menu.FindItem(Resource.Id.SearchMenuItem);
            _searchView = searchItem.ActionView as SearchView;

            _searchView.QueryTextSubmit += (sender, e) =>
            {
                Log.Debug(TAG, "QueryTextSubmit: " + e.Query);
                QueryPreferences.SetStoredQuery(Activity, e.Query);
                UpdateItems();
                searchItem.CollapseActionView();
                _searchView.ClearFocus();
                e.Handled = true;
            };

            _searchView.QueryTextChange += (sender, e) =>
            {
                Log.Debug(TAG, "QueryTextChange: " + e.NewText);
                e.Handled = false;
            };

            _searchView.SetOnSearchClickListener(this);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.ClearMenuItem:
                    QueryPreferences.SetStoredQuery(Activity, null);
                    UpdateItems();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);               
            }
        }

        private void UpdateItems()
        {
            var query = QueryPreferences.GetStoredQuery(Activity);
            FetchItemsAsync(query).ConfigureAwait(false);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var v = inflater.Inflate(Resource.Layout.PhotoGalleryFragment, container, false);
            _photoRecyclerView = v.FindViewById<RecyclerView>(Resource.Id.PhotoGalleryRecyclerViewFragment);
            _photoRecyclerView.SetLayoutManager(new GridLayoutManager(Activity, 3));

            SetupAdapter();

            return v;
        }

        private void SetupAdapter()
        {
            if (IsAdded)
            {
                _photoRecyclerView.SetAdapter(new PhotoAdapter(Activity, _items));
            }
        }

        private async Task FetchItemsAsync(string query)
        {
            var ff = new FlickrFetchr();

            if (query == null)
            {
                _items = await ff.FetchRecentPhotosAsync();
            } else
            {
                _items = await ff.SearchPhotosAsync(query);
            }
    
            SetupAdapter();
        }

        #region SearchView.IOnClickListener

        public void OnClick(View v)
        {
            var query = QueryPreferences.GetStoredQuery(Activity);
            _searchView.SetQuery(query, false);
        }

        #endregion
    }

    public class PhotoHolder : RecyclerView.ViewHolder
    {
        private ImageView _itemImageView;

        public PhotoHolder(View itemView) : base(itemView)
        {
            _itemImageView = itemView.FindViewById<ImageView>(Resource.Id.PhotoGalleryImageViewFragment);
        }

        public void BindGalleyItem(Context context, GalleryItem galleryItem)
        {
            Picasso.With(context)
                .Load(galleryItem.Url)
                .Placeholder(Resource.Drawable.bill_up_close)
                .Into(_itemImageView);
        }
    }

    public class PhotoAdapter : RecyclerView.Adapter
    {
        private IList<GalleryItem> _galleryItems;
        private Context _context;

        public PhotoAdapter(Context context, IList<GalleryItem> galleryItems)
        {
            _context = context;
            _galleryItems = galleryItems;
        }

        public override int ItemCount
        {
            get
            {
                return _galleryItems.Count;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var photoHolder = holder as PhotoHolder;
            var galleryItem = _galleryItems[position];
            photoHolder.BindGalleyItem(_context, galleryItem);
            
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(_context);
            var view = inflater.Inflate(Resource.Layout.GalleryItem, parent, false);
            return new PhotoHolder(view);
        }
    }
}