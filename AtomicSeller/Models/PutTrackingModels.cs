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

    public class PostDeliveryRequest
    {
        public List<shipments> shipments { get; set; }
    }


    public class PostDeliveryResponse
    {
        public ResponseHeader Header { get; set; }
        public List<PostTrackingNumberResponseData> Response { get; set; }
    }
    public class PostTrackingNumberRequest
    {
        public List<shipments> shipments { get; set; }
    }
    public class shipments
    {
        public string order_id { get; set; }
        public List<shipment_lines> shipment_lines { get; set; }
        public bool shipped { get; set; }
        public tracking tracking { get; set; }
    }

    public class shipment_lines
    {
        public string offer_sku { get; set; }
        public string order_line_id { get; set; }
        public Int32 quantity { get; set; }
    }

    public class tracking
    {
        public string carrier_code { get; set; }
        public string carrier_name { get; set; }
        public string tracking_number { get; set; }
        public string tracking_url { get; set; }
    }

    public class PostTrackingNumberResponse
    {
        public ResponseHeader Header { get; set; }
        public List<PostTrackingNumberResponseData> Response { get; set; }
    }

    public class PostTrackingNumberResponseData
    {
        public string id { get; set; }
        public string order_id { get; set; }
        public List<shipment_lines> shipment_lines { get; set; }
        public string status { get; set; }
        public tracking tracking { get; set; }
    }

}