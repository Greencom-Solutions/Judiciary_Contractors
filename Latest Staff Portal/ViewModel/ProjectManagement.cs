using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel;

public class ProjectProposals
{

    public string No { get; set; }
    public string RequesterName { get; set; }
    public string Name { get; set; }
    public string AdminUnitCode { get; set; }
    public string Global_Dimension_2_Code { get; set; }
    public string Administration_Unit_Name { get; set; }
    public string County { get; set; }
    public string County_Name { get; set; }
    public string Sub_County { get; set; }
    public string Sub_County_Name { get; set; }
    public string Request_Description { get; set; }
    public string Justification { get; set; }
    public string Request_Source { get; set; }
    public string Created_By { get; set; }
    public string Employee_No { get; set; }
    public string Employee_Name { get; set; }

    public string Request_Status { get; set; }
    public string Status { get; set; }
    public string LoggedInUserID { get; set; }
    public bool Design_Created { get; set; }
    public string pagetitle { get; set; }
    public bool IsManager { get; set; }
    public string Project_Type { get; set; }
    public List<SelectListItem> ListOfAdminUnits { get; set; }
    public List<SelectListItem> ListOfCounties { get; set; }
    public List<SelectListItem> ListOfEmployees { get; set; }
    public List<SelectListItem> ListOfRequestSource { get; set; }
}


public class TeamLeadSelections
{

    public string No { get; set; }
    public string User_Request_No { get; set; }
    public string Administrative_Unit { get; set; }
    public string Committee_Chair { get; set; }
    public string Chair_Name { get; set; }
    public string Date_of_Meeting { get; set; }
    public string Caseload { get; set; }
    public string Caseload_Category { get; set; }
    public string Project_Code { get; set; }
    public string Project_Name { get; set; }
    public string Objective_of_engagement { get; set; }
    public string Team_Lead { get; set; }
    public string Section_Lead_Area { get; set; }
    public bool Sent_to_team_Leads { get; set; }
    public bool isManager { get; set; }
    public bool Design_Created { get; set; }
    public List<SelectListItem> ListOfEmployees { get; set; }
}


public class ProjectTeamSelections
{
    public string No { get; set; }
    public string Project_Code { get; set; }
    public string Project_Name { get; set; }
    public string Geographical_Unit_Name { get; set; }
    public string Team_Lead { get; set; }
    public string Section_Lead_Area { get; set; }
    public string User_Request_No { get; set; }
    public bool Appointed { get; set; }
    public bool isManager { get; set; }
    public bool Design_Created { get; set; }

    public List<SelectListItem> ListOfTeamMembers { get; set; }
}



//Team leads Lines
public class TeamLeads
{

    public string Project_No { get; set; }
    public string Resource_No { get; set; }
    public string Resource_Name { get; set; }
    public string Specialty { get; set; }

    public bool isManager { get; set; }
    public List<SelectListItem> ListOfEmployees { get; set; }

}

//Project Team Lines
public class ProjectTeam
{
    public string Design_No { get; set; }
    public string No { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
    public string Expertise { get; set; }
    public int Approval_Sequence { get; set; }
    public string Involvement_Stage { get; set; }

    public bool isManager { get; set; }

    public List<SelectListItem> ListOfEmployees { get; set; }

}

public class StakeholderMeeting
{

    public string No { get; set; }
    public string Project_Code { get; set; }
    public string Project_Name { get; set; }
    public string Date_of_Meeting { get; set; }
    public bool isManager { get; set; }
}


public class StakeholderFeedback
{
    public string No { get; set; }
    public int Line_No { get; set; }
    public string Suggestion { get; set; }
    public string Location { get; set; }
    public string Proponent { get; set; }
    public string Status_Of_Suggestion { get; set; }
    public bool isManager { get; set; }
    public List<SelectListItem> ListOfEmployees { get; set; }
    public List<SelectListItem> ListOfStatus { get; set; }
}


//Preliminary Design Control
public class PreliminaryDesignControl
{
    public string No { get; set; }
    public string Project_Code { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Start_Date { get; set; }
    public string End_Date { get; set; }
    public string Committee { get; set; }
    public string Global_Dimension_2_Code { get; set; }
    public string Geographic_Location_Name { get; set; }
    public decimal Total_Estimated_Cost { get; set; }
    public string Project_Classifications { get; set; }
    public string Created_By { get; set; }
    public string Employee_No { get; set; }
    public string Employee_Name { get; set; }
    public string Rejection_Comments { get; set; }
    public string Deferal_Comments { get; set; }
    public string Design_Stage { get; set; }
    public string Status { get; set; }
    public string Proposed_Project { get; set; }
    public string Proposed_Project_Name { get; set; }
    public string Approved_Proposal { get; set; }
    public string Proposal_Name { get; set; }
    public string Team_Leader { get; set; }
    public string Design_Status { get; set; }
    public string Document_Type { get; set; }
    public string Design_Control_Type { get; set; }
    public string Current_Actioning_Member { get; set; }
    public string Design_Approval_Stage { get; set; }
    public string Situation_Analysis { get; set; }
    public string Relevance_of_proj_Idea { get; set; }
    public string Scope_of_the_project { get; set; }
    public string Logical_Framework { get; set; }
    public string Goal { get; set; }
    public string Project_Objectives_Outcome { get; set; }
    public string Proposed_Project_Outputs { get; set; }
    public string Project_Activities_and_Inputs { get; set; }
    public string institutional_Mandate { get; set; }
    public string Management_of_the_Project { get; set; }
    public string Monitoring_and_Evaluation { get; set; }
    public string Risk_and_Mitigation_Measures { get; set; }
    public string Project_Sustainability { get; set; }
    public string Project_Stakeholders_and_Collaborators { get; set; }
    public string Project_Readiness { get; set; }
    public bool Final_Design_Created { get; set; }

    public string Final_Design_Stage { get; set; }
}


public class DesignControlsLines
{
    public string Header_No { get; set; }
    public int Entry_No { get; set; }
    public string Attachment_Code { get; set; }
    public int Design_Control_No { get; set; }
    public string Design_Control_Type { get; set; }
    public string Name { get; set; }
    public int No_of_Items { get; set; }
}

public class GrandSummaryBOQLines
{
    public string Header_No { get; set; }
    public int Line_No { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public string Unit_of_Measure { get; set; }
    public decimal Unit_Cost { get; set; }
    public decimal Total_Cost { get; set; }
}

public class TeamMembersBOQLines
{

    public string Design_Code { get; set; }
    public string Member_No { get; set; }
    public int Entry_No { get; set; }
    public string Design_Stage { get; set; }
    public string Description { get; set; }
    public int quantity { get; set; }
    public decimal Unit_Price { get; set; }
    public decimal Total_Amount { get; set; }
    public List<SelectListItem> ListOfEmployees { get; set; }
}

public class LogicalFramework
{

    public string Design_No { get; set; }
    public int Entry_No { get; set; }
    public string Narative { get; set; }
    public string Indicators { get; set; }
    public string Sources_Means_of_Ver { get; set; }
    public string Assumptions { get; set; }
}
public class ConceiptAnalysis
{
    public string Project_No { get; set; }
    public string situationAnalysis { get; set; }
    public string relevance { get; set; }
    public string projectScope { get; set; }
    public string logicalFramework { get; set; }
    public string goal { get; set; }
    public string objectives { get; set; }
    public string projectOutput { get; set; }
    public string activitiesInput { get; set; }
    public string InstitutionalMandate { get; set; }
    public string ProjectManagement { get; set; }
    public string Monitoring { get; set; }
    public string Risk { get; set; }
    public string ProjectSustainability { get; set; }
    public string ProjectStakeholders { get; set; }
    public string ProjectReadiness { get; set; }
}



//contractor payment request
public class PaymentRequestCard
{
    public string No { get; set; }
    public string Contractor_No { get; set; }
    public string Contractor_Name { get; set; }
    public string Project_No { get; set; }
    public string Project_Name { get; set; }
    public string Document_Type { get; set; }
    public DateTime Date_of_Submittion { get; set; }
    public DateTime Date_of_Receipt { get; set; }
    public string Status { get; set; }
    public string Current_Approving_Member { get; set; }
    public string Approving_Member_Name { get; set; }
    public string Procurement_Comments { get; set; }

    //public static PaymentRequestCard FromJson(string json) => JsonConvert.DeserializeObject<PaymentRequestCard>(json);
}



public class ContractorPaymentRequest
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

public class PaymentRequestLine
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

public class ValuationProjectBoq
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


public class ContracList
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


public class Contract
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

public class ContractLine
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

public class ContractPaymentLine
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

public class PaymentsLine
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

public class AnnexSevenList
{
    public string No { get; set; }
    public string Name { get; set; }
    public decimal Total_Estimated_Cost { get; set; }
    public string Start_Date { get; set; }
    public string End_Date { get; set; }
    public string County { get; set; }
    public string Sub_County { get; set; }
    public decimal Budget_Absorption { get; set; }
    public decimal Outstanding_Project_Cost { get; set; }
    public decimal Cummulative_Expenditure { get; set; }
    public decimal Project_Completion_Status { get; set; }
    public decimal Current_Year { get; set; }
    public decimal Year_2 { get; set; }
    public decimal Year_3 { get; set; }
    public string Project_Status { get; set; }
}
