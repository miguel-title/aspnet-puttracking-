using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using AtomicCommonAPI.Models;

namespace MiraklMetroAPI.Models
{

    //Get Access Token Class
    public class GetAccessTokenResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
    }


    //GetOffer Class
    public class GetOfferResponse
    {
        public ResponseHeader Header { get; set; }
        public GetOfferResponseData Response { get; set; }
    }

    public class GetOfferResponseData
    {
        public List<offer> offers { get; set; }
        public Int32 total_count { get; set; }
    }

    public class offer
    {
        public bool active { get; set; }
        public List<all_prices> all_prices { get; set; }
        public bool allow_quote_requests { get; set; }
        public applicable_pricing applicable_pricing { get; set; }
        public string available_end_date { get; set; }
        public string available_start_date { get; set; }
        public string category_code { get; set; }
        public string category_label { get; set; }
        public List<string> channels { get; set; }
        public string currency_iso_code { get; set; }
        public string description { get; set; }
        public discount discount { get; set; }
        public Int32 favorite_rank { get; set; }
        public List<string> inactivity_reasons { get; set; }
        public Int32 leadtime_to_ship { get; set; }
        public logistic_class logistic_class { get; set; }
        public Int32 max_order_quantity { get; set; }
        public Int32 min_order_quantity { get; set; }
        public Int32 min_quantity_alert { get; set; }
        public string min_shipping_price { get; set; }
        public string min_shipping_price_additional { get; set; }
        public string min_shipping_type { get; set; }
        public string min_shipping_zone { get; set; }
        public List<offer_additional_fields> offer_additional_fields { get; set; }
        public Int64 offer_id { get; set; }
        public Int32 package_quantity { get; set; }
        public string price { get; set; }
        public string price_additional_info { get; set; }
        public List<product_references> product_references { get; set; }
        public string product_sku { get; set; }
        public string product_tax_code { get; set; }
        public string product_title { get; set; }
        public Int32 quantity { get; set; }
        public string shop_sku { get; set; }
        public string state_code { get; set; }
        public string total_price { get; set; }
    }

    public class all_prices
    {
        public string channel_code { get; set; }
        public string discount_end_date { get; set; }
        public string discount_start_date { get; set; }
        public string price { get; set; }
        public string unit_discount_price { get; set; }
        public string unit_origin_price { get; set; }
        public List<volume_prices> volume_prices { get; set; }
    }

    public class applicable_pricing
    {
        public string channel_code { get; set; }
        public string discount_end_date { get; set; }
        public string discount_start_date { get; set; }
        public string price { get; set; }
        public string unit_discount_price { get; set; }
        public string unit_origin_price { get; set; }
        public List<volume_prices> volume_prices { get; set; }
    }

    public class volume_prices 
    {    
        public string price { get; set; }
        public Int32 quantity_threshold { get; set; }
        public string unit_discount_price { get; set; }
        public string unit_origin_price { get; set; }
    }

    public class discount
    {
        public string discount_price { get; set; }
        public string end_date { get; set; }
        public string origin_price { get; set; }
        public List<ranges> ranges { get; set; }
        public string start_date { get; set; }
    }

    public class ranges
    {
        public string price { get; set; }
        public Int32 quantity_threshold { get; set; }
    }

    public class logistic_class
    {
        public string code { get; set; }
        public string label { get; set; }
    }

    public class offer_additional_fields
    {
        public string code { get; set; }
        public string type { get; set; }
        public string value { get; set; }
    }

    public class product_references
    {
        public string reference { get; set; }
        public string reference_type { get; set; }
    }

    //GetOrder Class
    
    public class GetOrderResponse
    {
        public ResponseHeader Header { get; set; }
        public GetOrderResponseData Response { get; set; }
    }

    public class GetOrderResponseData
    {
        public List<order> orders { get; set; }
        public Int32 total_count { get; set; }
    }

    public class order
    {
        public string acceptance_decision_date { get; set; }
        public bool can_cancel { get; set; }
        public bool can_shop_ship { get; set; }
        public channel channel { get; set; }
        public string commercial_id { get; set; }
        public string created_date { get; set; }
        public string debited_date { get; set; }
        public string currency_iso_code { get; set; }
        public customer customer { get; set; }
        public string customer_debited_date { get; set; }
        public string customer_notification_email { get; set; }
        public delivery_date delivery_date { get; set; }
        public fulfillment fulfillment { get; set; }
        public bool has_customer_message { get; set; }
        public bool has_incident { get; set; }
        public bool has_invoice { get; set; }
        public string last_updated_date { get; set; }
        public Int32 leadtime_to_ship { get; set; }
        public List<order_additional_fields> order_additional_fields { get; set; }
        public string order_id { get; set; }
        public List<order_lines> order_lines { get; set; }
        public string order_state { get; set; }
        public string order_state_reason_code { get; set; }
        public string order_state_reason_label { get; set; }
        public string order_tax_mode { get; set; }
        public string AdditionalCostOrDiscount { get; set; }
        public string EstimatedShipDateUtc { get; set; }
        public Int32 payment_duration { get; set; }
        public string payment_type { get; set; }
        public string payment_workflow { get; set; }
        public string paymentType { get; set; }
        public string price { get; set; }
        public promotions promotions { get; set; }
        public string quote_id { get; set; }
        public string shipping_carrier_code { get; set; }
        public string shipping_company { get; set; }
        public string shipping_deadline { get; set; }
        public string shipping_price { get; set; }
        public string shipping_pudo_id { get; set; }
        public string shipping_tracking { get; set; }
        public string shipping_tracking_url { get; set; }
        public string shipping_type_code { get; set; }
        public string shipping_type_label { get; set; }
        public string shipping_zone_code { get; set; }
        public string shipping_zone_label { get; set; }
        public string total_commission { get; set; }
        public string total_price { get; set; }
        public string transaction_date { get; set; }
        public string transaction_number { get; set; }
    }

    public class channel
    {
        public string code { get; set; }
        public string label { get; set; }
    }

    public class customer
    {
        public address billing_address { get; set; }
        public string civility { get; set; }
        public string customer_id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string locale { get; set; }
        public address shipping_address { get; set; }
    }

    public class address
    {
        public string additional_info { get; set; }
        public string city { get; set; }
        public string civility { get; set; }
        public string company { get; set; }
        public string country { get; set; }
        public string country_iso_code { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string phone { get; set; }
        public string phone_secondary { get; set; }
        public string state { get; set; }
        public string street_1 { get; set; }
        public string street_2 { get; set; }
        public string zip_code { get; set; }
    }

    public class delivery_date
    {
        public string earliest { get; set; }
        public string latest { get; set; }
    }

    public class fulfillment
    {
        public center center { get; set; }
    }

    public class center
    {
        public string code { get; set; }
    }

    public class order_additional_fields
    {
        public string code { get; set; }
        public string type { get; set; }
        public string value { get; set; }

    }

    public class order_lines
    {
        public bool can_refund { get; set; }
        public List<cancelations> cancelations { get; set; }
        public string category_code { get; set; }
        public string category_label { get; set; }
        public string commission_fee { get; set; }
        public string commission_rate_vat { get; set; }
        public List<commission_taxes> commission_taxes { get; set; }
        public string commission_vat { get; set; }
        public string created_date { get; set; }
        public string debited_date { get; set; }
        public string description { get; set; }
        public string last_updated_date { get; set; }
        public measurement measurement { get; set; }
        public Int64 offer_id { get; set; }
        public string offer_sku { get; set; }
        public List<order_line_additional_fields> order_line_additional_fields { get; set; }
        public string order_line_id { get; set; }
        public Int32 order_line_index { get; set; }
        public string order_line_state { get; set; }
        public string order_line_state_reason_code { get; set; }
        public string order_line_state_reason_label { get; set; }
        public string price { get; set; }
        public string price_additional_info { get; set; }
        public price_amount_breakdown price_amount_breakdown { get; set; }
        public string price_unit { get; set; }
        public List<product_medias> product_medias { get; set; }
        public string product_sku { get; set; }
        public string product_title { get; set; }
        public List<promotions> promotions { get; set; }
        public Int32 quantity { get; set; }
        public string received_date { get; set; }
        public List<refunds> refunds { get; set; }
        public string shipped_date { get; set; }
        public string shipping_price { get; set; }
        public string shipping_price_additional_unit { get; set; }
        public shipping_price_amount_breakdown shipping_price_amount_breakdown { get; set; }
        public string shipping_price_unit { get; set; }
        public List<shipping_taxes> shipping_taxes { get; set; }
        public List<taxes> taxes { get; set; }
        public string total_commission { get; set; }
        public string total_price { get; set; }

    }

    public class cancelations
    {
        public string amount { get; set; }
        public amount_breakdown amount_breakdown { get; set; }
        public string commission_amount { get; set; }
        public List<commission_taxes> commission_taxes { get; set; }
        public string commission_total_amount { get; set; }
        public string created_date { get; set; }
        public string id { get; set; }
        public Int32 quantity { get; set; }
        public string reason_code { get; set; }
        public string shipping_amount { get; set; }
        public shipping_amount_breakdown shipping_amount_breakdown { get; set; }
        public List<shipping_taxes> shipping_taxes { get; set; }
        public List<taxes> taxes { get; set; }
    }

    public class measurement
    {
        public string actual_measurement { get; set; }
        public string adjustment_limit { get; set; }
        public string measurement_unit { get; set; }
        public string ordered_measurement { get; set; }
    }

    public class order_line_additional_fields
    {
        public string code { get; set; }
        public string type { get; set; }
        public string value { get; set; }
    }

    public class price_amount_breakdown
    {
        public List<parts> parts { get; set; }
    }

    public class product_medias
    {
        public string media_url { get; set; }
        public string mime_type { get; set; }
        public string type { get; set; }
    }

    public class promotions
    {
        public bool apportioned { get; set; }
        public configuration configuration { get; set; }
        public string deduced_amount { get; set; }
        public string id { get; set; }
        public Int32 offered_quantity { get; set; }
    }

    public class configuration
    {
        public string amount_off { get; set; }
        public Int32 free_items_quantity { get; set; }
        public string internal_description { get; set; }
        public string percentage_off { get; set; }
        public string type { get; set; }
    }

    public class refunds
    {
        public string amount { get; set; }
        public amount_breakdown amount_breakdown { get; set; }
        public string commission_amount { get; set; }
        public string commission_tax_amount { get; set; }
        public List<commission_taxes> commission_taxes { get; set; }
        public string commission_total_amount { get; set; }
        public string created_date { get; set; }
        public string id { get; set; }
        public Int32 quantity { get; set; }
        public string reason_code { get; set; }
        public string shipping_amount { get; set; }
        public shipping_amount_breakdown shipping_amount_breakdown { get; set; }
        public List<shipping_taxes> shipping_taxes { get; set; }
        public string state { get; set; }
        public List<taxes> taxes { get; set; }
        public string transaction_date { get; set; }
        public string transaction_number { get; set; }
    }

    public class commission_taxes
    {
        public string amount { get; set; }
        public string code { get; set; }
    }

    public class shipping_amount_breakdown
    {
        public List<parts> parts { get; set; }
    }

    public class shipping_price_amount_breakdown
    {
        public List<parts> parts { get; set; }
    }

    public class shipping_taxes
    {
        public string amount { get; set; }
        public amount_breakdown amount_breakdown { get; set; }
        public string code { get; set; }
        public string rate { get; set; }
        public string tax_calculation_rule { get; set; }
    }

    public class taxes
    {
        public string amount { get; set; }
        public amount_breakdown amount_breakdown { get; set; }
        public string code { get; set; }
        public string rate { get; set; }
        public string tax_calculation_rule { get; set; }
    }

    public class amount_breakdown
    {
        public List<parts> parts { get; set; }
    }

    public class parts
    {
        public string amount { get; set; }
        public bool commissionable { get; set; }
        public bool debitable_from_customer { get; set; }
        public bool payable_to_shop { get; set; }
    }

    

    //Post Tracking Number Class

    public class PostTrackingNumberRequest
    {
        public List<shipment> shipments { get; set; }
    }

    public class shipment
    {
        public string order_id { get; set; }
        public List<shipment_line> shipment_lines { get; set; }
        public tracking tracking { get; set; }
        public bool shipped { get; set; }

    }

    public class shipment_line
    {
        public string offer_sku { get; set; }
        //public string order_line_id { get; set; }
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
        public PostTrackingNumberResponseData Response { get; set; }
    }

    public class PostTrackingNumberResponseData
    {
        public List<shipment_error> shipment_errors { get; set; }
        public List<shipment_success> Response { get; set; }
    }


    public class shipment_error
    {
        public string message { get; set; }
        public string order_id { get; set; }
    }

    public class shipment_success
    {
        public string id { get; set; }
        public string order_id { get; set; }
        //public List<shipment_lines> shipment_lines { get; set; }
        public string status { get; set; }
        public tracking tracking { get; set; }
    }


    //Post Delivery Status
    
    public class PostDeliveryRequest
    {
        public List<shipment> shipments { get; set; }

    }


    public class PostDeliveryResponse
    {
        public ResponseHeader Header { get; set; }
        public List<PostTrackingNumberResponseData> Response { get; set; }
    }
}