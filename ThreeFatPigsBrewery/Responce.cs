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

namespace ThreeFatPigsBrewery
    {

        public class BeerKegs
        {
            public int P_Id { get; set; }
            public string Address { get; set; }
            public string Name { get; set; }
            public string City { get; set; }
            public string Barcode { get; set; }
            public bool Delivered { get; set; }
            public int InvoiceNumber { get; set; }
        }
    }