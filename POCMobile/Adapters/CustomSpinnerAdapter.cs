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
using Java.Lang;
using POC.BusinessObjects;

namespace POCMobile.Adapters
{
    public class CustomSpinnerAdapter : BaseAdapter<LookupModel>
    {
        readonly Activity _context;
        private List<LookupModel> _items;

        public CustomSpinnerAdapter(Activity context,List<LookupModel> items)
        {
            this._context = context;
            this._items = items;
        }

        public override LookupModel this[int position]
        {
            get
            {
                return this._items[position];
            }
        }

        public override int Count
        {
            get
            {
                return this._items.Count;
            }
        }
        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }
        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = this._items[position];

            var view = (convertView ?? this._context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleSpinnerDropDownItem,
                parent, false));

            var name = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            name.Text = item.Description;
            return view;
        }
    }
}