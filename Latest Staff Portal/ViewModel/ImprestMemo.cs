using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class ImprestMemoList
    {
        public string No { get; set; }
        public string ReqDate { get; set; }
        public string Purpose { get; set; }
        public string Function { get; set; }
        public string BudgetCeter { get; set; }
        public string Status { get; set; }
    }

    public class NewImprestMemoRequisition
    {
        public string Dim1 { get; set; }
        public string Dim2 { get; set; }
        public string Dim3 { get; set; }
        public string Dim4 { get; set; }
        public string Dim5 { get; set; }
        public string RespC { get; set; }
        public string UoM { get; set; }
        public string ImprestDueType { get; set; }
        public List<SelectListItem> ListOfDim1 { get; set; }
        public List<SelectListItem> ListOfDim2 { get; set; }
        public List<SelectListItem> ListOfDim3 { get; set; }
        public List<SelectListItem> ListOfDim4 { get; set; }
        public List<SelectListItem> ListOfDim5 { get; set; }
        public List<SelectListItem> ListOfResponsibility { get; set; }
        public List<SelectListItem> ListOfImprestDue { get; set; }
        public List<SelectListItem> ListOfEmployeeList { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<SelectListItem> ListOfEmployeeLists { get; set; }
    }

    public class ImprestMemoTypes
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class EmployeeList
    {
        public string No { get; set; }
        public string Name { get; set; }
    }

    public class ImprestMemoTypes2
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class ImprestMemoTypesList
    {
        public string Code { get; set; }
        public string Code2 { get; set; }
        public List<SelectListItem> ListOfImprestTypes { get; set; }
        public List<SelectListItem> ListOfUnitMeasure { get; set; }
        public List<SelectListItem> ListOfDestination { get; set; }
        public List<SelectListItem> ListOfEmployeeList { get; set; }
        public List<SelectListItem> ListOfImprestTypes2 { get; set; }
    }

    public class ImprestMemoHeader
    {
        public string No { get; set; }
        public string DateNeeded { get; set; }
        public string DocumentDate { get; set; }
        public string Remarks { get; set; }
        public NewImprestMemoRequisition DocD { get; set; }
        public string Status { get; set; }
        public string TotalAmount { get; set; }
        public string RequestorNo { get; set; }
        public string RequestorName { get; set; }
        public string Dim1 { get; set; }
        public string Dim2 { get; set; }
        public string Dim3 { get; set; }
        public string Dim4 { get; set; }
        public string Dim5 { get; set; }
        public string ImprestDueType { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<SelectListItem> ListOfEmployeeList { get; set; }
        public List<SelectListItem> ListOfImprestDue { get; set; }
    }

    public class ImprestMemoLines
    {
        public string DocNo { get; set; }
        public string EmployeeNo { get; set; }
        public string AdvanceType { get; set; }
        public string Item { get; set; }
        public string ItemDesc { get; set; }
        public string ItemDesc2 { get; set; }
        public string Quantity { get; set; }
        public string UnitAmount { get; set; }
        public string Amount { get; set; }
        public string UoN { get; set; }
        public string Desination { get; set; }
        public string LnNo { get; set; }
    }

    public class ImprestMemoLinesList
    {
        public string Status { get; set; }
        public List<ImprestMemoLines> ListOfImprestMemoLines { get; set; }
    }

    public class ImprestMemoItemDetails
    {
        public string Code { get; set; }
        public List<SelectListItem> ListOfImprestTypes { get; set; }
        public List<SelectListItem> ListOfImprestTypes2 { get; set; }
        public List<SelectListItem> ListOfUoM { get; set; }
        public List<SelectListItem> ListOfDestination { get; set; }
        public ImprestMemoLines ItemDetails { get; set; }
    }

    public class ImprestMemoItemsList
    {
        public string Status { get; set; }
        public List<ImprestMemoLines> ListOfImprestMemoLines { get; set; }
    }

    public class ImprestMemoDocument
    {
        public ImprestMemoHeader DocHeader { get; set; }
        public NewImprestMemoRequisition DocD { get; set; }
        public List<ImprestMemoLines> ListOfImprestMemoLines { get; set; }
        public List<ImprestMemoNonStaff> ListOfImprestMemoNonStaff { get; set; }
        public List<ImprestMemoItems> ListOfImprestMemoItemsList { get; set; }
    }

    public class ImprestMemoNonStaff
    {
        public string Names { get; set; }
        public string ImprestMemoNo { get; set; }
        public string LineNo { get; set; }
        public string Organization { get; set; }
        public string Designation { get; set; }
    }

    public class ImprestMemoNonStaffList
    {
        public ImprestMemoNonStaff NonStaff { get; set; }
        public List<ImprestMemoNonStaff> ListOfNonstaff { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }
    }

    public class ImprestMemoItems
    {
        public string DocNo { get; set; }
        public string EmployeeNo { get; set; }
        public string AdvanceType { get; set; }
        public string Item { get; set; }
        public string ItemDesc { get; set; }
        public string ItemDesc2 { get; set; }
        public string Quantity { get; set; }
        public string UnitAmount { get; set; }
        public string Amount { get; set; }
        public string UoN { get; set; }
        public string Desination { get; set; }
        public string LnNo { get; set; }
    }
}