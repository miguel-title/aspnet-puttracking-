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
using MiraklDouglasAPI.Models;
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

            MiraklDouglas.Store _Store = new MiraklDouglas.Store();

            _Store.StoreAPIURL = API_BASE_URL_TEST;
            _Store.WoocommerceKey = API_KEY_TEST;

            List<MiraklDouglas.ProductVM> _ProductVMs = InitProductVMs();

            // This test site with fake data
            ResponseHeader _ResponseHeader = new MiraklDouglas().BulkUpdateStock(_Store, _ProductVMs);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult OxatisPutTrackingNumber()
        {
            MiraklDouglas.Store _Store = new MiraklDouglas.Store();

            _Store.StoreAPIURL = API_BASE_URL_PROD_Nocibe;
            _Store.WoocommerceKey = API_KEY_URL_PROD_Nocibe;

            List<MiraklDouglas.ProductVM> _ProductVMs = InitProductVMs();
            // Beware !!! This is production site with real data
            //ResponseHeader _ResponseHeader = new MiraklDouglas().BulkUpdateStock(_Store, _ProductVMs);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult MiraklPutTrackingNumber()
        {
            MiraklDouglas.Store _Store = new MiraklDouglas.Store();

            _Store.StoreAPIURL = API_BASE_URL_PROD_FeelUnique;
            _Store.WoocommerceKey = API_KEY_URL_PROD_FeelUnique;

            List<MiraklDouglas.ProductVM> _ProductVMs = InitProductVMs();

            // Beware !!! This is production site with real data
            ResponseHeader _ResponseHeader = new MiraklDouglas().BulkUpdateStock(_Store, _ProductVMs);

            return RedirectToAction("Index", "Home");
        }


    }
}