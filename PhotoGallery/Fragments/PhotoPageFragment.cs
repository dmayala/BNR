using Android.Net;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Webkit;
using Android.Widget;

namespace PhotoGallery.Fragments
{
    public class PhotoPageFragment : VisibleFragment
    {
        const string ArgUri = "photo_page_url";

        private Uri _uri;
        private WebView _webView;
        private ProgressBar _progressBar;

        public static PhotoPageFragment NewInstance(Uri uri)
        {
            var args = new Bundle();
            args.PutParcelable(ArgUri, uri);

            var fragment = new PhotoPageFragment();
            fragment.Arguments = args;
            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            _uri = Arguments.GetParcelable(ArgUri) as Uri;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var v = inflater.Inflate(Resource.Layout.PhotoPageFragment, container, false);

            _progressBar = v.FindViewById<ProgressBar>(Resource.Id.PhotoPageProgressBarFragment);
            _progressBar.Max = 100;

            _webView = v.FindViewById<WebView>(Resource.Id.PhotoPageWebViewFragment);
            _webView.Settings.JavaScriptEnabled = true;
            _webView.SetWebChromeClient(new PhotoPageWebChromeClient((AppCompatActivity) Activity, _progressBar));
            _webView.SetWebViewClient(new PhotoPageWebViewClient());
            _webView.LoadUrl(_uri.ToString());

            return v;
        }
    }

    public class PhotoPageWebChromeClient : WebChromeClient
    {
        private AppCompatActivity _activity;
        private ProgressBar _progressBar;

        public PhotoPageWebChromeClient(AppCompatActivity activity, ProgressBar progressBar)
        {
            _activity = activity;
            _progressBar = progressBar;
        }

        public override void OnProgressChanged(WebView view, int newProgress)
        {
            if (newProgress == 100)
            {
                _progressBar.Visibility = ViewStates.Gone;
            }
            else
            {
                _progressBar.Visibility = ViewStates.Visible;
                _progressBar.Progress = newProgress;
            }
        }

        public override void OnReceivedTitle(WebView view, string title)
        {
            _activity.SupportActionBar.Subtitle = title;
        }
    }

    public class PhotoPageWebViewClient : WebViewClient
    {
        public override bool ShouldOverrideUrlLoading(WebView view, string url)
        {
            return false;
        }
    }
}