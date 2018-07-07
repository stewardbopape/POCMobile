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
using Newtonsoft.Json;
using POC.BusinessObjects;
using POCMobile.Adapters;
using POCMobile.Services;
using ZXing.Mobile;

namespace POCMobile.Fragments
{
    public class fragHome : Android.Support.V4.App.DialogFragment
    {
       // ImageView imgView;
        EditText txtBarcode;
        //TextView _txtLocation;
        MainActivity _myActivity;
        Context _context;
        fragMap _parent;
        EditText txtIDNo;
        ImageView imgIDPhoto;
        Spinner cmbLanguage;

        public fragHome(fragMap fragParent)
        {
            this._parent = fragParent;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //int style =  DialogFragment.STYLE_NO_TITLE,
            SetStyle(StyleNoTitle, 0);
            _myActivity = (MainActivity)this.Activity;

        }
        

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            Dialog.Window.SetTitle("New Point");
            Dialog.SetCancelable(false);
            View view = inflater.Inflate(Resource.Layout.uiHome, container, false);


            _context = this.Context;


            //_txtLocation = view.FindViewById<TextView>(Resource.Id.txtLocation);

            //_txtLocation.Text = "Current Location Latitute: " + CurrentLocation.Latitude + " Longtude: " + CurrentLocation.Longitude;
            //imgView = view.FindViewById<ImageView>(Resource.Id.img_view);
            //txtBarcode = view.FindViewById<EditText> (Resource.Id.txtIDNumber);
            //txtIDNo = view.FindViewById<EditText>(Resource.Id.txtIDNo);

            //txtIDNo.Click += TxtIDNo_Click;
            //txtIDNo.SetOnClickListener(new View.IOnClickListener()
            //{


            //});

            //imgIDPhoto = view.FindViewById<ImageView>(Resource.Id.imgIDPhoto);
            //imgIDPhoto.Click += ImgIDPhoto_Click;


            //Button btnScan = view.FindViewById<Button>(Resource.Id.btnScan);
            //btnScan.Click += BtnScan_Click;

            Button btnCancel = view.FindViewById<Button>(Resource.Id.btnCancel);
            btnCancel.Click += (o, e) =>
            {
                this.Dismiss();
            };


            Button btnSave = view.FindViewById<Button>(Resource.Id.btnAdd);
            btnSave.Click += (o, e) =>
            {
                AddCustomerInformation();
            };

            var languages = GetLanguages();
           
            cmbLanguage = view.FindViewById<Spinner>(Resource.Id.cmbLanguage);
            cmbLanguage.Adapter = new CustomSpinnerAdapter(_myActivity, languages);
            cmbLanguage.ItemSelected += CmbLanguage_ItemSelected;

            return view;
            // return base.OnCreateView(inflater, container, savedInstanceState);
        }

        private void CmbLanguage_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private async void TxtIDNo_Click(object sender, EventArgs e)
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

        private void ImgIDPhoto_Click(object sender, EventArgs e)
        {
             Intent intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, 0);
        }

        private void AddCustomerInformation()
        {
            INFORMATION information = new INFORMATION();
            information.ID_NO = "8112135489081";
            information.ADDRESS1 = "400 Osprey";


            _parent.AddCustomerInformation(information);
            this.Dismiss();
        }

        //private async void  BtnScan_Click(object sender, EventArgs e)
        //{

        //    var MScanner = new MobileBarcodeScanner(_context);
        //    var Result = await MScanner.Scan();
        //    if (Result == null)
        //    {
        //        return;
        //    }
        //    //get the bar code text here 
        //    txtBarcode.Text = Result.Text;
        //}

       

       

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            try
            {
                Bitmap bitmap = (Bitmap)data.Extras.Get("data");
                imgIDPhoto.SetImageBitmap(bitmap);
            }
            catch { }
           
        }

        private List<LookupModel> GetLanguages()
        {
            List<LookupModel> langaues = new List<LookupModel>();
            langaues.Add(new LookupModel() { ID = "1", Description = "English" });
            langaues.Add(new LookupModel() { ID = "2", Description = "Afrikaans" });
            langaues.Add(new LookupModel() { ID = "3", Description = "Zulu" });
            langaues.Add(new LookupModel() { ID = "4", Description = "Sepedi" });
            langaues.Add(new LookupModel() { ID = "5", Description = "Sesotho" });
            langaues.Add(new LookupModel() { ID = "6", Description = "Xhosa" });
            langaues.Add(new LookupModel() { ID = "7", Description = "Venda" });
            langaues.Add(new LookupModel() { ID = "8", Description = "Tsonga" });
            langaues.Add(new LookupModel() { ID = "9", Description = "Ndebele" });
            langaues.Add(new LookupModel() { ID = "10", Description = "Zwati" });

            return langaues;

        }
    }
    

   
}