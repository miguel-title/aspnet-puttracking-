using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AtomicCommonAPI.Models;

namespace JoomAPI.Models
{

    //Get Access Token Class
    public class GetAccessTokenResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
    }

    //GetOrder Class

    public class GetOrderResponse
    {
        public ResponseHeader Header { get; set; }
        public GetOrderResponseData Response { get; set; }
    }


    public class GetOrderResponseData
    {
        public int code { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public Item[] items { get; set; }
    }

    public class Item
    {
        public string id { get; set; }
        public string storeId { get; set; }
        public string transactionId { get; set; }
        public string customerId { get; set; }
        public Shippingaddress shippingAddress { get; set; }
        public int quantity { get; set; }
        public string status { get; set; }
        public Product product { get; set; }
        public Priceinfo priceInfo { get; set; }
        public DateTime updateTimestamp { get; set; }
        public DateTime orderTimestamp { get; set; }
        public DateTime approvedTimestamp { get; set; }
        public string shippingAddressHash { get; set; }
        public Shippingoption shippingOption { get; set; }
        public string shippingMethod { get; set; }
        public string onlineShippingRequirement { get; set; }
        public bool onlineShippingRequired { get; set; }
    }

    public class Shippingaddress
    {
        public string name { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string phoneNumber { get; set; }
        public string streetAddress1 { get; set; }
        public string zipCode { get; set; }
    }

    public class Product
    {
        public string id { get; set; }
        public string sku { get; set; }
        public string name { get; set; }
        public Image image { get; set; }
        public Variant variant { get; set; }
    }

    public class Image
    {
        public Image1[] images { get; set; }
    }

    public class Image1
    {
        public int width { get; set; }
        public int height { get; set; }
        public string url { get; set; }
    }

    public class Variant
    {
        public string id { get; set; }
        public string sku { get; set; }
        public string size { get; set; }
        public string color { get; set; }
        public Image2 image { get; set; }
    }

    public class Image2
    {
    }

    public class Priceinfo
    {
        public string unitPrice { get; set; }
        public string shippingPrice { get; set; }
        public string origAmount { get; set; }
        public string orderPrice { get; set; }
        public string merchantRevenue { get; set; }
        public string commissionAmount { get; set; }
        public Discounts discounts { get; set; }
        public string orderCost { get; set; }
    }

    public class Discounts
    {
        public string sum { get; set; }
    }

    public class Shippingoption
    {
        public string warehouseId { get; set; }
        public string warehouseName { get; set; }
        public string warehouseType { get; set; }
        public string tierId { get; set; }
        public string tierType { get; set; }
        public string tierName { get; set; }
    }


    public class PostDeliveryResponse
    {
        public ResponseHeader Header { get; set; }
    }

    public class JoomCarriers
    {
        public int code { get; set; }
        public CarrierData data { get; set; }
    }

    public class CarrierData
    {
        public Carrier[] items { get; set; }
    }

    public class Carrier
    {
        public string id { get; set; }
        public string name { get; set; }
        public string nameLowercase { get; set; }
        public string nameChinese { get; set; }
        public string url { get; set; }
        public bool trusted { get; set; }
        public string[] supportedDestinationCountries { get; set; }
        public Onlineshippinginfo onlineShippingInfo { get; set; }
    }

    public class Onlineshippinginfo
    {
        public int minWeight { get; set; }
        public int maxWeight { get; set; }
        public string maxPrice { get; set; }
        public string note { get; set; }
        public bool supportsPickup { get; set; }
        public bool onlineOnly { get; set; }
    }


    public class ProductJson
    {
        [JsonProperty("items")]
        public JoomProduct Items { get; set; }
    }

    public class JoomProduct
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("sku")]
        public string Sku { get; set; }

        [JsonProperty("storeId")]
        public string StoreId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("category")]
        public Category Category { get; set; }

        [JsonProperty("categoryByJoom")]
        public CategoryByJoom CategoryByJoom { get; set; }

        [JsonProperty("mainImage")]
        public MainImage MainImage { get; set; }

        [JsonProperty("tags")]
        [JsonConverter(typeof(SingleOrArrayConverter<string>))]
        public List<string> Tags { get; set; }

        [JsonProperty("labels")]
        [JsonConverter(typeof(SingleOrArrayConverter<string>))]
        public List<string> Labels { get; set; }

        [JsonProperty("dangerKind")]
        public string DangerKind { get; set; }

        [JsonProperty("takeRate")]
        public string TakeRate { get; set; }

        [JsonProperty("brandId")]
        public string BrandId { get; set; }

        [JsonProperty("brandAuthorized")]
        public bool BrandAuthorized { get; set; }

        [JsonProperty("dangerousKind")]
        public string DangerousKind { get; set; }

        [JsonProperty("uploadTimestamp")]
        public string UploadTimestamp { get; set; }

        [JsonProperty("updateTimestamp")]
        public string UpdateTimestamp { get; set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("price")]
        public Price Price { get; set; }

        [JsonProperty("review")]
        public Review Review { get; set; }

        [JsonProperty("performanceMetrics")]
        public PerformanceMetrics PerformanceMetrics { get; set; }

        [JsonProperty("variants")]
        [JsonConverter(typeof(SingleOrArrayConverter<string>))]
        public List<string> Variants { get; set; }

        [JsonProperty("rating")]
        public Rating Rating { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("isPromoted")]
        public bool IsPromoted { get; set; }

        [JsonProperty("tierInfo")]
        public TierInfo TierInfo { get; set; }

        [JsonProperty("hasActiveVersion")]
        public bool HasActiveVersion { get; set; }
    }

    public class Category
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("takeRate")]
        public string TakeRate { get; set; }


    }

    public class CategoryByJoom
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("takeRate")]
        public string TakeRate { get; set; }
    }

    public class MainImage
    {
        [JsonProperty("origUrl")]
        public string OrigUrl { get; set; }

        [JsonProperty("imageState")]
        public string ImageState { get; set; }

        [JsonProperty("processed")]
        [JsonConverter(typeof(SingleOrArrayConverter<Processed>))]
        public List<Processed> Processed { get; set; }

    }

    public class Processed
    {
        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class Price
    {
        [JsonProperty("min")]
        public float Min { get; set; }

        [JsonProperty("max")]
        public float Max { get; set; }
    }

    public class Review
    {

    }

    public class PerformanceMetrics
    {
        [JsonProperty("metrics")]
        public Metrics Metrics { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class Metrics
    {
        [JsonProperty("cancelRate")]
        public CancelRate CancelRate { get; set; }

        [JsonProperty("refundByQualityRate")]
        public RefundByQualityRate RefundByQualityRate { get; set; }
    }

    public class CancelRate
    {
        [JsonProperty("status")]
        public string Status { get; set; }

    }

    public class RefundByQualityRate
    {
        [JsonProperty("status")]
        public string Status { get; set; }

    }


    public class Rating
    {
        [JsonProperty("average")]
        public float Average { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }

    public class TierInfo
    {
        [JsonProperty("tier")]
        public string Tier { get; set; }

    }

    public class UpdateProductRootobject
    {
        public UpdateProductItem[] items { get; set; }
    }

    public class UpdateProductItem
    {
        //public string variantId { get; set; }
        public string variantSku { get; set; }
        //public string warehouseId { get; set; }
        public string warehouseLabel { get; set; }
        public int inventory { get; set; }
        //public string currency { get; set; }
        //public string shippingPrice { get; set; }
    }

    public class JoomReturn
    {
        public int code { get; set; }
        public string message { get; set; }
        public Error[] errors { get; set; }
    }

    public class Error
    {
        public string code { get; set; }
        public string message { get; set; }
    }


    public class FulFillRootobject
    {
        public string provider { get; set; }
        public string providerId { get; set; }
        public string trackingNumber { get; set; }
    }


    public class SingleOrArrayConverter<T> : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(List<T>));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            if (token.Type == JTokenType.Array)
            {
                return token.ToObject<List<T>>();
            }
            return new List<T> { token.ToObject<T>() };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            List<T> list = (List<T>)value;
            if (list.Count == 1)
            {
                value = list[0];
            }
            serializer.Serialize(writer, value);
        }

        public override bool CanWrite
        {
            get { return true; }
        }




    }
}