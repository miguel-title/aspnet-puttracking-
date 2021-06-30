using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AtomicSeller.Helpers;
using AtomicSeller.ViewModels;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using AtomicCommonAPI.Models;
using AtomicSeller.Models;

namespace AtomicSeller.Controllers
{
    public class HomeController : BaseController
    {
        /*
         Priority 1 Joom: Fix a bug on existing feature : Put tracking number
         Priority 2 Oxatis: Develop from scratch PutTrackingNumber
         Priority 3 Mirakl:  Fix a bug on existing feature : Put tracking number
         */

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult JoomPutTrackingNumber()
        {
            JoomStore _Store = new JoomStore();
            _Store.APIURL = "https://api-merchant.joom.com";
            _Store.User= "sheny.ecommerce@gmail.com";
            _Store.Password = "Nahman148";
            _Store.ClientID = "b5849d8dcbe34a28";
            _Store.Clientsecret = "faf02e4c1a19b54da4e0da8f17a85b9e";

            string strProviderID = "1494503711005136775-195-61-709-1614392117";
            string strTrackingNumber = "RS921315778DE";
            string orderID = "63VM5MQV";
            // This test site with fake data
            ResponseHeader _ResponseHeader = new Joom().PutTrackingNumber(_Store, strProviderID, strTrackingNumber, orderID);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult OxatisPutTrackingNumber()
        {
            OxatisStore _Store = new OxatisStore();

            _Store.APIURL = "";
            _Store.AppID = "aa8026275ea4c18fb15bd1085d30b6d0";
            _Store.Token = "AF134034BD96236703D84BBB65";

            // Beware !!! This is production site with real data
            ResponseHeader _ResponseHeader = new Oxatis().PutTrackingNumber(_Store);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult MiraklPutTrackingNumber()
        {
            MiraklStore _Store = new MiraklStore();
            //_Store.APIKey = "aded029a-7b57-48a5-b492-4353596ae60d";
            _Store.APIKey = "1f038315-ec48-4fc5-82f3-11e87e6ad827";
            _Store.APIURL = "https://douglas2-dev.mirakl.net";

            bool deliveryStatus = false;
            string trackingNumber = "1Z2356F1ZJ98L9733M5";
            string carrier_name = "UPS";
            string carrier_code = "";
            string trackingurl = "https://wwwapps.ups.com/WebTracking/track?track=yes&trackNums={trackingId}";

            string orderID = "DGLDE000003220301-A";

            //// Beware !!! This is production site with real data
            ResponseHeader _ResponseHeader = new MiraklDouglas().PutTrackingNumber(_Store, orderID, deliveryStatus, trackingNumber, carrier_name, carrier_code, trackingurl);

            return RedirectToAction("Index", "Home");
        }


    }
}