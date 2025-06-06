using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class StoreRequisition
    {
        public string DocumentType { get; set; }
        public string No { get; set; }
        public string RequesterID { get; set; }
        public string RequestByNo { get; set; }
        public string RequestByName { get; set; }
        public string ShortcutDimension1Code { get; set; }
        public List<SelectListItem> ListOfDim1 { get; set; }

        public List<SelectListItem> ListOfDim2 { get; set; }

        public string ShortcutDimension2Code { get; set; }
        public string ResponsibilityCenter { get; set; }
        public string Status { get; set; }
        public string LocationCode { get; set; }
        public DateTime OrderDate { get; set; }
        public string Description { get; set; }
        public string RequisitionType { get; set; }
        public string BudgetItem { get; set; }
        public string CurrencyCode { get; set; }
        public List<SelectListItem> ListOfDepartment { get; set; }
        public List<SelectListItem> ListOfDivision { get; set; }
        public List<SelectListItem> ListOfSection { get; set; }
        public List<SelectListItem> ListOfDim3 { get; set; }
        public List<SelectListItem> ListOfDim4 { get; set; }
        public List<SelectListItem> ListOfDim5 { get; set; }
        public List<SelectListItem> ListOfDim6 { get; set; }
        public List<SelectListItem> ListOfDim7 { get; set; }
        public List<SelectListItem> ListOfDim8 { get; set; }
        public List<SelectListItem> ListOfResponsibility { get; set; }
        public List<SelectListItem> ListOfLocations { get; set; }
    }

    public class NewStoreRequisition
    {
        public string Department { get; set; }
        public string Division { get; set; }
        public string Section { get; set; }
        public string Dim3 { get; set; }
        public string Dim4 { get; set; }
        public string Dim5 { get; set; }
        public string Dim6 { get; set; }
        public string Dim7 { get; set; }
        public string Dim1Name { get; set; }
        public string Dim2Name { get; set; }
        public string Dim3Name { get; set; }
        public string Dim4Name { get; set; }
        public string Dim5Name { get; set; }
        public string Dim6Name { get; set; }
        public string Dim7Name { get; set; }
        public string Dim8Name { get; set; }
        public string RespC { get; set; }
        public List<SelectListItem> ListOfDepartment { get; set; }
        public List<SelectListItem> ListOfDivision { get; set; }
        public List<SelectListItem> ListOfSection { get; set; }
        public List<SelectListItem> ListOfDim3 { get; set; }
        public List<SelectListItem> ListOfDim4 { get; set; }
        public List<SelectListItem> ListOfDim5 { get; set; }
        public List<SelectListItem> ListOfDim6 { get; set; }
        public List<SelectListItem> ListOfDim7 { get; set; }
        public List<SelectListItem> ListOfDim8 { get; set; }
        public List<SelectListItem> ListOfResponsibility { get; set; }
        public string IssuingStore { get; set; }
        public List<SelectListItem> ListOfLocations { get; set; }
    }


    public class StoreRequisitionLine
    {
        public string DocumentType { get; set; }
        public string DocumentNo { get; set; }
        public int LineNo { get; set; }
        public string ItemCategory { get; set; }
        public List<SelectListItem> ListOfProcurementCategories { get; set; }
        public List<SelectListItem> ListOfItems { get; set; }
        public string ServiceItemCode { get; set; }
        public string Type { get; set; }
        public string No { get; set; }
        public List<SelectListItem> ListOfStandardTextCodes { get; set; }
        public string Description { get; set; }
        public string LocationCode { get; set; }
        public string VariantCode { get; set; }
        public string UnitOfMeasureCode { get; set; }
        public List<SelectListItem> UnitsOfMeasure { get; set; }
        public int QtyRequested { get; set; }
    }

    public class StoreRequisitionLineList
    {
        public string Status { get; set; }
        public List<StoreRequisitionLine> ListOfStoreRequisitionLine { get; set; }
    }

    public class StoreItemDetails
    {
        public List<SelectListItem> ListOfLocations { get; set; }
        public StoreRequisitionLine ItemDetails { get; set; }
    }

    public class StoreDocument
    {
        public StoreRequisition DocHeader { get; set; }
        public List<StoreRequisitionLine> ListOfStoreRequisitionLine { get; set; }
    }

    public class PartiallyIssuedStoreRequisition
    {
        public string DocumentType { get; set; }
        public string No { get; set; }
        public string RequesterID { get; set; }
        public string RequestByNo { get; set; }
        public string RequestByName { get; set; }
        public List<SelectListItem> LocationCode { get; set; }
        public DateTime OrderDate { get; set; }
        public string Description { get; set; }
        public string RequisitionType { get; set; }
        public string CurrencyCode { get; set; }
        public string Status { get; set; }
        public string ShortcutDimension1Code { get; set; }
        public string DepartmentName { get; set; }
        public string ShortcutDimension2Code { get; set; }
        public string ProjectName { get; set; }
        public string ProgramName { get; set; }

        public string SectionName { get; set; }
        public string ShortcutDimension3Code { get; set; }
        public string UnitName { get; set; }
        public decimal RequisitionAmount { get; set; }
        public int NoOfArchivedVersions { get; set; }
    }

    public class PartiallyIssuedStoreRequisitionList
    {
        public string Document_Type { get; set; }
        public string Document_No { get; set; }
        public int Line_No { get; set; }
        public bool Select { get; set; }
        public string Category { get; set; }
        public string Item_Category_Code { get; set; }
        public string Service_Item_Code { get; set; }
        public string Type { get; set; }
        public string No { get; set; }
        public string Description { get; set; }
        public string Variant_Code { get; set; }
        public string Employee_No { get; set; }
        public string Employee_Name { get; set; }
        public string Unit_of_Measure_Code { get; set; }
        public int Qty_Requested { get; set; }
        public int Quantity_issued { get; set; }
        public int Quantity_To_Issue { get; set; }
        public string Remarks { get; set; }
        public int Quantity_In_Store { get; set; }
        public int Qty_on_Purch_Order { get; set; }
        public string Location_Code { get; set; }
    }
}