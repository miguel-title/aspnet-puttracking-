using ProductAPI.Models;
using AtomicSeller.Models;
using AtomicSeller.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
//using Wavesoft.Models;
using OxatisAPI.Models;
using System.Text.RegularExpressions;
using AtomicCommonAPI.Models;

namespace AtomicSeller
{
    class Oxatis
    {
        public string OxCheckConnection(Store _Store)
        {
            string UpdatedFromTime = DateTime.Today.AddDays(-1).ToString();
            try
            {
                List<DEALVM> TestList = Oxatis_GetOrders(_Store, null, UpdatedFromTime);

                if (TestList.Count == 0) return "";

                DEALVM TestOrder = TestList.First();
                if (TestOrder.Customer.FirstName == "Error")
                {
                    //MarketPlaces.MarketPlacesCommon.CheckConnectionErrorMessage = TestOrder.Customer.LastName;
                    return TestOrder.Customer.LastName;
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public ResponseHeader OX_PutStatus(string DeliveryStatus, string TrackingNumber, string OrderKey, string WC_OrderID, string OrderID, Store _Store, string TrackingProviderKey = null, string TrackingProviderName = null, string Option = null)
        {
            return null;
        }

        public List<DEALVM> OxGetOrdersToDb(Store _Store, string CreatedFromTime = null, string UpdatedFromTime = null, string OrderId = null, string RuleKey = null)
        {

            try
            {
                //Tools.ErrorHandler("Oxatis Check. Store=" + _Store.StoreName, null, false, true, false);
                List<DEALVM> _DEALVMs = Oxatis_GetOrders(_Store, CreatedFromTime, UpdatedFromTime, OrderId);

                if (_DEALVMs != null)
                    foreach (DEALVM _DEALVM in _DEALVMs)
                        new DA_MT().UpdateOtherWiseInsertDEALVM(_DEALVM);

                return _DEALVMs;
            }
            catch (Exception ex)
            {
                try
                {
                    string Title = "Oxatis Error ErrorCode=" + ex.Message;
                    string ServerName = new DA_REF().GetGlobalInstanceSetting("ServerInstance");
                    string NL = "<br />";
                    string Body = ServerName + NL + NL;

                    Body += "Error message:" + ex.Message + NL + NL + "StackTrace:" + NL + NL + ex.StackTrace;

                    string Recipient = "support@atomicseller.fr";

                    new Email().SendMail(Title, Body, Recipient);
                }
                catch { }

                return null;
            }
        }

        public void GetOrdersTest()
        {
            Store _Store = new DA_MT().GetStoreByStoreKey("OXATISTEST");

            //List<DEALVM> _DEALVMs = Oxatis_GetOrders(_Store, DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:ss \"GMT\"zzz"), null, null);
            //string UpdatedFromTime = "2017-09-18T13:20:00";
            string CreatedFromTime = "2020-04-15T01:20:00";
            // ISO 8601 format (YYYY-MM-DDThh:mm:ss) is used for Datetime.

            List<DEALVM> _DEALVMs = Oxatis_GetOrders(_Store, CreatedFromTime, null, null, null);

        }

        #region Oxatis_GetOrders
        public List<DEALVM> Oxatis_GetOrders(Store _Store, string CreatedFromTime = null, string UpdatedFromTime = null, string OrderId = null, string RuleKey = null)
        {
            //HttpResponseMessage response = Ox_GetOrders(CreatedFromTime, UpdatedFromTime, OrderId);
            List<DEALVM> _DEALVMs = new List<DEALVM>();
            //DEALVM _DEALVM = new DEALVM();
            _DEALVMs = Ox_GetOrders(_Store, CreatedFromTime, UpdatedFromTime, OrderId, RuleKey);
            //rootObjects = JsonConvert.DeserializeObject<List<RootObject>>(response);

            string LanguageCode = "fr-FR";

            _DEALVMs = new CheckData().CheckDEALVMs(_DEALVMs, _Store, LanguageCode);

            return (_DEALVMs);
        }

        public struct WS_TransportStruct
        {
            public string Code { get; set; }
            public string Label { get; set; }
            public WS_TransportStruct(string _Code, string _Label)
            {
                Code = _Code;
                Label = _Label;
            }
        }

        public static List<WS_TransportStruct> WS_TransportList = new List<WS_TransportStruct> {
             new WS_TransportStruct("10", "SO Colissimo"),
             new WS_TransportStruct("12", "CMACGM_DOMAS"), // CMACGM
             new WS_TransportStruct("20", "Mondial Relay"),
             new WS_TransportStruct("30", "Colis Privé"),
             new WS_TransportStruct("40", "TNT Express Entreprise"),
             new WS_TransportStruct("41", "TNT Express Domicile"),
             new WS_TransportStruct("42", "TNT Express Relais Colis"),
             new WS_TransportStruct("50", "DPD Pick Up"), // Personalisé Botime. A mettre en paramètres
        };

        static String appID = ""; //"40400e534fd06961df8b09474e65c88f";
        static String Token = ""; //"AF1310433F4DB9C3DDDC44962B";
        static String OrderServicesURL = "http://webservices.oxatis.com/webservices/httpservices/OrderServices.aspx";
        static String ProductServicesURL = "http://webservices.oxatis.com/webservices/httpservices/ProductServices.aspx";
        //static String ProductServicesURL = "https://webservices.oxatis.com/webservices/Default.aspx";

        //public static HttpResponseMessage Ox_GetOrders(String CreatedFromTime = null, String UpdatedFromTime = null, String OrderId = null)
        public List<DEALVM> Ox_GetOrders(Store _Store, String CreatedFromTime = null, String UpdatedFromTime = null, String OrderId = null, string RuleKey = null)
        {
            if (!string.IsNullOrEmpty(_Store.Token))
            {
                appID = _Store.AppID;
                Token = _Store.Token;
            }
            else return null;

            String METHOD = string.Empty;
            String xml = string.Empty;

            if (!string.IsNullOrEmpty(RuleKey))
            {
                if (RuleKey == "NEWORDERS")
                {
                    METHOD = "OrderGetList";

                    xml = "<?xml version='1.0' encoding='utf-8'?>";
                    xml += "<OrderList xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>";
                    xml += "<PaymentStatusCode>" + "40" + "</PaymentStatusCode>";
                    /* PaymentStatusCode
                    0 Canceled
                    10 Technical error
                    14 Contacting payment processor
                    20 Payment in progress
                    30 Payment refused
                    40 Payment confirmed
                    */

                    //<PaymentStatusCode />
                    xml += "<ProgressStateID>49628</ProgressStateID>";
                    //<OrdersProcessed /> 
                    //xml += "<OrdersProcessed>" + "false" + "</OrdersProcessed>";
                    //<OrdersInvoiced /> 
                    //xml += "<OrdersShipped>" + "false" + "</OrdersShipped>";
                    //<OrdersShipped />

                    if (CreatedFromTime != null)
                    {
                        DateTime DateFilter = new Tools().ConvertStringToDate(CreatedFromTime, "fr-FR");
                        CreatedFromTime = DateFilter.ToString("yyyy-MM-ddTHH:mm:ss"); // 2019-04-15T01:20:00"
                        string OrderDateEnd = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");
                        xml += "<OrderDateStart>" + CreatedFromTime + "</OrderDateStart>";
                        xml += "<OrderDateEnd>" + OrderDateEnd + "</OrderDateEnd>";
                    }
                    xml += "</OrderList>";

                }
                else if (RuleKey == "MANUALLYCONFIRMEDORDERS")
                {
                    METHOD = "OrderGetManuallyConfirmed";
                    xml = "<?xml version='1.0' encoding='utf-8'?>";

                    xml += "<OrderList xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>";
                    xml += "</OrderList>";
                }
            }
            else
            {

                if (CreatedFromTime == null)
                {
                    if (UpdatedFromTime == null)
                    {
                        DateTime DateFilter = DateTime.Now.AddDays(-10);
                        CreatedFromTime = DateFilter.ToString("yyyy-MM-ddTHH:mm:ss");

                    }
                    else
                    {
                        //Tools.ErrorHandler("Please enter CreatedFromTime", null, false, true, false);
                        DateTime DateFilter = new Tools().ConvertStringToDate(UpdatedFromTime, "fr-FR");
                        CreatedFromTime = DateFilter.ToString("yyyy-MM-ddTHH:mm:ss"); // 2019-04-15T01:20:00"
                        //return null;
                    }
                }
                else
                {
                    DateTime DateFilter = new Tools().ConvertStringToDate(CreatedFromTime, "fr-FR");
                    CreatedFromTime = DateFilter.ToString("yyyy-MM-ddTHH:mm:ss"); // 2019-04-15T01:20:00"
                }

                string OrderDateEnd = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");

                METHOD = "OrderGetList";
                //String URL = "http://webservices.oxatis.com/webservices/httpservices/OrderServices.aspx";
                xml = "<?xml version='1.0' encoding='utf-8'?>";
                xml += "<OrderList xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>";
                xml += "<OrderDateStart>" + CreatedFromTime + "</OrderDateStart>";
                xml += "<OrderDateEnd>" + OrderDateEnd + "</OrderDateEnd>";
                xml += "</OrderList>";

            }

            List<DEALVM> _DEALVMs = new List<DEALVM>();

            /// 
            /// Order ID list
            /// 
            System.Xml.XmlDocument doc = OX_WebReq(OrderServicesURL, METHOD, xml);
            //string DebugXml = doc.InnerXml;

            int n = doc.ChildNodes.Count;
            XmlNode DataRequest = doc.ChildNodes.Item(1);
            string _StatusCode = DataRequest["StatusCode"].InnerText;

            if (_StatusCode != "200")
            {
                Tools.ErrorHandler(DataRequest["ErrorDetails"].InnerText, null, false, true, false);
                return null;
            }
            XmlNode OrderList = DataRequest.ChildNodes.Item(3);
            XmlNode OrderIds = OrderList.FirstChild;
            OrderIds = OrderIds.FirstChild;
            while (OrderIds != null)
            {
                if (OrderIds.Name == "OrderIDs")
                    break;
                OrderIds = OrderIds.NextSibling;
            }

            /// 
            /// Fetch each order
            /// 

            int nSize = OrderIds.ChildNodes.Count;

            //if (nSize > 500) nSize = 500; // Limited size to avoid servers overload

            //string jsonText = "[";
            for (int i = 0; i < nSize; i++)
            {
                DEALVM _DEALVM = new DEALVM();

                XmlNode item = OrderIds.ChildNodes.Item(i);
                String strId = item.InnerText;

                //Debug
                /*
                                string toto;
                                if (strId == "35194058" || strId == "35204438")
                                    toto = strId;
                                else
                                    continue;
                  */                  
                // Debug

                if (strId == OrderId && OrderId != null)
                {
                    METHOD = "OrderGetDetails";
                    xml = "<?xml version='1.0' encoding='utf-8'?>";
                    xml += "<Order xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>";
                    xml += "<OxID>" + strId + "</OxID>";
                    xml += "</Order>";
                    System.Xml.XmlDocument XMLorder = OX_WebReq(OrderServicesURL, METHOD, xml);

                    _DEALVM = Ox_OrderToDEALVM(XMLorder, _Store);
                }
                else if (OrderId == null)
                {
                    METHOD = "OrderGetDetails";
                    xml = "<?xml version='1.0' encoding='utf-8'?>";
                    xml += "<Order xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>";
                    xml += "<OxID>" + strId + "</OxID>";
                    xml += "</Order>";
                    System.Xml.XmlDocument XMLorder = OX_WebReq(OrderServicesURL, METHOD, xml);

                    _DEALVM = Ox_OrderToDEALVM(XMLorder, _Store);
                }
                if (_DEALVM != null) _DEALVMs.Add(_DEALVM);
            }
            return _DEALVMs;
        }

        private DEALVM Ox_OrderToDEALVM(System.Xml.XmlDocument _XMLResult, Store _Store)
        {
            DEALVM _DEALVM = new DEALVM();

            try { 

            string _StatusCode = _XMLResult["DataResultService"]["StatusCode"].InnerText;

            if (_StatusCode != "200")
            {
                Tools.ErrorHandler(_XMLResult["ErrorDetails"].InnerText, null, false, true, false);
                return null;
            }

                XmlNode _XMLOrder = _XMLResult["DataResultService"]["Data"].FirstChild;

                /*
                XmlNode _XMLOrder2 = _XMLResult["DataResultService"]["Data"].FirstChild;

                if (_XMLResult.InnerXml.Contains("41 Rue des cryptomerias"))
                {
                    _XMLOrder2 = _XMLResult["DataResultService"]["Data"].FirstChild;
                    //var toto5 = _XMLResult.ChildNodes;
                    //var toto4 = _XMLResult.ChildNodes.Item(1);
                    //var toto1 = _XMLResult.ChildNodes.Item(1).ChildNodes;
                    //var toto2 = _XMLResult.ChildNodes.Item(1).ChildNodes.Item(3);
                    //var toto3 = _XMLResult.ChildNodes.Item(1).ChildNodes.Item(3).FirstChild;
                }

                try
                {
                    _XMLOrder = _XMLResult.ChildNodes.Item(1).ChildNodes.Item(3).FirstChild;
                }
                catch (Exception ex)
                {
                    _XMLOrder = _XMLOrder2;
                }
                */

            OxatisAPI.Models.Order _OxOrder = XmlDocToOrder(_XMLOrder);
            if (_OxOrder == null)
                return null;

            //if (_OxOrder.OxID.StartsWith ("278"))
            //    toto = _OxOrder.OxID;

            //if (_OxOrder.OxID.StartsWith ("27810581"))
            //    toto = _OxOrder.OxID;

            switch (_OxOrder.ProgressStateID)
            {
                case "49628": // 49628 en préparation
                    return null;    
                    break;
                case "49629": // 49629 Undefined
                    return null;
                    break;
                case null:
                case "":
                    switch (_OxOrder.PaymentStatusCode)
                    {
                        case "0": // Canceled
                        case "10": // Technical error.
                        case "14": // Contacting payment processor.
                        case "20": // Payment in progress.
                        case "30": // Payment refused.
                            return null;
                            break;
                        case "40": // Payment confirmed.
                            break;
                    }
                    break;
            }                

            _DEALVM.Customer = new Models.Customer();
            _DEALVM.InitDealVM();
            _DEALVM.OrderVMs.First().Invoices = new List<Invoice>();
            _DEALVM.OrderVMs.First().Invoices.Add(new Invoice());

            // INVOICE         
            
            _DEALVM.OrderVMs.First().Invoices.First().InvoiceNr = _OxOrder.InvoiceID;
            _DEALVM.OrderVMs.First().Invoices.First().InvoicePath = _OxOrder.InvoiceURL;

            _DEALVM.OrderVMs.First().Invoices.First().InvoiceDate = _OxOrder.Date;

            _DEALVM.OrderVMs.First().Invoices.First().BillingFirstName = _OxOrder.BillingTitle + " " + _OxOrder.BillingFirstName;
            _DEALVM.OrderVMs.First().Invoices.First().BillingLastName = _OxOrder.BillingLastName;

            try { _DEALVM.OrderVMs.First().Invoices.First().BillingCompany = _OxOrder.BillingCompany;
            } catch { }

            string[] AdrLines = Regex.Split(_OxOrder.BillingAddress, "\r\n");
            try
            {
                _DEALVM.OrderVMs.First().Invoices.First().BillingAdr1 = AdrLines[0]; // 16 passage homme ENTREE AU 26  RUE DE\r\nCHARONNE CODE A 1026
                _DEALVM.OrderVMs.First().Invoices.First().BillingAdr2 = AdrLines[1];
            }
            catch { }


            _DEALVM.OrderVMs.First().Invoices.First().BillingZipCode = _OxOrder.BillingZipCode.ToString();
            _DEALVM.OrderVMs.First().Invoices.First().BillingCity = _OxOrder.BillingCity;
            _DEALVM.OrderVMs.First().Invoices.First().BillingState = _OxOrder.BillingState.ToString();

            _DEALVM.OrderVMs.First().Invoices.First().BillingCountryCode = _OxOrder.BillingCountryISOCode;
            _DEALVM.OrderVMs.First().Invoices.First().BillingCountry = _OxOrder.BillingCountryName;

            _DEALVM.OrderVMs.First().Invoices.First().BillingPhone =
                (string.IsNullOrEmpty(_OxOrder.BillingCellPhone) ? _OxOrder.BillingPhone : _OxOrder.BillingCellPhone);

            if (_OxOrder.OrderTaxDetails.Item!=null)
                {
                    _DEALVM.OrderVMs.First().Invoices.First().TotalExclVAT = _OxOrder.OrderTaxDetails.Item.TotalNetTaxExcl.ToString();
                    _DEALVM.OrderVMs.First().Invoices.First().TotalVAT = _OxOrder.OrderTaxDetails.Item.TotalNetVATAmount.ToString();
                }
                                
            _DEALVM.OrderVMs.First().Invoices.First().TotalAmount = _OxOrder.NetAmountDue.ToString();
            _DEALVM.OrderVMs.First().Invoices.First().InvoiceNr = _OxOrder.InvoiceID;
            _DEALVM.OrderVMs.First().Invoices.First().InvoicePath = _OxOrder.InvoiceURL;
            
            // SHIPPING
            _DEALVM.DeliveryVMs.First().Delivery.ParcelValueCurrency = _OxOrder.CurrencyCode;

            try
            {
            _DEALVM.DeliveryVMs.First().Delivery.RecipCompanyName = _OxOrder.ShippingCompany;
            } catch { }

            if (string.IsNullOrEmpty(_OxOrder.ShippingLastName) && string.IsNullOrEmpty(_OxOrder.ShippingAddress))
            {
                _DEALVM.DeliveryVMs.First().Delivery.RecipFirstName = _OxOrder.BillingTitle + " " + _OxOrder.BillingFirstName;
                _DEALVM.DeliveryVMs.First().Delivery.RecipLastName = _OxOrder.BillingLastName;

                _DEALVM.DeliveryVMs.First().Delivery.RecipAddr0 = _OxOrder.BillingAddressFloor + " " + _OxOrder.BillingAddressBuilding;

                string[] _AdrLines = Regex.Split(_OxOrder.BillingAddress, "\r\n");
                try
                {
                    _DEALVM.DeliveryVMs.First().Delivery.RecipAddr1 = _AdrLines[0]; // 16 passage homme ENTREE AU 26  RUE DE\r\nCHARONNE CODE A 1026
                    _DEALVM.DeliveryVMs.First().Delivery.RecipAddr2 = _AdrLines[1];
                    _DEALVM.DeliveryVMs.First().Delivery.RecipAddr3 = _AdrLines[2];
                }
                catch { }

                _DEALVM.DeliveryVMs.First().Delivery.RecipZip = _OxOrder.BillingZipCode;
                _DEALVM.DeliveryVMs.First().Delivery.RecipCity = _OxOrder.BillingCity;
                _DEALVM.DeliveryVMs.First().Delivery.RecipCountryISOCode = _OxOrder.BillingCountryISOCode;
                _DEALVM.DeliveryVMs.First().Delivery.RecipCountryLib = _OxOrder.BillingCountryName;

                try
                {
                        string PhoneNumber = new Tools().BestString(_OxOrder.ShippingPhone, _OxOrder.BillingPhone);
                        PhoneNumber = new Tools().BestString(PhoneNumber, _OxOrder.BillingCellPhone);
                        _DEALVM.DeliveryVMs.First().Delivery.RecipPhoneNumber = PhoneNumber;
                }
                catch { }

            }
            else
            {
                _DEALVM.DeliveryVMs.First().Delivery.RecipFirstName = _OxOrder.ShippingTitle + " " + _OxOrder.ShippingFirstName;
                _DEALVM.DeliveryVMs.First().Delivery.RecipLastName = _OxOrder.ShippingLastName;
                _DEALVM.DeliveryVMs.First().Delivery.RecipCompanyName = _OxOrder.ShippingCompany;

                string[] SAdrLines = Regex.Split(_OxOrder.ShippingAddress, "\r\n");
                try
                {
                    _DEALVM.DeliveryVMs.First().Delivery.RecipAddr1 = SAdrLines[0]; // 16 passage homme ENTREE AU 26  RUE DE\r\nCHARONNE CODE A 1026
                    _DEALVM.DeliveryVMs.First().Delivery.RecipAddr2 = SAdrLines[1];
                    _DEALVM.DeliveryVMs.First().Delivery.RecipAddr3 = SAdrLines[2];
                }
                catch { }


                _DEALVM.DeliveryVMs.First().Delivery.RecipAddr0 = _OxOrder.ShippingAddressFloor + " " + _OxOrder.ShippingAddressBuilding;
                //_DEALVM.ShipmentsVM.First().Delivery.RecipAdr1 = _OxOrder.ShippingAddressL1;
                //_DEALVM.ShipmentsVM.First().Delivery.RecipAdr2 = _OxOrder.ShippingAddress;
                _DEALVM.DeliveryVMs.First().Delivery.RecipZip = _OxOrder.ShippingZipCode;
                _DEALVM.DeliveryVMs.First().Delivery.RecipCity = _OxOrder.ShippingCity;
                _DEALVM.DeliveryVMs.First().Delivery.RecipCountryISOCode = _OxOrder.ShippingCountryISOCode;
                _DEALVM.DeliveryVMs.First().Delivery.RecipCountryLib = _OxOrder.ShippingCountryName;

                try
                {
                    string PhoneNumber = new Tools().BestString(_OxOrder.ShippingPhone, _OxOrder.BillingPhone);
                    PhoneNumber = new Tools().BestString(PhoneNumber, _OxOrder.BillingCellPhone);
                    _DEALVM.DeliveryVMs.First().Delivery.RecipPhoneNumber = PhoneNumber;
                }
                catch { }

            }

            _DEALVM.DeliveryVMs.First().Delivery.RecipEmail = _OxOrder.UserEmail;

            _DEALVM.DeliveryVMs.First().Delivery.ParcelWeight = _OxOrder.TotalWeight;
            _DEALVM.DeliveryVMs.First().Delivery.ParcelValue = _OxOrder.NetAmountDue.ToString();
            _DEALVM.DeliveryVMs.First().Delivery.ParcelValueCurrency = _OxOrder.CurrencyCode;
            _DEALVM.DeliveryVMs.First().Delivery.Shipping_method_id = _OxOrder.ShippingProcessorCode;

            if (string.IsNullOrEmpty (_OxOrder.ShippingProcessorCode))
            {
                _DEALVM.DeliveryVMs.First().Delivery.Shipping_method_id = _OxOrder.ShippingID;
                _DEALVM.DeliveryVMs.First().Delivery.Shipping_method_title = _OxOrder.ShippingMethodName;
            }

            else
                try
                {
                 _DEALVM.DeliveryVMs.First().Delivery.Shipping_method_title = //_OxOrder.TransportName;
                 WS_TransportList.Find(x => (x.Code == _OxOrder.ShippingProcessorCode)).Label;
                } catch { }


            _DEALVM.DeliveryVMs.First().Delivery.MerchantKey = _Store.MerchantKey;
            _DEALVM.DeliveryVMs.First().Delivery.ProductCode = _OxOrder.ShippingParam1;
            _DEALVM.DeliveryVMs.First().Delivery.TrackingNumber = _OxOrder.TrackingNumber;
            _DEALVM.DeliveryVMs.First().Delivery.TrackingInfo = _OxOrder.TrackingUrl;
            _DEALVM.DeliveryVMs.First().Delivery.ShipmentStatus = "T";

            try
            {
                //_DEALVM.ShipmentsVM.First().Delivery.DeliveryInstructions1 = _OxOrder.ProgressStateID;
                
                if (!string.IsNullOrEmpty(_OxOrder.ShippingInfo))
                    _DEALVM.DeliveryVMs.First().Delivery.DeliveryInstructions1 = _OxOrder.ShippingInfo;
            } catch {}

            // ORDER
            _DEALVM.OrderVMs.First().Order.StoreKey = _Store.StoreKey;
            _DEALVM.OrderVMs.First().Order.MerchantKey = _Store.MerchantKey;
            _DEALVM.OrderVMs.First().Order.Store_Name = _Store.StoreName;

            _DEALVM.OrderVMs.First().Order.Currency = _OxOrder.CurrencyCode ;
            _DEALVM.OrderVMs.First().Order.CheckoutMessage = _OxOrder.SpecialInstructions;
            _DEALVM.OrderVMs.First().Order.Purchase_date = _OxOrder.Date;

            long D1 = _OxOrder.Date.Ticks;
            long D3 = _OxOrder.PaymentStatusLastModifiedDate.Ticks;

            _DEALVM.OrderVMs.First().Order.ModificationDate = new DateTime(Math.Min(D1, D3));

            _DEALVM.OrderVMs.First().Order.CreationDate = _OxOrder.Date;
            _DEALVM.OrderVMs.First().Order.OrderKey = _OxOrder.OxID.ToString();
            _DEALVM.OrderVMs.First().Order.OrderID_Ext = _OxOrder.OxID.ToString();

            _DEALVM.OrderVMs.First().Order.shipping_price = _OxOrder.ShippingPriceTaxExcl.ToString();

            _DEALVM.OrderVMs.First().Order.TransactionPrice = _OxOrder.NetAmountDue.ToString();

            if (_OxOrder.PaymentMethodName != "NC")
                _DEALVM.OrderVMs.First().Order.PaymentInfo = _OxOrder.PaymentMethodName;

            //_DEALVM.ShipmentsVM.First().OrdersVM.First().Order.PaymentInfo = _OxOrder.PaymentStatusCode;

            /*   0 Canceled
                10 Technical error 
                14 Contacting payment processor
                20 Payment in progress
                30 Payment refused
                40 Payment confirmed
            */

            _DEALVM.OrderVMs.First().Order.PaymentReferenceID = _OxOrder.PaymentMethodID;
            _DEALVM.OrderVMs.First().Order.Payment_Date = _OxOrder.PaymentStatusLastModifiedDate;

            string _Status = (_OxOrder.PaymentStatusCode == "40") ? "processing" : "not validated";
            _DEALVM.OrderVMs.First().Order.Order_Status = _Status;
            _DEALVM.OrderVMs.First().Order.OrderStatus_Ext = _OxOrder.ProgressStateID + "-" + _OxOrder.PaymentStatusCode;

            string _ProgresStateID = _OxOrder.ProgressStateID == null ? "" : "_" + _OxOrder.ProgressStateID;

            //if (!string.IsNullOrEmpty (_OxOrder.ProgressStateID))
            //_DEALVM.ShipmentsVM.First().OrdersVM.First().Order.Order_Status += _ProgresStateID;

            string _PMProcessorCode = _OxOrder.PMProcessorCode == null ? "" : "_" + _OxOrder.PMProcessorCode.ToString();

            //if (!string.IsNullOrEmpty(_PMProcessorCode))
            //    _DEALVM.ShipmentsVM.First().OrdersVM.First().Order.Order_Status += _PMProcessorCode;


            _DEALVM.OrderVMs.First().Order.Buyer_s_email = _OxOrder.UserEmail;
            _DEALVM.OrderVMs.First().Order.Currency = _OxOrder.CurrencyCode;

            try
            {
                _DEALVM.OrderVMs.First().Order.CheckoutMessage = _OxOrder.SpecialInstructions;
            } catch { }


            /// 
            /// PRODUCTS
            /// 

            foreach (OrderItem _Item in _OxOrder.OrderItems) 
            {
                OrderProduct _OrderProduct = new OrderProduct();

                _OrderProduct.ProductName = _Item.ItemName;
                _OrderProduct.SKU = _Item.ItemSKU;
                _OrderProduct.CN23CategoryID = (int)Colissimo.CN23ProductCategory.CN23COMMERCIALGOOD;
                _OrderProduct.Quantity = Convert.ToInt32(_Item.Quantity);
                _OrderProduct.Tax = _Item.VATAmount.ToString();
                _OrderProduct.Rate = _Item.TaxRate.ToString();
                _OrderProduct.PriceExclTax = (_Item.NetPrice - _Item.VATAmount).ToString();
                //_OrderProduct.PriceExclTax = _Item.GrossAmount.ToString();
                _OrderProduct.Price = _Item.GrossPrice.ToString();
                //_Product.Price = _Item.ToString();

                _OrderProduct.SubTotalPriceExclTax = (_Item.Quantity * new Tools().StringToDouble(_OrderProduct.PriceExclTax)).ToString();
                _OrderProduct.SubTotalTax = (_Item.Quantity * _Item.VATAmount).ToString();

                _DEALVM.OrderVMs.First().OrderProducts.Add(_OrderProduct);
            }

            Delivery _Delivery = _DEALVM.DeliveryVMs.First().Delivery;
            ShippingServiceResponse _ShippingServiceReturn = new Shipping().SetShippingService(_Store, _Delivery, null, true);
            if (string.IsNullOrEmpty(_ShippingServiceReturn.ErrorMessage))
            {
                _DEALVM.DeliveryVMs.First().Delivery.CarrierServiceKey = _ShippingServiceReturn.CarrierServiceKey;
                _DEALVM.DeliveryVMs.First().Delivery.ShippingService = _ShippingServiceReturn.CarrierServiceID;
                _DEALVM.DeliveryVMs.First().Delivery.ShippingServiceID = _ShippingServiceReturn.CarrierServiceID;
                _DEALVM.DeliveryVMs.First().Delivery.ProductCode = _ShippingServiceReturn.ProductCode;
            }

                // <OrderTaxDetails> <TotalNetTaxExcl /> <TaxRate /> <TotalNetVATAmount /> </OrderTaxDetails>

                /*
                 <BundledItems><BundledItem> 
                    <ItemOXID /><ItemSKU /> <ItemName /> <Quantity /> <Option1Name /> <Option1Value /> <Option2Name /> <Option2Value /> <Option3Name /> <Option3Value /> <ItemMainImageURL /> <ItemThumbnailImageURL /> <Offered /> 
                </BundledItem> </BundledItems> 
                 */
            }
            catch (Exception ex)
            {
                string Title = "Oxatis Ox_OrderToDEALVM error " + DateTime.Now.ToString();
                string _ValidationErrorMessage = ex.Message;
                string _InnerException = new Tools().GetInnerException(ex);

                new Email().ReportErrorByEmail(Title, _ValidationErrorMessage, _InnerException, ex.StackTrace);

            }
            return _DEALVM;
        }

        #endregion

        #region Oxatis_GetProducts
        public GetProductsResponse Ox_GetProductsToDB(Store _Store, String UpdatedFromTime = null, String SKU = null)
        {
            if (!string.IsNullOrEmpty(_Store.Token))
            {
                appID = _Store.AppID;
                Token = _Store.Token;
            }
            else return null;

            GetProductsResponse _GetProductsResponse = new GetProductsResponse();

            // private String METHOD = "ProductGetList";

            DateTime _updatedFromTime = new Tools().ConvertStringToDate(UpdatedFromTime, "fr-FR");

            if (_updatedFromTime != new DateTime(1900, 1, 1))
                UpdatedFromTime = new Tools().ConvertDateToString(_updatedFromTime, "yyyy-MM-ddThh:mm:ss");

            if (string.IsNullOrEmpty(UpdatedFromTime))
                UpdatedFromTime = "2019-09-18T13:20:00";
            //if (CreatedFromTime == null) CreatedFromTime = "2011-09-18T13:20:00";

            String METHOD = "ProductGetList";
            //String URL = "http://webservices.oxatis.com/webservices/httpservices/ProductServices.aspx";
            /*
            var xml =
                "<?xml version='1.0' encoding='utf-8'?> <ProductList xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>";
                //+ "<LatestModifiedDateStart>" + CreatedFromTime + " </LatestModifiedDateStart>"
                //+ "<LatestModifiedDateEnd>" + updatedFromTime +" </LatestModifiedDateEnd>";
            xml += "</ProductList>";
            */

           string xml = "<?xml version='1.0' encoding='utf-8'?>";
            xml += "<ProductList xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>";
            //xml += "<LatestModifiedDateStart>2010-05-14T14:47:26.6453228+02:00</LatestModifiedDateStart>";            
            xml += "<LatestModifiedDateStart>" + UpdatedFromTime + "</LatestModifiedDateStart>";
            //xml += "<LatestModifiedDateEnd>2019-05-14T16:47:26.6453228+02:00</LatestModifiedDateEnd>";
            xml += "</ProductList>";

            System.Xml.XmlDocument doc = OX_WebReq(ProductServicesURL, METHOD, xml);
            int n = doc.ChildNodes.Count;

            XmlNode DataRequest = doc.ChildNodes.Item(1);

            if (DataRequest["StatusCode"].InnerText!="200")
            {
                ResponseHeader _GetProductResponseHeader = new ResponseHeader();
                _GetProductsResponse.Response = null;
                _GetProductResponseHeader.LanguageCode = "En";
                _GetProductResponseHeader.RequestStatus = "Error";
                _GetProductResponseHeader.ReturnCode = "OX0" + DataRequest["StatusCode"].InnerText;
                _GetProductResponseHeader.StatusCode = "400";
                _GetProductResponseHeader.ReasonPhrase = AtomicAPI.Helpers.Constants.GetReasonPhrase("400");
                _GetProductResponseHeader.ReturnMessage = DataRequest["ErrorDetails"].InnerText;
                _GetProductsResponse.Header = _GetProductResponseHeader;

                return _GetProductsResponse;
            }
            else
            {
                ResponseHeader _GetProductResponseHeader = new ResponseHeader();
                _GetProductResponseHeader.LanguageCode = "En";
                _GetProductResponseHeader.RequestStatus = "Ok";
                _GetProductResponseHeader.ReturnCode = "AS0000";
                _GetProductResponseHeader.StatusCode = "200";
                _GetProductResponseHeader.ReasonPhrase = AtomicAPI.Helpers.Constants.GetReasonPhrase("200");
                _GetProductResponseHeader.ReturnMessage = "";
                _GetProductsResponse.Header = _GetProductResponseHeader;

                _GetProductsResponse.Response = new ProductAPI.Models.GetProductsResponseData();
                _GetProductsResponse.Response.Products = new List<WSProductVM>();
            }

            XmlNode OrderList = DataRequest.ChildNodes.Item(3);
            XmlNode OrderIds = OrderList.FirstChild;
            OrderIds = OrderIds.FirstChild;
            while (OrderIds != null)
            {
                if (OrderIds.Name == "ProductsID")
                    break;
                OrderIds = OrderIds.NextSibling;
            }

            int nSize = OrderIds.ChildNodes.Count;

            for (int i = 0; i < nSize; i++)
            {
                XmlNode item = OrderIds.ChildNodes.Item(i);
                String strId = item.ChildNodes.Item(0).InnerText;
                if (strId == SKU && string.IsNullOrEmpty(SKU)==false)
                {
                    METHOD = "ProductGet";
                    xml =
                        "<?xml version='1.0' encoding='utf-8'?><Product xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'><OxID>" +
                        strId + "</OxID><ItemSKU></ItemSKU></Product>";
                    System.Xml.XmlDocument _XMLResponse = OX_WebReq(ProductServicesURL, METHOD, xml);
                    ///////////////

                    ProductAPI.Models.WSProductVM _WSProduct = new ProductAPI.Models.WSProductVM();
                    _WSProduct.Product = XmlDocToProduct(_XMLResponse.ChildNodes.Item(1).ChildNodes.Item(3).FirstChild, _Store.StoreKey);

                    _GetProductsResponse.Response.Products.Add(_WSProduct);

                }
                else if (string.IsNullOrEmpty(SKU))
                {
                    METHOD = "ProductGet";
                    xml =
                        "<?xml version='1.0' encoding='utf-8'?> <Product xmlns:xsi = 'http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd = 'http://www.w3.org/2001/XMLSchema'><OxID>" + strId + "</OxID><ItemSKU></ItemSKU></Product>";
                    System.Xml.XmlDocument _XMLResponse = OX_WebReq(ProductServicesURL, METHOD, xml);
                    ///////////////
                    XmlNode _XmlProduct = _XMLResponse.ChildNodes.Item(1).ChildNodes.Item(3).FirstChild;

                    //if (_Product.LastUpdateDate > new Tools().ConvertStringToDate(UpdatedFromTime, "fr-FR")))

                    ProductAPI.Models.WSProductVM _WSProduct = new ProductAPI.Models.WSProductVM();
                    _WSProduct.Product = XmlDocToProduct(_XmlProduct, _Store.StoreKey);

                    _GetProductsResponse.Response.Products.Add(_WSProduct);


                }
            }

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

            _GetProductsResponse.Header.ReturnMessage = _GetProductsResponse.Response.Products.Count.ToString() + " products found in " + _Store.StoreKey ;

            new ProductsCommon().ProductsToDB(_GetProductsResponse.Response.Products, _Store);

            return _GetProductsResponse;
        }

        #endregion

        private OxatisAPI.Models.Order XmlDocToOrder(System.Xml.XmlNode _XMLOrder)
        {
            AtomicSeller.Models.Order _Order = new Models.Order();
            OxatisAPI.Models.Order _OxOrder = null;

            XmlSerializer serializer = new XmlSerializer(typeof(OxatisAPI.Models.Order));
            using (XmlReader reader = new XmlNodeReader(_XMLOrder))
            {
                try
                {
                    _OxOrder = (OxatisAPI.Models.Order)serializer.Deserialize(reader);
                }
                catch (Exception ex)
                {
                    Tools.ErrorHandler("XmlDocToOrder error", ex, false, true, true);
                    Tools.ErrorHandler(_XMLOrder.InnerXml.ToString(), null, false, true, false);

                    string Title = "Oxatis XmlDocToOrder error " + DateTime.Now.ToString();
                    string _ValidationErrorMessage = ex.Message;
                    string _InnerException = new Tools().GetInnerException(ex);

                    new Email().ReportErrorByEmail(Title, _ValidationErrorMessage, _InnerException, ex.StackTrace);

                }
            }

            return _OxOrder;
        }

        private AtomicSeller.Models.Product XmlDocToProduct(System.Xml.XmlNode _XMLProduct, string StoreKey)
        {
            AtomicSeller.Models.Product _Product = new Models.Product();
            OxatisAPI.Models.Product _OxProduct = null;

            XmlSerializer serializer = new XmlSerializer(typeof(OxatisAPI.Models.Product));
            using (XmlReader reader = new XmlNodeReader(_XMLProduct))
            {
                try
                {
                    _OxProduct = (OxatisAPI.Models.Product)serializer.Deserialize(reader);
                }
                catch (Exception ex)
                {
                    _Product.ProductName = ex.InnerException.ToString() + ex.InnerException.StackTrace;
                    string toto = ex.ToString();
                    string titi = _XMLProduct.InnerXml.ToString();                    
                    return _Product;
                }
            }

            _Product.ProductID = Convert.ToInt32(_OxProduct.OxID);
            _Product.GTIN = _OxProduct.EANCODE;
            //string toto = _XMLProduct.InnerXml.ToString();
            _Product.BundleID = "";
            _Product.CN23CategoryID = (int)Colissimo.CN23ProductCategory.CN23COMMERCIALGOOD;
            //_Product.PriceExclTax = _OxProduct.Price.ToString();
            //_Product.PriceInclTax = _OxProduct.Price.ToString();
            //_Product.ProductID = _XMLProduct[""].InnerText;
            _Product.ProductName = _OxProduct.Name;
            _Product.SKU = _OxProduct.ItemSKU;
            //_Product.Tax = _OxProduct.TaxAmountList.ToString();
            _Product.StoreKey = StoreKey;
            _Product.TaxRate = _OxProduct.TaxRate.ToString();
            //_Product.VariationID = _OxProduct.;
            _Product.Weight = _OxProduct.Weight;
            _Product.ProductWidth = _OxProduct.DimensionWidth;
            _Product.ProductDepth = _OxProduct.DimensionHeight;
            _Product.ProductLength = _OxProduct.DimensionLength;
            if (_OxProduct.Price.VATIncluded == true)
                _Product.PriceInclTax = _OxProduct.Price.Value;
            else
                _Product.PriceExclTax = _OxProduct.Price.Value;

            _Product.LastUpdateDate = _OxProduct.LastUpdateDate;
             
            return _Product;
        }      

        private System.Xml.XmlDocument OX_WebReq(String URL, String METHOD, String XML)
        {
            HttpWebRequest wc = (HttpWebRequest)WebRequest.Create(URL);
            wc.ContentType = "application/x-www-form-urlencoded";
            wc.Method = "POST";
            wc.Headers.Add(HttpRequestHeader.AcceptLanguage, Thread.CurrentThread.CurrentUICulture.Name);

            StringBuilder _StringBuilder = new StringBuilder();
            _StringBuilder.Append("APPID=" + HttpUtility.UrlEncode(appID) + "&");
            _StringBuilder.Append("TOKEN=" + HttpUtility.UrlEncode(Token) + "&");
            _StringBuilder.Append("METHOD=" + HttpUtility.UrlEncode(METHOD) + "&");
            _StringBuilder.Append("DATA=" + HttpUtility.UrlEncode(XML));

            Byte[] l_Flux = Encoding.UTF8.GetBytes(_StringBuilder.ToString());

            wc.ContentLength = l_Flux.Length;
            Stream l_StreamFluxData = wc.GetRequestStream();
            l_StreamFluxData.Write(l_Flux, 0, l_Flux.Length);

            System.Xml.XmlDocument xresp = new System.Xml.XmlDocument();

            HttpWebResponse out_Response = (HttpWebResponse)wc.GetResponse();
            try
            {
                Stream l_StreamResponce = out_Response.GetResponseStream();
                StreamReader l_Reader = new StreamReader(l_StreamResponce, Encoding.UTF8);
                try
                {
                    string Result = l_Reader.ReadToEnd();
                    xresp.LoadXml(Result);
                }
                finally
                {
                    if (l_Reader != null)
                    {
                        l_Reader.Close();
                        l_Reader.Dispose();
                    }
                }
            }
            finally
            {
                out_Response.Close();
            }

            return xresp;
        }
    }
}