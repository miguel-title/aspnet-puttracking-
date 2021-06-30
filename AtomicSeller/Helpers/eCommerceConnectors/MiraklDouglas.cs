using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Web;
using AtomicCommonAPI.Models;
using AtomicSeller.Models;
using Newtonsoft.Json;

namespace AtomicSeller
{
    public class MiraklDouglas
    {

        private string SendPostHttpRequest(Models.MiraklStore _Store, String jsonParam)
        {
            const SslProtocols _Tls12 = (SslProtocols)0x00000C00;
            const SecurityProtocolType Tls12 = (SecurityProtocolType)_Tls12;
            ServicePointManager.SecurityProtocol = Tls12;

            string url = _Store.APIURL + "/api/shipments";

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.Headers.Add("Authorization", _Store.APIKey);

            using (StreamWriter writer = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                writer.WriteLine(jsonParam);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            var result = "";
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }

        public ResponseHeader PutTrackingNumber(Models.MiraklStore _Store, string orderID, string offersku, bool deliveryStatus, string trackingNumber, string carrier_name, string carrier_code, string trackingurl)
        {
            ResponseHeader _ResponseHeader = new ResponseHeader();
            _ResponseHeader.LanguageCode = "En";
            _ResponseHeader.RequestStatus = "Ok";
            _ResponseHeader.ReturnCode = "AS0000";
            _ResponseHeader.ReturnMessage = "";

            PostDeliveryRequest validateOrderRequest = new PostDeliveryRequest();
            shipments _shipments = new shipments();
            _shipments.order_id = orderID;
            _shipments.shipped = deliveryStatus;
            tracking _tracking = new tracking();
            _tracking.carrier_code = carrier_code;
            _tracking.carrier_name = carrier_name;
            _tracking.tracking_number = trackingNumber;
            _tracking.tracking_url = trackingurl;
            _shipments.tracking = _tracking;
            shipment_lines _shipment_lines = new shipment_lines();
            _shipment_lines.offer_sku = offersku;
            _shipment_lines.order_line_id = "";
            _shipment_lines.quantity = 1;
            _shipments.shipment_lines = new List<shipment_lines>();
            _shipments.shipment_lines.Add(_shipment_lines);

            validateOrderRequest.shipments = new List<shipments>();
            validateOrderRequest.shipments.Add(_shipments);

            string jsonParam = string.Empty;
            jsonParam = JsonConvert.SerializeObject(validateOrderRequest).ToString();

            string strOrderResult = string.Empty;
            try
            {
                strOrderResult = new MiraklDouglas().SendPostHttpRequest(_Store, jsonParam);
                List<PostTrackingNumberResponseData> orderResult = JsonConvert.DeserializeObject<List<PostTrackingNumberResponseData>>(strOrderResult);
            }
            catch (Exception ex)
            {
                _ResponseHeader.LanguageCode = "En";
                _ResponseHeader.RequestStatus = "Error";
                _ResponseHeader.ReturnCode = "WZ0"; ;
                _ResponseHeader.ReturnMessage = ex.Message;
            }

            return _ResponseHeader;
        }

    }
}