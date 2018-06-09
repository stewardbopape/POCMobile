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

namespace POCMobile.Fragments
{
    public class fragLogin : Android.Support.V4.App.Fragment
    {
        private MainActivity _mainActivity;
        private ProgressBar _progressBar;
        EditText _txtUserName ;
        EditText _txtPassword ;
        TextView _lblError ;

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
            else if (_txtUserName.Text == "stewardb" && _txtPassword.Text == "123456")
            {
                CurrentUser.UserName = _txtUserName.Text;
                var trans = FragmentManager.BeginTransaction();
                // trans.SetCustomAnimations(Resource.Animation.anim_in, Resource.Animation.anim_out);
                _progressBar.Visibility = ViewStates.Visible;

                _mainActivity.ShowSideMenu(false);
                trans.Replace(Resource.Id.fragmentContainer, new fragMap(), "map").AddToBackStack("map");
                trans.Commit();
                //_lblError.Text = "Successful";
            }
            else
            {
                _lblError.Text = "Invalid username or password";
            }
            Console.WriteLine("BtnLogin Exit");

        }

    }
}