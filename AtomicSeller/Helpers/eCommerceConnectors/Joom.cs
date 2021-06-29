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
using JoomAPI.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using RestSharp;
using ProductAPI.Models;
using AtomicCommonAPI.Models;

namespace AtomicSeller
{
    public class Joom
    {

        private static readonly HttpClient client = new HttpClient();
        private JObject details;
        //private string m_AccessToken;

        private string GetAccessToken(Store _Store)
        {
            //const SslProtocols _Tls12 = (SslProtocols)0x00000C00;
            //const SecurityProtocolType Tls12 = (SecurityProtocolType)_Tls12;
            //ServicePointManager.SecurityProtocol = Tls12;

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

            string Email = _Store.UserName; // a608f494@gorilla.club
            string APIURL = _Store.StoreAPIURL; // https://sandbox-202011-api-merchant.joomdev.net

            // https://api-merchant.joom.com/api/v3.
            // Base URL for sandbox is https://sandbox-merchant-api.dev.joom.it/api/v3.

            string Password = _Store.Password; // J27sqh%%

            var client = new RestClient(APIURL + "/api/v3/auth/signin/");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\"email\": \"" + Email + "\", \"password\": \"" + Password + "\"\r\n}", ParameterType.RequestBody);
            string jsonParam = string.Empty;
            string m_AccessToken = string.Empty;
            try
            {
                IRestResponse response = client.Execute(request);

                details = JObject.Parse(response.Content);
                var accesstokenObject = JObject.Parse(details["data"].ToString());
                m_AccessToken = "Bearer " + accesstokenObject["access_token"].ToString();

                // email & passowrd to joom API
                //jsonParam = JsonConvert.SerializeObject(response.Content).ToString();
            }
            catch(Exception ex)
            {
                Tools.ErrorHandler("Joom GetAccessToken error", ex);
            }
            return (m_AccessToken);
        }

        public ResponseHeader Joom_PutDeliveryInfo(Models.Store _Store, string OrderKey = null,
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
                return _DeliveryResponseHeader;
            }
            else
            {
                string ACCESS_TOKEN = GetAccessToken(_Store);


                string FulfillmentURL = _Store.StoreAPIURL + "/api/v3/orders/fulfill";

                string FULLUpdateURL = FulfillmentURL + "?id=" + OrderKey;

                //_OrderVM.Order.OrderKey = _JOrder.id;

                var client = new RestClient(FulfillmentURL);

                FulFillRootobject _FulFillBody = new JoomAPI.Models.FulFillRootobject();
                _FulFillBody.provider = TrackingProviderName;
                _FulFillBody.providerId = TrackingProviderKey;
                _FulFillBody.trackingNumber = TrackingNumber;

                string jsonParam = string.Empty;
                jsonParam = JsonConvert.SerializeObject(_FulFillBody).ToString();

                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", ACCESS_TOKEN);
                request.AddParameter("application/json", jsonParam, ParameterType.RequestBody);

                new DA_MT().InsertAuditTrail("Joom", "Joom_PutProductsInfo call", jsonParam, "");

                try
                {
                    IRestResponse response = client.Execute(request);

                    string PostProductInfo_JSON = JObject.Parse(response.Content).ToString();

                    new DA_MT().InsertAuditTrail("Joom", "Joom_PutProductsInfo call", jsonParam, "");

                    _DeliveryResponseHeader.LanguageCode = "En";
                    _DeliveryResponseHeader.RequestStatus = "Ok";
                    _DeliveryResponseHeader.ReturnCode = "AS0000";
                    _DeliveryResponseHeader.StatusCode = "200";
                    _DeliveryResponseHeader.ReasonPhrase = AtomicAPI.Helpers.Constants.GetReasonPhrase("200");
                    _DeliveryResponseHeader.ReturnMessage = PostProductInfo_JSON;

                }
                catch (Exception ex)
                {
                    _DeliveryResponseHeader.LanguageCode = "En";
                    _DeliveryResponseHeader.RequestStatus = "Error";
                    _DeliveryResponseHeader.ReturnCode = "ASxxxx";
                    _DeliveryResponseHeader.StatusCode = "400";
                    _DeliveryResponseHeader.ReasonPhrase = AtomicAPI.Helpers.Constants.GetReasonPhrase("400");
                    _DeliveryResponseHeader.ReturnMessage = ex.Message;

                    string Title = "Jomm PutDeliveryInfo severe error " + DateTime.Now.ToString();
                    string _ValidationErrorMessage = ex.Message;
                    string _InnerException = new Tools().GetInnerException(ex);

                    new Email().ReportErrorByEmail(Title, _ValidationErrorMessage, _InnerException, ex.StackTrace);

                }
            }
            return _DeliveryResponseHeader;
        }

        public List<string> Joom_ListCarriers(Models.Store _Store)
        {

            List<string> ReturnCarrierList = new List<string>();

            string API_BASE_URL = "";

            API_BASE_URL = _Store.StoreAPIURL;

            string strCarriersResult = string.Empty;
            string GetListAllCarriers_API_URL = API_BASE_URL + "/api/v3/shippers";
            string ACCESS_TOKEN = GetAccessToken(_Store);

            try
            {
                var client = new RestClient(GetListAllCarriers_API_URL);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);

                request.AddHeader("Authorization", ACCESS_TOKEN);
                IRestResponse response = client.Execute(request);

                string _GetCarriersResponseJson = response.Content;

                JoomCarriers _GetJoomCarriersData = new JoomCarriers();
                _GetJoomCarriersData = JsonConvert.DeserializeObject<JoomCarriers>(_GetCarriersResponseJson);

                foreach (var Carrier in _GetJoomCarriersData.data.items)
                {
                    ReturnCarrierList.Add(Carrier.id + " " + Carrier.name);
                }
                
            }
            catch (Exception ex)
            {
            }

            return ReturnCarrierList;
        }


    }
}
