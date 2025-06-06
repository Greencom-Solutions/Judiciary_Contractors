using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Web.WebPages.Html;
//using iTextSharp.text;
using Newtonsoft.Json;
using SelectListItem = System.Web.WebPages.Html.SelectListItem;

namespace Latest_Staff_Portal.ViewModel
{


    public class ContractorExtensionRequests
    {
        public string No { get; set; }
        public string Project_No { get; set; }
        public string Project_Name { get; set; }
        public string Contractor_No { get; set; }
        public string Contractor_Name { get; set; }
        public string Document_Type { get; set; }
        public DateTime Date_of_Submittion { get; set; }
        public DateTime Date_of_Receipt { get; set; }
        public string Received_By { get; set; }
        public string Status { get; set; }
        public int Project_Sum { get; set; }
        public int Retention_Amount { get; set; }
        public string Project_Manager { get; set; }
        public string Project_Manager_Name { get; set; }
        public string Team_Approval_Status { get; set; }
        public bool Invoiced { get; set; }
        public string Memo_Comments { get; set; }
        public int Approved_Amount { get; set; }
        public int Approved_Variation_Addition { get; set; }
        public int Apprroved_Variation_Omition { get; set; }
        public int Actual_Work_Progress { get; set; }
        public int Approved_Certificates { get; set; }
        public int Certificate_No { get; set; }
        public string Contract_No { get; set; }
        public int Final_Contract_Value { get; set; }
        public int Previously_Paid_Amount { get; set; }
        public string Revised_Completion_Date { get; set; }
        public string Valuation_Date { get; set; }
        public int Total_Net_Variation { get; set; }
        public string Current_Approving_Member { get; set; }
        public string Approving_Member_Name { get; set; }
        public string Asssigning_Employee { get; set; }
        public string Assigning_Emplyee_Name { get; set; }
        public string Deadline { get; set; }
        public string Team_Lead_General_Comments { get; set; }
        public string Team_Lead_Rejection_Comments { get; set; }
        public string Key_Comments { get; set; }
        public string Selected_SCM_Employee { get; set; }
        public string Selected_Employee_Name { get; set; }
        public string Selected_user_Id { get; set; }
        public string Committee_Assesment_Notes { get; set; }
        public string Initial_Contract_End_Date { get; set; }
        public string Action_Approved { get; set; }
        public string Extension_Period { get; set; }
        public string New_Contract_End_Date { get; set; }
        public string Director_SCM_Comments { get; set; }
        public string Director_SCM_Advice { get; set; }
        public string Professional_Opinion_Notes { get; set; }
        public string SCM_Status { get; set; }
        public string Procurement_Comments { get; set; }
        public string Committee_Sec_No { get; set; }
        public string Secretary_Name { get; set; }
        public string Crj_Comments { get; set; }
        public string CRJ_General_Comments { get; set; }
        public string Project_Manger_Communication { get; set; }

        public string Appealed_Period { get; set; }

        public string New_Appealed_Contract_End_Date { get; set; }
        public string Contract_Sum_Execution_PercentodatamediaReadLink { get; set; }
        public bool isManager { get; set; }
        public bool Sent_to_Ast_Directors { get; set; }
        public bool Addendum_Done { get; set; }
        public List<System.Web.Mvc.SelectListItem> ListOfEmployees { get; set; }

        public string Tender_Committee { get; set; }
        public List<System.Web.Mvc.SelectListItem> ListOfTenderCommittee { get; set; }
        public List<System.Web.Mvc.SelectListItem> ListOfProjects { get; set; }
        

        public string Completion_I_A_Recommendation { get; set; }
    }



    public class AmendmendRequestCard
    {

        public string No { get; set; }
        public string Project_No { get; set; }
        public string Project_Name { get; set; }
        public string Contractor_No { get; set; }
        public string Contractor_Name { get; set; }
        public string Document_Type { get; set; }
        public string Date_of_Submittion { get; set; }
        public string Date_of_Receipt { get; set; }
        public string Received_By { get; set; }
        public string Status { get; set; }
        public int Project_Sum { get; set; }
        public int Retention_Amount { get; set; }
        public string Project_Manager { get; set; }
        public string Project_Manager_Name { get; set; }
        public string Team_Approval_Status { get; set; }
        public bool Invoiced { get; set; }
        public string Memo_Comments { get; set; }
        public string Key_Comments { get; set; }
        public int Approved_Amount { get; set; }
        public int Approved_Variation_Addition { get; set; }
        public int Apprroved_Variation_Omition { get; set; }
        public int Actual_Work_Progress { get; set; }
        public int Approved_Certificates { get; set; }
        public int Certificate_No { get; set; }
        public string Contract_No { get; set; }
        public int Final_Contract_Value { get; set; }
        public int Previously_Paid_Amount { get; set; }
        public string Revised_Completion_Date { get; set; }
        public string Valuation_Date { get; set; }
        public int Total_Net_Variation { get; set; }
        public string Current_Approving_Member { get; set; }
        public string Approving_Member_Name { get; set; }
        public string Asssigning_Employee { get; set; }
        public string Assigning_Emplyee_Name { get; set; }
        public string Deadline { get; set; }
        public string Team_Lead_General_Comments { get; set; }
        public string Team_Lead_Rejection_Comments { get; set; }
        public string Selected_SCM_Employee { get; set; }
        public string Selected_Employee_Name { get; set; }
        public string Selected_user_Id { get; set; }
        public string Committee_Assesment_Notes { get; set; }
        public string Initial_Contract_End_Date { get; set; }
        public string Recomendation { get; set; }
        public string New_Contract_End_Date { get; set; }
        public string Director_SCM_Comments { get; set; }
        public string Professional_Opinion_Notes { get; set; }
        public string SCM_Status { get; set; }
        public string Procurement_Comments { get; set; }
        public string Committee_Sec_No { get; set; }
        public string Secretary_Name { get; set; }
        public string Crj_Comments { get; set; }
        public string CRJ_General_Comments { get; set; }
        public string Project_Manger_Communication { get; set; }
        public string Appealed_Period { get; set; }
        public string Action_Approved { get; set; }
        public string Extension_Period { get; set; }
        public string New_Appealed_Contract_End_Date { get; set; }
        public bool isManager { get; set; }
        public bool Sent_to_Ast_Directors { get; set; }
        public bool Addendum_Done { get; set; }
        public List<System.Web.Mvc.SelectListItem> ListOfEmployees { get; set; }

        public string Tender_Committee { get; set; }
        public List<System.Web.Mvc.SelectListItem> ListOfTenderCommittee { get; set; }


        public string Completion_I_A_Recommendation { get; set; }
    }


    public class ContractorPaymentRequestCard
    {

        public string No { get; set; }
        public string Project_No { get; set; }
        public string Project_Name { get; set; }
        public string Contractor_No { get; set; }
        public string Contractor_Name { get; set; }
        public string Document_Type { get; set; }
        public string Date_of_Submittion { get; set; }
        public string Date_of_Receipt { get; set; }
        public string Received_By { get; set; }
        public string Status { get; set; }
        public int Project_Sum { get; set; }
        public int Retention_Amount { get; set; }
        public string Project_Manager { get; set; }
        public string Project_Manager_Name { get; set; }
        public string Team_Approval_Status { get; set; }
        public bool Invoiced { get; set; }
        public string Memo_Comments { get; set; }
        public string Key_Comments { get; set; }
        public int Approved_Amount { get; set; }
        public int Approved_Variation_Addition { get; set; }
        public int Apprroved_Variation_Omition { get; set; }
        public int Actual_Work_Progress { get; set; }
        public int Approved_Certificates { get; set; }
        public int Certificate_No { get; set; }
        public string Contract_No { get; set; }
        public int Final_Contract_Value { get; set; }
        public int Previously_Paid_Amount { get; set; }
        public string Revised_Completion_Date { get; set; }
        public string Valuation_Date { get; set; }
        public int Total_Net_Variation { get; set; }
        public string Current_Approving_Member { get; set; }
        public string Approving_Member_Name { get; set; }
        public string Asssigning_Employee { get; set; }
        public string Assigning_Emplyee_Name { get; set; }
        public string Deadline { get; set; }
        public string Team_Lead_General_Comments { get; set; }
        public string Team_Lead_Rejection_Comments { get; set; }
        public bool Consolidated { get; set; }
        public string Selected_SCM_Employee { get; set; }
        public string Selected_Employee_Name { get; set; }
        public string Selected_user_Id { get; set; }
        public string I_x0026_A_Committee_Code { get; set; }
        public string Committee_Assesment_Notes { get; set; }
        public string Initial_Contract_End_Date { get; set; }
        public string Recomendation { get; set; }
        public string New_Contract_End_Date { get; set; }
        public string Director_SCM_Comments { get; set; }
        public string Professional_Opinion_Notes { get; set; }
        public string SCM_Status { get; set; }
        public string Procurement_Comments { get; set; }
        public string Committee_Sec_No { get; set; }
        public string Secretary_Name { get; set; }
        public string Crj_Comments { get; set; }
        public string CRJ_General_Comments { get; set; }
        public string Project_Manger_Communication { get; set; }
        public string Director_SCM_Advice { get; set; }
        [JsonProperty("Contract_Sum_Execution_Percent@odata.mediaReadLink")]
        public string Contract_Sum_Execution_PercentodatamediaReadLink { get; set; }
        public bool isManager { get; set; }
        public bool Sent_to_Ast_Directors { get; set; }
        public bool Addendum_Done { get; set; }
        public List<System.Web.Mvc.SelectListItem> ListOfEmployees { get; set; }

        public string Tender_Committee { get; set; }
        public List<System.Web.Mvc.SelectListItem> ListOfTenderCommittee { get; set; }


        public string Completion_I_A_Recommendation { get; set; }
    }





    public class PracticalCompletionForm
    {

        public string No { get; set; }
        public string Project_No { get; set; }
        public string Project_Name { get; set; }
        public string Contractor_No { get; set; }
        public string Contractor_Name { get; set; }
        public string Document_Type { get; set; }
        public string Date_of_Submittion { get; set; }
        public string Date_of_Receipt { get; set; }
        public string Received_By { get; set; }
        public string Status { get; set; }
        public decimal Project_Sum { get; set; }
        public decimal Retention_Amount { get; set; }
        public string Project_Manager { get; set; }
        public string Project_Manager_Name { get; set; }
        public string Team_Approval_Status { get; set; }
        public bool Invoiced { get; set; }
        public string Memo_Comments { get; set; }
        public int Approved_Amount { get; set; }
        public decimal Approved_Variation_Addition { get; set; }
        public decimal Apprroved_Variation_Omition { get; set; }
        public decimal Actual_Work_Progress { get; set; }
        public int Approved_Certificates { get; set; }
        public int Certificate_No { get; set; }
        public string Contract_No { get; set; }
        public decimal Final_Contract_Value { get; set; }
        public decimal Previously_Paid_Amount { get; set; }
        public string Revised_Completion_Date { get; set; }
        public string Valuation_Date { get; set; }
        public decimal Total_Net_Variation { get; set; }
        public string Current_Approving_Member { get; set; }
        public string Approving_Member_Name { get; set; }
        public string Asssigning_Employee { get; set; }
        public string Assigning_Emplyee_Name { get; set; }
        public string Deadline { get; set; }
        public string Team_Lead_General_Comments { get; set; }
        public string Team_Lead_Rejection_Comments { get; set; }
        public string Key_Comments { get; set; }
        public string Selected_SCM_Employee { get; set; }
        public string Selected_Employee_Name { get; set; }
        public string Selected_user_Id { get; set; }
        public string Committee_Assesment_Notes { get; set; }
        public string Initial_Contract_End_Date { get; set; }
        public string Completion_I_A_Recommendation { get; set; }
        public string New_Contract_End_Date { get; set; }
        public string Director_SCM_Comments { get; set; }
        public string Professional_Opinion_Notes { get; set; }
        public string SCM_Status { get; set; }
        public string Procurement_Comments { get; set; }
        public string Committee_Sec_No { get; set; }
        public string Secretary_Name { get; set; }
        public string Crj_Comments { get; set; }
        public string CRJ_General_Comments { get; set; }
        public string Project_Manger_Communication { get; set; }
        public bool Consolidated { get; set; }
        public bool Sent_to_Ast_Directors { get; set; }
        public string Action_Approved { get; set; }
        public string Extension_Period { get; set; }
        public string Director_SCM_Advice { get; set; }
    }

    public class MakingGoodDefectCard
    {

        public string No { get; set; }
        public string Project_No { get; set; }
        public string Project_Name { get; set; }
        public string Contractor_No { get; set; }
        public string Contractor_Name { get; set; }
        public string Document_Type { get; set; }
        public string Date_of_Submittion { get; set; }
        public string Date_of_Receipt { get; set; }
        public string Received_By { get; set; }
        public string Status { get; set; }
        public int Project_Sum { get; set; }
        public int Retention_Amount { get; set; }
        public string Project_Manager { get; set; }
        public string Project_Manager_Name { get; set; }
        public string Team_Approval_Status { get; set; }
        public bool Invoiced { get; set; }
        public string Memo_Comments { get; set; }
        public int Approved_Amount { get; set; }
        public int Approved_Variation_Addition { get; set; }
        public int Apprroved_Variation_Omition { get; set; }
        public int Actual_Work_Progress { get; set; }
        public int Approved_Certificates { get; set; }
        public int Certificate_No { get; set; }
        public string Contract_No { get; set; }
        public int Final_Contract_Value { get; set; }
        public int Previously_Paid_Amount { get; set; }
        public string Revised_Completion_Date { get; set; }
        public string Valuation_Date { get; set; }
        public int Total_Net_Variation { get; set; }
        public string Current_Approving_Member { get; set; }
        public string Approving_Member_Name { get; set; }
        public string Asssigning_Employee { get; set; }
        public string Assigning_Emplyee_Name { get; set; }
        public string Deadline { get; set; }
        public string Team_Lead_General_Comments { get; set; }
        public string Team_Lead_Rejection_Comments { get; set; }
        public string Key_Comments { get; set; }
        public bool Consolidated { get; set; }
        public string Selected_SCM_Employee { get; set; }
        public string Selected_Employee_Name { get; set; }
        public string Selected_user_Id { get; set; }
        public string Committee_Assesment_Notes { get; set; }
        public string Initial_Contract_End_Date { get; set; }
        public string Completion_I_A_Recommendation { get; set; }
        public string New_Contract_End_Date { get; set; }
        public string Director_SCM_Comments { get; set; }
        public string Professional_Opinion_Notes { get; set; }
        public string SCM_Status { get; set; }
        public string Procurement_Comments { get; set; }
        public string Committee_Sec_No { get; set; }
        public string Secretary_Name { get; set; }
        public string Crj_Comments { get; set; }
        public string CRJ_General_Comments { get; set; }
        public string Project_Manger_Communication { get; set; }
        public string Director_SCM_Advice { get; set; }
        public bool Sent_to_Ast_Directors { get; set; }
        public string Extension_Period { get; set; }

    }




    public class VariationProjectBoqs
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
        public int Remeasured_Qty { get; set; }
        public decimal Remeasured_Line_Amount { get; set; }
        public decimal Remeasured_Total_Amount { get; set; }
        public string Variation_Type { get; set; }
        public decimal Variation_Amount { get; set; }
        public string Entry_Type { get; set; }
    }


    public class ContractorExtensionRequestLines
    {

        public string Header_No { get; set; }
        public int Line_No { get; set; }
        public string section { get; set; }
        public string Request_Description { get; set; }
        public string Assistant_Director { get; set; }
        public string Assistant_Director_Name { get; set; }
        public string Assigned_Team_Member { get; set; }
        public string Resource_Name { get; set; }
        public string Deadline { get; set; }
        public string Review_Status { get; set; }
        public string Date_Submitted { get; set; }

        public List<System.Web.Mvc.SelectListItem> ListOfEmployees { get; set; }
        public List<SelectListItem> ListOfEmployees2 { get; set; }
    }


    public class ContractorRequestComments
    {
        public string Header_No { get; set; }
        public int Line_No { get; set; }
        public string Comments { get; set; }
        public string Written_By { get; set; }
        public string Written_At { get; set; }
        public string Author_Title { get; set; }
    }

    public class InspectionCommitteeMembers
    {
        public string Document_No { get; set; }
        public string Member_No { get; set; }
        public string Member_Name { get; set; }
        public string Title { get; set; }
        public string Assesment { get; set; }
        public List<System.Web.Mvc.SelectListItem> ListOfEmployees { get; set; }
        public List<System.Web.Mvc.SelectListItem> ListOfTitles { get; set; }
    }


    public class ContractorPaymentRequests
    {

        public string No { get; set; }
        public string Project_No { get; set; }
        public string Project_Name { get; set; }
        public string Contractor_No { get; set; }
        public string Contractor_Name { get; set; }
        public string Document_Type { get; set; }
        public DateTime Date_of_Submittion { get; set; }
        public DateTime Date_of_Receipt { get; set; }
        public string Received_By { get; set; }
        public string Status { get; set; }
        public int Project_Sum { get; set; }
        public int Retention_Amount { get; set; }
        public string Project_Manager { get; set; }
        public string Project_Manager_Name { get; set; }
        public string Team_Approval_Status { get; set; }
        public bool Invoiced { get; set; }
        public string Memo_Comments { get; set; }
        public string Key_Comments { get; set; }
        public int Approved_Amount { get; set; }
        public int Approved_Variation_Addition { get; set; }
        public int Apprroved_Variation_Omition { get; set; }
        public int Actual_Work_Progress { get; set; }
        public int Approved_Certificates { get; set; }
        public int Certificate_No { get; set; }
        public string Contract_No { get; set; }
        public int Final_Contract_Value { get; set; }
        public int Previously_Paid_Amount { get; set; }
        public string Revised_Completion_Date { get; set; }
        public string Valuation_Date { get; set; }
        public int Total_Net_Variation { get; set; }
        public string Current_Approving_Member { get; set; }
        public string Approving_Member_Name { get; set; }
        public string Asssigning_Employee { get; set; }
        public string Assigning_Emplyee_Name { get; set; }
        public string Deadline { get; set; }
        public string Team_Lead_General_Comments { get; set; }
        public string Team_Lead_Rejection_Comments { get; set; }
        public string Selected_SCM_Employee { get; set; }
        public string Selected_Employee_Name { get; set; }
        public string Selected_user_Id { get; set; }
        public string I_x0026_A_Committee_Code { get; set; }
        public string Committee_Assesment_Notes { get; set; }
        public string Initial_Contract_End_Date { get; set; }
        public string Recomendation { get; set; }
        public string New_Contract_End_Date { get; set; }
        public string Director_SCM_Comments { get; set; }
        public string Professional_Opinion_Notes { get; set; }
        public string SCM_Status { get; set; }
        public string Procurement_Comments { get; set; }
        public string Committee_Sec_No { get; set; }
        public string Secretary_Name { get; set; }
        public string Crj_Comments { get; set; }
        public string CRJ_General_Comments { get; set; }
        public string Project_Manger_Communication { get; set; }

        public bool isManager { get; set; }
        public bool Sent_to_Ast_Directors { get; set; }
        public bool Addendum_Done { get; set; }
        public bool Consolidated { get; set; }
    }

    public class PaymentRequestLines
    {
        public string Header_No { get; set; }
        public int Line_No { get; set; }
        public string Request_Description { get; set; }
        public string Section { get; set; }
        public string Assistant_Director { get; set; }
        public string Assistant_Director_Name { get; set; }
        public string Assigned_Team_Member { get; set; }
        public string Resource_Name { get; set; }
        public string Status { get; set; }
        public DateTime Date_Submitted { get; set; }
        public string Deadline { get; set; }
        public int No_of_Attachments { get; set; }
        public string Comments { get; set; }
    }

    public class ValuationProjectBoqs
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
        public string Completion_Status { get; set; }
        public string Comments { get; set; }
    }


    public class ProjectBoqs
    {

        public string Project_Code { get; set; }
        public int Entry_No { get; set; }
        public string Line_Type { get; set; }
        public string Section { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string UOM { get; set; }
        public int Unit_Price { get; set; }
        public int Line_Amount { get; set; }
        public int Total_valued_Qty { get; set; }
        public int Total_valued_Amount { get; set; }
    }



    public class MilestonesLines
    {

        public string Project_No { get; set; }
        public string Task_No { get; set; }
        public string Milestone_Code { get; set; }
        public string Milestone_Description { get; set; }
        public string Milestone_Start_Date { get; set; }
        public string Milestone_End_Date { get; set; }
        public string Notification_Period { get; set; }
        public string Notfification_Date { get; set; }
        public string Actual_Start_Date { get; set; }
        public string Actual_End_Date { get; set; }
        public string Status { get; set; }
    }
    public class ProjectAssignmentEntries
    {
        public int Entry_No { get; set; }
        public string Document_Type { get; set; }
        public string Document_No { get; set; }
        public string Assignment_Description { get; set; }
        public string Assigning_Employee { get; set; }
        public string Assigning_Employee_Name { get; set; }
        public string Assigned_Employee { get; set; }
        public string Assigned_Employee_Name { get; set; }
        public string Date_Sent { get; set; }
        public string Assigned_Date { get; set; }
        public string Contractor { get; set; }
        public string Project_Name { get; set; }
        public string Deadline { get; set; }
        public bool Assigned { get; set; }
        public List<System.Web.Mvc.SelectListItem> ListOfEmployees { get; set; }
    }



    public class InstructionRequestCard
    {

        public string No { get; set; }
        public string Contractor_No { get; set; }
        public string Contractor_Name { get; set; }
        public string Project_No { get; set; }
        public string Project_Name { get; set; }
        public string Document_Type { get; set; }
        public string Date_of_Submittion { get; set; }
        public string Date_of_Receipt { get; set; }
        public string Received_By { get; set; }
        public string Status { get; set; }
    }


    public class HOSRequestscard
    {

        public string No { get; set; }
        public string Project_No { get; set; }
        public string Project_Name { get; set; }
        public string Contractor_No { get; set; }
        public string Contractor_Name { get; set; }
        public string Request_Description { get; set; }
        public string Key_Comments { get; set; }
        public string Status { get; set; }
        public bool isManager { get; set; }

        public bool Sent_to_Ast_Directors { get; set; }
    }

    public class ProjectClosure
    {

        public string No { get; set; }
        public string Contractor_No { get; set; }
        public string Contractor_Name { get; set; }
        public string Project_No { get; set; }
        public string Project_Name { get; set; }
        public string Completion_Date { get; set; }
        public string Completion_Comments { get; set; }
        public string Portal_Status { get; set; }
        public string Approval_Status { get; set; }
    }

    public class JobList
    {

        public string No { get; set; }
        public string Description { get; set; }
        public string Bill_to_Customer_No { get; set; }
        public string Status { get; set; }
        public string Person_Responsible { get; set; }
        public string Search_Description { get; set; }
        public string Project_Manager { get; set; }
    }
    public class JobCard
    {

        public string No { get; set; }
        public string Description { get; set; }
        public string Project_Code { get; set; }
        public string Research_Center { get; set; }
        public string County_ID { get; set; }
        public string SubCounty { get; set; }
        public int Project_Sum { get; set; }
        public int valued_Amount { get; set; }
        public int _x0031_st_Moeity_Amount { get; set; }
        public int _x0032_nd_Moeity_Amount { get; set; }
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
        public double Contract_Amount { get; set; }
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
        public string Current_Workplan { get; set; }
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

    public class InspectionList
    {
        public string InspectionNo { get; set; }
        public string Type { get; set; }
        public string OrderNo { get; set; }
        public string Description { get; set; }
        public string CommiteeAppointmentNo { get; set; }
        public string InspectionDate { get; set; }
        public string Actual_Delivery_Date { get; set; }
        public string SupplierName { get; set; }
        public string GeneralCommitteRemarks { get; set; }
        public string DeliveryNoteNo { get; set; }

        public List<System.Web.Mvc.SelectListItem> ListOfCommitteeTypes { get; set; }

        public List<System.Web.Mvc.SelectListItem> ListOfIFSCodes { get; set; }
    }


    public class InspectionLines
    {
        public string Inspection_No { get; set; }
        public int Line_No { get; set; }
        public string Description { get; set; }
        public string TechnicalSpecification { get; set; }
        public string UnitofMeasure { get; set; }
        public int QuantityOrdered { get; set; }
        public string InspectionDecision { get; set; }
        public int AcceptedQuantity { get; set; }
        public int Variance { get; set; }
        public string ReasonsforVariance { get; set; }
        public string Remarks { get; set; }
    }

    public class MilestoneUpdatecard
    {

        public string No { get; set; }
        public string project_No { get; set; }
        public string Project_Name { get; set; }
        public string Contractor { get; set; }
        public string Contractor_Name { get; set; }
        public string Status { get; set; }
    }


}