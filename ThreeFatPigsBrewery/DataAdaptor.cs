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
using Java.Net;
using Android.Graphics;
using Java.IO;
using Android.Graphics.Drawables;
using Android.Util;
using System.Net;
using System.IO;


namespace ThreeFatPigsBrewery
{

	public class DataAdapter : BaseAdapter<BeerKegs> {

		List<BeerKegs> items;

		Activity context;
		public DataAdapter(Activity context, List<BeerKegs> items)
			: base()
		{
			this.context = context;
			this.items = items;
		}
		public override long GetItemId(int position)
		{
			return position;
		}
		public override BeerKegs this[int position]
		{
			get { return items[position]; }
		}
		public override int Count
		{
			get { return items.Count; }
		}
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var item = items[position];
			View view = convertView;
			if (view == null) // no view to re-use, create new
				view = context.LayoutInflater.Inflate(Resource.Layout.CustomRow, null);

			view.FindViewById<TextView>(Resource.Id.lblName).Text = "Name: " + item.Name;
			view.FindViewById<TextView>(Resource.Id.lblAddress).Text = "Address: " + item.Address;
            view.FindViewById<TextView>(Resource.Id.lblCity).Text = "city: " + item.City;
            view.FindViewById<TextView>(Resource.Id.lblInvoiceNumber).Text = "Invoice Number: " + item.InvoiceNumber.ToString();
            view.FindViewById<TextView>(Resource.Id.lblBarcode).Text = "Barcode: " + item.Barcode;
            view.FindViewById<TextView>(Resource.Id.lblDelivery).Text = "Delivery Verified: " + item.Delivered.ToString();


            //var imageBitmap = GetImageBitmapFromUrl(item.ImageURI);
            //view.FindViewById<ImageView> (Resource.Id.Image).SetImageBitmap (imageBitmap);
            return view;
        }

		//private Bitmap GetImageBitmapFromUrl(string url)
		//{
		//	Bitmap imageBitmap = null;
		//	if(!(url=="null"))
		//		using (var webClient = new WebClient())
		//		{
		//			var imageBytes = webClient.DownloadData(url);
		//			if (imageBytes != null && imageBytes.Length > 0)
		//			{
		//				imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
		//			}
		//		}

		//	return imageBitmap;
		//}

	}
}