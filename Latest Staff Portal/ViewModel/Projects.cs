using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Latest_Staff_Portal.ViewModel
{
    public class ProjectProposal
    {
        public string No { get; set; }
        public string Name { get; set; }
        public string GlobalDimension2Code { get; set; }
        public List<SelectListItem> ListOfDim2 { get; set; }
        public string AdministrationUnitName { get; set; }
        public string RequestDescription { get; set; }
        public string Justification { get; set; }
        public string CreatedBy { get; set; }
        public string EmployeeNo { get; set; }
        public string EmployeeName { get; set; }
        public string Status { get; set; }
        public string DeferalComments { get; set; }
    }

    public class DesignControlHeader
    {
        public string ProjectNo { get; set; }
        public string Name { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string AdministrativeUnit { get; set; }
        public string GeographicLocation { get; set; }
        public string TotalEstimatedCost { get; set; }
        public string ProjectClassification { get; set; }
        public string CreatedBy { get; set; }
        public string EmployeeNo { get; set; }
        public string EmployeeName { get; set; }
        public string Status { get; set; }
        public string CurrentActioningMember { get; set; }
        public string DesignApprovalStage { get; set; }
    }

    public class DesignControlTasks
    {
        public string HeaderNo { get; set; }
        public string EntryNo { get; set; }
        public string AttachmentCode { get; set; }
        public string DesignControlNo { get; set; }
        public string DesignControlType { get; set; }
        public string Name { get; set; }
        public string NoOfItems { get; set; }
    }



    public class ActiveProjects
    {
        public string No { get; set; }
        public string Description { get; set; }
        public string Project_Code { get; set; }
        public string Research_Center { get; set; }
        public string County_ID { get; set; }
        public string SubCounty { get; set; }
        public int Project_Sum { get; set; }
        public int first_Moeity_Amount { get; set; }
        public int second_Moeity_Amount { get; set; }
        public string Contract_Agreement_Date { get; set; }
        public string Intended_Completion_Date { get; set; }
        public string Date_of_Taking_Over { get; set; }
        public string Global_Dimension_2_Code { get; set; }
        public string Sell_to_Customer_No { get; set; }
        public string Sell_to_Customer_Name { get; set; }
        public string Sell_to_Address { get; set; }
        public string Sell_to_Address_2 { get; set; }
        public string Sell_to_City { get; set; }
        public string Sell_to_County { get; set; }
        public string Sell_to_Post_Code { get; set; }
        public string Sell_to_Country_Region_Code { get; set; }
        public string Sell_to_Contact_No { get; set; }
        public string SellToPhoneNo { get; set; }
        public string SellToMobilePhoneNo { get; set; }
        public string SellToEmail { get; set; }
        public string Sell_to_Contact { get; set; }
        public string Search_Description { get; set; }
        public string External_Document_No { get; set; }
        public string Your_Reference { get; set; }
        public string Person_Responsible { get; set; }
        public string Blocked { get; set; }
        public string Last_Date_Modified { get; set; }
        public string Opportunity_Reference { get; set; }
        public string Fund_Opportunity_Name { get; set; }
        public int Grant_Amount { get; set; }
        public bool exchequer { get; set; }
        public string Tender_No { get; set; }
        public string Contractor_No { get; set; }
        public string Contractor_Name { get; set; }
        public float Contract_Amount { get; set; }
        public string Project_Manager_Name { get; set; }
        public int Projected_Monthly_Cashflow { get; set; }
        public int Actual_Monthly_Cashflow { get; set; }
        public int Cashflow_Variance { get; set; }
        public int Audited_Acc_Current_Ratios { get; set; }
        public int Valuation_No { get; set; }
        public int Valuation_Amount { get; set; }
        public int Physical_Progress { get; set; }
        public string Revised_Finish_Date { get; set; }
        public string Contract_Duration { get; set; }
        public bool _x0033__Months_Cont_Expiry_Notice { get; set; }
        public string Perfomace_Bond_Expiry { get; set; }
        public bool _x0033__Months_PB_Expiry_Notice { get; set; }
        public string Insurance_Expiry { get; set; }
        public string Programm_of_Work { get; set; }
        public string Date_of_Site_Meeting { get; set; }
        public string Date_of_Last_Site_Minutes { get; set; }
        public string Date_of_Last_MC_Report { get; set; }
        public int Expected_Monthly_Progress { get; set; }
        public int Actual_Monthly_Progress { get; set; }
        public int Progress_Variance { get; set; }
        public string Date_of_Last_Defects_Report { get; set; }
        public int Cum_No_of_Defects_Reports { get; set; }
        public int Cum_No_of_Req_for_Amendment { get; set; }
        public string Status { get; set; }
        public string Job_Posting_Group { get; set; }
        public string WIP_Method { get; set; }
        public string WIP_Posting_Method { get; set; }
        public bool Allow_Schedule_Contract_Lines { get; set; }
        public bool Apply_Usage_Link { get; set; }
        public int Percent_Completed { get; set; }
        public int Percent_Invoiced { get; set; }
        public int Percent_of_Overdue_Planning_Lines { get; set; }
        public string BillToOptions { get; set; }
        public string Bill_to_Customer_No { get; set; }
        public string Bill_to_Name { get; set; }
        public string Funding_Source { get; set; }
        public string Project_Category { get; set; }
        public string Project_Funding_Contract_No { get; set; }
        public string External_Contract_Reference { get; set; }
        public string Bill_to_Address { get; set; }
        public string Bill_to_Address_2 { get; set; }
        public string Bill_to_City { get; set; }
        public string Bill_to_County { get; set; }
        public string Bill_to_Post_Code { get; set; }
        public string Bill_to_Country_Region_Code { get; set; }
        public string Bill_to_Contact_No { get; set; }
        public string ContactPhoneNo { get; set; }
        public string ContactMobilePhoneNo { get; set; }
        public string ContactEmail { get; set; }
        public string Bill_to_Contact { get; set; }
        public string Project_Manager { get; set; }
        public DateTime Datetime_Sent_to_Inititation { get; set; }
        public string Initiation_Comments { get; set; }
        public DateTime Datetime_Sent_to_Planning { get; set; }
        public string Planning_Comments { get; set; }
        public DateTime Datetime_Sent_to_Monitoring { get; set; }
        public string Monitoring_Comments { get; set; }
        public string Payment_Terms_Code { get; set; }
        public string Payment_Method_Code { get; set; }
        public string ShippingOptions { get; set; }
        public string Ship_to_Code { get; set; }
        public string Ship_to_Name { get; set; }
        public string Ship_to_Address { get; set; }
        public string Ship_to_Address_2 { get; set; }
        public string Ship_to_City { get; set; }
        public string Ship_to_County { get; set; }
        public string Ship_to_Post_Code { get; set; }
        public string Ship_to_Country_Region_Code { get; set; }
        public string Ship_to_Contact { get; set; }
        public string Starting_Date { get; set; }
        public string Ending_Date { get; set; }
        public string Creation_Date { get; set; }
        public string Currency_Code { get; set; }
        public string Invoice_Currency_Code { get; set; }
        public string Price_Calculation_Method { get; set; }
        public string Cost_Calculation_Method { get; set; }
        public string Exch_Calculation_Cost { get; set; }
        public string Exch_Calculation_Price { get; set; }
        public string WIP_Posting_Date { get; set; }
        public int Total_WIP_Sales_Amount { get; set; }
        public int Applied_Sales_G_L_Amount { get; set; }
        public int Total_WIP_Cost_Amount { get; set; }
        public int Applied_Costs_G_L_Amount { get; set; }
        public int Recog_Sales_Amount { get; set; }
        public int Recog_Costs_Amount { get; set; }
        public int Recog_Profit_Amount { get; set; }
        public int Recog_Profit_Percent { get; set; }
        public int Acc_WIP_Costs_Amount { get; set; }
        public int Acc_WIP_Sales_Amount { get; set; }
        public int Calc_Recog_Sales_Amount { get; set; }
        public int Calc_Recog_Costs_Amount { get; set; }
        public string WIP_G_L_Posting_Date { get; set; }
        public int Total_WIP_Sales_G_L_Amount { get; set; }
        public int Total_WIP_Cost_G_L_Amount { get; set; }
        public int Recog_Sales_G_L_Amount { get; set; }
        public int Recog_Costs_G_L_Amount { get; set; }
        public int Recog_Profit_G_L_Amount { get; set; }
        public int Recog_Profit_G_L_Percent { get; set; }
        public int Calc_Recog_Sales_G_L_Amount { get; set; }
        public int Calc_Recog_Costs_G_L_Amount { get; set; }
    }



    public class ProjectBQLines
    {

        public string Project_Code { get; set; }
        public int Entry_No { get; set; }
        public string Line_Type { get; set; }
        public string Section { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string UOM { get; set; }
        public decimal Unit_Price { get; set; }
        public decimal Line_Amount { get; set; }
        public int Total_valued_Qty { get; set; }
        public decimal Total_valued_Amount { get; set; }
    }

}