using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using RestSharp;
using System.Net;

namespace ThreeFatPigsBrewery
{
    [Activity(Label = "ThreeFatPigsBrewery", MainLauncher = true, Icon = "@drawable/TFProundIcn")]
    public class MainActivity : Activity
    {
        Button btnScan;
        Button btnRcds;
        Button btnAdd;
        Button btnSearch;
        TextView txtBarcode;
        TextView txtAddress;
        TextView txtCity;
        TextView txtInvoice;
        TextView txtName;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it

            btnScan = FindViewById<Button>(Resource.Id.btnScan);
            btnScan.Click += BtnScan_Click;
            txtBarcode = FindViewById<TextView>(Resource.Id.txtBarcode);
            txtAddress = FindViewById<TextView>(Resource.Id.txtAddress);
            txtCity = FindViewById<TextView>(Resource.Id.txtCity);
            txtInvoice = FindViewById<TextView>(Resource.Id.txtInvoice);
            txtName = FindViewById<TextView>(Resource.Id.txtName);
            btnAdd = FindViewById<Button>(Resource.Id.btnAdd);
            btnRcds = FindViewById<Button>(Resource.Id.btnRcds);
            btnSearch = FindViewById<Button>(Resource.Id.btnSearch);

            btnAdd.Click += BtnAdd_Click;

            btnRcds.Click += BtnRcds_Click;

            btnSearch.Click += BtnSearch_Click;

            

        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            var Search = new Intent(this, typeof(Search));

            Search.PutExtra("Barcode", txtBarcode.Text);
            //edititem.PutExtra("Details", ToDoItem.Details);
            Search.PutExtra("City", txtCity.Text);

            StartActivity(Search);

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
           

           
               // Uri requestUri = new Uri("http://threefatpigs.azurewebsites.net/api/BeerKegs");
                string json = "";

                BeerKegs objBeer = new BeerKegs();

                objBeer.Name = txtName.Text;
                objBeer.Address = txtAddress.Text;
                objBeer.City = txtCity.Text;
                objBeer.Barcode = txtBarcode.Text;
                objBeer.InvoiceNumber = Convert.ToInt16(txtInvoice.Text);

                var client = new RestClient(@"http://threefatpigs.azurewebsites.net/");

                var request = new RestRequest("api/BeerKegs", Method.POST);

                // Json to post.
                json = Newtonsoft.Json.JsonConvert.SerializeObject(objBeer);

                request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
                request.RequestFormat = DataFormat.Json;

                try
                {
                    client.ExecuteAsync(request, response =>
                    {
                        if (response.StatusCode == HttpStatusCode.Created)
                        {
                            Toast.MakeText(this, "Record Added Successfully", ToastLength.Long).Show();

                        }
                        else
                        {
                            Toast.MakeText(this, "Something went wrong", ToastLength.Long).Show();
                        }
                    });
                }
                catch (Exception error)
                {
                    Toast.MakeText(this, "Something went wrong" + error.Message, ToastLength.Long).Show();
                }
            txtBarcode.Text = "";
            txtAddress.Text = "";
            txtCity.Text = "";
            txtInvoice.Text = "";
            txtName.Text = "";

        }
    



    private void BtnRcds_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(Records));
        }

        private async void BtnScan_Click(object sender, EventArgs e)
        {
            var scanner = new ZXing.Mobile.MobileBarcodeScanner();
            var result = await scanner.Scan();

            if (result != null)
            {
                Toast.MakeText(this, "Scanned Barcode" + result.Text, ToastLength.Long).Show();
                txtBarcode.Text = (result.Text);
            }
            else
            {
                StartActivity(typeof(MainActivity));
            }
        }
        //    public void onbackpressed()
        //{
        //    Finish();
        //}
                


        

    }
}

