using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

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
        public string Contract_Sum_Execution_PercentodatamediaReadLink { get; set; }
        public bool isManager { get; set; }
        public bool Sent_to_Ast_Directors { get; set; }
        public bool Addendum_Done { get; set; }
        public List<System.Web.Mvc.SelectListItem> ListOfEmployees { get; set; }
        public List<System.Web.Mvc.SelectListItem> ListOfProjects { get; set; }
        
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
        public string Request_Description { get; set; }
        public string Assistant_Director { get; set; }
        public string Assistant_Director_Name { get; set; }
        public string Assigned_Team_Member { get; set; }
        public string Resource_Name { get; set; }
        public string Deadline { get; set; }
        public string Review_Status { get; set; }
        public string Date_Submitted { get; set; }

        public List<System.Web.Mvc.SelectListItem> ListOfEmployees { get; set; }
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
        public string Professional_Opinion_Notes { get; set; }
        public string SCM_Status { get; set; }
        public string Procurement_Comments { get; set; }
        public string Committee_Sec_No { get; set; }
        public string Secretary_Name { get; set; }
        public string Crj_Comments { get; set; }
        public string CRJ_General_Comments { get; set; }
        public string Project_Manger_Communication { get; set; }
        public string Contract_Sum_Execution_PercentodatamediaReadLink { get; set; }
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
        public int Unit_Price { get; set; }
        public int Line_Amount { get; set; }
        public int Executed_Quantity { get; set; }
        public int Actualised_Amount { get; set; }
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
    }

    public class ProjectManagerInstCard
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

    public class MeetingCard
    {
       
        public string No { get; set; }
        public string Contractor_No { get; set; }
        public string Contractor_Name { get; set; }
        public string Project_No { get; set; }
        public string Project_Name { get; set; }
        public string Document_Type { get; set; }
        public DateTime Date_of_Submittion { get; set; }
        public DateTime Date_of_Receipt { get; set; }
        public string Received_By { get; set; }
        public string Status { get; set; }
        public string Date_of_meeting { get; set; }
        public string Next_meeting_Date { get; set; }
        public string Key_Comments { get; set; }
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


    public class SignupModel
    {
        public string VendorName { get; set; }
        public string ContactName { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Phonenumber { get; set; }
        public string Taxpin { get; set; }
        public string KraPin { get; set; }
        public string Email { get; set; }
        public string Password1 { get; set; }
        public string Password2 { get; set; }
        public string IDNoorRegNo { get; set; }
    }

}