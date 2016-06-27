using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace ThreeFatPigsBrewery
{
    class RESThandler
    {

        
            private string url;
            private IRestResponse response;
            private RestRequest request;

            public RESThandler()
            {
                url = "";
            }


            public RESThandler(string lurl)
            {
                url = lurl;
                request = new RestRequest();
            }

            public void AddParameter(string name, string value)
            {
                if (request != null)
                {
                    request.AddParameter(name, value);
                }
            }

            public List<BeerKegs> ExecuteRequest()
            {
                var client = new RestClient(url);

                response = client.Execute(request);

                BeerKegs objRoot = new BeerKegs();

                List<BeerKegs> LstBeerKegs = new List<BeerKegs>();
                LstBeerKegs = JsonConvert.DeserializeObject<List<BeerKegs>>(response.Content);

                return LstBeerKegs;


            }

            public async Task<List<BeerKegs>> ExecuteRequestAsync()
            {

                var client = new RestClient(url);
                var request = new RestRequest();

                response = await client.ExecuteTaskAsync(request);

            List<BeerKegs> LstBeerKegs = new List<BeerKegs>();
            LstBeerKegs = JsonConvert.DeserializeObject<List<BeerKegs>>(response.Content);

            return LstBeerKegs;
        }


        }
    }

