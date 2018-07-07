using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace POCMobile.Services
{
    public class NetworkReachability
    {
        Activity activity;
        Thread checkNetworkActiveThread;
        ConnectivityManager connectivityManager;
        bool isDialogShowing;
        AlertDialog.Builder builder;
        public NetworkReachability(Activity activity)
        {
            this.activity = activity;
            connectivityManager= (ConnectivityManager) activity.GetSystemService(Context.ConnectivityService);
            this.doAlertDialog();
            this.CheckNetworkRechability();
        }

        private void CheckNetworkRechability()
        {
            checkNetworkActiveThread = new Thread(new ThreadStart(CheckNetworkAvailable));
            checkNetworkActiveThread.Start();
        }

        private async void CheckNetworkAvailable()
        {
            bool isNetwork = await Task.Run(() => this.NetworkReachableOrNot());
            if (!isNetwork)
            {
                activity.RunOnUiThread(() =>
                {

                    try
                    {
                        if (!this.isDialogShowing)
                        {
                            isDialogShowing = true;
                            builder.Show();
                        }
                    }
                    catch { }
                });
            }
            else
            {
                isDialogShowing = false;
                this.CheckNetworkAvailable();
            }
        }

        private bool NetworkReachableOrNot()
        {
            var activeConnection = connectivityManager.ActiveNetworkInfo;
            if ((activeConnection != null) && activeConnection.IsConnected)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void doAlertDialog()
        {
            if (builder != null)
                builder.Dispose();

            builder = new AlertDialog.Builder(activity);
            builder.SetTitle("Network not reachable");
            builder.SetMessage("Please check network");
            builder.SetCancelable(true);
            builder.SetNegativeButton("Retry", AlertRetryClick);
            builder.SetPositiveButton("Cancel", AlertCancelClick);
        }
        private void AlertRetryClick(object sender1, DialogClickEventArgs args)
        {
            isDialogShowing = false;
            this.CheckNetworkAvailable();
        }

        private void AlertCancelClick(object sender1, DialogClickEventArgs args)
        {
            System.Environment.Exit(0);
        }
    }
}