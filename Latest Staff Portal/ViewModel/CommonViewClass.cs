using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class DocumentNumber
    {
        public string Code { get; set; }
        public string Status { get; set; }
        public string DocType { get; set; }
    }

    public class RespCenter
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class ImpDuetyp
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class AcademicYearList
    {
        public string Code { get; set; }
    }

    public class ApprovalEntries
    {
        public string DocNo { get; set; }
        public string UserID { get; set; }
        public string DateSendForApproval { get; set; }
        public string DueDate { get; set; }
        public string Status { get; set; }
        public int Sequence { get; set; }
    }

    public class DimensionValues
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string DName { get; set; }
        public List<SelectListItem> ListOfDim1 { get; set; }
        public List<SelectListItem> ListOfDim2 { get; set; }
        public List<SelectListItem> ListOfDim3 { get; set; }
        public List<SelectListItem> ListOfDim4 { get; set; }
        public List<SelectListItem> ListOfDim5 { get; set; }
        public List<SelectListItem> ListOfDim6 { get; set; }
        public List<SelectListItem> ListOfDim7 { get; set; }
        public List<SelectListItem> ListOfDim8 { get; set; }
    }

    public class SemesterList
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
    public class BankDetails
    {
        public string BankCode { get; set; }
        public string BankBranch { get; set; }
        public string BankAccount { get; set; }
    }
    public class Locations
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class LocationList
    {
        public string Code { get; set; }
        public List<SelectListItem> ListOfLocations { get; set; }
        public List<SelectListItem> ListOfProcurementTypes { get; set; }

        public List<SelectListItem> ListOfSolicitationType { get; set; }

        public List<SelectListItem> ListOfVendorCategory { get; set; }

        public List<SelectListItem> ListOfFundingSource { get; set; }

        public List<SelectListItem> ListOfContracts { get; set; }
    }

    public class NewStoreLine
    {
        public string Code { get; set; }
        public List<SelectListItem> ListOfItems { get; set; }
        public List<SelectListItem> ListOfLocations { get; set; }
    }

    public class CommonDropDownList
    {
        public string BankCode { get; set; }
        public List<SelectListItem> ListOfSchools { get; set; }
        public List<SelectListItem> ListOfDepartments { get; set; }
        public List<SelectListItem> ListOfCampus { get; set; }
        public List<SelectListItem> ListOfRespC { get; set; }
        public List<SelectListItem> ListOfBankCodes { get; set; }
    }

    public class DropdownList
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }

    public class DropdownListData
    {
        public List<SelectListItem> ListOfddlData { get; set; }
    }

    public class ListOfGlBudge
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class FAQs
    {
        public string No { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public DateTime Created_At { get; set; }
    }

    public class NoticeBoard
    {
        public int No { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Priority { get; set; }
        public DateTime Date_Posted { get; set; }
        public DateTime Expiration_Date { get; set; }
    }

    public class NotificationsViewModel
    {
        public List<NoticeBoard> Notices { get; set; }
        public List<NoticeBoard> Memos { get; set; }
    }

    public class Feedback
    {
        public string No { get; set; }
        public string Employee_No { get; set; }
        public string Employee_Name { get; set; }
        public string Implementing_Unit { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
    }
    public class ValueText
    {
        public string value { get; set; }
    }
    public class EdmsDocuments
    {
        public int LineNo { get; set; }
        public string DocumentNo { get; set; }
        public string Link { get; set; }
        public string Module { get; set; }
        public string FileType { get; set; }
        public string User { get; set; }
        public string FileName { get; set; }
        public string DocumentId { get; set; }
        public string OrderNo { get; set; }
    }
}