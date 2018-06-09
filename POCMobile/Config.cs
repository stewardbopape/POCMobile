using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace POCMobile
{
    public enum MapTapMode : int
    {
        Select = 1,
        Add = 2,
    }
    public static class Config
    {
        private static string _appVersionName = string.Empty;
        private static string _appVersionCode = string.Empty;
        private static string _baseServiceUrl;
        static Config()
        {
            _baseServiceUrl = "https://services.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer";
        }


        public static string AppVersionName
        {
            get { return _appVersionName; }
            set { _appVersionName = value; }
        }
        public static string AppVersionCode
        {
            get { return _appVersionCode; }
            set { _appVersionCode = value; }
        }
        public static string BASE_SERVICE_URL
        {
            get
            {
                return _baseServiceUrl;
            }

        }
        public static MapTapMode MapTapMode
        {
            get; set;
        }
    }
    public static class CurrentLocation
    {
        private static double _latitude;
        private static double _logitude;
        private static string _address;
        public static double Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }
        public static double Longitude
        {
            get { return _logitude; }
            set { _logitude = value; }
        }
        public static string Address
        {
            get { return _address; }
            set { _address = value; }
        }
    }
    public static class CurrentUser
    {
        private static string _username;
        public static string UserName
        {
            get { return _username; }
            set { _username = value; }
        }


    }

    public static class DigitalGlobe
    {
        public static string UserName { get; set; }
        public static string Password { get; set; }
        public static bool Active { get; set; }
        public static string DFBaseMapUrl { get; set; }

    }
}