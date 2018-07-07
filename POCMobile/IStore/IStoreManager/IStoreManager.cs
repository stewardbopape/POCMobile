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
using POCMobile.Services;
using SQLite.Net.Interop;

namespace POCMobile.IStore
{
    public interface IStoreManager
    {
        ISQLitePlatform Platform { get; set; }
        void GetObject(IServiceDeletegate<object> handler, GetAction action, params object[] param);

        void PostObject(IPostServiceDelegate<object> handler, PostObject<object> postObject);

    }

    public interface IPostServiceDelegate<T>
    {
        void HandlePostResults(ResultObj<T> resultObject);
    }
    public interface IServiceDeletegate<T>
    {
        void HandleServiceResults(T resultRootObject, bool isSuccessfull, string message);


    }
    public interface IServiceDeletegateList<T>
    {
        void HandleServiceResults(List<T> resultRootObject, bool isSuccessfull, string message);

    }

}