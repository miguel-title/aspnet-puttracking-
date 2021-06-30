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

        private string GetAccessToken(JoomStore _Store)
        {
            //const SslProtocols _Tls12 = (SslProtocols)0x00000C00;
            //const SecurityProtocolType Tls12 = (SecurityProtocolType)_Tls12;
            //ServicePointManager.SecurityProtocol = Tls12;

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

            string Email = _Store.User; // a608f494@gorilla.club
            string APIURL = _Store.APIURL; // https://sandbox-202011-api-merchant.joomdev.net

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
            }
            return (m_AccessToken);
        }

        public List<string> Joom_ListCarriers(Models.JoomStore _Store)
        {

            List<string> ReturnCarrierList = new List<string>();

            string API_BASE_URL = "";

            API_BASE_URL = _Store.APIURL;

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

        public ResponseHeader PutTrackingNumber(Models.JoomStore _Store, string strProviderID, string strTrackingNumber, string orderID)
        {
            ResponseHeader _header = new ResponseHeader();


            string ACCESS_TOKEN = GetAccessToken(_Store);
            
            string API_BASE_URL = "";
            API_BASE_URL = _Store.APIURL;
            string PutTracking_API_URL = API_BASE_URL + "/api/v3/orders/modifyTracking?id=" + orderID;

            var client = new RestClient(PutTracking_API_URL);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", ACCESS_TOKEN);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\r\n    \"providerId\": \"" + strProviderID + "\",\r\n    \"trackingNumber\": \"" + strTrackingNumber + "\"\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                _header.LanguageCode = "En";
                _header.RequestStatus = "Error";
                _header.ReturnCode = "ASxxxx";
                _header.StatusCode = "200";
                _header.ReturnMessage = "Successful!";
            }
            else
            {
                _header.LanguageCode = "En";
                _header.RequestStatus = "Error";
                _header.ReturnCode = "ASxxxx";
                _header.StatusCode = response.StatusCode.ToString();
                _header.ReturnMessage = response.ErrorMessage;

            }

            return _header;
        }
    }
}
