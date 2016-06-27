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
using RestSharp;
using System.Net;

namespace ThreeFatPigsBrewery
{
    [Activity(Label = "Records", Theme = "@android:style/Theme.DeviceDefault")]
    public class Records : Activity
    {


        List<BeerKegs> TFP = new List<BeerKegs>();
        ListView LVbeer;
        RESThandler objRest;
        AlertDialog alertDialog;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.List);

            LVbeer = FindViewById<ListView>(Resource.Id.LVbeer);

            getbeerkegs();

            LVbeer.ItemClick += LVbeer_Click;

        }

        private void LVbeer_Click(object sender, AdapterView.ItemClickEventArgs e)
        {
            alertDialog = new AlertDialog.Builder(this).Create();
            
            CheckBox cbDelivery = new CheckBox(this);

            var BeerKeg = TFP[e.Position];


            alertDialog.SetTitle("Delivery Status for " + BeerKeg.P_Id + " " + BeerKeg.InvoiceNumber );
            cbDelivery.Text = "delivered";
            alertDialog.SetView(cbDelivery);
            alertDialog.SetButton("submit", (s, ev) =>
            {
                if (cbDelivery.Checked == true)
                {
                    UpdateKeg(BeerKeg);

                }
            }

            );

            alertDialog.SetButton2("main menu", (s, ev) =>
            {
                Finish();

            }

            );


            alertDialog.SetButton2("delete", (s, ev) =>
            {
                deletekeg(BeerKeg.P_Id);

            }
            );

            alertDialog.SetCancelable(true);
            alertDialog.Show();
        }

        public void deletekeg(int id)
        {
            var client = new RestClient(@"http://threefatpigs.azurewebsites.net/");

            var request = new RestRequest("api/BeerKegs/" + id, Method.DELETE);

            // Json to post.
            string json = "";

            request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;

            try
            {
                client.ExecuteAsync(request, response =>
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Toast.MakeText(this, "Record deleted Successfully", ToastLength.Long).Show();
                        
                    }
                    else
                    {
                        Toast.MakeText(this, "Something went wrong", ToastLength.Long).Show();
                    }
                });

                getbeerkegs();
            }

            

            catch (Exception error)
            {
                Toast.MakeText(this, "Something went wrong" + error.Message, ToastLength.Long).Show();
            }



        }
        public void UpdateKeg(BeerKegs objBeer)
        {
            objBeer.Delivered = true;
            var client = new RestClient(@"http://threefatpigs.azurewebsites.net/");

            var request = new RestRequest("api/BeerKegs/" + objBeer.P_Id, Method.PUT);


            // Json to post.
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(objBeer);

            request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;

            try
            {
                client.ExecuteAsync(request, response =>
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Toast.MakeText(this, "Record updated Successfully", ToastLength.Long).Show();

                    }
                    else
                    {
                        Toast.MakeText(this, "Something went wrong", ToastLength.Long).Show();
                    }
                });

               getbeerkegs();

            }
            catch (Exception error)
            {
                Toast.MakeText(this, "Something went wrong" + error.Message, ToastLength.Long).Show();
            }
        }






        public async void getbeerkegs()
        {

            objRest = new RESThandler(@"http://threefatpigs.azurewebsites.net/api/BeerKegs");
            var Response = await objRest.ExecuteRequestAsync();
            TFP = Response;

            Console.WriteLine(Response);

            LVbeer.Adapter = new DataAdapter(this, Response);
            

        }


    }
}