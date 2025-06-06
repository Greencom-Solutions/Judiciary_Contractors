using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Latest_Staff_Portal.CustomSecurity;
using Latest_Staff_Portal.Models;
using Latest_Staff_Portal.ViewModel;
using Newtonsoft.Json.Linq;

namespace Latest_Staff_Portal.Controllers
{
    public class ContractsController : Controller
    {
        // GET: Contracts
        public ActionResult ContractsList()
        {
            try
            {
                if (Session["Username"] == null)
                    return RedirectToAction("Login", "Home");
                return View();
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
 
        }

        public PartialViewResult ContractsListPartialView()

        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<ContractsList> contracts = new List<ContractsList>();

            string staffName = employeeView.Name;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            string page = ""; // Declare 'page' once at the top.
            /*page = $"Contract?$filter=Created_By eq '{employeeView.UserID}'&$format=json";*/
            page = $"Contract?$filter=Document_Type eq 'Blanket Order' and Status eq 'Open' and Contract_Status eq 'signed'&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    ContractsList contract = new ContractsList
                    {
                        Document_Type = (string)config["Document_Type"],
                        No = (string)config["No"],
                        Buy_from_Vendor_No = (string)config["Buy_from_Vendor_No"],
                        Buy_from_Vendor_Name = (string)config["Buy_from_Vendor_Name"],
                        VAT_Registration_No = (string)config["VAT_Registration_No"],
                        Buy_from_Address = (string)config["Buy_from_Address"],
                        Buy_from_Address_2 = (string)config["Buy_from_Address_2"],
                        Buy_from_City = (string)config["Buy_from_City"],
                        Buy_from_Contact = (string)config["Buy_from_Contact"],
                        Buy_from_Country_Region_Code = (string)config["Buy_from_Country_Region_Code"],
                        Language_Code = (string)config["Language_Code"],
                        Contract_Description = (string)config["Contract_Description"],
                        Contract_Start_Date = (string)config["Contract_Start_Date"],
                        Contract_End_Date = (string)config["Contract_End_Date"],
                        Notice_of_Award_No = (string)config["Notice_of_Award_No"],
                        Awarded_Bid_No = (string)config["Awarded_Bid_No"],
                        Award_Tender_Sum_Inc_Taxes = (int)config["Award_Tender_Sum_Inc_Taxes"],
                        IFS_Code = (string)config["IFS_Code"],
                        Tender_Name = (string)config["Tender_Name"],
                        Contract_Type = (string)config["Contract_Type"],
                        Governing_Laws = (string)config["Governing_Laws"],
                        Contract_Status = (string)config["Contract_Status"],
                        Procuring_Entity_PE_Name = (string)config["Procuring_Entity_PE_Name"],
                        PE_Representative = (string)config["PE_Representative"],
                        Your_Reference = (string)config["Your_Reference"]


                    };
                    contracts.Add(contract);

                }
            }
            return PartialView("~/Views/Contracts/PartialView/ContractsListPartialView.cshtml", contracts);
        }

        [HttpPost]
        public ActionResult ContractsDocumentView(string No)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            List<Contracts> Contracts = new List<Contracts>();

            string page = $"Contract?$filter=No eq '{No}'&$format=json";
            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                var config = details["value"].FirstOrDefault();
                if (config != null)
                {
                    Contracts contract = new Contracts
                    {
                        No = (string)config["No"],
                        Document_Type = (string)config["Document_Type"],
                        Document_Date = (string)config["Document_Date"],
                        Contract_Description = (string)config["Contract_Description"],
                        Contract_Start_Date = (string)config["Contract_Start_Date"],
                        Contract_Duration = (string)config["Contract_Duration"],
                        Contract_End_Date = (string)config["Contract_End_Date"],
                        Institution = (string)config["Institution"],
                        Party_Description = (string)config["Party_Description"],
                        Vendor_Party = (string)config["Vendor_Party"],
                        Company_Head = (string)config["Company_Head"],
                        Mou_Description = (string)config["Mou_Description"],
                        Scope_of_Service = (string)config["Scope_of_Service"],
                        Scope_Description = (string)config["Scope_Description"],
                        Vendor_Description = (string)config["Vendor_Description"],
                        Notice_of_Award_No = (string)config["Notice_of_Award_No"],
                        Awarded_Bid_No = (string)config["Awarded_Bid_No"],
                        Award_Tender_Sum_Inc_Taxes = (int?)config["Award_Tender_Sum_Inc_Taxes"] ?? 0,
                        IFS_Code = (string)config["IFS_Code"],
                        Tender_Name = (string)config["Tender_Name"],
                        Serial_No = (string)config["Serial_No"],
                        Type = (string)config["Type"],
                        Location = (string)config["Location"],
                        Area_Space = (int?)config["Area_Space"] ?? 0,
                        Payment_Status = (string)config["Payment_Status"],
                        Job = (string)config["Job"],
                        Contract_Type = (string)config["Contract_Type"],
                        Governing_Laws = (string)config["Governing_Laws"],
                        Contract_Status = (string)config["Contract_Status"],
                        Status = (string)config["Status"],
                        Procuring_Entity_PE_Name = (string)config["Procuring_Entity_PE_Name"],
                        PE_Representative = (string)config["PE_Representative"],
                        Amount = (int?)config["Amount"] ?? 0,
                        Due_Date = (string)config["Due_Date"],
                        Your_Reference = (string)config["Your_Reference"],
                        Reason_For_ammendment = (string)config["Reason_For_ammendment"],
                        Renewed = (bool?)config["Renewed"] ?? false,
                        Renewed_Date = (string)config["Renewed_Date"],
                        Shortcut_Dimension_1_Code = (string)config["Shortcut_Dimension_1_Code"],
                        Shortcut_Dimension_2_Code = (string)config["Shortcut_Dimension_2_Code"],
                        Shortcut_Dimension_3_Code = (string)config["Shortcut_Dimension_3_Code"],
                        Preliminary_Evaluation_Date = (string)config["Preliminary_Evaluation_Date"],
                        Buy_from_Vendor_No = (string)config["Buy_from_Vendor_No"],
                        Buy_from_Vendor_Name = (string)config["Buy_from_Vendor_Name"],
                        VAT_Registration_No = (string)config["VAT_Registration_No"],
                        Buy_from_Address = (string)config["Buy_from_Address"],
                        Buy_from_Address_2 = (string)config["Buy_from_Address_2"],
                        Buy_from_Post_Code = (string)config["Buy_from_Post_Code"],
                        Buy_from_City = (string)config["Buy_from_City"],
                        Buy_from_Contact_No = (string)config["Buy_from_Contact_No"],
                        Buy_from_Country_Region_Code = (string)config["Buy_from_Country_Region_Code"],
                        Language_Code = (string)config["Language_Code"]


                    };
                    Contracts.Add(contract);
                }
            }
            return PartialView(Contracts.FirstOrDefault());
        }

        public PartialViewResult ContractsLines(string No)
        {
            try
            {
                List<ContractLines> contractLines = new List<ContractLines>();

                string page = "contractLines?$filter=Document_No eq '" + No + "'&$format=json";
                HttpWebResponse httpResponse = Credentials.GetOdataData(page);
                using var streamReader = new StreamReader(httpResponse.GetResponseStream());
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);

                foreach (JObject config in details["value"])
                {

                    // Create the Lines object
                    ContractLines line = new ContractLines
                    {
                        Document_No = (string)config["Document_No"],
                        Line_No = (int)config["Line_No"],
                        Type = (string)config["Type"],
                        No = (string)config["No"],
                        Cross_Reference_No = (string)config["Cross_Reference_No"],
                        Variant_Code = (string)config["Variant_Code"],
                        VAT_Prod_Posting_Group = (string)config["VAT_Prod_Posting_Group"],
                        Description = (string)config["Description"],
                        Quantity = (int)config["Quantity"],
                        Unit_of_Measure_Code = (string)config["Unit_of_Measure_Code"],
                        Unit_of_Measure = (string)config["Unit_of_Measure"],
                        Fee_Type = (string)config["Fee_Type"],
                        Direct_Unit_Cost = (int)config["Direct_Unit_Cost"],
                        Indirect_Cost_Percent = (int)config["Indirect_Cost_Percent"],
                        Unit_Cost_LCY = (int)config["Unit_Cost_LCY"],
                        Budget = (string)config["Budget"],
                        Budget_Line = (string)config["Budget_Line"],
                        Shortcut_Dimension_1_Code = (string)config["Shortcut_Dimension_1_Code"],
                        Unit_Price_LCY = (int)config["Unit_Price_LCY"],
                        Line_Discount_Percent = (int)config["Line_Discount_Percent"],
                        Line_Amount = (int)config["Line_Amount"],
                        Line_Discount_Amount = (int)config["Line_Discount_Amount"],
                        Location_Code = (string)config["Location_Code"],
                        Allow_Invoice_Disc = (bool)config["Allow_Invoice_Disc"],
                        Qty_to_Receive = (int)config["Qty_to_Receive"],
                        Quantity_Received = (int)config["Quantity_Received"],
                        Quantity_Invoiced = (int)config["Quantity_Invoiced"],
                        Expected_Receipt_Date = (string)config["Expected_Receipt_Date"],
                        Shortcut_Dimension_2_Code = (string)config["Shortcut_Dimension_2_Code"],
                        ShortcutDimCode_x005B_3_x005D_ = (string)config["ShortcutDimCode_x005B_3_x005D_"],
                        ShortcutDimCode_x005B_4_x005D_ = (string)config["ShortcutDimCode_x005B_4_x005D_"],
                        ShortcutDimCode_x005B_5_x005D_ = (string)config["ShortcutDimCode_x005B_5_x005D_"],
                        ShortcutDimCode_x005B_6_x005D_ = (string)config["ShortcutDimCode_x005B_6_x005D_"],
                        ShortcutDimCode_x005B_7_x005D_ = (string)config["ShortcutDimCode_x005B_7_x005D_"],
                        ShortcutDimCode_x005B_8_x005D_ = (string)config["ShortcutDimCode_x005B_8_x005D_"],
                        AmountBeforeDiscount = (double)config["AmountBeforeDiscount"],
                        Invoice_Discount_Amount = (int)config["Invoice_Discount_Amount"],
                        Invoice_Disc_Pct = (int)config["Invoice_Disc_Pct"],
                        Total_Amount_Excl_VAT = (double)config["Total_Amount_Excl_VAT"],
                        Total_VAT_Amount = (double)config["Total_VAT_Amount"],
                        Total_Amount_Incl_VAT = (int)config["Total_Amount_Incl_VAT"]

                    };

                    // Add the object to the list
                    contractLines.Add(line);
                }
               
                return PartialView("~/Views/Contracts/PartialView/ContractsLines.cshtml", contractLines);
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }



    }
}