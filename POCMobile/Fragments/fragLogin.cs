using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using POC.BusinessObjects;
using POCMobile.Services;

namespace POCMobile.Fragments
{
    public class fragLogin : Android.Support.V4.App.Fragment, IServiceDeletegate<object>
    {
        private MainActivity _mainActivity;
        private ProgressBar _progressBar;
        EditText _txtUserName ;
        EditText _txtPassword ;
        TextView _lblError ;
        private CL_USERS _user;
        POCService _service;

        TextView _txtAppVersion;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

           

            // Create your fragment here
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            _txtAppVersion.Text = Config.AppVersionName + " V: " + Config.AppVersionCode;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View view = inflater.Inflate(Resource.Layout.uiLogin, container, false);

             _txtUserName = view.FindViewById<TextInputLayout>(Resource.Id.txtInputLayoutUsername).EditText;
             _txtPassword = view.FindViewById<TextInputLayout>(Resource.Id.txtInputLayoutPassword).EditText;
             _lblError = view.FindViewById<TextView>(Resource.Id.lblError);
              Button btnLogin = view.FindViewById<Button>(Resource.Id.btnLogin);


            _progressBar = view.FindViewById<ProgressBar>(Resource.Id.progressBar1);
            _txtAppVersion = view.FindViewById<TextView>(Resource.Id.appversion);
            _mainActivity = (MainActivity)this.Activity;
            _progressBar.Visibility = ViewStates.Invisible;


            btnLogin.Click += BtnLogin_Click;

            return view;

            // return base.OnCreateView(inflater, container, savedInstanceState);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            Console.WriteLine("BtnLogin Entry");

          

            _lblError.Text = string.Empty;
            if (_txtUserName.Text.Length == 0 || _txtPassword.Text.Length == 0)
            {
                _lblError.Text = "Username or password is required";
            }
            else
            {
                GetAction action = Config.GetActions.Where(o => o.Code == ActionCode.login).SingleOrDefault();

                if (action == null)
                    throw new Exception(Config.ErrMissingAction);

                object[] param = new[] { _txtUserName.Text, _txtPassword.Text };

                _service = new POCService();
                _progressBar.Visibility = ViewStates.Visible;
                Console.WriteLine("BtnLogin Calling the service");
                _service.GetObject(this, action, param);
            }
            Console.WriteLine("BtnLogin Exit");

        }

        public void HandleServiceResults(object resultRootObject, bool isSuccessfull, ActionCode resultType, string message)
        {
            _mainActivity = (MainActivity)this.Activity;
            _user = new CL_USERS();

            if (resultRootObject != null)
            {
                JsonSerializerSettings serSettings = new JsonSerializerSettings();
                serSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                if (resultType == ActionCode.login)
                {
                    var resultObj = JsonConvert.DeserializeObject<ResultObj<CL_USERS>>(resultRootObject.ToString(), serSettings);

                    if (resultObj.isSuccessful)
                    {

                        _user = JsonConvert.DeserializeObject<ResultObj<CL_USERS>>(resultRootObject.ToString(), serSettings).Data;

                        //if (_user.isValidUser)
                        //{
                            _mainActivity.RunOnUiThread(() =>
                            {
                               
                                _lblError.Text = string.Empty;
                               

                                CurrentUser.UserName = _txtUserName.Text;
                                var trans = FragmentManager.BeginTransaction();
                                // trans.SetCustomAnimations(Resource.Animation.anim_in, Resource.Animation.anim_out);
                                _progressBar.Visibility = ViewStates.Visible;

                                _mainActivity.ShowSideMenu(false);
                                trans.Replace(Resource.Id.fragmentContainer, new fragMap(), "map").AddToBackStack("map");
                                trans.Commit();
                            });
                        //}
                        //else
                        //{
                        //    _mainActivity.RunOnUiThread(() =>
                        //    {
                        //        _progressBar.Visibility = ViewStates.Gone;
                        //        _lblError.Text = resultObj.Error;
                        //    });
                        //}
                    }
                    else
                    {
                        _mainActivity.RunOnUiThread(() =>
                        {
                            _progressBar.Visibility = ViewStates.Gone;
                            _lblError.Text = resultObj.Error;
                        });
                    }
                }
            }
            else
            {
                _mainActivity.RunOnUiThread(() =>
                {
                    _progressBar.Visibility = ViewStates.Gone;
                    _lblError.Text = Config.ErrServiceCallError;
                });
            }
        }

    }
}
