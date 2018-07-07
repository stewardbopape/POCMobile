using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using POC.BusinessObjects;
using POCMobile.Services;
using SQLiteNetExtensions;
using SQLiteNetExtensionsAsync;
using SQLite.Net.Attributes;
using SQLite.Net;
using SQLiteNetExtensions.Extensions;
using SQLite.Net.Interop;
using Newtonsoft.Json.Serialization;

namespace POCMobile.IStore.Offline
{
    public class LocalStoreManager : IStoreManager, IDisposable
    {
        private SQLiteConnection _conn;
        private ISQLitePlatform platform;
        private string dbPath = string.Empty;
        JsonSerializerSettings serSettings;

        public LocalStoreManager(ISQLitePlatform platform)
        {
            dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + Config.DB_NAME);
            _conn = new SQLiteConnection(platform, dbPath);

            serSettings = new JsonSerializerSettings();
            serSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            if (platform != null)
                Platform = platform;
            CreateTables(); //create tables
        }

        protected void CreateTables()
        {

        }
        public void ClearDatabase()
        {



        }

        public ISQLitePlatform Platform
        {
            get
            {
                return platform;
            }
            set { platform = value; }
        }


        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public void GetObject(IServiceDeletegate<object> handler, GetAction action, params object[] param)
        {
            ResultObj<dynamic> result = new ResultObj<dynamic>();
            try
            {
                string error = string.Empty;
                switch (action.Code)
                {
                    case ActionCode.login:
                        //result.Data = _conn..GetAllWithChildren<OfficerModel>().Where(o => o.USERNAME.ToLower() == param[0].ToString().ToLower() && o.PASSWORD == param[1].ToString()).FirstOrDefault();
                        error = "Invalid username or password";
                        break;
                }

                if (result.Data != null)
                {
                    result.isSuccessful = true;
                    result.Error = string.Empty;
                }
                else
                {
                    result.isSuccessful = false;
                    result.Error = error;
                }

                if (action.Code == ActionCode.login)
                {
                    handler.HandleServiceResults(JsonConvert.SerializeObject(result), result.isSuccessful, result.Error);
                }
                else
                    handler.HandleServiceResults(JsonConvert.SerializeObject(result.Data), result.isSuccessful, result.Error);

            }
            catch { }
        }

        public void PostObject(IPostServiceDelegate<object> handler, PostObject<object> postObject)
        {
            ResultObj<object> result = new ResultObj<object>();
            result.Error = string.Empty;
            result.isSuccessful = true;

            try
            {
                var json = JsonConvert.SerializeObject(postObject.Data);

                switch (postObject.PostAction.Code)
                {
                    case ActionCode.login:

                        //SaveCoverpage(JsonConvert.DeserializeObject<CoverPageModel>(json));
                        handler.HandlePostResults(result);
                        break;

                }

            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
                result.isSuccessful = false;
                handler.HandlePostResults(result);
            }
        }

        public IEnumerable<object> GetLookup(LookupTypeCode lookup, params object[] param)
        {
            switch (lookup)
            {
                case LookupTypeCode.GetCitiesByRegion:
                    ;//return _conn.GetAllWithChildren<CityModel>().Where(o => o.LinkID == Convert.ToInt16(param[0])).ToList();
                    break;

            }

            return null;
        }
        public ResultObj<object> SaveLookup(String jsonData, LookupTypeCode code)
        {
            ResultObj<object> result = new ResultObj<object>();
            if (jsonData != string.Empty)
            {
                try
                {
                   
                    result.isSuccessful = true;
                    result.Error = string.Empty;
                }
                catch (Exception ex)
                {
                    result.isSuccessful = false;
                    result.Error = ex.Message;
                }
            }
            return result;
        }
        //public ResultObj<object> UpdateQuote(int quoteId, int workflowId, int outletSurveyId)
        //{
        //    ResultObj<object> result = new ResultObj<object>();
        //    result.isSuccessful = false;
        //    result.Error = string.Empty;

        //    try
        //    {
        //        if (_conn.Table<QuoteModel>().Where(o => o.QUOTE_ID == quoteId).Count() > 0)
        //        {
        //            var sql = "UPDATE QuoteModel SET WORKFLOW_ID= ? WHERE QUOTE_ID= ?";

        //            _conn.Execute(sql, workflowId, quoteId);

        //        }
        //        //inform outlest Survey can approve all
        //        if (workflowId == (int)WorkflowStatus.SupervisorApproved || (workflowId == (int)WorkflowStatus.SupervisorRejected))
        //        {
        //            this.UpdateOutletOnReject(outletSurveyId, workflowId);
        //        }

        //        result.isSuccessful = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.isSuccessful = false;
        //        result.Error = ex.Message;
        //    }

        //    return result;

        //}

    }
}