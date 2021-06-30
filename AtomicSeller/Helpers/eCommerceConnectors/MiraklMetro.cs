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