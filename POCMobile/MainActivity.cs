using System;
using System.Collections.Generic;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using POCMobile.Fragments;
using SupportFragment = Android.Support.V4.App.Fragment;

namespace POCMobile
{
    //[Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true, ConfigurationChanges = Android.Content.PM.ConfigChanges.ScreenSize | Android.Content.PM.ConfigChanges.Orientation,ScreenOrientation = ScreenOrientation.Landscape)]
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true, ConfigurationChanges = Android.Content.PM.ConfigChanges.ScreenSize | Android.Content.PM.ConfigChanges.Orientation)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener, ILocationListener
    {
        private Stack<SupportFragment> mStackFragment;
        DrawerLayout drawer;
        Android.Support.V7.Widget.Toolbar toolbar;
        LocationManager locMgr;
        string locationProvider;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
             toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            //FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            //fab.Click += FabOnClick;

            drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);
            InitializeLocationManager();
            GetAppVersionName();
            ShowSideMenu(true);
            var trans = SupportFragmentManager.BeginTransaction();
            trans.Replace(Resource.Id.fragmentContainer, new fragLogin(), "login");
            trans.Commit();


        }
        
        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if(drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.nav_camera)
            {
                // Handle the camera action
            }
            else if (id == Resource.Id.nav_gallery)
            {

            }
            else if (id == Resource.Id.nav_slideshow)
            {

            }
            else if (id == Resource.Id.nav_manage)
            {

            }
            else if (id == Resource.Id.nav_share)
            {

            }
            else if (id == Resource.Id.nav_send)
            {

            }
            else if (id == Resource.Id.log_out)
            {
                SupportFragmentManager.PopBackStack();
                StartActivity(typeof(MainActivity));


            }


            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }

        public void SetToolBarTitle(string title)
        {
            SupportActionBar.Title = title;
        }

        private void GetAppVersionName()
        {
            PackageManager manager = this.PackageManager;
            PackageInfo info = manager.GetPackageInfo(this.PackageName, 0);
            Config.AppVersionName = info.VersionName;
            Config.AppVersionCode = info.VersionCode.ToString();
        }
        public void ShowSideMenu(bool locked)
        {
            if (locked)
            {
                toolbar.Visibility = ViewStates.Gone;
                drawer.SetDrawerLockMode(DrawerLayout.LockModeLockedClosed);
            }
            else
            {
                toolbar.Visibility = ViewStates.Visible;
                drawer.SetDrawerLockMode(DrawerLayout.LockModeUnlocked);
            }

        }
        private void InitializeLocationManager()
        {

            Criteria locationCriteria = new Criteria();
            locationCriteria.Accuracy = Accuracy.Fine;
            locationCriteria.PowerRequirement = Power.Low;

            if (locMgr == null)
            {
                locMgr = GetSystemService(Context.LocationService) as LocationManager;
            }

            locationProvider = locMgr.GetBestProvider(locationCriteria, true);
            if (locationProvider != null)
            {

                locMgr.RequestLocationUpdates(locationProvider, 2000, 1, this);
            }
            else
            {
                Toast.MakeText(this, "No location providers available", ToastLength.Long).Show();
            }

        }
        public void OnLocationChanged(Location location)
        {
            CurrentLocation.Latitude = location.Latitude;
            CurrentLocation.Longitude = location.Longitude;
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

        //private void InitializeLocationManager()
        //{

        //    Criteria locationCriteria = new Criteria();
        //    locationCriteria.Accuracy = Accuracy.Fine;
        //    locationCriteria.PowerRequirement = Power.Low;

        //    if (locMgr == null)
        //    {
        //        locMgr = GetSystemService(Context.LocationService) as LocationManager;
        //    }

        //    locationProvider = locMgr.GetBestProvider(locationCriteria, true);
        //    if (locationProvider != null)
        //    {

        //        locMgr.RequestLocationUpdates(locationProvider, 2000, 1, this);
        //    }
        //    else
        //    {
        //        Toast.MakeText(this, "No location providers available", ToastLength.Long).Show();
        //    }

        //}
        //public void OnLocationChanged(Location location)
        //{
        //    CurrentLocation.Latitude = location.Latitude;
        //    CurrentLocation.Longitude = location.Longitude;
        //}

        //public void OnProviderDisabled(string provider)
        //{
        //    throw new NotImplementedException();
        //}

        //public void OnProviderEnabled(string provider)
        //{
        //    throw new NotImplementedException();
        //}

        //public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

