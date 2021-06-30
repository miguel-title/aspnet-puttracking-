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

        private string SendPutHttpRequest(Models.MiraklStore _Store, String jsonParam, string strOrderID)
        {
            const SslProtocols _Tls12 = (SslProtocols)0x00000C00;
            const SecurityProtocolType Tls12 = (SecurityProtocolType)_Tls12;
            ServicePointManager.SecurityProtocol = Tls12;

            string url = _Store.APIURL + "/api/orders/" + strOrderID + "/tracking";

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "PUT";
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

        public ResponseHeader PutTrackingNumber(Models.MiraklStore _Store, string orderID, bool deliveryStatus, string trackingNumber, string carrier_name, string carrier_code, string trackingurl)
        {
            ResponseHeader _ResponseHeader = new ResponseHeader();
            _ResponseHeader.LanguageCode = "En";
            _ResponseHeader.RequestStatus = "Ok";
            _ResponseHeader.ReturnCode = "AS0000";
            _ResponseHeader.ReturnMessage = "";

            PutTrackingRequest _tracking = new PutTrackingRequest();
            _tracking.carrier_code = carrier_code;
            _tracking.carrier_name = carrier_name;
            _tracking.tracking_number = trackingNumber;
            _tracking.tracking_url = trackingurl;


            string jsonParam = string.Empty;
            jsonParam = JsonConvert.SerializeObject(_tracking).ToString();

            string strOrderResult = string.Empty;
            try
            {
                new MiraklDouglas().SendPutHttpRequest(_Store, jsonParam, orderID);
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