using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class PurchaseRequisition
    {
        public string DocumentType { get; set; }
        public string No { get; set; }
        public string RequesterID { get; set; }
        public string RequestByNo { get; set; }
        public string RequestByName { get; set; }
        public string ShortcutDimension2Code { get; set; }
        public string DepartmentName { get; set; }
        public string ProjectName { get; set; }
        public string Status { get; set; }
        public string LocationCode { get; set; }
        public DateTime OrderDate { get; set; }
        public string Description { get; set; }
        public string RequisitionType { get; set; }
        public string CurrencyCode { get; set; }
        public string Job { get; set; }
        public string JobName { get; set; }
        public string Document_Type { get; set; }
        public string PRN_Type { get; set; }
        public int PrnType { get; set; }
        public string Document_Date { get; set; }
        public string Location_Code { get; set; }
        public List<SelectListItem> ListOfLocations { get; set; }
        public string Requisition_Product_Group { get; set; }
        public int Product_Group_ { get; set; }
        public string Priority_Level { get; set; }
        public int priority { get; set; }
        public string Requester_ID { get; set; }
        public string Request_By_No { get; set; }
        public string Request_By_Name { get; set; }
        public string Shortcut_Dimension_1_Code { get; set; }
        public string LocationName { get; set; }
        public List<SelectListItem> ListOfDim1 { get; set; }
        public string Shortcut_Dimension_2_Code { get; set; }
        public string AdminUnitName { get; set; }
        public List<SelectListItem> ListOfDim2 { get; set; }
        public string Procurement_Plan_ID { get; set; }
        public string PP_Planning_Category { get; set; }
        public List<SelectListItem> ListOfPlanningCategories { get; set; }
        public string Job_Task_No { get; set; }
        public decimal PP_Total_Budget { get; set; }
        public decimal PP_Total_Commitments { get; set; }
        public decimal PP_Total_Available_Budget { get; set; }
        public decimal Requisition_Amount { get; set; }
        public string DimensionSetId { get; set; }

    }

    public class PurchaseRequisitionLine
    {
        public string ContractNoToPay { get; set; }
        public string Document_Type { get; set; }
        public string Document_No { get; set; }
        public int Line_No { get; set; }
        public string ProcurementPlanID { get; set; }
        public string Type { get; set; }
        public int ProcurementPlanItemNo { get; set; }
        public List<SelectListItem> ListOfProcurementPlanEntries { get; set; }
        public List<SelectListItem> ListOfProcurementCategories { get; set; }

        public string Control6 { get; set; }
        public string No { get; set; }
        public string Description { get; set; }
        public string TechnicalSpecifications { get; set; }
        public string UnitofMeasureCode { get; set; }
        public int QuantityInStore { get; set; }
        public int Quantity { get; set; }
        public string DirectUnitCostInclVAT { get; set; }
        public decimal Amount { get; set; }
        public decimal AmountIncludingVAT { get; set; }
        public string CurrencyCode { get; set; }
        public string LocationCode { get; set; }
        public decimal UnitCostLCY { get; set; }
        public string VATProdPostingGroup { get; set; }
        public string ItemCategoryCode { get; set; }
        public string Shortcut_Dimension_1_Code { get; set; }
        public string Budget { get; set; }
        public string Budget_Line { get; set; }
        public decimal PPTotalAvailableBudget { get; set; }
        public string Shortcut_Dimension_2_Code { get; set; }
        public string PPFundingSourceID { get; set; }
        public decimal PPTotalBudget { get; set; }
        public decimal PPTotalActualCosts { get; set; }
        public decimal PPTotalCommitments { get; set; }
        public string PPSolicitationType { get; set; }
        public string PPProcurementMethod { get; set; }
        public string PPPreferenceReservationCode { get; set; }
        public string PRNConversionProcedure { get; set; }
        public string Status { get; set; }
    }


    public class PurchaseItemDetails
    {
        public List<SelectListItem> ListOfLocations { get; set; }
        public PurchaseRequisitionLine ItemDetails { get; set; }
        public List<SelectListItem> ListOfGlBudgets { get; set; }
        public List<SelectListItem> ListOfExpenseCodes { get; set; }
        public List<SelectListItem> ListOfUnitsOfMeasure { get; set; }
        public string ExpenseCode { get; set; }
        public string UoM { get; set; }
        public string Location { get; set; }
        public string RequisitionType { get; set; }
    }

    public class ListOfExpenseCodes
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }


    /*    public class ApprovedTenderInvitation
        {
            public string Code { get; set; }
            public string Procurement_Method { get; set; }
            public string Solicitation_Type { get; set; }
            public string External_Document_No { get; set; }
            public string Procurement_Type { get; set; }
            public string Procurement_Category_ID { get; set; }
            public string Project_ID { get; set; }
            public string Assigned_Procurement_Officer { get; set; }
            public string Road_Code { get; set; }
            public string Description { get; set; }
            public string Currency_Code { get; set; }
        }*/

    public class ProcurementPlan
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string ConsolidatedProcurementPlan { get; set; }
        public DateTime DocumentDate { get; set; }
        public string CorporateStrategicPlanID { get; set; }
        public List<SelectListItem> ListofStrategicPlans { get; set; }
        public string FinancialBudgetID { get; set; }
        public List<SelectListItem> ListOfFinancialBudgets { get; set; }
        public string FinancialYearCode { get; set; }
        public List<SelectListItem> ListOfFinancialYears { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string GlobalDimension1Code { get; set; }
        public List<SelectListItem> ListOfDim1 { get; set; }
        public string GlobalDimension2Code { get; set; }
        public string Admin_Unit_Name { get; set; }
        public List<SelectListItem> ListOfDim2 { get; set; }
        public string GlobalDimension3Code { get; set; }
        public List<SelectListItem> ListOfDim3 { get; set; }
        public decimal TotalBudgetGoods { get; set; }
        public decimal TotalBudgetWorks { get; set; }
        public decimal TotalBudgetServices { get; set; }
        public decimal TotalBudgetedSpend { get; set; }
        public decimal TotalActualGoods { get; set; }
        public decimal TotalActualWorks { get; set; }
        public decimal TotalActualServices { get; set; }
        public decimal TotalActualSpend { get; set; }
        public decimal SpendPercent { get; set; }
        public string ApprovalStatus { get; set; }
        public string CreatedBy { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public TimeSpan TimeCreated { get; set; }
    }

    public class ProcurementPlanLine
    {
        public int PPLineNo { get; set; }
        public string ProcurementPlanID { get; set; }
        public string DocumentType { get; set; }
        public string PlanningCategory { get; set; }
        public string Description { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal TotalBudgetedCost { get; set; }
        public List<SelectListItem> ListOfProcurementTypes { get; set; }

        public List<SelectListItem> ListOfSolicitationType { get; set; }

        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public string Status { get; set; }
    }


    public class ProcurementTask
    {
        public string docNo { get; set; }
        public string ProcurementPlanID { get; set; }
        public int EntryNo { get; set; }
        public string PlanningCategory { get; set; }
        public string DocumentType { get; set; }
        public string ItemType { get; set; }
        public string ItemNo { get; set; }
        public string Description { get; set; }
        public string ProcurementType { get; set; }
        public string SolicitationType { get; set; }
        public string ProcurementMethod { get; set; }
        public string AlternativeProcurementMethods { get; set; }
        public string PreferenceReservationCode { get; set; }
        public string FundingSourceID { get; set; }
        public string RequisitionProductGroup { get; set; }
        public string ProcurementUse { get; set; }
        public string InvitationNoticeType { get; set; }
        public string PlanningDate { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public decimal LineBudgetCost { get; set; }
        public decimal TotalActualCosts { get; set; }
        public decimal TotalPRNCommitments { get; set; }
        public decimal AvailableProcurementBudget { get; set; }
        public string GlobalDimension1Code { get; set; }
        public string GlobalDimension2Code { get; set; }
        public string ShortcutDimension3Code { get; set; }
        public string ShortcutDimension4Code { get; set; }
        public string ShortcutDimension5Code { get; set; }
        public string ShortcutDimension6Code { get; set; }
        public string ShortcutDimension7Code { get; set; }
        public string ShortcutDimension8Code { get; set; }
        public string ProcurementStartDate { get; set; }
        public string ProcurementEndDate { get; set; }
        public int ProcurementDurationDays { get; set; }
        public string UserID { get; set; }
        public string LastDateModified { get; set; }
        public string BudgetControlJobNo { get; set; }
        public string BudgetControlJobTaskNo { get; set; }
        public string BudgetAccount { get; set; }
        public decimal Q1Quantity { get; set; }
        public decimal Q1Amount { get; set; }
        public decimal Q1Budget { get; set; }
        public decimal Q2Quantity { get; set; }
        public decimal Q2Amount { get; set; }
        public decimal Q2Budget { get; set; }
        public decimal Q3Quantity { get; set; }
        public decimal Q3Amount { get; set; }
        public decimal Q3Budget { get; set; }
        public decimal Q4Quantity { get; set; }
        public decimal Q4Amount { get; set; }
        public decimal Q4Budget { get; set; }
        public string WorkPlanProjectID { get; set; }
        public string WorkplanTaskNo { get; set; }
        public string ActivityNo { get; set; }
        public string SubActivityNo { get; set; }
        public decimal AGPOPercent { get; set; }
        public decimal AGPOAmount { get; set; }
        public decimal PWDPercent { get; set; }
        public decimal PWDAmount { get; set; }
        public decimal WomenPercent { get; set; }
        public decimal WomenAmount { get; set; }
        public decimal YouthPercent { get; set; }
        public decimal YouthAmount { get; set; }
        public decimal MarginOfPreference { get; set; }
        public decimal CitizenContractorsPercent { get; set; }
        public string Status { get; set; }

        public string DocumentStatus { get; set; }
        public string ActionTaken { get; set; }
    }

    public class ExpensePRNLine
    {
        public string DocumentNo { get; set; }
        public int LineNo { get; set; }
        public int ProcPlanEntryNo { get; set; }
        public string Item { get; set; }
        public string EntryNo { get; set; }
        public List<SelectListItem> ListOfProcurementPlanNos { get; set; }
        public List<SelectListItem> ListOfProcurementItems { get; set; }
        public string ProcType { get; set; }
        public int pType { get; set; }
        public string No { get; set; }
        public string Name { get; set; }
        public string UnitOfMeasure { get; set; }
        public int Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Total { get; set; }
        public string ExpenseDescription { get; set; }
        public string GLAccount { get; set; }
        public string Status { get; set; }
        public string RecalledBy { get; set; }
        public DateTime RecalledOn { get; set; }
        public string SourceLineNo { get; set; }
    }

    public class ExpensePRNLineList
    {
        public string Status { get; set; }
        public List<ExpensePRNLine> ListOfExpensePRNLine { get; set; }
    }


    public class ApprovedPurchaseRequisition
    {
        public string DocumentType { get; set; }
        public string No { get; set; }
        public string PRNType { get; set; }
        public DateTime DocumentDate { get; set; }
        public string LocationCode { get; set; }
        public string RequisitionNo { get; set; }
        public string RequisitionProductGroup { get; set; }
        public string RequesterID { get; set; }
        public string RequestByNo { get; set; }
        public string RequestByName { get; set; }
        public string ShortcutDimension1Code { get; set; }
        public string ShortcutDimension2Code { get; set; }
        public string AssignedOfficer { get; set; }
        public string AssignedUserID { get; set; }
        public string DepartmentName { get; set; }
        public string ProjectName { get; set; }
        public DateTime OrderDate { get; set; }
        public string ProcurementPlanID { get; set; }
        public string PurchaserCode2 { get; set; }
        public string PPPlanningCategory { get; set; }
        public string Description { get; set; }
        public decimal PPTotalBudget { get; set; }
        public string PPSolicitationType { get; set; }
        public string OtherProcurementMethods { get; set; }
        public string PPInvitationNoticeType { get; set; }
        public string PPPreferenceReservationCode { get; set; }
        public decimal PPTotalActualCosts { get; set; }
        public string PRNConversionProcedure { get; set; }
        public string Status { get; set; }
        public string LinkedIFSNo { get; set; }
        public string LinkedLPONo { get; set; }
        public string ConsolidateToIFSNo { get; set; }
        public bool OrderedPRN { get; set; }

        public decimal ApprovedRequisitionAmount { get; set; }
        public decimal RequisitionAmount { get; set; }
        public string ProcurementType { get; set; }
        public string ProcessType { get; set; }
        public string PurchaseType { get; set; }
        public string Job { get; set; }
        public string JobName { get; set; }

        public List<SelectListItem> ListOfSolisitationTypes { get; set; }
        public string PPBidSelectionMethod { get; set; }
        public string PPProcurementMethod { get; set; }
        public List<SelectListItem> ListOfPPPreferenceReservationCodes { get; set; }
        public List<SelectListItem> ListOfIFSNos { get; set; }

        public string LocationName { get; set; }
        public string AdminUnitName { get; set; }
    }

    public class ContractPaymentDetailsLines
    {
        public string Document_No { get; set; }
        public int Line_No { get; set; }
        public string Contract_No { get; set; }
        public int Item_Line_No { get; set; }
        public string Item_No { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int Unit_Price { get; set; }
        public int Amount { get; set; }
        public int LineLineNo { get; set; }
    }

    public class ApprovedPRNLine
    {
        public bool LPO_Created { get; set; }
        public bool Line_Selected { get; set; }
        public string DocNo { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNo { get; set; }
        public int LineNo { get; set; }
        public string Type { get; set; }
        public string ProcurementPlanID { get; set; }
        public string Status { get; set; }
        public int ProcurementPlanEntryNo { get; set; }
        public string ItemDescription { get; set; }
        public string PPSolicitationType { get; set; }
        public string PPProcurementMethod { get; set; }
        public string OtherProcurementMethods { get; set; }
        public string PPPreferenceReservationCode { get; set; }
        public string ItemCategoryCode { get; set; }
        public string No { get; set; }
        public string Description { get; set; }
        public string UnitOfMeasureCode { get; set; }
        public int Quantity { get; set; }
        public decimal DirectUnitCost { get; set; }
        public decimal Amount { get; set; }
        public decimal AmountIncludingVAT { get; set; }
        public string CurrencyCode { get; set; }
        public string LocationCode { get; set; }
        public decimal UnitCostLCY { get; set; }
        public string ContractNoToPay { get; set; }
        public List<SelectListItem> ContractDropdownList { get; set; }
    }

    public class PurchaseOrdersList
    {
        public string Document_Type { get; set; }
        public string No { get; set; }
        public string Buy_from_Vendor_No { get; set; }
        public string Order_Address_Code { get; set; }
        public string Buy_from_Vendor_Name { get; set; }
        public string Vendor_Authorization_No { get; set; }
        public string Buy_from_Post_Code { get; set; }
        public string Buy_from_Country_Region_Code { get; set; }
        public string Buy_from_Contact { get; set; }
        public string Pay_to_Vendor_No { get; set; }
        public string Pay_to_Name { get; set; }
        public string Pay_to_Post_Code { get; set; }
        public string Pay_to_Country_Region_Code { get; set; }
        public string Pay_to_Contact { get; set; }
        public string Ship_to_Code { get; set; }
        public string Ship_to_Name { get; set; }
        public string Ship_to_Post_Code { get; set; }
        public string Ship_to_Country_Region_Code { get; set; }
        public string Ship_to_Contact { get; set; }
        public string Posting_Date { get; set; }
        public string Shortcut_Dimension_1_Code { get; set; }
        public string Shortcut_Dimension_2_Code { get; set; }
        public string Location_Code { get; set; }
        public string Purchaser_Code { get; set; }
        public string Assigned_User_ID { get; set; }
        public string Currency_Code { get; set; }
        public string Document_Date { get; set; }
        public string Status { get; set; }
        public string Payment_Terms_Code { get; set; }
        public string Due_Date { get; set; }
        public int Payment_Discount_Percent { get; set; }
        public string Payment_Method_Code { get; set; }
        public string Shipment_Method_Code { get; set; }
        public string Requested_Receipt_Date { get; set; }
        public string Job_Queue_Status { get; set; }
        public int Amount_Received_Not_Invoiced_excl_VAT_LCY { get; set; }
        public int Amount_Received_Not_Invoiced_LCY { get; set; }
        public int Amount { get; set; }
        public int Amount_Including_VAT { get; set; }
        public string Posting_Description { get; set; }
        public string Invitation_For_Supply_No { get; set; }
        public string Requisition_No { get; set; }
        public object Procurement_Type { get; set; }
    }

    public class ApprovedPRNLineList
    {
        public string Status { get; set; }
        public List<ApprovedPRNLine> ListOfApprovedPRNLine { get; set; }
    }


    /*    public class InvitationToTender
        {
            public string Project_ID;
            public string Assigned_Procurement_Officer;
            public string Road_Code;
            public string Currency_Code;
            public string odataetag { get; set; }
            public string Code { get; set; }
            public string Invitation_Notice_Type { get; set; }
            public string Document_Date { get; set; }
            public string Tender_Name { get; set; }
            public string Tender_Summary { get; set; }
            public string External_Document_No { get; set; }
            public string Stage_1_EOI_Invitation { get; set; }
            public string PRN_No { get; set; }
            public string Global_Dimension_1_Code { get; set; }
            public string Global_Dimension_2_Code { get; set; }
            public string Location_Code { get; set; }
            public string Description { get; set; }
            public string Procurement_Type { get; set; }
            public string Requisition_Product_Group { get; set; }
            public string Procurement_Category_ID { get; set; }
            public string Lot_No { get; set; }
            public string Target_Bidder_Group { get; set; }
            public string Solicitation_Type { get; set; }
            public string Bid_Selection_Method { get; set; }
            public string Requisition_Template_ID { get; set; }
            public string Bid_Scoring_Template { get; set; }
            public string Bid_Opening_Committe { get; set; }
            public string Bid_Evaluation_Committe { get; set; }
            public string Procurement_Method { get; set; }
            public bool Enforce_Mandatory_Pre_bid_Visi { get; set; }
            public string Mandatory_Pre_bid_Visit_Date { get; set; }
            public string Prebid_Meeting_Address { get; set; }
            public string Prebid_Meeting_Register_ID { get; set; }
            public string Bid_Envelop_Type { get; set; }
            public bool Sealed_Bids { get; set; }
            public string Tender_Validity_Duration { get; set; }
            public string Tender_Validity_Expiry_Date { get; set; }
            public string Purchaser_Code { get; set; }
            public string Language_Code { get; set; }
            public bool Mandatory_Special_Group_Reserv { get; set; }
            public bool Bid_Tender_Security_Required { get; set; }
            public int Bid_Security_Percent { get; set; }
            public int Bid_Security_Amount_LCY { get; set; }
            public string Bid_Security_Validity_Duration { get; set; }
            public string Bid_Security_Expiry_Date { get; set; }
            public bool Insurance_Cover_Required { get; set; }
            public bool Performance_Security_Required { get; set; }
            public int Performance_Security_Percent { get; set; }
            public bool Advance_Payment_Security_Req { get; set; }
            public int Advance_Payment_Security_Percent { get; set; }
            public int Advance_Amount_Limit_Percent { get; set; }
            public string Appointer_of_Bid_Arbitrator { get; set; }
            public string Status { get; set; }
            public bool Published { get; set; }
            public string Bid_Submission_Method { get; set; }
            public string Cancel_Reason_Code { get; set; }
            public string Cancellation_Date { get; set; }
            public string Cancellation_Secret_Code { get; set; }
            public int No_of_Submission { get; set; }
            public DateTime Created_Date_Time { get; set; }
            public string Created_by { get; set; }
            public string Submission_Start_Date { get; set; }
            public string Submission_Start_Time { get; set; }
            public string Submission_End_Date { get; set; }
            public string Submission_End_Time { get; set; }
            public string Bid_Opening_Date { get; set; }
            public string Bid_Opening_Time { get; set; }
            public string Bid_Opening_Venue { get; set; }
            public string Procuring_Entity_Name_Contact { get; set; }
            public string Address { get; set; }
            public string Address_2 { get; set; }
            public string Post_Code { get; set; }
            public string City { get; set; }
            public string Country_Region_Code { get; set; }
            public string Phone_No { get; set; }
            public string E_Mail { get; set; }
            public string Tender_Box_Location_Code { get; set; }
            public string Primary_Tender_Submission { get; set; }
            public string Primary_Engineer_Contact { get; set; }
            public string Requesting_Region { get; set; }
            public string Requesting_Directorate { get; set; }
            public string Requesting_Department { get; set; }
            public string Procurement_Plan_ID { get; set; }
            public int Procurement_Plan_Entry_No { get; set; }
            public string Job { get; set; }
            public string Job_Task_No { get; set; }
            public string PP_Planning_Category { get; set; }
            public string Document_Status { get; set; }
            public string PP_Funding_Source_ID { get; set; }
            public double PP_Total_Budget { get; set; }
            public int PP_Total_Actual_Costs { get; set; }
            public int PP_Total_Commitments { get; set; }
            public double PP_Total_Available_Budget { get; set; }
            public string PP_Preference_Reservation_Code { get; set; }
            public string Financial_Year_Code { get; set; }
        }*/


    public class ProcurementPlanAmmendments
    {

        public string Code { get; set; }
        public string Global_Dimension_1_Code { get; set; }
        public string Global_Dimension_2_Code { get; set; }
        public string Type { get; set; }
        public string PlanNo { get; set; }
        public string Description { get; set; }
        public string ApprovalStatus { get; set; }
        public string Ammendment_Reason { get; set; }
        public string CreatedBy { get; set; }
        public string DateCreated { get; set; }
        public string TimeCreated { get; set; }

        public List<SelectListItem> ListOfDim1 { get; set; }
        public List<SelectListItem> ListOfDim2 { get; set; }

        public List<SelectListItem> ListOfProcurementPlans { get; set; }
    }

    public class ProcurementPlanAmmendmentsLines
    { 
        public int PP_Line_No { get; set; }
        public string Procurement_Plan_ID { get; set; }
        public string Document_Type { get; set; }
        public string PlanningCategory { get; set; }
        public string Description { get; set; }
        public int TotalQuantity { get; set; }
        public string Total_Budgeted_Cost { get; set; }

    }


    public class ProcurementPlanActivityScheduleLines
    {
       
        public string Procurement_Plan_ID { get; set; }
        public int PP_Entry { get; set; }
        public string Planning_Category { get; set; }
        public int Line_no { get; set; }
        public string Procurement_Method { get; set; }
        public string Activity_Code { get; set; }
        public int Planned_Days { get; set; }
        public int Actual_Days { get; set; }
        public string Planned_Dates { get; set; }
        public string Actual_Dates { get; set; }
    }


    public class ProcurementPlanRevisionEntries
    {
        public string ProcurementPlanID { get; set; }
        public int EntryNo { get; set; }
        public string docNo { get; set; }
        public string docType { get; set; }
        public string Planning_Category { get; set; }
        public string Document_Type { get; set; }
        public bool Modified { get; set; }
        public string RevisedPlanNo { get; set; }
        public int VoteItem { get; set; }
        public string PlanItemType { get; set; }
        public string PlanItemNo { get; set; }
        public string Description { get; set; }
        public string Procurement_Type { get; set; }
        public string Solicitation_Type { get; set; }
        public string Procurement_Method { get; set; }
        public string Alternative_Procurement_Methods { get; set; }
        public string Preference_Reservation_Code { get; set; }
        public string Funding_Source_ID { get; set; }
        public string Requisition_Product_Group { get; set; }
        public string Procurement_Use { get; set; }
        public string Invitation_Notice_Type { get; set; }
        public string Planning_Date { get; set; }
        public int Quantity { get; set; }
        public int Unit_Cost { get; set; }
        public int Line_Budget_Cost { get; set; }
        public int Total_Actual_Costs { get; set; }
        public int Total_PRN_Commitments { get; set; }
        public int Available_Procurement_Budget { get; set; }
        public string Global_Dimension_1_Code { get; set; }
        public string Global_Dimension_2_Code { get; set; }
        public string Shortcut_Dimension_3_Code { get; set; }
        public string Shortcut_Dimension_4_Code { get; set; }
        public string Shortcut_Dimension_5_Code { get; set; }
        public string Shortcut_Dimension_6_Code { get; set; }
        public string Shortcut_Dimension_7_Code { get; set; }
        public string Shortcut_Dimension_8_Code { get; set; }
        public string Procurement_Start_Date { get; set; }
        public string Procurement_End_Date { get; set; }
        public int Procurement_Duration_Days { get; set; }
        public string User_ID { get; set; }
        public string Last_Date_Modified { get; set; }
        public string Budget_Control_Job_No { get; set; }
        public string Budget_Control_Job_Task_No { get; set; }
        public string Budget_Account { get; set; }
        public int Q1_Quantity { get; set; }
        public int Q1_Amount { get; set; }
        public int Q1_Budget { get; set; }
        public int Q2_Quantity { get; set; }
        public int Q2_Amount { get; set; }
        public int Q2_Budget { get; set; }
        public int Q3_Quantity { get; set; }
        public int Q3_Amount { get; set; }
        public int Q3_Budget { get; set; }
        public int Q4_Quantity { get; set; }
        public int Q4_Amount { get; set; }
        public int Q4_Budget { get; set; }
        public string Work_Plan_Project_ID { get; set; }
        public string Workplan_Task_No { get; set; }
        public string Activity_No { get; set; }
        public string Sub_Activity_No { get; set; }
        public int AGPO_Percent { get; set; }
        public int AGPO_Amount { get; set; }
        public int PWD_Percent { get; set; }
        public int PWD_Amount { get; set; }
        public int Women_Percent { get; set; }
        public int Women_Amount { get; set; }
        public int Youth_Percent { get; set; }
        public int Youth_Amount { get; set; }
        public int Margin_of_Preference { get; set; }
        public int Citizen_Contractors_Percent { get; set; }
        public string Action_Taken { get; set; }
        public string Status { get; set; }
        public bool Blocked { get; set; }
        public List<SelectListItem> ListOfVoteItems { get; set; }
        public List<SelectListItem> ListOfItems { get; set; }
        public List<SelectListItem> ListOfProcTypes { get; set; }
        public List<SelectListItem> ListOfSolicitationTypes { get; set; }
        public List<SelectListItem> ListOfSpecialVendor { get; set; }
        public List<SelectListItem> ListOfFundingSourceID { get; set; }
        public List<SelectListItem> ListOfDim1 { get; set; }
        public List<SelectListItem> ListOfDim2 { get; set; }
        public List<SelectListItem> ListOfVote { get; set; }
        public List<SelectListItem> ListOfSubProgram { get; set; }
        public List<SelectListItem> ListOfClass { get; set; }
        public List<SelectListItem> ListOfFundingSource { get; set; }
        public List<SelectListItem> ListOfBudgetAcc { get; set; }      
        public List<SelectListItem> ListOfSubActivity { get; set; }
        public List<SelectListItem> ListOfActivity { get; set; }
    }

    public class RevisionEntriesList
    {
        public string Approval_Status { get; set; }
        public List<ProcurementPlanRevisionEntries> ListOfRevisionEntriesLines { get; set; }
    }


    public class ProcurementPlanRevisionEntriesViewModel
    {
        public List<SelectListItem> ListOfVoteItems { get; set; }
        public List<SelectListItem> ListOfItems { get; set; }
        public List<SelectListItem> ListOfProcTypes { get; set; }
        public List<SelectListItem> ListOfSolicitationTypes { get; set; }
        public List<SelectListItem> ListOfSpecialVendor { get; set; }
        public List<SelectListItem> ListOfFundingSourceID { get; set; }
        public List<SelectListItem> ListOfFundingSource { get; set; }
        public List<SelectListItem> ListOfDim1 { get; set; }
        public List<SelectListItem> ListOfDim2 { get; set; }
        public List<SelectListItem> ListOfBudgetAcc { get; set; }
        public List<SelectListItem> ListOfVote { get; set; }
        public List<SelectListItem> ListOfSubProgram { get; set; }
        public List<SelectListItem> ListOfClass { get; set; }
        public List<SelectListItem> ListOfSubActivity { get; set; }
        public List<SelectListItem> ListOfActivity { get; set; }      
        public List<ProcurementPlanRevisionEntries> RevisionEntries { get; set; }
    }

    public class PurchaseOrderArchive
    {
        public string Document_Type { get; set; }
        public string No { get; set; }
        public int Doc_No_Occurrence { get; set; }
        public int Version_No { get; set; }
        public string Buy_from_Vendor_No { get; set; }
        public string Buy_from_Contact_No { get; set; }
        public string Buy_from_Vendor_Name { get; set; }
        public string Buy_from_Address { get; set; }
        public string Buy_from_Address_2 { get; set; }
        public string Buy_from_City { get; set; }
        public string Buy_from_County { get; set; }
        public string Buy_from_Post_Code { get; set; }
        public string Buy_from_Country_Region_Code { get; set; }
        public string Buy_from_Contact { get; set; }
        public string BuyFromContactPhoneNo { get; set; }
        public string BuyFromContactMobilePhoneNo { get; set; }
        public string BuyFromContactEmail { get; set; }
        public string Posting_Date { get; set; }
        public string VAT_Reporting_Date { get; set; }
        public string Order_Date { get; set; }
        public string Document_Date { get; set; }
        public string Vendor_Order_No { get; set; }
        public string Vendor_Shipment_No { get; set; }
        public string Vendor_Invoice_No { get; set; }
        public string Order_Address_Code { get; set; }
        public string Purchaser_Code { get; set; }
        public string Responsibility_Center { get; set; }
        public string Status { get; set; }
        public string Pay_to_Vendor_No { get; set; }
        public string Pay_to_Contact_No { get; set; }
        public string Pay_to_Name { get; set; }
        public string Pay_to_Address { get; set; }
        public string Pay_to_Address_2 { get; set; }
        public string Pay_to_City { get; set; }
        public string Pay_to_County { get; set; }
        public string Pay_to_Post_Code { get; set; }
        public string Pay_to_Country_Region_Code { get; set; }
        public string Pay_to_Contact { get; set; }
        public string PayToContactPhoneNo { get; set; }
        public string PayToContactMobilePhoneNo { get; set; }
        public string PayToContactEmail { get; set; }
        public string Shortcut_Dimension_1_Code { get; set; }
        public string Shortcut_Dimension_2_Code { get; set; }
        public string Payment_Terms_Code { get; set; }
        public string Due_Date { get; set; }
        public int Payment_Discount_Percent { get; set; }
        public string Payment_Method_Code { get; set; }
        public string On_Hold { get; set; }
        public bool Prices_Including_VAT { get; set; }
        public bool Tax_Liable { get; set; }
        public string Tax_Area_Code { get; set; }
        public string Ship_to_Name { get; set; }
        public string Ship_to_Address { get; set; }
        public string Ship_to_Address_2 { get; set; }
        public string Ship_to_City { get; set; }
        public string Ship_to_County { get; set; }
        public string Ship_to_Post_Code { get; set; }
        public string Ship_to_Country_Region_Code { get; set; }
        public string Ship_to_Contact { get; set; }
        public string Location_Code { get; set; }
        public string Inbound_Whse_Handling_Time { get; set; }
        public string Shipment_Method_Code { get; set; }
        public string Lead_Time_Calculation { get; set; }
        public string Requested_Receipt_Date { get; set; }
        public string Promised_Receipt_Date { get; set; }
        public string Expected_Receipt_Date { get; set; }
        public string Sell_to_Customer_No { get; set; }
        public string Ship_to_Code { get; set; }
        public string Currency_Code { get; set; }
        public string Transaction_Type { get; set; }
        public string Transaction_Specification { get; set; }
        public string Transport_Method { get; set; }
        public string Entry_Point { get; set; }
        public string Area { get; set; }
        public string Archived_By { get; set; }
        public string Date_Archived { get; set; }
        public string Time_Archived { get; set; }
        public bool Interaction_Exist { get; set; }
    }



}