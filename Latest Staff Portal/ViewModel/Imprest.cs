using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class ImprestMemo
    {
        public string No { get; set; }
        public string WarrantVoucherType { get; set; }
        public string WarrantNo { get; set; }
        public string Date { get; set; }
        public string Project { get; set; }
        public string ProjectDescription { get; set; }
        public string Subject { get; set; }
        public string Objective { get; set; }
        public string TermsOfReference { get; set; }
        public string UserID { get; set; }
        public string Requestor { get; set; }
        public string RequestorName { get; set; }
        public string DestinationName { get; set; }
        public string ImprestNaration { get; set; }
        public string Status { get; set; }
        public string GlobalDimension1Code { get; set; }
        public string DepartmentName { get; set; }
        public string GlobalDimension2Code { get; set; }
        public string ProjectName { get; set; }
        public decimal TotalSubsistenceAllowance { get; set; }
        public decimal TotalFuelCosts { get; set; }
        public decimal TotalMaintenanceCosts { get; set; }
        public decimal TotalCasualsCost { get; set; }
        public decimal TotalOtherCosts { get; set; }
        public string StrategicPlan { get; set; }
        public string ReportingYearCode { get; set; }
        public string WorkplanCode { get; set; }
        public string ActivityCode { get; set; }
        public string ExpenditureRequisitionCode { get; set; }
        public string ReasonToReopen { get; set; }
        public string From { get; set; }
        public string Destination { get; set; }
        public string TimeOut { get; set; }
        public string JourneyRoute { get; set; }
        public string WorkTypeFilter { get; set; }
        public string DimensionSetId { get; set; }
    }

    public class NewImprestRequisition
    {
        public string Dim1 { get; set; }
        public string Dim2 { get; set; }
        public string Dim3 { get; set; }
        public string Dim4 { get; set; }
        public string Dim5 { get; set; }
        public string Dim6 { get; set; }
        public string Dim7 { get; set; }
        public string Dim8 { get; set; }
        public string RespC { get; set; }
        public string UoM { get; set; }
        public string ImprestDueType { get; set; }
        public List<SelectListItem> ListOfDim1 { get; set; }
        public List<SelectListItem> ListOfDim2 { get; set; }
        public List<SelectListItem> ListOfDim3 { get; set; }
        public List<SelectListItem> ListOfDim4 { get; set; }
        public List<SelectListItem> ListOfDim5 { get; set; }
        public List<SelectListItem> ListOfDim6 { get; set; }
        public List<SelectListItem> ListOfDim7 { get; set; }
        public List<SelectListItem> ListOfDim8 { get; set; }
        public List<SelectListItem> ListOfResponsibility { get; set; }
        public List<SelectListItem> ListOfImprestDue { get; set; }
        public string Dim1Name { get; set; }
        public string Dim2Name { get; set; }
        public string Dim3Name { get; set; }
        public string Dim4Name { get; set; }
        public string Dim5Name { get; set; }
        public string Dim6Name { get; set; }
        public string Dim7Name { get; set; }
        public string Dim8Name { get; set; }
    }

    public class ImprestTypes
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class ImprestTypes2
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class ImprestTypesList
    {
        public string Code { get; set; }
        public List<SelectListItem> ListOfImprestTypes { get; set; }
        public List<SelectListItem> ListOfImprestTypes2 { get; set; }
        public List<SelectListItem> ListOfUnitMeasure { get; set; }
        public List<SelectListItem> ListOfDestination { get; set; }
        public string Days { get; set; }
    }

    public class ImprestHeader
    {
        public string No { get; set; }
        public string TravelType { get; set; }
        public string DateNeeded { get; set; }
        public string DateofTravel { get; set; }
        public string DateofReturn { get; set; }
        public string Remarks { get; set; }
        public NewImprestRequisition DocD { get; set; }
        public string Status { get; set; }
        public string CurrencyCode { get; set; }
        public string TotalAmount { get; set; }
        public string RequestorNo { get; set; }
        public string RequestorName { get; set; }
        public string AccountNo { get; set; }
        public string Dim1 { get; set; }
        public string Dim2 { get; set; }
        public string Dim3 { get; set; }
        public string Dim4 { get; set; }
        public string Dim5 { get; set; }
        public string Dim6 { get; set; }
        public string Dim7 { get; set; }
        public string RespC { get; set; }
        public string ImprestDueType { get; set; }
        public string Dim1Name { get; set; }
        public string Dim2Name { get; set; }
        public string Dim3Name { get; set; }
        public string Dim4Name { get; set; }
        public string Dim5Name { get; set; }
        public string Dim6Name { get; set; }
        public string Dim7Name { get; set; }
        public string Dim8Name { get; set; }
        public List<ImprestLine> ListOfWorkshopLines { get; set; }
    }

    public class SafariTeam
    {
        public string ImprestMemoNo { get; set; }
        public int LineNo { get; set; }
        public string WorkType { get; set; }
        public string Type { get; set; }
        public string TypeOfExpense { get; set; }
        public string No { get; set; }
        public string GLAccount { get; set; }
        public string TaskNo { get; set; }
        public string Name { get; set; }
        public string UnitOfMeasure { get; set; }
        public string CurrencyCode { get; set; }
        public int TimePeriod { get; set; }
        public decimal DirectUnitCost { get; set; }
        public decimal Entitlement { get; set; }
        public decimal TransportCosts { get; set; }
        public decimal TotalEntitlement { get; set; }
        public decimal OutstandingAmount { get; set; }
        public string TasksToCarryOut { get; set; }
        public string ExpectedOutput { get; set; }
        public decimal Delivery { get; set; }
        public bool ProjectLead { get; set; }
    }

    public class ImprestLine
    {
        public string DocumentNo { get; set; }
        public int LineNo { get; set; }
        public string Payee { get; set; }
        public string EmployeeNo { get; set; }
        public List<SelectListItem> ListOfEmployees { get; set; }

        public string EmployeeName { get; set; }
        public string JobGroup { get; set; }
        public string GLAccount { get; set; }
        public string Destination { get; set; }
        public List<SelectListItem> ListOfDestinations { get; set; }

        public string VoteItem { get; set; }
        public List<SelectListItem> ListOfClaims { get; set; }
        public int Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public string RecalledBy { get; set; }
        public string RecalledOn { get; set; }
        public string SourceLineNo { get; set; }

    }

    public class ImprestLinesList
    {
        public string Status { get; set; }
        public List<ImprestLine> ListOfImprestLines { get; set; }
    }

    public class SafariTeamList
    {
        public string Status { get; set; }
        public List<SafariTeam> ListOfSafariTeam { get; set; }
    }

    public class ImprestItemDetails
    {
        public string Code { get; set; }
        public List<SelectListItem> ListOfImprestTypes { get; set; }
        public List<SelectListItem> ListOfImprestTypes2 { get; set; }
        public List<SelectListItem> ListOfUoM { get; set; }
        public List<SelectListItem> ListOfDestination { get; set; }
        public ImprestLine ItemDetails { get; set; }
        public string Description { get; set; }
        public string Amount { get; set; }
        public string Quantity { get; set; }
        public string NoofDays { get; set; }
    }

    public class ImprestDocument
    {
        public ImprestHeader DocHeader { get; set; }
        public List<ImprestLine> ListOfImprestLines { get; set; }
    }

    public class StandingImprest
    {
        public string No { get; set; }
        public string Date { get; set; }
        public string PostingDate { get; set; }
        public string StandingImprestType { get; set; }
        public string ChequeDate { get; set; }
        public string PayingBankAccount { get; set; }
        public string BankName { get; set; }
        public string PaymentNarration { get; set; }
        public string CurrencyCode { get; set; }
        public string TotalAmount { get; set; }
        public string ShortcutDimension1Code { get; set; }
        public string CourtStation { get; set; }
        public string DepartmentName { get; set; }
        public string ShortcutDimension2Code { get; set; }
        public string AdminUnit { get; set; }
        public string ProjectName { get; set; }
        public string StrategicPlan { get; set; }
        public string ReportingYearCode { get; set; }
        public string WorkplanCode { get; set; }
        public string ActivityCode { get; set; }
        public string Status { get; set; }
        public bool Posted { get; set; }
        public string PostedBy { get; set; }
        public string PostedDate { get; set; }
        public string PayMode { get; set; }
        public string ChequeNo { get; set; }
        public string Payee { get; set; }
        public string CreatedBy { get; set; }
        public decimal TotalAmountLCY { get; set; }
        public string Project { get; set; }
        public string ProjectDescription { get; set; }
        public string DimensionSetId { get; set; }
        public string AvailableAmount { get; set; }
        public string CommittedAmount { get; set; }
        public string AieReceipt { get; set; }
        public string AieReceiptAmount { get; set; }
        public string ExpenditureRequisitionCode { get; set; }
        public string ValidatedBankName { get; set; }


    }

    public class StandingImprestLine
    {
        public string No { get; set; }
        public int LineNo { get; set; }
        public string AccountType { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string ShortcutDimension1Code { get; set; }
        public string ShortcutDimension2Code { get; set; }
        public string PayeeBankCode { get; set; }
        public string PayeeBankName { get; set; }
        public string PayeeBankBranchCode { get; set; }
        public string PayeeBankBranchName { get; set; }
        public string PayeeBankAccountNo { get; set; }
        public string PayeeBankAccName { get; set; }
        public string Status { get; set; }
        public string ValidatedBankName { get; set; }
    }

    public class ImprestWarranties
    {
        public string No { get; set; }
        public string Date { get; set; }
        public string PostingDate { get; set; }
        public string AccountType { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public string ReferenceNo { get; set; }
        public string PayMode { get; set; }
        public string ChequeNo { get; set; }
        public string PayingBankAccount { get; set; }
        public string BankName { get; set; }
        public string TravelDate { get; set; }
        public string PaymentNarration { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public string StrategicPlan { get; set; }
        public string ReportingYearCode { get; set; }
        public string WorkplanCode { get; set; }
        public string ActivityCode { get; set; }
        public string ExpenditureRequisitionCode { get; set; }
        public string ImprestMemoNo { get; set; }
        public string ShortcutDimension1Code { get; set; }
        public string LocationName { get; set; }
        public string AdminUnitName { get; set; }
        public string ShortcutDimension2Code { get; set; }
        public string ImprestAmount { get; set; }
        public string ImprestDeadline { get; set; }
        public string DimensionSetId { get; set; }
        public string Payee { get; set; }
        public string PayeeBankAccount { get; set; }
        public string PayeeBankBranch { get; set; }
        public string PayeeBankCode { get; set; }
        public string PayeeBankName { get; set; }
        public string PayeeBranchName { get; set; }
        public string PayeeContact { get; set; }
        public string Posted { get; set; }
        public string AvailableAmount { get; set; }
        public string CommittedAmount { get; set; }
        public string AieReceipt { get; set; }
        public string AieReceiptAmount { get; set; }
        public string ValidatedBankName { get; set; }
    }

    public class WarrantImprestLines
    {
        public string No { get; set; }
        public int LineNo { get; set; }
        public string AdvanceType { get; set; }
        public string AccountType { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public string Purpose { get; set; }
        public decimal DailyRate { get; set; }
        public int NoOfDays { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Amount { get; set; }
        public string Project { get; set; }
        public string TaskNo { get; set; }
    }

    public class WarrantImprestLinesList
    {
        public string Status { get; set; }
        public List<WarrantImprestLines> ListOfWarrantImprestLines { get; set; }
    }

    public class SpecialImprest
    {
        public string No { get; set; }
        public string Date { get; set; }
        public string PostingDate { get; set; }
        public string StandingImprestType { get; set; }
        public string ChequeDate { get; set; }
        public string PayingBankAccount { get; set; }
        public string BankName { get; set; }
        public string PaymentNarration { get; set; }
        public string CurrencyCode { get; set; }
        public string TotalAmount { get; set; }
        public string ShortcutDimension1Code { get; set; }
        public string ShortcutDimension2Code { get; set; }
        public string StrategicPlan { get; set; }
        public string ReportingYearCode { get; set; }
        public string WorkplanCode { get; set; }
        public string ActivityCode { get; set; }
        public string ExpenditureRequisitionCode { get; set; }
        public string Status { get; set; }
        public bool Posted { get; set; }
        public string PostedBy { get; set; }
        public string PostedDate { get; set; }
        public string PayMode { get; set; }
        public string Payee { get; set; }
        public string DimensionSetId { get; set; }
        public string AvailableAmount { get; set; }
        public string CommittedAmount { get; set; }
        public string AieReceipt { get; set; }
        public string AieReceiptAmount { get; set; }
        public string ValidatedBankName { get; set; }

    }
}