using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Esri.ArcGISRuntime;
using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Security;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.UI;
using Esri.ArcGISRuntime.UI.Controls;

namespace POCMobile.Fragments
{
    public class fragMap : Android.Support.V4.App.Fragment, View.IOnTouchListener, ILocationListener
    {
        bool zoomtoLocation = false;
        LocationManager locMgr;
        string locationProvider;
        MapView _mapView;
        MainActivity _mainActivity;
        FragLocationDialog _dialogLocation;
        FloatingActionButton _flbtnAdd;
        FloatingActionButton _flbtrack;
        Esri.ArcGISRuntime.Mapping.Map _map;
        GraphicsOverlay _graphicOverlay;
        public fragMap()
        {

            //InitializeApp();
        }


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View view = inflater.Inflate(Resource.Layout.uiMap, container, false);
          
        
            _mapView = view.FindViewById<MapView>(Resource.Id.MyMapView);

            this._flbtnAdd = view.FindViewById(Resource.Id.flbtnAdd) as FloatingActionButton;
            this._flbtnAdd.Click += _flbtnAdd_Click;

            this._mainActivity = (MainActivity)this.Activity;

            this._mainActivity.SetToolBarTitle("Welcomes " + CurrentUser.UserName);

            this._flbtrack = view.FindViewById(Resource.Id.flblocation) as FloatingActionButton;
            this._flbtrack.Click += (o, e)=>{

                zoomtoLocation = !zoomtoLocation;
                if (zoomtoLocation)
                {
                    _mapView.SetViewpointScaleAsync(2150);
                    GetCurrentLcoation();
                }
            };

            return view;


        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            Initialize();
            GetCurrentLcoation();

        }

        private void Initialize()
        {
            
            this._map = new Map();
            _map.InitialViewpoint = new Viewpoint(InitalExtent);

            var _base = new ArcGISMapImageLayer(new Uri(Config.BASE_SERVICE_URL));
            _map.Basemap.BaseLayers.Add(_base);

            this._mapView.GeoViewTapped += _mapView_GeoViewTapped;
            this._mapView.Map = _map;
            //this._mapView.Map.MaxScale = 100;

            AddGraphicOverlay();

        }
        private Envelope InitalExtent
        {
            get
            {
                Envelope initialLocation = new Envelope(16.451910000000055, -34.83416999999997, 32.944980000000044, -22.12502999999998, new Esri.ArcGISRuntime.Geometry.SpatialReference(4148));
                return initialLocation;
            }
        }
        private void AddGraphicOverlay()
        {

            _graphicOverlay = new GraphicsOverlay();
            if (_mapView.GraphicsOverlays.Count > 0)
                _mapView.GraphicsOverlays.RemoveAt(0);
            _mapView.GraphicsOverlays.Add(_graphicOverlay);

        }

        private void _flbtnAdd_Click(object sender, EventArgs e)
        {
            Config.MapTapMode = MapTapMode.Add;
            fragHome fragement = new fragHome();
            var trans = FragmentManager.BeginTransaction();
            //trans.SetCustomAnimations(Resource.Animation.gla_there_come, Resource.Animation.gla_there_gone);
            fragement.Show(trans, "add");
        }

        private async void _mapView_GeoViewTapped(object sender, GeoViewInputEventArgs e)
        {
            try
            {               
                
                        CreateNewFeature(e.Location);               
             
            }
            catch (Exception ex)
            {
                _mainActivity.RunOnUiThread(() =>
                {
                    ShowToastMessage(ex.Message);
                });
            }


        }
        private void CreateNewFeature(MapPoint point)
        {
           
        }

        private void ShowToastMessage(string toastMessage)
        {
            Toast toast = Toast.MakeText(this.Context, toastMessage, ToastLength.Short);
            toast.SetGravity(GravityFlags.Top | GravityFlags.CenterHorizontal, 0, 250);
            TextView v = (TextView)toast.View.FindViewById(Android.Resource.Id.Message);
            v.SetTextColor(Android.Graphics.Color.White);
            toast.View.SetBackgroundColor(Android.Graphics.Color.Black);

            toast.Show();
        }
       

        #region "Location"
        private void GetCurrentLcoation()
        {

            Criteria locationCriteria = new Criteria();
            locationCriteria.Accuracy = Accuracy.Fine;
            locationCriteria.PowerRequirement = Power.Low;

            if (locMgr == null)
            {
                locMgr = _mainActivity.GetSystemService(Context.LocationService) as LocationManager;
            }

            locationProvider = locMgr.GetBestProvider(locationCriteria, true);
            if (locationProvider != null)
            {

                locMgr.RequestLocationUpdates(locationProvider, 2000, 1, this);
            }
            else
            {
                this._dialogLocation = new Fragments.FragLocationDialog();

                //var trans = FragmentManager.BeginTransaction();               
                //this._dialogLocation.Show(trans, "location");
                Toast.MakeText(this.Context, "No location providers available", ToastLength.Long).Show();
            }

        }
        public void OnLocationChanged(Location location)
        {
            CurrentLocation.Latitude = location.Latitude;
            CurrentLocation.Longitude = location.Longitude;
            MapPoint point = new MapPoint(location.Longitude, location.Latitude, new Esri.ArcGISRuntime.Geometry.SpatialReference(4148));
            UpdateUserLocationOnMap(point);
        }

        public void OnProviderDisabled(string provider)
        {

        }

        public void OnProviderEnabled(string provider)
        {

        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {

        }
        public void UpdateUserLocationOnMap(MapPoint point)
        {

            SimpleLineSymbol outlineSymbol = new SimpleLineSymbol(SimpleLineSymbolStyle.Solid, System.Drawing.Color.Red, 4);

            SimpleMarkerSymbol buoyMarker = new SimpleMarkerSymbol(SimpleMarkerSymbolStyle.Circle, System.Drawing.Color.Red, 10);
            buoyMarker.Outline = outlineSymbol;

            Graphic g = new Graphic(point);
            g.Symbol = buoyMarker;

            try
            {
                if (_mapView.GraphicsOverlays.Count > 0)
                {
                    if (_mapView.GraphicsOverlays[0].Graphics.Count > 0)
                        _mapView.GraphicsOverlays[0].Graphics.Clear();
                }
                _mapView.GraphicsOverlays[0].Graphics.Add(g);
                if (zoomtoLocation)
                {
                   //Envelope env = new Envelope(point.X - 50000, point.Y -50000, point.X + 5000, point.Y+ 5000,new Esri.ArcGISRuntime.Geometry.SpatialReference(4148));
                   // double tolerance = 10;
                   // double mapTolerance = tolerance * _mapView.UnitsPerPixel;
                   // var selectionEnvelope = new Envelope(point.X - mapTolerance, point.Y - mapTolerance, point.X + mapTolerance,
                   //point.Y + mapTolerance, new Esri.ArcGISRuntime.Geometry.SpatialReference(4148));

                 
                  
                    //_map.InitialViewpoint = viewpoint;

                   
                     _mapView.SetViewpointGeometryAsync(point.Extent);
                   







                }
                  
                   
            }
            catch { }

        }
        #endregion
        public bool OnTouch(View v, MotionEvent e)
        {
            return true;
        }


        private void InitializeApp()
        {
            //ArcGISRuntimeEnvironment.SetLicense("runtimelite,1000,rud8694580570,none,NKMFA0PL4SP2F5KHT113");

        }


    }
}