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
using POC.BusinessObjects;

namespace POCMobile
{
    public enum MapTapMode : int
    {
        Select = 1,
        Add = 2,
    }
    public enum LookupTypeCode : int
    {
        Undefined = -1,
        GetCitiesByRegion = 0,
        GetAllSurbyByRegion,
        GetSuburbsByCityID,
        GetCountries,
        GetCollectionTypes,
        GetStreetTypes,
        GetCollectionWeeks,
        GetOrigin,
        GetValidateKg,
        GetOutLetReasons,
        GetSPDPurposes,
        GetProductByBrandID,
        GetAllProducts,
        GetAllBrands,
        GetAllQuantities,
        GetAllUnityOfSizes,
        GetAllProductByBrands,
        GetBrandsByIndicatorProductID,
        GetQuantityByIndProdID,
        GetUnityOfSizeByIndProdID,

        GetOutletsByOfficerID,
        GetQuotesByOutletSurveryIDAndOfficerID,
        GetCoverPageByOutletSurveryID,
        Login,
        GetoutletsBySupervisorID,
        GetQuotesByOutletSurveryIDAndSupervisorID,
        GetAllocationOfficersByRegionID
    };
    public class LookupAction
    {
        public LookupTypeCode Code { get; set; }
        public string Url { get; set; }
    }

    public static class Config
    {
        private static string _appVersionName = string.Empty;
        private static string _appVersionCode = string.Empty;
        //private static string _baseServiceUrl;
        // public static string BASE_SERVICE_URL = "http://164.151.130.41:8088/cpimobile2/api/";
        //public static string BASE_SERVICE_URL = "http://geoinfo.statssa.gov.za/cpimobile/api/";
        //public static string BASE_SERVICE_URL = "http://geoinfo.statssa.gov.za/cpimobileqa/api/";
    //   public static string BASE_SERVICE_URL = "http://10.0.2.2:56613/api/";

        public static List<GetAction> GetActions;
        public static List<PostAction> PostActions;
        public static List<LookUpAction> SetupActions;

        public static string DB_NAME = "POCMobile.db3";
        public static int DB_VERSION = 1;

        static Config()
        {
            // _baseServiceUrl = "https://services.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer";

            GetActions = new List<GetAction>();
            GetActions.Add(new GetAction() { Code = ActionCode.login, Url = @"user/login/" });

            //CONFIGURING THE GET LOOKUP ACTIONS
            SetupActions = new List<LookUpAction>();

            //CONFIGURING THE POST ACTIONS
            PostActions = new List<PostAction>();
            PostActions.Add(new PostAction() { Code = ActionCode.AddInformation, Url = @"information/save/" });
        }
        public static string ErrServiceCallError;
        public static string ErrMissingAction;

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
                //return "http://10.0.2.2/pocapi/api/";

                return "http://10.112.10.85/pocapi/api/";
               
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