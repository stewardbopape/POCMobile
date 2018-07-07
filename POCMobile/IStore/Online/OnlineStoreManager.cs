using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using POC.BusinessObjects;
using SQLite.Net.Interop;

namespace POCMobile.IStore.Online
{
    public class OnlineStoreManager : IStoreManager, IDisposable
    {
        HttpClient httpClient;

        ISQLitePlatform platform;
        HttpResponseMessage response;
        public OnlineStoreManager()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.MaxResponseContentBufferSize = 2512000;
            //    httpClient.DefaultRequestHeaders.Host = AppConstants.BASE_SERVICE_URL;
            //httpClient.BaseAddress = new Uri(AppConstants.BASE_SERVICE_URL);

        }
        public ISQLitePlatform Platform
        {
            get
            {
                return platform;
            }

            set
            {
                platform = value;
            }
        }

        public async void GetObject(IServiceDeletegate<object> handler, GetAction action, params object[] param)
        {
            try
            {
                var uri = new Uri(String.Format(Config.BASE_SERVICE_URL + action.Url + String.Join("/", param), string.Empty));
                response = await httpClient.GetAsync(uri).ConfigureAwait(false); ;
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerSettings serSettings = new JsonSerializerSettings();
                    serSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    var data = response.Content.ReadAsStringAsync();

                    handler.HandleServiceResults(data.Result, true, string.Empty);
                }
                else
                    handler.HandleServiceResults(null, false, "Failed to connect to the web server, verify that you have airtime or switch off mobile data to work offline");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("A task was canceled"))
                    handler.HandleServiceResults(null, false, "Failed to connect to the web server, verify that you have airtime or switch off mobile data to work offline");
                else
                    handler.HandleServiceResults(null, false, ex.Message);
            }
        }
        public object GetObject(GetAction action, params object[] param)
        {
            string result = string.Empty;
            try
            {
                var uri = new Uri(String.Format(Config.BASE_SERVICE_URL + action.Url + String.Join("/", param), string.Empty));
                response = httpClient.GetAsync(uri).Result;
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerSettings serSettings = new JsonSerializerSettings();
                    serSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    var data = response.Content.ReadAsStringAsync();

                    return data.Result;
                }
                else
                    return "Failed to connect to the web server, verify that you have airtime or switch off mobile data to work offline";
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("A task was canceled"))
                    return "Failed to connect to the web server, verify that you have airtime or switch off mobile data to work offline";
                else
                    return ex.Message;
            }

        }

        public async void PostObject(IPostServiceDelegate<object> handler, PostObject<object> postObject)
        {
            ResultObj<object> result = new ResultObj<object>();

            try
            {
                var uri = new Uri(String.Format(Config.BASE_SERVICE_URL + postObject.PostAction.Url));
                var json = JsonConvert.SerializeObject(postObject);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                response = await httpClient.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerSettings serSettings = new JsonSerializerSettings();
                    serSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    var data = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<ResultObj<object>>(data.ToString());
                    handler.HandlePostResults(result);
                }
            }
            catch (Exception ex)
            {
                result.isSuccessful = false;
                result.Error = ex.Message;
                handler.HandlePostResults(result);
            }
        }

        public ResultObj<object> PostObject(PostObject<object> postObject)
        {
            ResultObj<object> result = new ResultObj<object>();

            try
            {
                var uri = new Uri(String.Format(Config.BASE_SERVICE_URL + postObject.PostAction.Url));
                var json = JsonConvert.SerializeObject(postObject);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                response = httpClient.PostAsync(uri, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerSettings serSettings = new JsonSerializerSettings();
                    serSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    var data = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<ResultObj<object>>(data.ToString());

                }
                return result;
            }
            catch (Exception ex)
            {
                result.isSuccessful = false;
                result.Error = ex.Message;
                return result;
            }
        }

        public object GetLookups(LookupAction actionOption, params object[] param)
        {
            string result = string.Empty;
            string paramValues = string.Empty;
            try
            {
                if (param != null)
                    paramValues = String.Join("/", param);


                var uri = new Uri(String.Format(Config.BASE_SERVICE_URL + actionOption.Url + paramValues, string.Empty));
                httpClient.Timeout = new TimeSpan(0, 20, 20);
                response = httpClient.GetAsync(uri).Result;
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerSettings serSettings = new JsonSerializerSettings();
                    serSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    result = response.Content.ReadAsStringAsync().Result;
                }
                return result;
            }
            catch 
            {
                //handler.HandleServiceResults(null, false, ex.Message);

                return null;
            }
        }
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

       
    }
}