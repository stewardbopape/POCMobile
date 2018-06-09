using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Esri.ArcGISRuntime.Geometry;
using ZXing.Mobile;

namespace POCMobile.Fragments
{
    public class fragHome : Android.Support.V4.App.DialogFragment
    {
        ImageView imgView;
        EditText txtBarcode;
        MapPoint _point;
        TextView _txtLocation;
        Context _context;
    

        //public fragHome(MapPoint point)
        //{
        //    this._point = point;
        //}
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            
          

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            Dialog.Window.SetTitle("New Point");
            Dialog.SetCancelable(false);
            View view = inflater.Inflate(Resource.Layout.uiHome, container, false);


            _context = this.Context;


            _txtLocation = view.FindViewById<TextView>(Resource.Id.txtLocation);

            _txtLocation.Text = "Current Location Latitute: " + CurrentLocation.Latitude + " Longtude: " + CurrentLocation.Longitude;
            imgView = view.FindViewById<ImageView>(Resource.Id.img_view);

            txtBarcode = view.FindViewById<EditText> (Resource.Id.txtIDNumber);

            Button btn = view.FindViewById<Button>(Resource.Id.btnPicture);
            
            btn.Click += Btn_Click;

            Button btnScan = view.FindViewById<Button>(Resource.Id.btnScan);
            btnScan.Click += BtnScan_Click;

            Button btnCancel = view.FindViewById<Button>(Resource.Id.btnCancel);
            btnCancel.Click += (o, e) =>
            {
                this.Dismiss();
            };


            Button btnSave = view.FindViewById<Button>(Resource.Id.btnAdd);
            btnSave.Click += (o, e) =>
            {
                Toast.MakeText(this.Context, "Your information will be saved", ToastLength.Long).Show();
                this.Dismiss();
            };

            return view;
            // return base.OnCreateView(inflater, container, savedInstanceState);
        }

        

        private async void  BtnScan_Click(object sender, EventArgs e)
        {

            var MScanner = new MobileBarcodeScanner(_context);
            var Result = await MScanner.Scan();
            if (Result == null)
            {
                return;
            }
            //get the bar code text here 
            txtBarcode.Text = Result.Text;
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, 0);
        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            Bitmap bitmap = (Bitmap)data.Extras.Get("data");
            imgView.SetImageBitmap(bitmap);
        }
    }
    
}