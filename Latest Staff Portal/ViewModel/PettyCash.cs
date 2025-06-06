using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class PettyCashList
    {
        public string No { get; set; }
        public string ReqDate { get; set; }
        public string Payment_Narration { get; set; }
        public string Function { get; set; }
        public string BudgetCeter { get; set; }
        public string Status { get; set; }
        public string TotalAmount { get; set; }
    }

    public class NewPettyCashRequisition
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
        public string Dim1Name { get; set; }
        public string Dim2Name { get; set; }
        public string Dim3Name { get; set; }
        public string Dim4Name { get; set; }
        public string Dim5Name { get; set; }
        public string Dim6Name { get; set; }
        public string Dim7Name { get; set; }
        public string Dim8Name { get; set; }
        public string UoM { get; set; }
        public string PettyCashDueType { get; set; }
        public List<SelectListItem> ListOfDim1 { get; set; }
        public List<SelectListItem> ListOfDim2 { get; set; }
        public List<SelectListItem> ListOfDim3 { get; set; }
        public List<SelectListItem> ListOfDim4 { get; set; }
        public List<SelectListItem> ListOfDim5 { get; set; }
        public List<SelectListItem> ListOfDim6 { get; set; }
        public List<SelectListItem> ListOfDim7 { get; set; }
        public List<SelectListItem> ListOfDim8 { get; set; }

        public List<SelectListItem> ListOfResponsibility { get; set; }
        public List<SelectListItem> ListOfPettyCashDue { get; set; }
    }

    public class PettyCashTypes
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class PettyCashTypesList
    {
        public string Code { get; set; }
        public List<SelectListItem> ListOfPettyCashTypes { get; set; }
        public List<SelectListItem> ListOfUnitMeasure { get; set; }
        public List<SelectListItem> ListOfDestination { get; set; }
    }

    public class PettyCashHeader
    {
        public string No { get; set; }
        public string DateNeeded { get; set; }
        public string Remarks { get; set; }
        public NewPettyCashRequisition DocD { get; set; }
        public string Status { get; set; }
        public string TotalAmount { get; set; }
        public string RequestorNo { get; set; }
        public string RequestorName { get; set; }
        public string Dim1 { get; set; }
        public string Dim2 { get; set; }
        public string Dim3 { get; set; }
        public string Dim4 { get; set; }
        public string Dim5 { get; set; }
        public string Dim6 { get; set; }
        public string Dim7 { get; set; }
        public string Dim8 { get; set; }
        public string Dim1Name { get; set; }
        public string Dim2Name { get; set; }
        public string Dim3Name { get; set; }
        public string Dim4Name { get; set; }
        public string Dim5Name { get; set; }
        public string Dim6Name { get; set; }
        public string Dim7Name { get; set; }
        public string Dim8Name { get; set; }
        public string PettyCashDueType { get; set; }
        public string RespC { get; set; }
    }

    public class PettyCashLines
    {
        public string DocNo { get; set; }
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

    public class PettyCashLinesList
    {
        public string Status { get; set; }
        public List<PettyCashLines> ListOfPettyCashLines { get; set; }
    }

    public class PettyCashItemDetails
    {
        public string Code { get; set; }
        public List<SelectListItem> ListOfPettyCashTypes { get; set; }
        public List<SelectListItem> ListOfUoM { get; set; }
        public List<SelectListItem> ListOfDestination { get; set; }
        public PettyCashLines ItemDetails { get; set; }
    }

    public class PettyCashDocument
    {
        public PettyCashHeader DocHeader { get; set; }
        public List<PettyCashLines> ListOfPettyCashLines { get; set; }
    }
}