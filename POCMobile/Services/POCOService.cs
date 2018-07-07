using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace POCMobile.Services
{
    public class POCService : IPOCService, IDisposable
    {
        HttpClient httpClient;
        HttpResponseMessage responseMessage;

        public POCService()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.MaxResponseContentBufferSize = 2147483647;
        }


        public async void GetObject(IServiceDeletegate<object> handler, GetAction action, params object[] prms)
        {
            try
            {

                string parameters = string.Empty;
                if (prms != null)
                    parameters = String.Join("/", prms);

                var uri = new Uri(String.Format(Config.BASE_SERVICE_URL + action.Url + parameters, string.Empty));


                responseMessage = await httpClient.GetAsync(uri).ConfigureAwait(false);
                if (responseMessage.IsSuccessStatusCode)
                {
                    JsonSerializerSettings serSettings = new JsonSerializerSettings();
                    serSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    var data = responseMessage.Content.ReadAsStringAsync();

                    handler.HandleServiceResults(data.Result, true, action.Code, string.Empty);
                }
                else
                    handler.HandleServiceResults(null, false, action.Code, "Failed to connect to the web server, verify that you have airtime or switch off mobile data to work offline");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("A task was canceled"))
                    handler.HandleServiceResults(null, false, action.Code, "Failed to connect to the web server, verify that you have airtime or switch off mobile data to work offline");
                else
                    handler.HandleServiceResults(null, false, action.Code, ex.Message);
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

                responseMessage = await httpClient.PostAsync(uri, content);
                if (responseMessage.IsSuccessStatusCode)
                {
                    JsonSerializerSettings serSettings = new JsonSerializerSettings();
                    serSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    var data = responseMessage.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<ResultObj<object>>(data.ToString());
                    handler.HandlePostResults(result);
                }
                else
                {
                    result.Error = responseMessage.StatusCode.ToString();
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

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }


    public interface IPOCService
    {
        void GetObject(IServiceDeletegate<object> handler, GetAction action, params object[] prms);
        void PostObject(IPostServiceDelegate<object> handler, PostObject<object> postObject);
    }
    public interface IPostServiceDelegate<T>
    {
        void HandlePostResults(ResultObj<T> resultObject);
    }
    public interface IServiceDeletegate<T>
    {
        void HandleServiceResults(T resultRootObject, bool isSuccessfull, ActionCode resultType, string message);

    }
    public interface IServiceDeletegateList<T>
    {
        void HandleServiceResults(List<T> resultRootObject, bool isSuccessfull, string message);
    }
}