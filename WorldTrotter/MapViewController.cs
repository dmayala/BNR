using MapKit;
using System;
using System.Diagnostics;
using UIKit;

namespace WorldTrotter
{
    public partial class MapViewController : UIViewController
    {
        private MKMapView _mapView;

        protected MapViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void LoadView()
        {
            // Create a map view
            _mapView = new MKMapView();

            // Set it as *the* view of this view controller
            View = _mapView;

            var segmentedControl = new UISegmentedControl(new string[] { "Standard", "Hybrid", "Satellite" })
            {
                BackgroundColor = UIColor.White.ColorWithAlpha(0.5f),
                SelectedSegment = 0,
                TranslatesAutoresizingMaskIntoConstraints = false
            };

            segmentedControl.ValueChanged += (sender, e) => {
                var selectedSegmentId = (sender as UISegmentedControl).SelectedSegment;
                switch (selectedSegmentId)
                {
                    case 0:
                        _mapView.MapType = MKMapType.Standard;
                        break;
                    case 1:
                        _mapView.MapType = MKMapType.Hybrid;
                        break;
                    case 2:
                        _mapView.MapType = MKMapType.Satellite;
                        break;
                    default:
                        break;
                }
            };

            View.AddSubview(segmentedControl);

            // Create constraints from view anchors
            var topConstraint = segmentedControl.TopAnchor.ConstraintEqualTo(TopLayoutGuide.GetBottomAnchor(), 8);

            var margins = View.LayoutMarginsGuide;
            var leadingConstraint = segmentedControl.LeadingAnchor.ConstraintEqualTo(margins.LeadingAnchor);
            var trailingConstraint = segmentedControl.TrailingAnchor.ConstraintEqualTo(margins.TrailingAnchor);

            // Activate the constaints
            topConstraint.Active = true;
            leadingConstraint.Active = true;
            trailingConstraint.Active = true;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Debug.WriteLine("MapViewController loaded its view.");
        }

    }
}