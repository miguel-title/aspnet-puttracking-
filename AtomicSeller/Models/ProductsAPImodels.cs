using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using AtomicAPI.Helpers;
using AtomicSeller.Models;
using AtomicCommonAPI.Models;

namespace ProductAPI.Models
{
    public class GetProductsRequest
    {
        public RequestHeader Header { get; set; }
        public GetProductsData Request { get; set; }

    }


    public class GetProductsInfoRequest
    {
        public RequestHeader Header { get; set; }
        public GetProductsInfoData Request { get; set; }

    }

    public class PutProductsInfoRequest
    {
        public RequestHeader Header { get; set; }
        public PutProductsInfoRequestData Request { get; set; }
    }


    public class PutProductsRequest
    {
        public RequestHeader Header { get; set; }
        public PutProductsData Request { get; set; }

    }
    public class PutProductsData
    {
        public string Option { get; set; }
        public string LanguageCode { get; set; }
        public string StoreKey { get; set; }
        public List<WSProductVM> Products { get; set; }
    }

    /*
    public class Header
    {
        public string Token { get; set; }
        public string Option { get; set; }

    }
    */

    public class GetProductsData
    {
        public string SKU { get; set; }
        public string Stock { get; set; }
        public string LanguageCode { get; set; }
        public string StoreKey { get; set; }
        public string ProductId { get; set; }
        public string Option { get; set; }
        public string UpdatedFromTime { get; set; }
    }


    public class GetProductsInfoResponse
    {
        public ResponseHeader Header { get; set; }
        public GetProductsInfoResponseData Response { get; set; }
    }

    public class PutProductsInfoResponse
    {
        public ResponseHeader Header { get; set; }
        public PutProductsInfoResponseData Response { get; set; }
    }

    public class PutProductInfoRequest
    {
        public RequestHeader Header { get; set; }
        public PutProductsInfoRequestData Request { get; set; }

    }


    public class PutProductsResponse
    {
        public ResponseHeader Header { get; set; }
        public PutProductsResponseData Response { get; set; }
    }

    public class GetProductsResponse
    {
        public ResponseHeader Header { get; set; }
        public GetProductsResponseData Response { get; set; }
    }


    public class GetProductsResponseData
    {
        public List<WSProductVM> Products { get; set; }
    }

    public class PutProductsInfoResponseData
    {
        public List<ProductDM> Products { get; set; }
    }

    public class PutProductsResponseData
    {
        public List<WSProductVM> Products { get; set; }
    }

    public class GetProductsInfoResponseData
    {
        public List<ProductDM> Products { get; set; }
    }

    public class Bundle
    {
        public string SKU { get; set; }
        public string BundleID { get; set; }
    }

}