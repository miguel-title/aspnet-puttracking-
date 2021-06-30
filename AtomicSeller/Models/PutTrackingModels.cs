using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AtomicCommonAPI.Models;

namespace AtomicSeller.Models
{
    public partial class JoomStore
    {
        public string APIURL { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string ClientID { get; set; }
        public string Clientsecret { get; set; }
    }


    public partial class OxatisStore
    {
        public string APIURL { get; set; }
        public string AppID { get; set; }
        public string Token { get; set; }
    }

    public partial class MiraklStore
    {
        public string APIKey { get; set; }
        public string APIURL { get; set; }
    }

    public class PutTrackingRequest
    {
        public string carrier_code { get; set; }
        public string carrier_name { get; set; }
        public string tracking_number { get; set; }
        public string tracking_url { get; set; }
    }


}