using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OxatisAPI.Models
{
 
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Order
    {

        private string oxIDField;

        private System.DateTime dateField;

        private string billingFirstNameField;

        private string billingLastNameField;

        private decimal subTotalVATField;

        private decimal shippingPriceTaxExclField;

        private decimal netAmountDueField;

        private decimal shippingVATAmountField;

        private string paymentStatusCodeField;

        private string ProgressStateIDField;

        private bool followUpField;

        private string sourceTypeIDField;

        private byte orderFlagsField;

        private decimal paymentFeesTaxExcField;

        private decimal paymentFeesVATAmountField;

        private byte loyaltyPointsUsedField;

        private string userEmailField;

        private string userOxIDField;

        private string billingTitleField;

        private string billingCompanyField;

        private string billingAddressField;

        private string billingAddressL1Field;

        private string billingAddressFloorField;

        private string billingAddressBuildingField;

        private string billingAddressStreetField;

        private string billingAddressOtherInfoField;

        private string billingZipCodeField;

        private string billingCityField;

        private string billingStateField;

        private string billingCountryISOCodeField;

        private string billingCountryNameField;

        private string billingPhoneField;

        private string billingCellPhoneField;

        private string billingFaxField;

        private string companyVATNumberField;

        private string shippingCompanyField;

        private string shippingTitleField;

        private string shippingFirstNameField;

        private string shippingLastNameField;

        private string shippingAddressField;

        private string shippingAddressL1Field;

        private string shippingAddressFloorField;

        private string shippingAddressBuildingField;

        private string shippingAddressStreetField;

        private string shippingAddressOtherInfoField;

        private string shippingZipCodeField;

        private string shippingCityField;

        private string shippingStateField;

        private string shippingCountryISOCodeField;

        private string shippingCountryNameField;

        private string shippingPhoneField;

        private string shippingInfoField;

        private bool vATIncludedField;

        private bool ecoTaxIncludedField;

        private decimal subTotalNetField;

        private decimal subTotalNetDiscountedField;

        private decimal globalDiscountRateField;

        private decimal globalDiscountAmountField;

        private string ShippingIDField;

        private string ShippingMethodNameField;

        private decimal shippingTaxRateField;

        private decimal shippingPriceTaxInclField;

        private decimal ecoTaxAmountTaxInclField;

        private string languageField;

        private string paymentMethodIDField;

        private string paymentMethodNameField;

        private uint pMProcessorCodeField;

        private System.DateTime paymentStatusLastModifiedDateField;

        private string remoteIPAddrField;

        private string salesRepCodeField;

        private string specialInstructionsField;

        private string currencyCodeField;

        private string fiscalCodeField; 

        private string sourceOrderIDField;

        private DateTime InvoiceDateField;

        private string InvoiceIDField;
        private string InvoiceFileNameField;
        private string InvoiceURLField;
        private string InternalNoteField;
        private string ShippingProcessorCodeField;
        private string ShippingParam1Field;
        private string ShippingParam2Field;

        private string TrackingNumberField;
        private string TrackingUrlField;
        private string TransportNameField;

        private string SourceOrderIDField;
        private string ShippedField;
        private string CustomFieldText1Field;
        private string CustomFieldText2Field;
        private string CustomFieldText3Field;
        private string CustomFieldText4Field;
        private string CustomFieldNumeric1Field;
        private string CustomFieldNumeric2Field;
        private string CustomFieldDateField;

        private string amountTaxesVATExcField;

        private decimal amountTaxesVATIncField;

        private decimal amountTaxesVATAmountField;

        private object shippingAmountTaxListField;

        private object pMTFeesTaxFormulaField;

        private object pMTFeesAmountTaxListField;

        private object shippingUserAddressLabelField;

        private decimal paymentFeesTaxIncField;

        private decimal paymentFeesTaxRateField;

        private OrderOrderTaxDetails orderTaxDetailsField;

        private string totalWeightField;

        private OrderItem[] orderItemsField;

        /// <remarks/>
        public string OxID
        {
            get
            {
                return this.oxIDField;
            }
            set
            {
                this.oxIDField = value;
            }
        }

        /// <remarks/>
        public System.DateTime Date
        {
            get
            {
                return this.dateField;
            }
            set
            {
                this.dateField = value;
            }
        }

        /// <remarks/>
        public string BillingFirstName
        {
            get
            {
                return this.billingFirstNameField;
            }
            set
            {
                this.billingFirstNameField = value;
            }
        }

        /// <remarks/>
        public string BillingLastName
        {
            get
            {
                return this.billingLastNameField;
            }
            set
            {
                this.billingLastNameField = value;
            }
        }

        /// <remarks/>
        public decimal SubTotalVAT
        {
            get
            {
                return this.subTotalVATField;
            }
            set
            {
                this.subTotalVATField = value;
            }
        }

        /// <remarks/>
        public decimal ShippingPriceTaxExcl
        {
            get
            {
                return this.shippingPriceTaxExclField;
            }
            set
            {
                this.shippingPriceTaxExclField = value;
            }
        }

        /// <remarks/>
        public decimal NetAmountDue
        {
            get
            {
                return this.netAmountDueField;
            }
            set
            {
                this.netAmountDueField = value;
            }
        }

        /// <remarks/>
        public decimal ShippingVATAmount
        {
            get
            {
                return this.shippingVATAmountField;
            }
            set
            {
                this.shippingVATAmountField = value;
            }
        }

        /// <remarks/>
        public string PaymentStatusCode
        {
            get
            {
                return this.paymentStatusCodeField;
            }
            set
            {
                this.paymentStatusCodeField = value;
            }
        }

        public bool FollowUp
        {
            get
            {
                return this.followUpField;
            }
            set
            {
                this.followUpField = value;
            }
        }

        public string SourceTypeID
        {
            get
            {
                return this.sourceTypeIDField;
            }
            set
            {
                this.sourceTypeIDField = value;
            }
        }

        public string ProgressStateID
        {
            get
            {
                return this.ProgressStateIDField;
            }
            set
            {
                this.ProgressStateIDField = value;
            }
        }

        /// <remarks/>
        public decimal PaymentFeesTaxExc
        {
            get
            {
                return this.paymentFeesTaxExcField;
            }
            set
            {
                this.paymentFeesTaxExcField = value;
            }
        }

        /// <remarks/>
        public decimal PaymentFeesVATAmount
        {
            get
            {
                return this.paymentFeesVATAmountField;
            }
            set
            {
                this.paymentFeesVATAmountField = value;
            }
        }

        /// <remarks/>
        public byte LoyaltyPointsUsed
        {
            get
            {
                return this.loyaltyPointsUsedField;
            }
            set
            {
                this.loyaltyPointsUsedField = value;
            }
        }

        /// <remarks/>
        public string UserEmail
        {
            get
            {
                return this.userEmailField;
            }
            set
            {
                this.userEmailField = value;
            }
        }

        /// <remarks/>
        public string UserOxID
        {
            get
            {
                return this.userOxIDField;
            }
            set
            {
                this.userOxIDField = value;
            }
        }

        /// <remarks/>
        public string BillingTitle
        {
            get
            {
                return this.billingTitleField;
            }
            set
            {
                this.billingTitleField = value;
            }
        }

        /// <remarks/>
        public string BillingCompany
        {
            get
            {
                return this.billingCompanyField;
            }
            set
            {
                this.billingCompanyField = value;
            }
        }

        /// <remarks/>
        public string BillingAddress
        {
            get
            {
                return this.billingAddressField;
            }
            set
            {
                this.billingAddressField = value;
            }
        }

        /// <remarks/>
        public string BillingAddressL1
        {
            get
            {
                return this.billingAddressL1Field;
            }
            set
            {
                this.billingAddressL1Field = value;
            }
        }

        /// <remarks/>
        public string BillingAddressFloor
        {
            get
            {
                return this.billingAddressFloorField;
            }
            set
            {
                this.billingAddressFloorField = value;
            }
        }

        /// <remarks/>
        public string BillingAddressBuilding
        {
            get
            {
                return this.billingAddressBuildingField;
            }
            set
            {
                this.billingAddressBuildingField = value;
            }
        }

        /// <remarks/>
        public string BillingAddressStreet
        {
            get
            {
                return this.billingAddressStreetField;
            }
            set
            {
                this.billingAddressStreetField = value;
            }
        }

        /// <remarks/>
        public string BillingAddressOtherInfo
        {
            get
            {
                return this.billingAddressOtherInfoField;
            }
            set
            {
                this.billingAddressOtherInfoField = value;
            }
        }

        /// <remarks/>
        public string BillingZipCode
        {
            get
            {
                return this.billingZipCodeField;
            }
            set
            {
                this.billingZipCodeField = value;
            }
        }

        /// <remarks/>
        public string BillingCity
        {
            get
            {
                return this.billingCityField;
            }
            set
            {
                this.billingCityField = value;
            }
        }

        /// <remarks/>
        public string BillingState
        {
            get
            {
                return this.billingStateField;
            }
            set
            {
                this.billingStateField = value;
            }
        }

        /// <remarks/>
        public string BillingCountryISOCode
        {
            get
            {
                return this.billingCountryISOCodeField;
            }
            set
            {
                this.billingCountryISOCodeField = value;
            }
        }

        /// <remarks/>
        public string BillingCountryName
        {
            get
            {
                return this.billingCountryNameField;
            }
            set
            {
                this.billingCountryNameField = value;
            }
        }

        /// <remarks/>
        public string BillingPhone
        {
            get
            {
                return this.billingPhoneField;
            }
            set
            {
                this.billingPhoneField = value;
            }
        }

        /// <remarks/>
        public string BillingCellPhone
        {
            get
            {
                return this.billingCellPhoneField;
            }
            set
            {
                this.billingCellPhoneField = value;
            }
        }

        /// <remarks/>
        public string BillingFax
        {
            get
            {
                return this.billingFaxField;
            }
            set
            {
                this.billingFaxField = value;
            }
        }

        /// <remarks/>
        public string CompanyVATNumber
        {
            get
            {
                return this.companyVATNumberField;
            }
            set
            {
                this.companyVATNumberField = value;
            }
        }

        /// <remarks/>
        public string ShippingCompany
        {
            get
            {
                return this.shippingCompanyField;
            }
            set
            {
                this.shippingCompanyField = value;
            }
        }

        /// <remarks/>
        public string ShippingTitle
        {
            get
            {
                return this.shippingTitleField;
            }
            set
            {
                this.shippingTitleField = value;
            }
        }

        /// <remarks/>
        public string ShippingFirstName
        {
            get
            {
                return this.shippingFirstNameField;
            }
            set
            {
                this.shippingFirstNameField = value;
            }
        }

        /// <remarks/>
        public string ShippingLastName
        {
            get
            {
                return this.shippingLastNameField;
            }
            set
            {
                this.shippingLastNameField = value;
            }
        }

        /// <remarks/>
        public string ShippingAddress
        {
            get
            {
                return this.shippingAddressField;
            }
            set
            {
                this.shippingAddressField = value;
            }
        }

        /// <remarks/>
        public string ShippingAddressL1
        {
            get
            {
                return this.shippingAddressL1Field;
            }
            set
            {
                this.shippingAddressL1Field = value;
            }
        }

        /// <remarks/>
        public string ShippingAddressFloor
        {
            get
            {
                return this.shippingAddressFloorField;
            }
            set
            {
                this.shippingAddressFloorField = value;
            }
        }

        /// <remarks/>
        public string ShippingAddressBuilding
        {
            get
            {
                return this.shippingAddressBuildingField;
            }
            set
            {
                this.shippingAddressBuildingField = value;
            }
        }

        /// <remarks/>
        public string ShippingAddressStreet
        {
            get
            {
                return this.shippingAddressStreetField;
            }
            set
            {
                this.shippingAddressStreetField = value;
            }
        }

        /// <remarks/>
        public string ShippingAddressOtherInfo
        {
            get
            {
                return this.shippingAddressOtherInfoField;
            }
            set
            {
                this.shippingAddressOtherInfoField = value;
            }
        }

        /// <remarks/>
        public string ShippingZipCode
        {
            get
            {
                return this.shippingZipCodeField;
            }
            set
            {
                this.shippingZipCodeField = value;
            }
        }

        /// <remarks/>
        public string ShippingCity
        {
            get
            {
                return this.shippingCityField;
            }
            set
            {
                this.shippingCityField = value;
            }
        }

        /// <remarks/>
        public string ShippingState
        {
            get
            {
                return this.shippingStateField;
            }
            set
            {
                this.shippingStateField = value;
            }
        }

        /// <remarks/>
        public string ShippingCountryISOCode
        {
            get
            {
                return this.shippingCountryISOCodeField;
            }
            set
            {
                this.shippingCountryISOCodeField = value;
            }
        }

        /// <remarks/>
        public string ShippingCountryName
        {
            get
            {
                return this.shippingCountryNameField;
            }
            set
            {
                this.shippingCountryNameField = value;
            }
        }

        /// <remarks/>
        public string ShippingPhone
        {
            get
            {
                return this.shippingPhoneField;
            }
            set
            {
                this.shippingPhoneField = value;
            }
        }

        /// <remarks/>
        public string ShippingInfo
        {
            get
            {
                return this.shippingInfoField;
            }
            set
            {
                this.shippingInfoField = value;
            }
        }

        /// <remarks/>
        public bool VATIncluded
        {
            get
            {
                return this.vATIncludedField;
            }
            set
            {
                this.vATIncludedField = value;
            }
        }

        /// <remarks/>
        public bool EcoTaxIncluded
        {
            get
            {
                return this.ecoTaxIncludedField;
            }
            set
            {
                this.ecoTaxIncludedField = value;
            }
        }

        /// <remarks/>
        public decimal SubTotalNet
        {
            get
            {
                return this.subTotalNetField;
            }
            set
            {
                this.subTotalNetField = value;
            }
        }

        /// <remarks/>
        public decimal SubTotalNetDiscounted
        {
            get
            {
                return this.subTotalNetDiscountedField;
            }
            set
            {
                this.subTotalNetDiscountedField = value;
            }
        }

        /// <remarks/>
        public decimal GlobalDiscountRate
        {
            get
            {
                return this.globalDiscountRateField;
            }
            set
            {
                this.globalDiscountRateField = value;
            }
        }

        /// <remarks/>
        public decimal GlobalDiscountAmount
        {
            get
            {
                return this.globalDiscountAmountField;
            }
            set
            {
                this.globalDiscountAmountField = value;
            }
        }

        public string ShippingID
        {
            get
            {
                return this.ShippingIDField;
            }
            set
            {
                this.ShippingIDField = value;
            }
        }


        public string ShippingMethodName
        {
            get
            {
                return this.ShippingMethodNameField;
            }
            set
            {
                this.ShippingMethodNameField = value;
            }
        }

        /// <remarks/>
        public decimal ShippingTaxRate
        {
            get
            {
                return this.shippingTaxRateField;
            }
            set
            {
                this.shippingTaxRateField = value;
            }
        }

        /// <remarks/>
        public decimal ShippingPriceTaxIncl
        {
            get
            {
                return this.shippingPriceTaxInclField;
            }
            set
            {
                this.shippingPriceTaxInclField = value;
            }
        }

        /// <remarks/>
        public decimal EcoTaxAmountTaxIncl
        {
            get
            {
                return this.ecoTaxAmountTaxInclField;
            }
            set
            {
                this.ecoTaxAmountTaxInclField = value;
            }
        }

        /// <remarks/>
        public string Language
        {
            get
            {
                return this.languageField;
            }
            set
            {
                this.languageField = value;
            }
        }

        /// <remarks/>
        public string PaymentMethodID
        {
            get
            {
                return this.paymentMethodIDField;
            }
            set
            {
                this.paymentMethodIDField = value;
            }
        }

        /// <remarks/>
        public string PaymentMethodName
        {
            get
            {
                return this.paymentMethodNameField;
            }
            set
            {
                this.paymentMethodNameField = value;
            }
        }

        /// <remarks/>
        public uint PMProcessorCode
        {
            get
            {
                return this.pMProcessorCodeField;
            }
            set
            {
                this.pMProcessorCodeField = value;
            }
        }

        /// <remarks/>
        public System.DateTime PaymentStatusLastModifiedDate
        {
            get
            {
                return this.paymentStatusLastModifiedDateField;
            }
            set
            {
                this.paymentStatusLastModifiedDateField = value;
            }
        }

        /// <remarks/>
        public string RemoteIPAddr
        {
            get
            {
                return this.remoteIPAddrField;
            }
            set
            {
                this.remoteIPAddrField = value;
            }
        }

        /// <remarks/>
        public string SalesRepCode
        {
            get
            {
                return this.salesRepCodeField;
            }
            set
            {
                this.salesRepCodeField = value;
            }
        }

        /// <remarks/>
        public string SpecialInstructions
        {
            get
            {
                return this.specialInstructionsField;
            }
            set
            {
                this.specialInstructionsField = value;
            }
        }

        /// <remarks/>
        public string CurrencyCode
        {
            get
            {
                return this.currencyCodeField;
            }
            set
            {
                this.currencyCodeField = value;
            }
        }

        public DateTime InvoiceDate
        {
            get
            {
                return this.InvoiceDateField;
            }
            set
            {
                this.InvoiceDateField = value;
            }
        }


        public string InvoiceID
        {
            get
            {
                return this.InvoiceIDField;
            }
            set
            {
                this.InvoiceIDField = value;
            }
        }

        public string InvoiceFileName
        {
            get
            {
                return this.InvoiceFileNameField;
            }
            set
            {
                this.InvoiceFileNameField = value;
            }
        }


        public string InvoiceURL
        {
            get
            {
                return this.InvoiceURLField;
            }
            set
            {
                this.InvoiceURLField = value;
            }
        }


        public string InternalNote
        {
            get
            {
                return this.InternalNoteField;
            }
            set
            {
                this.InternalNoteField = value;
            }
        }

        public string ShippingProcessorCode
        {
            get
            {
                return this.ShippingProcessorCodeField;
            }
            set
            {
                this.ShippingProcessorCodeField = value;
            }
        }

        public string ShippingParam1
        {
            get
            {
                return this.ShippingParam1Field;
            }
            set
            {
                this.ShippingParam1Field = value;
            }
        }

        public string ShippingParam2
        {
            get
            {
                return this.ShippingParam2Field;
            }
            set
            {
                this.ShippingParam2Field = value;
            }
        }

        /// <remarks/>
        public string FiscalCode
        {
            get
            {
                return this.fiscalCodeField;
            }
            set
            {
                this.fiscalCodeField = value;
            }
        }


        public string TrackingNumber
        {
            get
            {
                return this.TrackingNumberField;
            }
            set
            {
                this.TrackingNumberField = value;
            }
        }

        public string TrackingUrl
        {
            get
            {
                return this.TrackingUrlField;
            }
            set
            {
                this.TrackingUrlField = value;
            }
        }

        public string TransportName
        {
            get
            {
                return this.TransportNameField;
            }
            set
            {
                this.TransportNameField = value;
            }
        }


        public string SourceOrderID
        {
            get
            {
                return this.SourceOrderIDField;
            }
            set
            {
                this.SourceOrderIDField = value;
            }
        }

        public string Shipped
        {
            get
            {
                return this.ShippedField;
            }
            set
            {
                this.ShippedField = value;
            }
        }

        public string CustomFieldText1
        {
            get
            {
                return this.CustomFieldText1Field;
            }
            set
            {
                this.CustomFieldText1Field = value;
            }
        }

        public string CustomFieldText2
        {
            get
            {
                return this.CustomFieldText2Field;
            }
            set
            {
                this.CustomFieldText2Field = value;
            }
        }

        public string CustomFieldText3
        {
            get
            {
                return this.CustomFieldText3Field;
            }
            set
            {
                this.CustomFieldText3Field = value;
            }
        }

        public string CustomFieldText4
        {
            get
            {
                return this.CustomFieldText4Field;
            }
            set
            {
                this.CustomFieldText4Field = value;
            }
        }

        public string CustomFieldNumeric1
        {
            get
            {
                return this.CustomFieldNumeric1Field;
            }
            set
            {
                this.CustomFieldNumeric1Field = value;
            }
        }

        public string CustomFieldNumeric2
        {
            get
            {
                return this.CustomFieldNumeric2Field;
            }
            set
            {
                this.CustomFieldNumeric2Field = value;
            }
        }

        public string CustomFieldDate
        {
            get
            {
                return this.CustomFieldDateField;
            }
            set
            {
                this.CustomFieldDateField = value;
            }
        }


        /// <remarks/>
        public string AmountTaxesVATExc
        {
            get
            {
                return this.amountTaxesVATExcField;
            }
            set
            {
                this.amountTaxesVATExcField = value;
            }
        }

        /// <remarks/>
        public decimal AmountTaxesVATInc
        {
            get
            {
                return this.amountTaxesVATIncField;
            }
            set
            {
                this.amountTaxesVATIncField = value;
            }
        }

        /// <remarks/>
        public decimal AmountTaxesVATAmount
        {
            get
            {
                return this.amountTaxesVATAmountField;
            }
            set
            {
                this.amountTaxesVATAmountField = value;
            }
        }

        /// <remarks/>
        public object ShippingAmountTaxList
        {
            get
            {
                return this.shippingAmountTaxListField;
            }
            set
            {
                this.shippingAmountTaxListField = value;
            }
        }

        /// <remarks/>
        public object PMTFeesTaxFormula
        {
            get
            {
                return this.pMTFeesTaxFormulaField;
            }
            set
            {
                this.pMTFeesTaxFormulaField = value;
            }
        }

        /// <remarks/>
        public object PMTFeesAmountTaxList
        {
            get
            {
                return this.pMTFeesAmountTaxListField;
            }
            set
            {
                this.pMTFeesAmountTaxListField = value;
            }
        }

        /// <remarks/>
        public object ShippingUserAddressLabel
        {
            get
            {
                return this.shippingUserAddressLabelField;
            }
            set
            {
                this.shippingUserAddressLabelField = value;
            }
        }

        /// <remarks/>
        public decimal PaymentFeesTaxInc
        {
            get
            {
                return this.paymentFeesTaxIncField;
            }
            set
            {
                this.paymentFeesTaxIncField = value;
            }
        }

        /// <remarks/>
        public decimal PaymentFeesTaxRate
        {
            get
            {
                return this.paymentFeesTaxRateField;
            }
            set
            {
                this.paymentFeesTaxRateField = value;
            }
        }

        /// <remarks/>
        public OrderOrderTaxDetails OrderTaxDetails
        {
            get
            {
                return this.orderTaxDetailsField;
            }
            set
            {
                this.orderTaxDetailsField = value;
            }
        }

        /// <remarks/>
        public string TotalWeight
        {
            get
            {
                return this.totalWeightField;
            }
            set
            {
                this.totalWeightField = value;
            }
        }

       
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Item", IsNullable = false)]
        public OrderItem[] OrderItems
        {
            get
            {
                return this.orderItemsField;
            }
            set
            {
                this.orderItemsField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OrderOrderTaxDetails
    {

        private OrderOrderTaxDetailsItem itemField;

        /// <remarks/>
        public OrderOrderTaxDetailsItem Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OrderOrderTaxDetailsItem
    {

        private decimal totalNetTaxExclField;

        private decimal taxRateField;

        private decimal totalNetVATAmountField;

        /// <remarks/>
        public decimal TotalNetTaxExcl
        {
            get
            {
                return this.totalNetTaxExclField;
            }
            set
            {
                this.totalNetTaxExclField = value;
            }
        }

        /// <remarks/>
        public decimal TaxRate
        {
            get
            {
                return this.taxRateField;
            }
            set
            {
                this.taxRateField = value;
            }
        }

        /// <remarks/>
        public decimal TotalNetVATAmount
        {
            get
            {
                return this.totalNetVATAmountField;
            }
            set
            {
                this.totalNetVATAmountField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OrderItem
    {

        private string oxIDField;

        private string itemOxIDField;

        private string itemSKUField;

        private string itemSKUOriginalField;

        private string itemNameField;

        private decimal grossPriceField;

        private decimal grossAmountField;

        private decimal taxRateField;

        private decimal discountRateField;

        private decimal netPriceField;

        private decimal netAmountField;

        private decimal vATAmountField;

        private byte ecoTaxValueTaxInclField;

        private string Option1NameField;

        private string Option1ValueField;

        private string taxAmountsField;

        private uint quantityField;

        private object bundledItemsField;

        /// <remarks/>
        public string OxID
        {
            get
            {
                return this.oxIDField;
            }
            set
            {
                this.oxIDField = value;
            }
        }

        /// <remarks/>
        public string ItemOxID
        {
            get
            {
                return this.itemOxIDField;
            }
            set
            {
                this.itemOxIDField = value;
            }
        }

        /// <remarks/>
        public string ItemSKU
        {
            get
            {
                return this.itemSKUField;
            }
            set
            {
                this.itemSKUField = value;
            }
        }

        /// <remarks/>
        public string ItemSKUOriginal
        {
            get
            {
                return this.itemSKUOriginalField;
            }
            set
            {
                this.itemSKUOriginalField = value;
            }
        }

        /// <remarks/>
        public string ItemName
        {
            get
            {
                return this.itemNameField;
            }
            set
            {
                this.itemNameField = value;
            }
        }

        /// <remarks/>
        public decimal GrossPrice
        {
            get
            {
                return this.grossPriceField;
            }
            set
            {
                this.grossPriceField = value;
            }
        }

        /// <remarks/>
        public decimal GrossAmount
        {
            get
            {
                return this.grossAmountField;
            }
            set
            {
                this.grossAmountField = value;
            }
        }

        /// <remarks/>
        public decimal TaxRate
        {
            get
            {
                return this.taxRateField;
            }
            set
            {
                this.taxRateField = value;
            }
        }

        /// <remarks/>
        public decimal DiscountRate
        {
            get
            {
                return this.discountRateField;
            }
            set
            {
                this.discountRateField = value;
            }
        }

        /// <remarks/>
        public decimal NetPrice
        {
            get
            {
                return this.netPriceField;
            }
            set
            {
                this.netPriceField = value;
            }
        }

        /// <remarks/>
        public decimal NetAmount
        {
            get
            {
                return this.netAmountField;
            }
            set
            {
                this.netAmountField = value;
            }
        }

        /// <remarks/>
        public decimal VATAmount
        {
            get
            {
                return this.vATAmountField;
            }
            set
            {
                this.vATAmountField = value;
            }
        }

        /// <remarks/>
        public byte EcoTaxValueTaxIncl
        {
            get
            {
                return this.ecoTaxValueTaxInclField;
            }
            set
            {
                this.ecoTaxValueTaxInclField = value;
            }
        }

        public string Option1Name
        {
            get
            {
                return this.Option1NameField;
            }
            set
            {
                this.Option1NameField = value;
            }
        }

        public string Option1Value
        {
            get
            {
                return this.Option1ValueField;
            }
            set
            {
                this.Option1ValueField = value;
            }
        }

        /// <remarks/>
        public string TaxAmounts
        {
            get
            {
                return this.taxAmountsField;
            }
            set
            {
                this.taxAmountsField = value;
            }
        }

        /// <remarks/>
        public uint Quantity
        {
            get
            {
                return this.quantityField;
            }
            set
            {
                this.quantityField = value;
            }
        }

        /// <remarks/>
        public object BundledItems
        {
            get
            {
                return this.bundledItemsField;
            }
            set
            {
                this.bundledItemsField = value;
            }
        }
    }




}