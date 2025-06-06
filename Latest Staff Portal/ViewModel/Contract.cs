using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Latest_Staff_Portal.ViewModel
{


    public class ContractsList
    {
       
        public string Document_Type { get; set; }
        public string No { get; set; }
        public string Buy_from_Vendor_No { get; set; }
        public string Buy_from_Vendor_Name { get; set; }
        public string VAT_Registration_No { get; set; }
        public string Buy_from_Address { get; set; }
        public string Buy_from_Address_2 { get; set; }
        public string Buy_from_City { get; set; }
        public string Buy_from_Contact { get; set; }
        public string Buy_from_Country_Region_Code { get; set; }
        public string Language_Code { get; set; }
        public string Contract_Description { get; set; }
        public string Contract_Start_Date { get; set; }
        public string Contract_End_Date { get; set; }
        public string Notice_of_Award_No { get; set; }
        public string Awarded_Bid_No { get; set; }
        public int Award_Tender_Sum_Inc_Taxes { get; set; }
        public string IFS_Code { get; set; }
        public string Tender_Name { get; set; }
        public string Contract_Type { get; set; }
        public string Governing_Laws { get; set; }
        public string Contract_Status { get; set; }
        public string Procuring_Entity_PE_Name { get; set; }
        public string PE_Representative { get; set; }
        public string Your_Reference { get; set; }
    }


    public class Contracts
    {

        public string Document_Type { get; set; }
        public string No { get; set; }
        public string Document_Date { get; set; }
        public string Contract_Description { get; set; }
        public string Contract_Start_Date { get; set; }
        public string Contract_Duration { get; set; }
        public string Contract_End_Date { get; set; }
        public string Institution { get; set; }
        public string Party_Description { get; set; }
        public string Vendor_Party { get; set; }
        public string Company_Head { get; set; }
        public string Mou_Description { get; set; }
        public string Scope_of_Service { get; set; }
        public string Scope_Description { get; set; }
        public string Vendor_Description { get; set; }
        public string Notice_of_Award_No { get; set; }
        public string Awarded_Bid_No { get; set; }
        public int Award_Tender_Sum_Inc_Taxes { get; set; }
        public string IFS_Code { get; set; }
        public string Tender_Name { get; set; }
        public string Serial_No { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public int Area_Space { get; set; }
        public string Payment_Status { get; set; }
        public string Job { get; set; }
        public string Contract_Type { get; set; }
        public string Governing_Laws { get; set; }
        public string Contract_Status { get; set; }
        public string Status { get; set; }
        public string Procuring_Entity_PE_Name { get; set; }
        public string PE_Representative { get; set; }
        public int Amount { get; set; }
        public string Due_Date { get; set; }
        public string Your_Reference { get; set; }
        public string Reason_For_ammendment { get; set; }
        public bool Renewed { get; set; }
        public string Renewed_Date { get; set; }
        public string Shortcut_Dimension_1_Code { get; set; }
        public string Shortcut_Dimension_2_Code { get; set; }
        public string Shortcut_Dimension_3_Code { get; set; }
        public string Preliminary_Evaluation_Date { get; set; }
        public string Buy_from_Vendor_No { get; set; }
        public string Buy_from_Vendor_Name { get; set; }
        public string VAT_Registration_No { get; set; }
        public string Buy_from_Address { get; set; }
        public string Buy_from_Address_2 { get; set; }
        public string Buy_from_Post_Code { get; set; }
        public string Buy_from_City { get; set; }
        public string Buy_from_Contact_No { get; set; }
        public string Buy_from_Country_Region_Code { get; set; }
        public string Language_Code { get; set; }
    }

    public class ContractLines
    {
        public string Document_No { get; set; }
        public int Line_No { get; set; }
        public string Type { get; set; }
        public string No { get; set; }
        public string Cross_Reference_No { get; set; }
        public string Variant_Code { get; set; }
        public string VAT_Prod_Posting_Group { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string Unit_of_Measure_Code { get; set; }
        public string Unit_of_Measure { get; set; }
        public string Fee_Type { get; set; }
        public int Direct_Unit_Cost { get; set; }
        public int Indirect_Cost_Percent { get; set; }
        public int Unit_Cost_LCY { get; set; }
        public string Budget { get; set; }
        public string Budget_Line { get; set; }
        public string Shortcut_Dimension_1_Code { get; set; }
        public int Unit_Price_LCY { get; set; }
        public int Line_Discount_Percent { get; set; }
        public int Line_Amount { get; set; }
        public int Line_Discount_Amount { get; set; }
        public string Location_Code { get; set; }
        public bool Allow_Invoice_Disc { get; set; }
        public int Qty_to_Receive { get; set; }
        public int Quantity_Received { get; set; }
        public int Quantity_Invoiced { get; set; }
        public string Expected_Receipt_Date { get; set; }
        public string Shortcut_Dimension_2_Code { get; set; }
        public string ShortcutDimCode_x005B_3_x005D_ { get; set; }
        public string ShortcutDimCode_x005B_4_x005D_ { get; set; }
        public string ShortcutDimCode_x005B_5_x005D_ { get; set; }
        public string ShortcutDimCode_x005B_6_x005D_ { get; set; }
        public string ShortcutDimCode_x005B_7_x005D_ { get; set; }
        public string ShortcutDimCode_x005B_8_x005D_ { get; set; }
        public double AmountBeforeDiscount { get; set; }
        public int Invoice_Discount_Amount { get; set; }
        public int Invoice_Disc_Pct { get; set; }
        public double Total_Amount_Excl_VAT { get; set; }
        public double Total_VAT_Amount { get; set; }
        public int Total_Amount_Incl_VAT { get; set; }

        public List<SelectListItem> ListOfDim1 { get; set; }
        public List<SelectListItem> ListOfDim2 { get; set; }
       
    }

    public class ContractPaymentLines
    {
        public string ETag { get; set; }
        public string DocumentType { get; set; }
        public string No { get; set; }
        public string InstalmentCode { get; set; }
        public string PaymentCertificateType { get; set; }
        public string Description { get; set; }
        public decimal PaymentPercent { get; set; }
        public string CurrencyCode { get; set; }
        public decimal PlannedAmount { get; set; }
        public decimal PlannedAmountLCY { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal PaidAmountLCY { get; set; }
    }

    public class PaymentLine
    {
        public string OdataEtag { get; set; }
        public string DocumentType { get; set; }
        public string No { get; set; }
        public string InstalmentCode { get; set; }
        public string PaymentCertificateType { get; set; }
        public string Description { get; set; }
        public decimal PaymentPercent { get; set; }
        public string CurrencyCode { get; set; }
        public decimal PlannedAmount { get; set; }
        public decimal PlannedAmountLcy { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal PaidAmountLcy { get; set; }

        public List<SelectListItem> ListOfCurrencyCodes { get; set; }

        public List<SelectListItem> PaymentCertificateType4 { get; set; }

        public string SelectedCertificateType { get; set; }
    }
}