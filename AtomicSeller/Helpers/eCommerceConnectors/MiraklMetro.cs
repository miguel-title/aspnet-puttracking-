using AtomicSeller.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Web;
using AtomicSeller.ViewModels;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using System.Text;
using MiraklMetroAPI.Models;
using ProductAPI.Models;
using AtomicCommonAPI.Models;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace AtomicSeller
{
    public class MiraklMetro
    {
        //private static String API_KEY = "aded029a-7b57-48a5-b492-4353596ae60d";
        private static String API_BASE_URL = "https://Metro-marketplace.mirakl.net";




        public ResponseHeader MiraklMetro_PutDeliveryInfo(Models.Store _Store, string DeliveryID, string OrderKey = null,
    string OrderID = null, string OrderID_Ext = null, string DeliveryStatus = null, string ShippingID = null, string TrackingNumber = null,
    string TrackingProviderKey = null, string TrackingProviderName = null,
    string TrackingURL = null)
        {
            ResponseHeader _DeliveryResponseHeader = new ResponseHeader();

            if (string.IsNullOrEmpty(TrackingNumber) && string.IsNullOrEmpty(OrderID_Ext) && string.IsNullOrEmpty(DeliveryStatus))
            {
                _DeliveryResponseHeader.LanguageCode = "En";
                _DeliveryResponseHeader.RequestStatus = "Error";
                _DeliveryResponseHeader.ReturnCode = "ASxxxx";
                _DeliveryResponseHeader.StatusCode = "404";
                _DeliveryResponseHeader.ReasonPhrase = AtomicAPI.Helpers.Constants.GetReasonPhrase("404");
                _DeliveryResponseHeader.ReturnMessage = "Either TrackingNumber and OrderID_Ext and DeliveryStatus empty!";
                //return _DeliveryResponseHeader;
            }
            else
            {

                /*
                 * 
                 * PUT OR23 - Update carrier tracking information for a specific order
                 
                 /api/orders/{order_id}/tracking
                 
                 {
      "carrier_code": "UPS",
      "carrier_name": "UPS",
      "carrier_url": "https://ups.com",
      "tracking_number": 5555
    }

                */
                PostDeliveryRequest validateOrderRequest = new PostDeliveryRequest();

                tracking _tracking = new tracking();
                _tracking.carrier_code = TrackingProviderKey;
                _tracking.carrier_name = TrackingProviderName;
                _tracking.tracking_number = TrackingNumber;
                _tracking.tracking_url = TrackingURL;

                shipment _shipment = new MiraklMetroAPI.Models.shipment();
                _shipment.tracking = _tracking;
                _shipment.order_id = OrderID_Ext;
                _shipment.shipped = true;


                List<DeliveryProduct> _DeliveryProducts = new DA_MT().GetDeliveryProducts(new Tools().ConvertStringToInt(DeliveryID));

                _shipment.shipment_lines = new List<shipment_line>();

                int i = 1;
                foreach (DeliveryProduct _DeliveryProduct in _DeliveryProducts)
                {
                    shipment_line _shipment_line = new shipment_line();

                    _shipment_line.offer_sku = _DeliveryProduct.SKU; // "TAME822";
                    //_shipment_line.order_line_id = OrderID_Ext + "-"+ (i++).ToString();
                    _shipment_line.quantity = (int)_DeliveryProduct.Quantity;
                    _shipment.shipment_lines.Add(_shipment_line);

                }

                validateOrderRequest.shipments = new List<shipment>();
                validateOrderRequest.shipments.Add(_shipment);

                string jsonParam = string.Empty;
                jsonParam = JsonConvert.SerializeObject(validateOrderRequest).ToString();

                string strShipmentResult = string.Empty;
                string GetValidateShipment_API_URL = API_BASE_URL + "/api/shipments";
                try
                {
                    strShipmentResult = new MiraklMetro().SendPostHttpRequest(GetValidateShipment_API_URL, jsonParam, _Store.WoocommerceKey);
                    PostTrackingNumberResponseData orderResult = JsonConvert.DeserializeObject<PostTrackingNumberResponseData>(strShipmentResult);

                    if (orderResult.shipment_errors.Count == 0)
                    {
                        _DeliveryResponseHeader.LanguageCode = "En";
                        _DeliveryResponseHeader.RequestStatus = "Ok";
                        _DeliveryResponseHeader.ReturnCode = "AS0000";
                        _DeliveryResponseHeader.StatusCode = "200";
                        _DeliveryResponseHeader.ReasonPhrase = AtomicAPI.Helpers.Constants.GetReasonPhrase("200");
                        _DeliveryResponseHeader.ReturnMessage = strShipmentResult;
                    }
                    else
                    {
                        string ErrorMessage = "";

                        foreach(var error in orderResult.shipment_errors)
                        {
                            ErrorMessage += error.message + "\n";
                        }

                        _DeliveryResponseHeader.LanguageCode = "En";
                        _DeliveryResponseHeader.RequestStatus = "Error";
                        _DeliveryResponseHeader.ReturnCode = "AS0xxx";
                        _DeliveryResponseHeader.StatusCode = "400";
                        _DeliveryResponseHeader.ReasonPhrase = AtomicAPI.Helpers.Constants.GetReasonPhrase("400");
                        _DeliveryResponseHeader.ReturnMessage = ErrorMessage + strShipmentResult;
                    }


                }
                catch (Exception ex)
                {
                    _DeliveryResponseHeader.LanguageCode = "En";
                    _DeliveryResponseHeader.RequestStatus = "Error";
                    _DeliveryResponseHeader.ReturnCode = "ASxxxx";
                    _DeliveryResponseHeader.StatusCode = "500";
                    _DeliveryResponseHeader.ReasonPhrase = AtomicAPI.Helpers.Constants.GetReasonPhrase("500");
                    _DeliveryResponseHeader.ReturnMessage = ex.Message;
                }

            }
            return _DeliveryResponseHeader;
        }

        /*
        public PostTrackingNumberResponse Metro_PostTrackingNumber(bool deliveryStatus, string trackingNumber, string carrier_name, string carrier_code, string trackingurl)
        {
            PostTrackingNumberResponse _validateorderResult = new PostTrackingNumberResponse();
            ResponseHeader _ResponseHeader = new ResponseHeader();
            _ResponseHeader.LanguageCode = "En";
            _ResponseHeader.RequestStatus = "Ok";
            _ResponseHeader.ReturnCode = "AS0000";
            _ResponseHeader.ReturnMessage = "";
            _validateorderResult.Header = _ResponseHeader;

            PostDeliveryRequest validateOrderRequest = new PostDeliveryRequest();
            shipments _shipments = new shipments();
            _shipments.order_id = "9-32241702-A";
            _shipments.shipped = deliveryStatus;
            tracking _tracking = new tracking();
            _tracking.carrier_code = carrier_code;
            _tracking.carrier_name = carrier_name;
            _tracking.tracking_number = trackingNumber;
            _tracking.tracking_url = trackingurl;
            _shipments.tracking = _tracking;
            shipment_lines _shipment_lines = new shipment_lines();
            _shipment_lines.offer_sku = "OFFER_SKU_1";
            _shipment_lines.order_line_id = "9-32241702-1";
            _shipment_lines.quantity = 1;
            _shipments.shipment_lines = new List<shipment_lines>();
            _shipments.shipment_lines.Add(_shipment_lines);

            validateOrderRequest.shipments = new List<shipments>();
            validateOrderRequest.shipments.Add(_shipments);

            string jsonParam = string.Empty;
            jsonParam = JsonConvert.SerializeObject(validateOrderRequest).ToString();

            string strShipmentResult = string.Empty;
            string GetValidateShipment_API_URL = API_BASE_URL + "/api/shipments";
            try
            {
                strShipmentResult = new MiraklMetro().SendPostHttpRequest(GetValidateShipment_API_URL, jsonParam);
                PostTrackingNumberResponseData orderResult = JsonConvert.DeserializeObject<PostTrackingNumberResponseData>(strShipmentResult);

                _validateorderResult.Response = orderResult;
            }
            catch (Exception ex)
            {
                _ResponseHeader.LanguageCode = "En";
                _ResponseHeader.RequestStatus = "Error";
                _ResponseHeader.ReturnCode = "WZ0";


                _ResponseHeader.ReturnMessage = ex.Message;
                _validateorderResult.Header = _ResponseHeader;
            }

            return _validateorderResult;
        }
        */


    private string SendPostHttpRequest(String url, String jsonParam, string API_KEY)
    {
        //const SslProtocols _Tls12 = (SslProtocols)0x00000C00;
        //const SecurityProtocolType Tls12 = (SecurityProtocolType)_Tls12;
        //ServicePointManager.SecurityProtocol = Tls12;

        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";
        httpWebRequest.Accept = "application/json";
        httpWebRequest.Headers.Add("Authorization", API_KEY);

        using (StreamWriter writer = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            writer.WriteLine(jsonParam);
        }

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        var result = "";
            try {
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
        return result;
            }

        private string SendPutHttpRequest(String url, String jsonParam, string API_KEY)
        {
            //const SslProtocols _Tls12 = (SslProtocols)0x00000C00;
            //const SecurityProtocolType Tls12 = (SecurityProtocolType)_Tls12;
            //ServicePointManager.SecurityProtocol = Tls12;

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "PUT";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.Headers.Add("Authorization", API_KEY);

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


    }

}