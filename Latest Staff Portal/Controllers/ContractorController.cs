using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using System.Web.Services.Description;
using Latest_Staff_Portal.Models;
using Latest_Staff_Portal.ViewModel;
using Newtonsoft.Json.Linq;

namespace Latest_Staff_Portal.Controllers
{
    public class ContractorController : Controller
    {

        //contractor extension
        public ActionResult ContractorExtensionRequests()
        {
            try
            {
                if (Session["Username"] == null)
                    return RedirectToAction("Login", "Login");
                return View();
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }
        public PartialViewResult ContractorExtensionRequestsListPartialView(string status)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<ContractorExtensionRequests> ExtensionRequestsList = new List<ContractorExtensionRequests>();

            string title = status;

            ViewBag.title = title;


            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            string page = ""; // Declare 'page' once at the top.
            /*page = $"Contract?$filter=Created_By eq '{employeeView.UserID}'&$format=json";*/
            if (status=="Contractor")
            {
                page = $"ExtensionRequestCard?$filter=Document_Type eq 'Extension' and SCM_Status eq '{status}' and Contractor_No eq '{employeeView.No}'&$format=json";
            }
            else {
                page = $"ExtensionRequestCard?$filter=Document_Type eq 'Extension' and Status eq '{status}' and Contractor_No eq '{employeeView.No}'&$format=json";
            }
                

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    ContractorExtensionRequests ExtensionRequest = new ContractorExtensionRequests
                    {
                        No = (string)config["No"],
                        Project_No = (string)config["Project_No"],
                        Project_Name = (string)config["Project_Name"],
                        Contractor_No = (string)config["Contractor_No"],
                        Contractor_Name = (string)config["Contractor_Name"],
                        Document_Type = (string)config["Document_Type"],
                        Date_of_Submittion = (DateTime?)config["Date_of_Submittion"] ?? default,
                        Date_of_Receipt = (DateTime?)config["Date_of_Receipt"] ?? default,
                        Received_By = (string)config["Received_By"],
                        Status = (string)config["Status"],
                        Project_Sum = (int?)config["Project_Sum"] ?? 0,
                        Retention_Amount = (int?)config["Retention_Amount"] ?? 0,
                        Project_Manager = (string)config["Project_Manager"],
                        Project_Manager_Name = (string)config["Project_Manager_Name"],
                        Team_Approval_Status = (string)config["Team_Approval_Status"],
                        Invoiced = (bool?)config["Invoiced"] ?? false,
                        Memo_Comments = (string)config["Memo_Comments"],
                        Approved_Amount = (int?)config["Approved_Amount"] ?? 0,
                        Approved_Variation_Addition = (int?)config["Approved_Variation_Addition"] ?? 0,
                        Apprroved_Variation_Omition = (int?)config["Apprroved_Variation_Omition"] ?? 0,
                        Actual_Work_Progress = (int?)config["Actual_Work_Progress"] ?? 0,
                        Approved_Certificates = (int?)config["Approved_Certificates"] ?? 0,
                        Certificate_No = (int?)config["Certificate_No"] ?? 0,
                        Contract_No = (string)config["Contract_No"],
                        Final_Contract_Value = (int?)config["Final_Contract_Value"] ?? 0,
                        Previously_Paid_Amount = (int?)config["Previously_Paid_Amount"] ?? 0,
                        Revised_Completion_Date = (string)config["Revised_Completion_Date"],
                        Valuation_Date = (string)config["Valuation_Date"],
                        Total_Net_Variation = (int?)config["Total_Net_Variation"] ?? 0,
                        Current_Approving_Member = (string)config["Current_Approving_Member"],
                        Approving_Member_Name = (string)config["Approving_Member_Name"],
                        Asssigning_Employee = (string)config["Asssigning_Employee"],
                        Assigning_Emplyee_Name = (string)config["Assigning_Emplyee_Name"],
                        Deadline = (string)config["Deadline"],
                        Team_Lead_General_Comments = (string)config["Team_Lead_General_Comments"],
                        Team_Lead_Rejection_Comments = (string)config["Team_Lead_Rejection_Comments"],
                        Key_Comments = (string)config["Key_Comments"],
                        Selected_SCM_Employee = (string)config["Selected_SCM_Employee"],
                        Selected_Employee_Name = (string)config["Selected_Employee_Name"],
                        Selected_user_Id = (string)config["Selected_user_Id"],
                        Committee_Assesment_Notes = (string)config["Committee_Assesment_Notes"],
                        Initial_Contract_End_Date = (string)config["Initial_Contract_End_Date"],
                        Action_Approved = (string)config["Action_Approved"],
                        Extension_Period = (string)config["Extension_Period"],
                        New_Contract_End_Date = (string)config["New_Contract_End_Date"],
                        Director_SCM_Comments = (string)config["Director_SCM_Comments"],
                        Professional_Opinion_Notes = (string)config["Professional_Opinion_Notes"],
                        SCM_Status = (string)config["SCM_Status"],
                        Procurement_Comments = (string)config["Procurement_Comments"],
                        Committee_Sec_No = (string)config["Committee_Sec_No"],
                        Secretary_Name = (string)config["Secretary_Name"],
                        Crj_Comments = (string)config["Crj_Comments"],
                        CRJ_General_Comments = (string)config["CRJ_General_Comments"],
                        Project_Manger_Communication = (string)config["Project_Manger_Communication"],
                        Contract_Sum_Execution_PercentodatamediaReadLink = (string)config["Contract_Sum_Execution_PercentodatamediaReadLink"],
                        Addendum_Done= (bool)config["Addendum_Done"]
                    };
                    ExtensionRequestsList.Add(ExtensionRequest);

                }
            }
            return PartialView("~/Views/Contractor/PartialView/ContractorExtensionRequestsListPartialView.cshtml", ExtensionRequestsList);
        }
        public PartialViewResult ContractorExtensionTeamApprovalListPartialView(string teamApprovalStatus)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<ContractorExtensionRequests> ExtensionRequestsList = new List<ContractorExtensionRequests>();

            string title = "Team Approval";

            ViewBag.title = title;
            ViewBag.teamApprovalStatus = teamApprovalStatus;

            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            string page = ""; // Declare 'page' once at the top.
            /*page = $"Contract?$filter=Created_By eq '{employeeView.UserID}'&$format=json";*/
            page = $"ExtensionRequestCard?$filter=Document_Type eq 'Extension' and Status eq 'Team Approval' and Team_Approval_Status eq '{teamApprovalStatus}'&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    ContractorExtensionRequests ExtensionRequest = new ContractorExtensionRequests
                    {
                        No = (string)config["No"],
                        Project_No = (string)config["Project_No"],
                        Project_Name = (string)config["Project_Name"],
                        Contractor_No = (string)config["Contractor_No"],
                        Contractor_Name = (string)config["Contractor_Name"],
                        Document_Type = (string)config["Document_Type"],
                        Date_of_Submittion = (DateTime?)config["Date_of_Submittion"] ?? default,
                        Date_of_Receipt = (DateTime?)config["Date_of_Receipt"] ?? default,
                        Received_By = (string)config["Received_By"],
                        Status = (string)config["Status"],
                        Project_Sum = (int?)config["Project_Sum"] ?? 0,
                        Retention_Amount = (int?)config["Retention_Amount"] ?? 0,
                        Project_Manager = (string)config["Project_Manager"],
                        Project_Manager_Name = (string)config["Project_Manager_Name"],
                        Team_Approval_Status = (string)config["Team_Approval_Status"],
                        Invoiced = (bool?)config["Invoiced"] ?? false,
                        Memo_Comments = (string)config["Memo_Comments"],
                        Approved_Amount = (int?)config["Approved_Amount"] ?? 0,
                        Approved_Variation_Addition = (int?)config["Approved_Variation_Addition"] ?? 0,
                        Apprroved_Variation_Omition = (int?)config["Apprroved_Variation_Omition"] ?? 0,
                        Actual_Work_Progress = (int?)config["Actual_Work_Progress"] ?? 0,
                        Approved_Certificates = (int?)config["Approved_Certificates"] ?? 0,
                        Certificate_No = (int?)config["Certificate_No"] ?? 0,
                        Contract_No = (string)config["Contract_No"],
                        Final_Contract_Value = (int?)config["Final_Contract_Value"] ?? 0,
                        Previously_Paid_Amount = (int?)config["Previously_Paid_Amount"] ?? 0,
                        Revised_Completion_Date = (string)config["Revised_Completion_Date"],
                        Valuation_Date = (string)config["Valuation_Date"],
                        Total_Net_Variation = (int?)config["Total_Net_Variation"] ?? 0,
                        Current_Approving_Member = (string)config["Current_Approving_Member"],
                        Approving_Member_Name = (string)config["Approving_Member_Name"],
                        Asssigning_Employee = (string)config["Asssigning_Employee"],
                        Assigning_Emplyee_Name = (string)config["Assigning_Emplyee_Name"],
                        Deadline = (string)config["Deadline"],
                        Team_Lead_General_Comments = (string)config["Team_Lead_General_Comments"],
                        Team_Lead_Rejection_Comments = (string)config["Team_Lead_Rejection_Comments"],
                        Key_Comments = (string)config["Key_Comments"],
                        Selected_SCM_Employee = (string)config["Selected_SCM_Employee"],
                        Selected_Employee_Name = (string)config["Selected_Employee_Name"],
                        Selected_user_Id = (string)config["Selected_user_Id"],
                        Committee_Assesment_Notes = (string)config["Committee_Assesment_Notes"],
                        Initial_Contract_End_Date = (string)config["Initial_Contract_End_Date"],
                        Action_Approved = (string)config["Action_Approved"],
                        Extension_Period = (string)config["Extension_Period"],
                        New_Contract_End_Date = (string)config["New_Contract_End_Date"],
                        Director_SCM_Comments = (string)config["Director_SCM_Comments"],
                        Professional_Opinion_Notes = (string)config["Professional_Opinion_Notes"],
                        SCM_Status = (string)config["SCM_Status"],
                        Procurement_Comments = (string)config["Procurement_Comments"],
                        Committee_Sec_No = (string)config["Committee_Sec_No"],
                        Secretary_Name = (string)config["Secretary_Name"],
                        Crj_Comments = (string)config["Crj_Comments"],
                        CRJ_General_Comments = (string)config["CRJ_General_Comments"],
                        Project_Manger_Communication = (string)config["Project_Manger_Communication"],
                        Contract_Sum_Execution_PercentodatamediaReadLink = (string)config["Contract_Sum_Execution_PercentodatamediaReadLink"]
                    };
                    ExtensionRequestsList.Add(ExtensionRequest);

                }
            }
            return PartialView("~/Views/Contractor/PartialView/ContractorExtensionRequestsListPartialView.cshtml", ExtensionRequestsList);
        }
        [HttpPost]
        public ActionResult ContractorExtensionRequestsDocumentView(string No)
        {
            if (Session["Username"] == null)

            {
                return RedirectToAction("Login", "Login");
            }
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;

            var isManager = IsManager(UserID);
            var isCRJ = IsCRJ(StaffNo);

            List<ContractorExtensionRequests> ContractorExtensionRequests = new List<ContractorExtensionRequests>();

            string page = $"ExtensionRequestCard?$filter=No eq '{No}'&$format=json";
            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                var config = details["value"].FirstOrDefault();
                if (config != null)
                {
                    ContractorExtensionRequests contractorExtensionRequest = new ContractorExtensionRequests
                    {
                        No = (string)config["No"],
                        Project_No = (string)config["Project_No"],
                        Project_Name = (string)config["Project_Name"],
                        Contractor_No = (string)config["Contractor_No"],
                        Contractor_Name = (string)config["Contractor_Name"],
                        Document_Type = (string)config["Document_Type"],
                        Date_of_Submittion = config["Date_of_Submittion"]?.ToObject<DateTime>() ?? default,
                        Date_of_Receipt = config["Date_of_Receipt"]?.ToObject<DateTime>() ?? default,
                        Received_By = (string)config["Received_By"],
                        Status = (string)config["Status"],
                        Team_Approval_Status = (string)config["Team_Approval_Status"],
                        Memo_Comments = (string)config["Memo_Comments"],
                        Key_Comments = (string)config["Key_Comments"],
                        Current_Approving_Member = (string)config["Current_Approving_Member"],
                        Approving_Member_Name = (string)config["Approving_Member_Name"],
                        Asssigning_Employee = (string)config["Asssigning_Employee"],
                        Assigning_Emplyee_Name = (string)config["Assigning_Emplyee_Name"],
                        Deadline = (string)config["Deadline"],
                        Team_Lead_General_Comments = (string)config["Team_Lead_General_Comments"],
                        Team_Lead_Rejection_Comments = (string)config["Team_Lead_Rejection_Comments"],
                        Selected_SCM_Employee = (string)config["Selected_SCM_Employee"],
                        Selected_Employee_Name = (string)config["Selected_Employee_Name"],
                        Selected_user_Id = (string)config["Selected_user_Id"],
                        Committee_Assesment_Notes = (string)config["Committee_Assesment_Notes"],
                        Initial_Contract_End_Date = (string)config["Initial_Contract_End_Date"],
                        Action_Approved = (string)config["Action_Approved"],
                        Extension_Period = (string)config["Extension_Period"],
                        New_Contract_End_Date = (string)config["New_Contract_End_Date"],
                        Director_SCM_Comments = (string)config["Director_SCM_Comments"],
                        Professional_Opinion_Notes = (string)config["Professional_Opinion_Notes"],
                        SCM_Status = (string)config["SCM_Status"],
                        Procurement_Comments = (string)config["Procurement_Comments"],
                        Crj_Comments = (string)config["Crj_Comments"],
                        CRJ_General_Comments = (string)config["CRJ_General_Comments"],
                        Project_Manger_Communication = (string)config["Project_Manger_Communication"],
                        Appealed_Period = (string)config["Appealed_Period"],
                        New_Appealed_Contract_End_Date = (string)config["New_Appealed_Contract_End_Date"],
                        isManager = isManager,
                        Sent_to_Ast_Directors = (bool)config["Sent_to_Ast_Directors"],
                        Addendum_Done= (bool)config["Addendum_Done"]



                    };
                    ContractorExtensionRequests.Add(contractorExtensionRequest);
                }
            }
            ViewBag.loggedInUserID = StaffNo;
            ViewBag.isCRJ = isCRJ;
            return PartialView(ContractorExtensionRequests.FirstOrDefault());
        }
        public PartialViewResult ContractorRequestLinesPartialView(string No)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<ContractorExtensionRequestLines> ExtensionReqLinesList = new List<ContractorExtensionRequestLines>();

            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;


            string page = $"ContractorRequestLines?$filter=Header_No eq '{No}'&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    ContractorExtensionRequestLines extensionReqLines = new ContractorExtensionRequestLines
                    {
                        Header_No = (string)config["Header_No"],
                        Line_No = (int)config["Line_No"],
                        Request_Description = (string)config["Request_Description"],
                        Assistant_Director = (string)config["Assistant_Director"],
                        Assistant_Director_Name = (string)config["Assistant_Director_Name"],
                        Assigned_Team_Member = (string)config["Assigned_Team_Member"],
                        Resource_Name = (string)config["Resource_Name"],
                        Deadline = (string)config["Deadline"],
                        Review_Status = (string)config["Review_Status"],
                        Date_Submitted = (string)config["Date_Submitted"],
                        section = (string)config["Section"],

                    };
                    ExtensionReqLinesList.Add(extensionReqLines);

                }
            }
            ViewBag.DocNo = No;
            return PartialView("~/Views/Contractor/PartialView/ContractorRequestLinesPartialView.cshtml", ExtensionReqLinesList);
        }
        public PartialViewResult AssignAssistantDirector(string Document_No, string Entry_No)
        {
            ContractorExtensionRequestLines assignAssDir = new ContractorExtensionRequestLines();
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

            if (employeeView == null)
            {
                return PartialView("Error", "Session expired or Employee data not found.");
            }

            List<DropdownList> employees = new List<DropdownList>();
            string pageSec = $"Employees?$filter=Global_Dimension_2_Code eq '9011002101'&$format=json";
            try
            {
                HttpWebResponse httpResponseSec = Credentials.GetOdataData(pageSec);
                if (httpResponseSec == null || httpResponseSec.StatusCode != HttpStatusCode.OK)
                {
                    return PartialView("Error", "Failed to fetch employee data.");
                }

                using (var stream = httpResponseSec.GetResponseStream())
                using (var streamReader = new StreamReader(stream))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    if (details["value"] == null || !details["value"].Any())
                    {
                        return PartialView("Error", "No employees found.");
                    }

                    foreach (JObject config in details["value"])
                    {
                        employees.Add(new DropdownList
                        {
                            Text = (string)config["FullName"] + "(" + (string)config["No"] + ")",
                            Value = (string)config["No"]
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return PartialView("Error", ex.Message);
            }

            assignAssDir.Header_No = Document_No;
            assignAssDir.Line_No = int.Parse(Entry_No);
            assignAssDir.ListOfEmployees = employees.Select(x =>
                new SelectListItem
                {
                    Text = x.Text,
                    Value = x.Value
                }).ToList();

            return PartialView("~/Views/Contractor/PartialView/AssignAssistantDirector.cshtml", assignAssDir);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SubmitAssignedAssistantDirector(ContractorExtensionRequestLines assignedEmpl)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                string responsibleEmployeeNo = employeeView.No;
                string userID = employeeView.UserID;
                bool result = false;
                result = Credentials.ObjNav.FnUpdateNonPaymentLines(
                    assignedEmpl.Header_No,
                    assignedEmpl.Line_No,
                    assignedEmpl.Assistant_Director
                );


                if (result)
                {
                    string redirectUrl = "Success.";
                    return Json(new { message = redirectUrl, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { message = "Error submitting request. Try again.", success = false },
                        JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public PartialViewResult ContractorRequestCommentsPartialView(string No)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<ContractorRequestComments> ExtensionReqCommentsList = new List<ContractorRequestComments>();

            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;

            string page = $"ContractorRequestComments?$filter=Header_No eq '{No}'&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    ContractorRequestComments extensionReqComments = new ContractorRequestComments
                    {
                        Header_No = (string)config["Header_No"],
                        Line_No = (int)config["Line_No"],
                        Comments = (string)config["Comments"],
                        Written_By = (string)config["Written_By"],
                        Written_At = (string)config["Written_At"],
                        Author_Title = (string)config["Author_Title"]
                    };
                    ExtensionReqCommentsList.Add(extensionReqComments);

                }
            }
            return PartialView("~/Views/Contractor/PartialView/ContractorRequestCommentsPartialView.cshtml", ExtensionReqCommentsList);
        }
        public PartialViewResult TenderEvaluationCommitteePartialView(string No)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<InspectionCommitteeMembers> membersList = new List<InspectionCommitteeMembers>();

            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;

            string page = $"InspectionCommitteeMembers?$filter=Document_No eq '{No}'&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    InspectionCommitteeMembers member = new InspectionCommitteeMembers
                    {
                        Document_No = (string)config["Document_No"],
                        Member_No = (string)config["Member_No"],
                        Member_Name = (string)config["Member_Name"],
                        Title = (string)config["Title"],
                        Assesment = (string)config["Assesment"],

                    };
                    membersList.Add(member);
                }
            }
            return PartialView("~/Views/Contractor/PartialView/TenderEvaluationCommitteePartialView.cshtml", membersList);
        }
        public PartialViewResult NewExtensionReqLine(string DocNo)
        {
            ContractorExtensionRequestLines extensionLine = new ContractorExtensionRequestLines();
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

            #region Employees     
            List<DropdownList> employees = new List<DropdownList>();
            string pageSec = $"Employees?$filter=Global_Dimension_2_Code eq '9011002101'&$format=json";
            HttpWebResponse httpResponseSec = Credentials.GetOdataData(pageSec);
            using (var streamReader = new StreamReader(httpResponseSec.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    DropdownList dropdownList = new DropdownList();
                    dropdownList.Text = (string)config["FullName"] + " (" + (string)config["No"] + ")";
                    dropdownList.Value = (string)config["No"];
                    employees.Add(dropdownList);
                }
            }
            #endregion

            extensionLine.Header_No = DocNo;
            extensionLine.ListOfEmployees = employees.Select(x =>
                new SelectListItem()
                {
                    Text = x.Text,
                    Value = x.Value
                }).ToList();

            return PartialView("~/Views/Contractor/PartialView/NewExtensionReqLine.cshtml", extensionLine);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SubmitExtensionReqLine(ContractorExtensionRequestLines reqLine)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                string responsibleEmployeeNo = employeeView.No;
                string userID = employeeView.UserID;
                bool result = false;
                result = Credentials.ObjNav.FnCreatePaymentRequestLines(
                    reqLine.Header_No,
                    reqLine.Request_Description,
                    reqLine.section,
                   reqLine.Assistant_Director
                /* DateTime.ParseExact(reqLine.Deadline, "dd/MM/yyyy", CultureInfo.InvariantCulture),*/
                );

                if (result)
                {
                    string redirectUrl = "Record successfully created";
                    return Json(new { message = redirectUrl, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { message = "Error submitting record. Try again.", success = false },
                        JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ProjectAssignmentEntries()
        {
            try
            {
                if (Session["Username"] == null)
                    return RedirectToAction("Login", "Login");
                return View();
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }
        public PartialViewResult ProjectAssignmentEntriesPartialView(string DocType)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<ProjectAssignmentEntries> ProjectAssignmentEntriesList = new List<ProjectAssignmentEntries>();

            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            string page = $"Prj_AssignmentEntries?$filter=Document_Type eq '{DocType}' and Assigning_Employee eq '{StaffNo}'&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    ProjectAssignmentEntries assignmentEntry = new ProjectAssignmentEntries
                    {
                        Entry_No = (int)config["Entry_No"],
                        Document_Type = (string)config["Document_Type"],
                        Document_No = (string)config["Document_No"],
                        Assignment_Description = (string)config["Assignment_Description"],
                        Assigning_Employee = (string)config["Assigning_Employee"],
                        Assigning_Employee_Name = (string)config["Assigning_Employee_Name"],
                        Assigned_Employee = (string)config["Assigned_Employee"],
                        Assigned_Employee_Name = (string)config["Assigned_Employee_Name"],
                        Date_Sent = (string)config["Date_Sent"],
                        Assigned_Date = (string)config["Assigned_Date"],
                        Contractor = (string)config["Contractor"],
                        Project_Name = (string)config["Project_Name"],
                        Deadline = (string)config["Deadline"],
                        Assigned = (bool)config["Assigned"]
                    };

                    ProjectAssignmentEntriesList.Add(assignmentEntry);
                }
            }

            return PartialView("~/Views/Contractor/PartialView/ProjectAssignmentEntriesPartialView.cshtml", ProjectAssignmentEntriesList);
        }
        public PartialViewResult AssignEmployee(string Document_No, string Entry_No)
        {
            ProjectAssignmentEntries assignment = new ProjectAssignmentEntries();
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

            if (employeeView == null)
            {
                return PartialView("Error", "Session expired or Employee data not found.");
            }

            List<DropdownList> employees = new List<DropdownList>();
            string pageSec = $"Employees?$filter=Global_Dimension_2_Code eq '9011002101'&$format=json";
            try
            {
                HttpWebResponse httpResponseSec = Credentials.GetOdataData(pageSec);
                if (httpResponseSec == null || httpResponseSec.StatusCode != HttpStatusCode.OK)
                {
                    return PartialView("Error", "Failed to fetch employee data.");
                }

                using (var stream = httpResponseSec.GetResponseStream())
                using (var streamReader = new StreamReader(stream))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    if (details["value"] == null || !details["value"].Any())
                    {
                        return PartialView("Error", "No employees found.");
                    }

                    foreach (JObject config in details["value"])
                    {
                        employees.Add(new DropdownList
                        {
                            Text = (string)config["FullName"] + "(" + (string)config["No"] + ")",
                            Value = (string)config["No"]
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return PartialView("Error", ex.Message);
            }

            assignment.Document_No = Document_No;
            assignment.Entry_No = int.Parse(Entry_No);
            assignment.ListOfEmployees = employees.Select(x =>
                new SelectListItem
                {
                    Text = x.Text,
                    Value = x.Value
                }).ToList();

            return PartialView("~/Views/Contractor/PartialView/AssignEmployee.cshtml", assignment);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SubmitAssignedEmployeeRequest(ProjectAssignmentEntries assignedEmpl)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                string responsibleEmployeeNo = employeeView.No;
                string userID = employeeView.UserID;
                bool result = false;
                result = Credentials.ObjNav.FnSelectEmployeeToAssign(
                    assignedEmpl.Entry_No,
                    assignedEmpl.Assigned_Employee
                );


                /*  if (result)
                  {*/
                string redirectUrl = "Success.";
                return Json(new { message = redirectUrl, success = true }, JsonRequestBehavior.AllowGet);
                /* }*/
                /* else
                 {
                     return Json(new { message = "Error submitting request. Try again.", success = false },
                         JsonRequestBehavior.AllowGet);
                 }*/
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult AssignEmployeeAction(ProjectAssignmentEntries assignedEmpl)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                string responsibleEmployeeNo = employeeView.No;
                string userID = employeeView.UserID;
                bool result = false;
                Credentials.ObjNav.FnAssignEmployee(
                     assignedEmpl.Entry_No
                 );


                /* if (result)
                 {*/
                string redirectUrl = "Success.";
                return Json(new { message = redirectUrl, success = true }, JsonRequestBehavior.AllowGet);
                /* }
                 else
                 {
                     return Json(new { message = "Error submitting request. Try again.", success = false },
                         JsonRequestBehavior.AllowGet);
                 }*/
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }


        //contractor extension actions
        public JsonResult ReceiveContractorExtensionRequest(string DocNo)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                string staffNo = Session["Username"].ToString();
                string msg = "";
                string userId = employeeView.UserID;
                bool result = false;
                result = Credentials.ObjNav.FnReceiveContractorRequest(DocNo, staffNo);
                if (result)
                {
                    msg = "Record Successfully Received";
                    return Json(new { message = msg, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    msg = "Record Not received. Try again";
                    return Json(new { message = msg, success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SendExtensionReqToAssistantDirector(string DocNo)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                string staffNo = Session["Username"].ToString();
                string msg = "";
                string userId = employeeView.UserID;
                bool result = false;
                result = Credentials.ObjNav.FnSubmitContractorRequestToAstDirectors(DocNo, staffNo);
                if (result)
                {
                    msg = "Record Successfully Submitted";
                    return Json(new { message = msg, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    msg = "Record Not submitted. Try again";
                    return Json(new { message = msg, success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SubmitToTeamLeadForApproval(string DocNo, string Memo_Comments, string Key_Comments)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                string staffNo = Session["Username"].ToString();
                string msg = "";
                string userId = employeeView.UserID;
                bool result = false;
                result = Credentials.ObjNav.FnSubmitExtensionToTeamLead(DocNo, Key_Comments);
                if (result)
                {
                    msg = "Record Successfully Submitted";
                    return Json(new { message = msg, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    msg = "Record Not submitted. Try again";
                    return Json(new { message = msg, success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult TeamLeadApproveExtSubmission(string DocNo, string Team_Lead_General_Comments)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                string staffNo = Session["Username"].ToString();
                string msg = "";
                string userId = employeeView.UserID;
                bool result = false;
                result = Credentials.ObjNav.FnTeamLeadApproveExtSubmission(DocNo, staffNo, Team_Lead_General_Comments);
                if (result)
                {
                    msg = "Record Successfully Submitted";
                    return Json(new { message = msg, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    msg = "Record Not submitted. Try again";
                    return Json(new { message = msg, success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult RejectSubmission(string DocNo, string Team_Lead_Rejection_Comments)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                string staffNo = Session["Username"].ToString();
                string msg = "";
                string userId = employeeView.UserID;
                bool result = false;
                result = Credentials.ObjNav.FnTeamLeadRejectExtSubmission(DocNo, staffNo, Team_Lead_Rejection_Comments);
                if (result)
                {
                    msg = "Record Rejected";
                    return Json(new { message = msg, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    msg = "Record Not submitted. Try again";
                    return Json(new { message = msg, success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult ApproveAndSubmitToCRJ(string DocNo, string Key_Comments)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                string staffNo = Session["Username"].ToString();
                string msg = "";
                string userId = employeeView.UserID;
                bool result = false;
                result = Credentials.ObjNav.FnPMApproveExtandSubmitToCRJ(DocNo, staffNo, Key_Comments);
                if (result)
                {
                    msg = "Record Successfully Approved";
                    return Json(new { message = msg, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    msg = "Record Not submitted. Try again";
                    return Json(new { message = msg, success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult PMRejectSubmission(string DocNo, string Team_Lead_Rejection_Comments)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                string staffNo = Session["Username"].ToString();
                string msg = "";
                string userId = employeeView.UserID;
                bool result = false;
                result = Credentials.ObjNav.FnPMRejectExtRequest(DocNo, staffNo, Team_Lead_Rejection_Comments);
                if (result)
                {
                    msg = "Record Rejected";
                    return Json(new { message = msg, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    msg = "Record Not submitted. Try again";
                    return Json(new { message = msg, success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SendToSupplyChain(string DocNo, string Key_Comments)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                string staffNo = Session["Username"].ToString();
                string msg = "";
                string userId = employeeView.UserID;
                bool result = false;
                result = Credentials.ObjNav.FnCRJApproveAndSubmitToDSCM(DocNo, staffNo, Key_Comments);
                if (result)
                {
                    msg = "Record Successfully Approved";
                    return Json(new { message = msg, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    msg = "Record Not submitted. Try again";
                    return Json(new { message = msg, success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult CRJRejectSubmission(string DocNo, string Key_Comments)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                string staffNo = Session["Username"].ToString();
                string msg = "";
                string userId = employeeView.UserID;
                bool result = false;
                result = Credentials.ObjNav.FnCRJRejectExtRequest(DocNo, staffNo, Key_Comments);
                if (result)
                {
                    msg = "Record Rejected";
                    return Json(new { message = msg, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    msg = "Request Not submitted. Try again";
                    return Json(new { message = msg, success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AcknowledgeAction(string DocNo)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                string staffNo = Session["Username"].ToString();
                string msg = "";
                string userId = employeeView.UserID;
                bool result = false;
                /*result = Credentials.ObjNav.FnAcknowledgeRequest();*/
                if (result)
                {
                    msg = "Record Rejected";
                    return Json(new { message = msg, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    msg = "Request Not submitted. Try again";
                    return Json(new { message = msg, success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        //contractor ammended actions
        public JsonResult SendToContractorForUpload(string DocNo, string Key_Comments)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                string staffNo = Session["Username"].ToString();
                string msg = "";
                string userId = employeeView.UserID;
                bool result = false;
                result = Credentials.ObjNav.FnSendtoContractorForUpload(DocNo, staffNo, Key_Comments);
                if (result)
                {
                    msg = "Success";
                    return Json(new { message = msg, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    msg = "Record Not received. Try again";
                    return Json(new { message = msg, success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult ApproveUploadedBQS(string DocNo, string Key_Comments)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                string staffNo = Session["Username"].ToString();
                string msg = "";
                string userId = employeeView.UserID;
                bool result = false;
                result = Credentials.ObjNav.FnApproveUploadedBqs(DocNo, staffNo, Key_Comments);
                if (result)
                {
                    msg = "Success";
                    return Json(new { message = msg, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    msg = "Record Not received. Try again";
                    return Json(new { message = msg, success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SendRejectedUploadedBQsToContractor(string DocNo, string Key_Comments)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                string staffNo = Session["Username"].ToString();
                string msg = "";
                string userId = employeeView.UserID;
                bool result = false;
                result = Credentials.ObjNav.FnSendRejectedBqstoContractor(DocNo, staffNo, Key_Comments);
                if (result)
                {
                    msg = "Success";
                    return Json(new { message = msg, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    msg = "Record Not received. Try again";
                    return Json(new { message = msg, success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult ProjectBOQLinesPartialView(string No)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<VariationProjectBoqs> variationProjectBoqList = new List<VariationProjectBoqs>();

            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;

            string page = $"VariationProjectBoqs?$filter=Project_Code eq '{No}'&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    VariationProjectBoqs variationProjectBoq = new VariationProjectBoqs
                    {
                        Project_Code = (string)config["Project_Code"],
                        Entry_No = (int)config["Entry_No"],
                        Line_Type = (string)config["Line_Type"],
                        Section = (string)config["Section"],
                        Description = (string)config["Description"],
                        Quantity = (int)config["Quantity"],
                        UOM = (string)config["UOM"],
                        Unit_Price = (decimal)config["Unit_Price"],
                        Line_Amount = (decimal)config["Line_Amount"],
                        Remeasured_Qty = (int)config["Remeasured_Qty"],
                        Remeasured_Line_Amount = (decimal)config["Remeasured_Line_Amount"],
                        Remeasured_Total_Amount = (decimal)config["Remeasured_Total_Amount"],
                        Variation_Type = (string)config["Variation_Type"],
                        Variation_Amount = (decimal)config["Variation_Amount"],
                        Entry_Type = (string)config["Entry_Type"]


                    };
                    variationProjectBoqList.Add(variationProjectBoq);

                }
            }
            return PartialView("~/Views/Contractor/PartialView/ProjectBOQLinesPartialView.cshtml", variationProjectBoqList);
        }

        //contractor Ammended
        public ActionResult ContractorAmmendedRequests()
        {
            try
            {
                if (Session["Username"] == null)
                    return RedirectToAction("Login", "Login");
                return View();
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }
        public PartialViewResult ContractorAmendedRequestsListPartialView(string status)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<ContractorExtensionRequests> ExtensionRequestsList = new List<ContractorExtensionRequests>();

            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            string title = status;

            ViewBag.title = title;


            string page = $"AmendmendRequestCard?$filter=Document_Type eq 'Amendmend' and Status eq '{status}' and Contractor_No eq '{employeeView.No}'&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    ContractorExtensionRequests extensionRequest = new ContractorExtensionRequests
                    {
                        No = (string)config["No"],
                        Project_No = (string)config["Project_No"],
                        Project_Name = (string)config["Project_Name"],
                        Contractor_No = (string)config["Contractor_No"],
                        Contractor_Name = (string)config["Contractor_Name"],
                        Document_Type = (string)config["Document_Type"],
                        Date_of_Submittion = (DateTime?)config["Date_of_Submittion"] ?? default,
                        Date_of_Receipt = (DateTime?)config["Date_of_Receipt"] ?? default,
                        Received_By = (string)config["Received_By"],
                        Status = (string)config["Status"],
                        Project_Sum = (int?)config["Project_Sum"] ?? 0,
                        Retention_Amount = (int?)config["Retention_Amount"] ?? 0,
                        Project_Manager = (string)config["Project_Manager"],
                        Project_Manager_Name = (string)config["Project_Manager_Name"],
                        Team_Approval_Status = (string)config["Team_Approval_Status"],
                        Invoiced = (bool?)config["Invoiced"] ?? false,
                        Memo_Comments = (string)config["Memo_Comments"],
                        Approved_Amount = (int?)config["Approved_Amount"] ?? 0,
                        Approved_Variation_Addition = (int?)config["Approved_Variation_Addition"] ?? 0,
                        Apprroved_Variation_Omition = (int?)config["Apprroved_Variation_Omition"] ?? 0,
                        Actual_Work_Progress = (int?)config["Actual_Work_Progress"] ?? 0,
                        Approved_Certificates = (int?)config["Approved_Certificates"] ?? 0,
                        Certificate_No = (int?)config["Certificate_No"] ?? 0,
                        Contract_No = (string)config["Contract_No"],
                        Final_Contract_Value = (int?)config["Final_Contract_Value"] ?? 0,
                        Previously_Paid_Amount = (int?)config["Previously_Paid_Amount"] ?? 0,
                        Revised_Completion_Date = (string)config["Revised_Completion_Date"],
                        Valuation_Date = (string)config["Valuation_Date"],
                        Total_Net_Variation = (int?)config["Total_Net_Variation"] ?? 0,
                        Current_Approving_Member = (string)config["Current_Approving_Member"],
                        Approving_Member_Name = (string)config["Approving_Member_Name"],
                        Asssigning_Employee = (string)config["Asssigning_Employee"],
                        Assigning_Emplyee_Name = (string)config["Assigning_Emplyee_Name"],
                        Deadline = (string)config["Deadline"],
                        Team_Lead_General_Comments = (string)config["Team_Lead_General_Comments"],
                        Team_Lead_Rejection_Comments = (string)config["Team_Lead_Rejection_Comments"],
                        Key_Comments = (string)config["Key_Comments"],
                        Selected_SCM_Employee = (string)config["Selected_SCM_Employee"],
                        Selected_Employee_Name = (string)config["Selected_Employee_Name"],
                        Selected_user_Id = (string)config["Selected_user_Id"],
                        Committee_Assesment_Notes = (string)config["Committee_Assesment_Notes"],
                        Initial_Contract_End_Date = (string)config["Initial_Contract_End_Date"],
                        Action_Approved = (string)config["Action_Approved"],
                        Extension_Period = (string)config["Extension_Period"],
                        New_Contract_End_Date = (string)config["New_Contract_End_Date"],
                        Director_SCM_Comments = (string)config["Director_SCM_Comments"],
                        Professional_Opinion_Notes = (string)config["Professional_Opinion_Notes"],
                        SCM_Status = (string)config["SCM_Status"],
                        Procurement_Comments = (string)config["Procurement_Comments"],
                        Committee_Sec_No = (string)config["Committee_Sec_No"],
                        Secretary_Name = (string)config["Secretary_Name"],
                        Crj_Comments = (string)config["Crj_Comments"],
                        CRJ_General_Comments = (string)config["CRJ_General_Comments"],
                        Project_Manger_Communication = (string)config["Project_Manger_Communication"],
                        Contract_Sum_Execution_PercentodatamediaReadLink = (string)config["Contract_Sum_Execution_PercentodatamediaReadLink"]
                    };
                    ExtensionRequestsList.Add(extensionRequest);

                }
            }
            return PartialView("~/Views/Contractor/PartialView/ContractorAmmendedRequestsListPartialView.cshtml", ExtensionRequestsList);
        }
        public PartialViewResult ContractorAmmendedTeamApprovalListPartialView(string teamApprovalStatus)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<ContractorExtensionRequests> ExtensionRequestsList = new List<ContractorExtensionRequests>();

            string title = "Team Approval";

            ViewBag.title = title;
            ViewBag.teamApprovalStatus = teamApprovalStatus;

            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            string page = ""; // Declare 'page' once at the top.
            /*page = $"Contract?$filter=Created_By eq '{employeeView.UserID}'&$format=json";*/
            page = $"AmendmendRequestCard?$filter=Document_Type eq 'Amendmend' and Status eq 'Team Approval' and Team_Approval_Status eq '{teamApprovalStatus}'  and Contractor_No eq '{employeeView.No}'&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    ContractorExtensionRequests ExtensionRequest = new ContractorExtensionRequests
                    {
                        No = (string)config["No"],
                        Project_No = (string)config["Project_No"],
                        Project_Name = (string)config["Project_Name"],
                        Contractor_No = (string)config["Contractor_No"],
                        Contractor_Name = (string)config["Contractor_Name"],
                        Document_Type = (string)config["Document_Type"],
                        Date_of_Submittion = (DateTime?)config["Date_of_Submittion"] ?? default,
                        Date_of_Receipt = (DateTime?)config["Date_of_Receipt"] ?? default,
                        Received_By = (string)config["Received_By"],
                        Status = (string)config["Status"],
                        Project_Sum = (int?)config["Project_Sum"] ?? 0,
                        Retention_Amount = (int?)config["Retention_Amount"] ?? 0,
                        Project_Manager = (string)config["Project_Manager"],
                        Project_Manager_Name = (string)config["Project_Manager_Name"],
                        Team_Approval_Status = (string)config["Team_Approval_Status"],
                        Invoiced = (bool?)config["Invoiced"] ?? false,
                        Memo_Comments = (string)config["Memo_Comments"],
                        Approved_Amount = (int?)config["Approved_Amount"] ?? 0,
                        Approved_Variation_Addition = (int?)config["Approved_Variation_Addition"] ?? 0,
                        Apprroved_Variation_Omition = (int?)config["Apprroved_Variation_Omition"] ?? 0,
                        Actual_Work_Progress = (int?)config["Actual_Work_Progress"] ?? 0,
                        Approved_Certificates = (int?)config["Approved_Certificates"] ?? 0,
                        Certificate_No = (int?)config["Certificate_No"] ?? 0,
                        Contract_No = (string)config["Contract_No"],
                        Final_Contract_Value = (int?)config["Final_Contract_Value"] ?? 0,
                        Previously_Paid_Amount = (int?)config["Previously_Paid_Amount"] ?? 0,
                        Revised_Completion_Date = (string)config["Revised_Completion_Date"],
                        Valuation_Date = (string)config["Valuation_Date"],
                        Total_Net_Variation = (int?)config["Total_Net_Variation"] ?? 0,
                        Current_Approving_Member = (string)config["Current_Approving_Member"],
                        Approving_Member_Name = (string)config["Approving_Member_Name"],
                        Asssigning_Employee = (string)config["Asssigning_Employee"],
                        Assigning_Emplyee_Name = (string)config["Assigning_Emplyee_Name"],
                        Deadline = (string)config["Deadline"],
                        Team_Lead_General_Comments = (string)config["Team_Lead_General_Comments"],
                        Team_Lead_Rejection_Comments = (string)config["Team_Lead_Rejection_Comments"],
                        Key_Comments = (string)config["Key_Comments"],
                        Selected_SCM_Employee = (string)config["Selected_SCM_Employee"],
                        Selected_Employee_Name = (string)config["Selected_Employee_Name"],
                        Selected_user_Id = (string)config["Selected_user_Id"],
                        Committee_Assesment_Notes = (string)config["Committee_Assesment_Notes"],
                        Initial_Contract_End_Date = (string)config["Initial_Contract_End_Date"],
                        Action_Approved = (string)config["Action_Approved"],
                        Extension_Period = (string)config["Extension_Period"],
                        New_Contract_End_Date = (string)config["New_Contract_End_Date"],
                        Director_SCM_Comments = (string)config["Director_SCM_Comments"],
                        Professional_Opinion_Notes = (string)config["Professional_Opinion_Notes"],
                        SCM_Status = (string)config["SCM_Status"],
                        Procurement_Comments = (string)config["Procurement_Comments"],
                        Committee_Sec_No = (string)config["Committee_Sec_No"],
                        Secretary_Name = (string)config["Secretary_Name"],
                        Crj_Comments = (string)config["Crj_Comments"],
                        CRJ_General_Comments = (string)config["CRJ_General_Comments"],
                        Project_Manger_Communication = (string)config["Project_Manger_Communication"],
                        Contract_Sum_Execution_PercentodatamediaReadLink = (string)config["Contract_Sum_Execution_PercentodatamediaReadLink"]
                    };
                    ExtensionRequestsList.Add(ExtensionRequest);

                }
            }
            return PartialView("~/Views/Contractor/PartialView/ContractorAmendedRequestsListPartialView.cshtml", ExtensionRequestsList);
        }
        public PartialViewResult ContractorAmmendedContractorListPartialView(string ContractorStatus)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<AmendmendRequestCard> ExtensionRequestsList = new List<AmendmendRequestCard>();

            string title = ContractorStatus;

            ViewBag.title = title;
            ViewBag.teamApprovalStatus = ContractorStatus;

            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            string page = ""; // Declare 'page' once at the top.
            /*page = $"Contract?$filter=Created_By eq '{employeeView.UserID}'&$format=json";*/
            page = $"AmendmendRequestCard?$filter=Document_Type eq 'Amendmend' and Status eq 'Contractor' and Contractor_Status eq '{ContractorStatus}'  and Contractor_No eq '{employeeView.No}'&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    AmendmendRequestCard ExtensionRequest = new AmendmendRequestCard
                    {
                        No = (string)config["No"],
                        Project_No = (string)config["Project_No"],
                        Project_Name = (string)config["Project_Name"],
                        Contractor_No = (string)config["Contractor_No"],
                        Contractor_Name = (string)config["Contractor_Name"],
                        Document_Type = (string)config["Document_Type"],
                        Date_of_Submittion = (string)config["Date_of_Submittion"],
                        Date_of_Receipt = (string)config["Date_of_Receipt"],
                        Received_By = (string)config["Received_By"],
                        Status = (string)config["Status"],
                        Project_Sum = (int?)config["Project_Sum"] ?? 0,
                        Retention_Amount = (int?)config["Retention_Amount"] ?? 0,
                        Project_Manager = (string)config["Project_Manager"],
                        Project_Manager_Name = (string)config["Project_Manager_Name"],
                        Team_Approval_Status = (string)config["Team_Approval_Status"],
                        Invoiced = (bool?)config["Invoiced"] ?? false,
                        Memo_Comments = (string)config["Memo_Comments"],
                        Approved_Amount = (int?)config["Approved_Amount"] ?? 0,
                        Approved_Variation_Addition = (int?)config["Approved_Variation_Addition"] ?? 0,
                        Apprroved_Variation_Omition = (int?)config["Apprroved_Variation_Omition"] ?? 0,
                        Actual_Work_Progress = (int?)config["Actual_Work_Progress"] ?? 0,
                        Approved_Certificates = (int?)config["Approved_Certificates"] ?? 0,
                        Certificate_No = (int?)config["Certificate_No"] ?? 0,
                        Contract_No = (string)config["Contract_No"],
                        Final_Contract_Value = (int?)config["Final_Contract_Value"] ?? 0,
                        Previously_Paid_Amount = (int?)config["Previously_Paid_Amount"] ?? 0,
                        Revised_Completion_Date = (string)config["Revised_Completion_Date"],
                        Valuation_Date = (string)config["Valuation_Date"],
                        Total_Net_Variation = (int?)config["Total_Net_Variation"] ?? 0,
                        Current_Approving_Member = (string)config["Current_Approving_Member"],
                        Approving_Member_Name = (string)config["Approving_Member_Name"],
                        Asssigning_Employee = (string)config["Asssigning_Employee"],
                        Assigning_Emplyee_Name = (string)config["Assigning_Emplyee_Name"],
                        Deadline = (string)config["Deadline"],
                        Team_Lead_General_Comments = (string)config["Team_Lead_General_Comments"],
                        Team_Lead_Rejection_Comments = (string)config["Team_Lead_Rejection_Comments"],
                        Key_Comments = (string)config["Key_Comments"],
                        Selected_SCM_Employee = (string)config["Selected_SCM_Employee"],
                        Selected_Employee_Name = (string)config["Selected_Employee_Name"],
                        Selected_user_Id = (string)config["Selected_user_Id"],
                        Committee_Assesment_Notes = (string)config["Committee_Assesment_Notes"],
                        Initial_Contract_End_Date = (string)config["Initial_Contract_End_Date"],
                        Action_Approved = (string)config["Action_Approved"],
                        Extension_Period = (string)config["Extension_Period"],
                        New_Contract_End_Date = (string)config["New_Contract_End_Date"],
                        Director_SCM_Comments = (string)config["Director_SCM_Comments"],
                        Professional_Opinion_Notes = (string)config["Professional_Opinion_Notes"],
                        SCM_Status = (string)config["SCM_Status"],
                        Contractor_Status = (string)config["Contractor_Status"],
                        Procurement_Comments = (string)config["Procurement_Comments"],
                        Committee_Sec_No = (string)config["Committee_Sec_No"],
                        Secretary_Name = (string)config["Secretary_Name"],
                        Crj_Comments = (string)config["Crj_Comments"],
                        CRJ_General_Comments = (string)config["CRJ_General_Comments"],
                        Project_Manger_Communication = (string)config["Project_Manger_Communication"],

                    };
                    ExtensionRequestsList.Add(ExtensionRequest);

                }
            }
            return PartialView("~/Views/Contractor/PartialView/ContractorAmmendedContractorListPartialView.cshtml", ExtensionRequestsList);
        }
        [HttpPost]
        public ActionResult ContractorAmmendedRequestsDocumentView(string No)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            var isManager = IsManager(UserID);
            var isCRJ = IsCRJ(StaffNo);
            List<AmendmendRequestCard> ContractorrAmmendedRequests = new List<AmendmendRequestCard>();

            string page = $"AmendmendRequestCard?$filter=No eq '{No}'&$format=json";
            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                var config = details["value"].FirstOrDefault();
                if (config != null)
                {
                    AmendmendRequestCard ContractorrAmmendedRequest = new AmendmendRequestCard
                    {
                        No = (string)config["No"],
                        Project_No = (string)config["Project_No"],
                        Project_Name = (string)config["Project_Name"],
                        Contractor_No = (string)config["Contractor_No"],
                        Contractor_Name = (string)config["Contractor_Name"],
                        Document_Type = (string)config["Document_Type"],
                        Date_of_Submittion = (string)config["Date_of_Submittion"],
                        Date_of_Receipt = (string)config["Date_of_Receipt"],
                        Received_By = (string)config["Received_By"],
                        Status = (string)config["Status"],
                        Project_Sum = (int?)config["Project_Sum"] ?? 0,
                        Retention_Amount = (int?)config["Retention_Amount"] ?? 0,
                        Project_Manager = (string)config["Project_Manager"],
                        Project_Manager_Name = (string)config["Project_Manager_Name"],
                        Team_Approval_Status = (string)config["Team_Approval_Status"],
                        Invoiced = (bool?)config["Invoiced"] ?? false,
                        Memo_Comments = (string)config["Memo_Comments"],
                        Key_Comments = (string)config["Key_Comments"],
                        Approved_Amount = (int?)config["Approved_Amount"] ?? 0,
                        Approved_Variation_Addition = (int?)config["Approved_Variation_Addition"] ?? 0,
                        Apprroved_Variation_Omition = (int?)config["Apprroved_Variation_Omition"] ?? 0,
                        Actual_Work_Progress = (int?)config["Actual_Work_Progress"] ?? 0,
                        Approved_Certificates = (int?)config["Approved_Certificates"] ?? 0,
                        Certificate_No = (int?)config["Certificate_No"] ?? 0,
                        Contract_No = (string)config["Contract_No"],
                        Final_Contract_Value = (int?)config["Final_Contract_Value"] ?? 0,
                        Previously_Paid_Amount = (int?)config["Previously_Paid_Amount"] ?? 0,
                        Revised_Completion_Date = (string)config["Revised_Completion_Date"],
                        Valuation_Date = (string)config["Valuation_Date"],
                        Total_Net_Variation = (int?)config["Total_Net_Variation"] ?? 0,
                        Current_Approving_Member = (string)config["Current_Approving_Member"],
                        Approving_Member_Name = (string)config["Approving_Member_Name"],
                        Asssigning_Employee = (string)config["Asssigning_Employee"],
                        Assigning_Emplyee_Name = (string)config["Assigning_Emplyee_Name"],
                        Deadline = (string)config["Deadline"],
                        Team_Lead_General_Comments = (string)config["Team_Lead_General_Comments"],
                        Team_Lead_Rejection_Comments = (string)config["Team_Lead_Rejection_Comments"],
                        Selected_SCM_Employee = (string)config["Selected_SCM_Employee"],
                        Selected_Employee_Name = (string)config["Selected_Employee_Name"],
                        Selected_user_Id = (string)config["Selected_user_Id"],
                        Committee_Assesment_Notes = (string)config["Committee_Assesment_Notes"],
                        Initial_Contract_End_Date = (string)config["Initial_Contract_End_Date"],
                        Recomendation = (string)config["Recomendation"],
                        New_Contract_End_Date = (string)config["New_Contract_End_Date"],
                        Director_SCM_Comments = (string)config["Director_SCM_Comments"],
                        Professional_Opinion_Notes = (string)config["Professional_Opinion_Notes"],
                        SCM_Status = (string)config["SCM_Status"],
                        Procurement_Comments = (string)config["Procurement_Comments"],
                        Committee_Sec_No = (string)config["Committee_Sec_No"],
                        Secretary_Name = (string)config["Secretary_Name"],
                        Crj_Comments = (string)config["Crj_Comments"],
                        CRJ_General_Comments = (string)config["CRJ_General_Comments"],
                        Project_Manger_Communication = (string)config["Project_Manger_Communication"],
                        Appealed_Period = (string)config["Appealed_Period"],
                        Action_Approved = (string)config["Action_Approved"],
                        Extension_Period = (string)config["Extension_Period"],
                        New_Appealed_Contract_End_Date = (string)config["New_Appealed_Contract_End_Date"],
                        isManager = (bool?)config["isManager"] ?? false,
                        Sent_to_Ast_Directors = (bool?)config["Sent_to_Ast_Directors"] ?? false,
                        Addendum_Done = (bool?)config["Addendum_Done"] ?? false,
                        Tender_Committee = (string)config["Tender_Committee"],
                        Completion_I_A_Recommendation = (string)config["Completion_I_A_Recommendation"]
                        // NOTE: You will need to manually set `ListOfEmployees` and `ListOfTenderCommittee` if needed from other sources.


                    };
                    ContractorrAmmendedRequests.Add(ContractorrAmmendedRequest);
                }
            }
            ViewBag.loggedInUserID = StaffNo;
            ViewBag.isManager = isManager;
            ViewBag.isCRJ = isCRJ;
            return PartialView(ContractorrAmmendedRequests.FirstOrDefault());
        }







        //contractor Instruction
        public ActionResult ContractorInstructionRequests()
        {
            try
            {
                if (Session["Username"] == null)
                    return RedirectToAction("Login", "Login");
                return View();
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }
        public PartialViewResult ContractorInstructionRequestsListPartialView(string status)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<InstructionRequestCard> ExtensionRequestsList = new List<InstructionRequestCard>();

            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            string title = status;

            ViewBag.title = title;

            string page = $"InstructionRequestCard?$filter=Document_Type eq 'Instructions' and Status eq '{status}' and Contractor_No eq '{employeeView.No}'&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    InstructionRequestCard extensionRequest = new InstructionRequestCard
                    {
                        No = (string)config["No"],
                        Project_No = (string)config["Project_No"],
                        Project_Name = (string)config["Project_Name"],
                        Contractor_No = (string)config["Contractor_No"],
                        Contractor_Name = (string)config["Contractor_Name"],
                        Document_Type = (string)config["Document_Type"],
                        Date_of_Submittion = (string)config["Date_of_Submittion"],
                        Date_of_Receipt = (string)config["Date_of_Receipt"],
                        Received_By = (string)config["Received_By"],
                        Status = (string)config["Status"],
                    };
                    ExtensionRequestsList.Add(extensionRequest);

                }
            }
            return PartialView("~/Views/Contractor/PartialView/ContractorInstructionRequestsListPartialView.cshtml", ExtensionRequestsList);
        }
        public PartialViewResult ContractorInstructionTeamApprovalListPartialView(string teamApprovalStatus)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<InstructionRequestCard> ExtensionRequestsList = new List<InstructionRequestCard>();

            string title = "Team Approval";

            ViewBag.title = title;
            ViewBag.teamApprovalStatus = teamApprovalStatus;

            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            string page = ""; // Declare 'page' once at the top.
            /*page = $"Contract?$filter=Created_By eq '{employeeView.UserID}'&$format=json";*/
            page = $"InstructionRequestCard?$filter=Document_Type eq 'Instructions' and Status eq 'Team Approval' and Team_Approval_Status eq '{teamApprovalStatus}'&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    InstructionRequestCard ExtensionRequest = new InstructionRequestCard
                    {
                        No = (string)config["No"],
                        Project_No = (string)config["Project_No"],
                        Project_Name = (string)config["Project_Name"],
                        Contractor_No = (string)config["Contractor_No"],
                        Contractor_Name = (string)config["Contractor_Name"],
                        Document_Type = (string)config["Document_Type"],
                        Date_of_Submittion = (string)config["Date_of_Submittion"],
                        Date_of_Receipt = (string)config["Date_of_Receipt"],
                        Received_By = (string)config["Received_By"],
                        Status = (string)config["Status"],
                    };
                    ExtensionRequestsList.Add(ExtensionRequest);

                }
            }
            return PartialView("~/Views/Contractor/PartialView/ContractorInstructionRequestsListPartialView.cshtml", ExtensionRequestsList);
        }
        [HttpPost]
        public ActionResult ContractorInstructionRequestsDocumentView(string No)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            var isManager = IsManager(UserID);
            //var isSigningMember = IsSigningMember(UserID);

            List<InstructionRequestCard> ContractorInstructionRequests = new List<InstructionRequestCard>();

            string page = $"InstructionRequestCard?$filter=No eq '{No}'&$format=json";
            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                var config = details["value"].FirstOrDefault();
                if (config != null)
                {
                    InstructionRequestCard contractorInstructionRequest = new InstructionRequestCard
                    {
                        No = (string)config["No"],
                        Project_No = (string)config["Project_No"],
                        Project_Name = (string)config["Project_Name"],
                        Contractor_No = (string)config["Contractor_No"],
                        Contractor_Name = (string)config["Contractor_Name"],
                        Document_Type = (string)config["Document_Type"],
                        Date_of_Submittion = (string)config["Date_of_Submittion"],
                        Date_of_Receipt = (string)config["Date_of_Receipt"],
                        Received_By = (string)config["Received_By"],
                        Status = (string)config["Status"],
                        Instruction_Request = (string)config["Instruction_Request"],
                        Instruction_Notes = (string)config["Instruction_Notes"],
                        Current_Approving_Member = (string)config["Current_Approving_Member"],
                        Approving_Member_Name = (string)config["Approving_Member_Name"],
                        Asssigning_Employee = (string)config["Asssigning_Employee"],
                        Assigning_Emplyee_Name = (string)config["Assigning_Emplyee_Name"],
                        Deadline = (string)config["Deadline"],
                        Key_Comments = (string)config["Key_Comments"],
                        Signing_Comments = (string)config["Signing_Comments"],
                        Team_Lead_General_Comments = (string)config["Team_Lead_General_Comments"],
                        Team_Lead_Rejection_Comments = (string)config["Team_Lead_Rejection_Comments"],
                        Consolidated = Convert.ToBoolean(config["Consolidated"]),
                        Team_Approval_Status = (string)config["Team_Approval_Status"],
                        Sent_to_Ast_Directors = false,
                        PM_Comments = (string)config["PM_Comments"],
                        PM_Rejection_Comments = (string)config["PM_Rejection_Comments"],


                    };
                    ContractorInstructionRequests.Add(contractorInstructionRequest);
                }
            }
            ViewBag.loggedInUserID = StaffNo;
            ViewBag.isManager = isManager;
            return PartialView(ContractorInstructionRequests.FirstOrDefault());
        }


        //contractor Approval
        public ActionResult ContractorApprovalRequests()
        {
            try
            {
                if (Session["Username"] == null)
                    return RedirectToAction("Login", "Login");
                return View();
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }
        public PartialViewResult ContractorApprovalRequestsListPartialView(string status)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<InstructionRequestCard> ExtensionRequestsList = new List<InstructionRequestCard>();

            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            string title = status;

            ViewBag.title = title;

            string page = $"ApprovalRequestCard?$filter=Document_Type eq 'Approval' and Status eq '{status}' and Contractor_No eq '{employeeView.No}'&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    InstructionRequestCard extensionRequest = new InstructionRequestCard
                    {
                        No = (string)config["No"],
                        Project_No = (string)config["Project_No"],
                        Project_Name = (string)config["Project_Name"],
                        Contractor_No = (string)config["Contractor_No"],
                        Contractor_Name = (string)config["Contractor_Name"],
                        Document_Type = (string)config["Document_Type"],
                        Date_of_Submittion = (string)config["Date_of_Submittion"],
                        Date_of_Receipt = (string)config["Date_of_Receipt"],
                        Received_By = (string)config["Received_By"],
                        Status = (string)config["Status"],
                    };
                    ExtensionRequestsList.Add(extensionRequest);

                }
            }
            return PartialView("~/Views/Contractor/PartialView/ContractorApprovalRequestsListPartialView.cshtml", ExtensionRequestsList);
        }
        public PartialViewResult ContractorApprovalTeamApprovalListPartialView(string teamApprovalStatus)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<InstructionRequestCard> ExtensionRequestsList = new List<InstructionRequestCard>();

            string title = "Team Approval";

            ViewBag.title = title;
            ViewBag.teamApprovalStatus = teamApprovalStatus;

            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            string page = ""; // Declare 'page' once at the top.
            /*page = $"Contract?$filter=Created_By eq '{employeeView.UserID}'&$format=json";*/
            page = $"ApprovalRequestCard?$filter=Document_Type eq 'Approval' and Status eq 'Team Approval' and Team_Approval_Status eq '{teamApprovalStatus}'&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    InstructionRequestCard ExtensionRequest = new InstructionRequestCard
                    {
                        No = (string)config["No"],
                        Project_No = (string)config["Project_No"],
                        Project_Name = (string)config["Project_Name"],
                        Contractor_No = (string)config["Contractor_No"],
                        Contractor_Name = (string)config["Contractor_Name"],
                        Document_Type = (string)config["Document_Type"],
                        Date_of_Submittion = (string)config["Date_of_Submittion"],
                        Date_of_Receipt = (string)config["Date_of_Receipt"],
                        Received_By = (string)config["Received_By"],
                        Status = (string)config["Status"],

                    };
                    ExtensionRequestsList.Add(ExtensionRequest);

                }
            }
            return PartialView("~/Views/Contractor/PartialView/ContractorApprovalRequestsListPartialView.cshtml", ExtensionRequestsList);
        }
        [HttpPost]
        public ActionResult ContractorApprovalRequestsDocumentView(string No)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            var isManager = IsManager(UserID);
            List<InstructionRequestCard> ContractorApprovalRequests = new List<InstructionRequestCard>();

            string page = $"ApprovalRequestCard?$filter=No eq '{No}'&$format=json";
            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                var config = details["value"].FirstOrDefault();
                if (config != null)
                {
                    InstructionRequestCard ContractorApprovalRequest = new InstructionRequestCard
                    {
                        No = (string)config["No"],
                        Project_No = (string)config["Project_No"],
                        Project_Name = (string)config["Project_Name"],
                        Contractor_No = (string)config["Contractor_No"],
                        Contractor_Name = (string)config["Contractor_Name"],
                        Document_Type = (string)config["Document_Type"],
                        Date_of_Submittion = (string)config["Date_of_Submittion"],
                        Date_of_Receipt = (string)config["Date_of_Receipt"],
                        Received_By = (string)config["Received_By"],
                        Status = (string)config["Status"],
                        Instruction_Request = (string)config["Instruction_Request"],
                        Instruction_Notes = (string)config["Instruction_Notes"],
                        Current_Approving_Member = (string)config["Current_Approving_Member"],
                        Approving_Member_Name = (string)config["Approving_Member_Name"],
                        Asssigning_Employee = (string)config["Asssigning_Employee"],
                        Assigning_Emplyee_Name = (string)config["Assigning_Emplyee_Name"],
                        Deadline = (string)config["Deadline"],
                        Key_Comments = (string)config["Key_Comments"],
                        Signing_Comments = (string)config["Signing_Comments"],
                        Team_Lead_General_Comments = (string)config["Team_Lead_General_Comments"],
                        Team_Lead_Rejection_Comments = (string)config["Team_Lead_Rejection_Comments"],
                        Consolidated = Convert.ToBoolean(config["Consolidated"]),
                        Team_Approval_Status = (string)config["Team_Approval_Status"],
                        Sent_to_Ast_Directors = false

                    };
                    ContractorApprovalRequests.Add(ContractorApprovalRequest);
                }
            }
            ViewBag.loggedInUserID = StaffNo;
            ViewBag.isManager = isManager;
            return PartialView(ContractorApprovalRequests.FirstOrDefault());
        }



        //contractor payment
        public ActionResult ContractorPaymentRequests()
        {
            try
            {
                if (Session["Username"] == null)
                    return RedirectToAction("Login", "Login");
                return View();
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }
        public PartialViewResult ContractorPaymentRequestsListPartialView(string status)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<ContractorPaymentRequests> PaymentRequestsList = new List<ContractorPaymentRequests>();
            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            string title = status;
            ViewBag.title = title;


            string page = $"PaymentRequestCard?$filter=Document_Type eq 'Payments' and Status eq '{status}'  and Contractor_No eq '{employeeView.No}'&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    ContractorPaymentRequests PaymentRequest = new ContractorPaymentRequests
                    {
                        No = (string)config["No"],
                        Project_No = (string)config["Project_No"],
                        Project_Name = (string)config["Project_Name"],
                        Contractor_No = (string)config["Contractor_No"],
                        Contractor_Name = (string)config["Contractor_Name"],
                        Document_Type = (string)config["Document_Type"],
                        Date_of_Submittion = (DateTime?)config["Date_of_Submittion"] ?? default,
                        Date_of_Receipt = (DateTime?)config["Date_of_Receipt"] ?? default,
                        Received_By = (string)config["Received_By"],
                        Status = (string)config["Status"],
                        Project_Sum = (int?)config["Project_Sum"] ?? 0,
                        Retention_Amount = (int?)config["Retention_Amount"] ?? 0,
                        Project_Manager = (string)config["Project_Manager"],
                        Project_Manager_Name = (string)config["Project_Manager_Name"],
                        Team_Approval_Status = (string)config["Team_Approval_Status"],
                        Invoiced = (bool?)config["Invoiced"] ?? false,
                        Memo_Comments = (string)config["Memo_Comments"],
                        Approved_Amount = (int?)config["Approved_Amount"] ?? 0,
                        Approved_Variation_Addition = (int?)config["Approved_Variation_Addition"] ?? 0,
                        Apprroved_Variation_Omition = (int?)config["Apprroved_Variation_Omition"] ?? 0,
                        Actual_Work_Progress = (int?)config["Actual_Work_Progress"] ?? 0,
                        Approved_Certificates = (int?)config["Approved_Certificates"] ?? 0,
                        Certificate_No = (int?)config["Certificate_No"] ?? 0,
                        Contract_No = (string)config["Contract_No"],
                        Final_Contract_Value = (int?)config["Final_Contract_Value"] ?? 0,
                        Previously_Paid_Amount = (int?)config["Previously_Paid_Amount"] ?? 0,
                        Revised_Completion_Date = (string)config["Revised_Completion_Date"],
                        Valuation_Date = (string)config["Valuation_Date"],
                        Total_Net_Variation = (int?)config["Total_Net_Variation"] ?? 0,
                        Current_Approving_Member = (string)config["Current_Approving_Member"],
                        Approving_Member_Name = (string)config["Approving_Member_Name"],
                        Asssigning_Employee = (string)config["Asssigning_Employee"],
                        Assigning_Emplyee_Name = (string)config["Assigning_Emplyee_Name"],
                        Deadline = (string)config["Deadline"],
                        Team_Lead_General_Comments = (string)config["Team_Lead_General_Comments"],
                        Team_Lead_Rejection_Comments = (string)config["Team_Lead_Rejection_Comments"],
                        Key_Comments = (string)config["Key_Comments"],
                        Selected_SCM_Employee = (string)config["Selected_SCM_Employee"],
                        Selected_Employee_Name = (string)config["Selected_Employee_Name"],
                        Selected_user_Id = (string)config["Selected_user_Id"],
                        Committee_Assesment_Notes = (string)config["Committee_Assesment_Notes"],
                        Initial_Contract_End_Date = (string)config["Initial_Contract_End_Date"],

                        New_Contract_End_Date = (string)config["New_Contract_End_Date"],
                        Director_SCM_Comments = (string)config["Director_SCM_Comments"],
                        Professional_Opinion_Notes = (string)config["Professional_Opinion_Notes"],
                        SCM_Status = (string)config["SCM_Status"],
                        Procurement_Comments = (string)config["Procurement_Comments"],
                        Committee_Sec_No = (string)config["Committee_Sec_No"],
                        Secretary_Name = (string)config["Secretary_Name"],
                        Crj_Comments = (string)config["Crj_Comments"],
                        CRJ_General_Comments = (string)config["CRJ_General_Comments"],
                        Project_Manger_Communication = (string)config["Project_Manger_Communication"],


                    };
                    PaymentRequestsList.Add(PaymentRequest);

                }
            }
            return PartialView("~/Views/Contractor/PartialView/ContractorPaymentRequestsListPartialView.cshtml", PaymentRequestsList);
        }
        public PartialViewResult ContractorPaymentTeamApprovalRequestsListPartialView(string teamApprovalStatus)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<ContractorPaymentRequests> PaymentRequestsList = new List<ContractorPaymentRequests>();

            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;

            string title = "Team Approval";

            ViewBag.title = title;
            string page = "";




            if (teamApprovalStatus == "New")
            {
                page = $"PaymentRequestCard?$filter=Document_Type eq 'Payments' and Status eq 'Team Approval' and Team_Approval_Status eq '{teamApprovalStatus}' and Current_Approving_Member eq '{StaffNo}'  and Contractor_No eq '{employeeView.No}'&$format=json";
            }
            else
            {
                page = $"PaymentRequestCard?$filter=Document_Type eq 'Payments' and Status eq 'Team Approval' and Team_Approval_Status eq '{teamApprovalStatus}' and Asssigning_Employee eq '{StaffNo}'  and Contractor_No eq '{employeeView.No}'&$format=json";
            }

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    ContractorPaymentRequests PaymentRequest = new ContractorPaymentRequests
                    {
                        No = (string)config["No"],
                        Project_No = (string)config["Project_No"],
                        Project_Name = (string)config["Project_Name"],
                        Contractor_No = (string)config["Contractor_No"],
                        Contractor_Name = (string)config["Contractor_Name"],
                        Document_Type = (string)config["Document_Type"],
                        Date_of_Submittion = (DateTime?)config["Date_of_Submittion"] ?? default,
                        Date_of_Receipt = (DateTime?)config["Date_of_Receipt"] ?? default,
                        Received_By = (string)config["Received_By"],
                        Status = (string)config["Status"],
                        Project_Sum = (int?)config["Project_Sum"] ?? 0,
                        Retention_Amount = (int?)config["Retention_Amount"] ?? 0,
                        Project_Manager = (string)config["Project_Manager"],
                        Project_Manager_Name = (string)config["Project_Manager_Name"],
                        Team_Approval_Status = (string)config["Team_Approval_Status"],
                        Invoiced = (bool?)config["Invoiced"] ?? false,
                        Memo_Comments = (string)config["Memo_Comments"],
                        Approved_Amount = (int?)config["Approved_Amount"] ?? 0,
                        Approved_Variation_Addition = (int?)config["Approved_Variation_Addition"] ?? 0,
                        Apprroved_Variation_Omition = (int?)config["Apprroved_Variation_Omition"] ?? 0,
                        Actual_Work_Progress = (int?)config["Actual_Work_Progress"] ?? 0,
                        Approved_Certificates = (int?)config["Approved_Certificates"] ?? 0,
                        Certificate_No = (int?)config["Certificate_No"] ?? 0,
                        Contract_No = (string)config["Contract_No"],
                        Final_Contract_Value = (int?)config["Final_Contract_Value"] ?? 0,
                        Previously_Paid_Amount = (int?)config["Previously_Paid_Amount"] ?? 0,
                        Revised_Completion_Date = (string)config["Revised_Completion_Date"],
                        Valuation_Date = (string)config["Valuation_Date"],
                        Total_Net_Variation = (int?)config["Total_Net_Variation"] ?? 0,
                        Current_Approving_Member = (string)config["Current_Approving_Member"],
                        Approving_Member_Name = (string)config["Approving_Member_Name"],
                        Asssigning_Employee = (string)config["Asssigning_Employee"],
                        Assigning_Emplyee_Name = (string)config["Assigning_Emplyee_Name"],
                        Deadline = (string)config["Deadline"],
                        Team_Lead_General_Comments = (string)config["Team_Lead_General_Comments"],
                        Team_Lead_Rejection_Comments = (string)config["Team_Lead_Rejection_Comments"],
                        Key_Comments = (string)config["Key_Comments"],
                        Selected_SCM_Employee = (string)config["Selected_SCM_Employee"],
                        Selected_Employee_Name = (string)config["Selected_Employee_Name"],
                        Selected_user_Id = (string)config["Selected_user_Id"],
                        Committee_Assesment_Notes = (string)config["Committee_Assesment_Notes"],
                        Initial_Contract_End_Date = (string)config["Initial_Contract_End_Date"],

                        New_Contract_End_Date = (string)config["New_Contract_End_Date"],
                        Director_SCM_Comments = (string)config["Director_SCM_Comments"],
                        Professional_Opinion_Notes = (string)config["Professional_Opinion_Notes"],
                        SCM_Status = (string)config["SCM_Status"],
                        Procurement_Comments = (string)config["Procurement_Comments"],
                        Committee_Sec_No = (string)config["Committee_Sec_No"],
                        Secretary_Name = (string)config["Secretary_Name"],
                        Crj_Comments = (string)config["Crj_Comments"],
                        CRJ_General_Comments = (string)config["CRJ_General_Comments"],
                        Project_Manger_Communication = (string)config["Project_Manger_Communication"],


                    };
                    PaymentRequestsList.Add(PaymentRequest);

                }
            }
            return PartialView("~/Views/Contractor/PartialView/ContractorPaymentRequestsListPartialView.cshtml", PaymentRequestsList);
        }

        [HttpPost]
        public ActionResult ContractorPaymentRequestsDocumentView(string No)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            var isManager = IsManager(UserID);
            var isCRJ = IsCRJ(StaffNo);
            List<ContractorPaymentRequests> ContractorPaymentRequests = new List<ContractorPaymentRequests>();

            string page = $"PaymentRequestCard?$filter=No eq '{No}'&$format=json";
            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                var config = details["value"].FirstOrDefault();
                if (config != null)
                {
                    ContractorPaymentRequests contractorPaymentRequest = new ContractorPaymentRequests
                    {
                        No = (string)config["No"],
                        Project_No = (string)config["Project_No"],
                        Project_Name = (string)config["Project_Name"],
                        Contractor_No = (string)config["Contractor_No"],
                        Contractor_Name = (string)config["Contractor_Name"],
                        Document_Type = (string)config["Document_Type"],
                        Date_of_Submittion = (DateTime?)config["Date_of_Submittion"] ?? DateTime.MinValue,
                        Date_of_Receipt = (DateTime?)config["Date_of_Receipt"] ?? DateTime.MinValue,
                        Received_By = (string)config["Received_By"],
                        Status = (string)config["Status"],
                        Project_Sum = (int?)config["Project_Sum"] ?? 0,
                        Retention_Amount = (int?)config["Retention_Amount"] ?? 0,
                        Project_Manager = (string)config["Project_Manager"],
                        Project_Manager_Name = (string)config["Project_Manager_Name"],
                        Team_Approval_Status = (string)config["Team_Approval_Status"],
                        Invoiced = (bool?)config["Invoiced"] ?? false,
                        Memo_Comments = (string)config["Memo_Comments"],
                        Key_Comments = (string)config["Key_Comments"],
                        Approved_Amount = (int?)config["Approved_Amount"] ?? 0,
                        Approved_Variation_Addition = (int?)config["Approved_Variation_Addition"] ?? 0,
                        Apprroved_Variation_Omition = (int?)config["Apprroved_Variation_Omition"] ?? 0,
                        Actual_Work_Progress = (int?)config["Actual_Work_Progress"] ?? 0,
                        Approved_Certificates = (int?)config["Approved_Certificates"] ?? 0,
                        Certificate_No = (int?)config["Certificate_No"] ?? 0,
                        Contract_No = (string)config["Contract_No"],
                        Final_Contract_Value = (int?)config["Final_Contract_Value"] ?? 0,
                        Previously_Paid_Amount = (int?)config["Previously_Paid_Amount"] ?? 0,
                        Revised_Completion_Date = (string)config["Revised_Completion_Date"],
                        Valuation_Date = (string)config["Valuation_Date"],
                        Total_Net_Variation = (int?)config["Total_Net_Variation"] ?? 0,
                        Current_Approving_Member = (string)config["Current_Approving_Member"],
                        Approving_Member_Name = (string)config["Approving_Member_Name"],
                        Asssigning_Employee = (string)config["Asssigning_Employee"],
                        Assigning_Emplyee_Name = (string)config["Assigning_Emplyee_Name"],
                        Deadline = (string)config["Deadline"],
                        Team_Lead_General_Comments = (string)config["Team_Lead_General_Comments"],
                        Team_Lead_Rejection_Comments = (string)config["Team_Lead_Rejection_Comments"],
                        Selected_SCM_Employee = (string)config["Selected_SCM_Employee"],
                        Selected_Employee_Name = (string)config["Selected_Employee_Name"],
                        Selected_user_Id = (string)config["Selected_user_Id"],
                        I_x0026_A_Committee_Code = (string)config["I_x0026_A_Committee_Code"],
                        Committee_Assesment_Notes = (string)config["Committee_Assesment_Notes"],
                        Initial_Contract_End_Date = (string)config["Initial_Contract_End_Date"],
                        Recomendation = (string)config["Recomendation"],
                        New_Contract_End_Date = (string)config["New_Contract_End_Date"],
                        Director_SCM_Comments = (string)config["Director_SCM_Comments"],
                        Professional_Opinion_Notes = (string)config["Professional_Opinion_Notes"],
                        SCM_Status = (string)config["SCM_Status"],
                        Procurement_Comments = (string)config["Procurement_Comments"],
                        Committee_Sec_No = (string)config["Committee_Sec_No"],
                        Secretary_Name = (string)config["Secretary_Name"],
                        Crj_Comments = (string)config["Crj_Comments"],
                        CRJ_General_Comments = (string)config["CRJ_General_Comments"],
                        Project_Manger_Communication = (string)config["Project_Manger_Communication"],
                        isManager = isManager,
                        Sent_to_Ast_Directors = (bool?)config["Sent_to_Ast_Directors"] ?? false,
                        Addendum_Done = (bool?)config["Addendum_Done"] ?? false,
                        Consolidated = (bool?)config["Consolidated"] ?? false



                    };
                    ContractorPaymentRequests.Add(contractorPaymentRequest);
                }
            }
            ViewBag.isManager = isManager;
            ViewBag.loggedInUserID = StaffNo;
            ViewBag.isCRJ = isCRJ;
            return PartialView(ContractorPaymentRequests.FirstOrDefault());
        }
        public PartialViewResult ValuingMembersLinesPartialView(string No)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<PaymentRequestLines> PaymentRequestsLinesList = new List<PaymentRequestLines>();

            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;


            string page = $"PaymentRequestLines?$filter=Header_No eq '{No}'&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    PaymentRequestLines PaymentRequestLines = new PaymentRequestLines
                    {
                        Header_No = (string)config["Header_No"],
                        Line_No = (int)config["Line_No"],
                        Request_Description = (string)config["Request_Description"],
                        Section = (string)config["Section"],
                        Assistant_Director = (string)config["Assistant_Director"],
                        Assistant_Director_Name = (string)config["Assistant_Director_Name"],
                        Assigned_Team_Member = (string)config["Assigned_Team_Member"],
                        Resource_Name = (string)config["Resource_Name"],
                        Status = (string)config["Status"],
                        Deadline = (string)config["Deadline"],
                        No_of_Attachments = (int)config["No_of_Attachments"],
                        Comments = (string)config["Comments"],
                    };
                    PaymentRequestsLinesList.Add(PaymentRequestLines);

                }
            }
            return PartialView("~/Views/Contractor/PartialView/ValuingMembersLinesPartialView.cshtml", PaymentRequestsLinesList);
        }
        public PartialViewResult ValuationProjectBoqsLinesPartialView(string No)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<ValuationProjectBoqs> ValuationProjectBoqsList = new List<ValuationProjectBoqs>();

            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;


            string page = $"PracticalCompProjectBoqs?$filter=Project_Code eq '{No}'&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    ValuationProjectBoqs ValuationProjectBoq = new ValuationProjectBoqs
                    {
                        Project_Code = (string)config["Project_Code"],
                        Entry_No = (int)config["Entry_No"],
                        Line_Type = (string)config["Line_Type"],
                        Section = (string)config["Section"],
                        Quantity = (int)config["Quantity"],
                        Description = (string)config["Description"],
                        UOM = (string)config["UOM"],
                        Unit_Price = (decimal?)config["Unit_Price"] ?? 0,
                        Line_Amount = (decimal?)config["Line_Amount"] ?? 0,
                        Total_valued_Amount = (decimal?)config["Total_valued_Amount"] ?? 0,
                        Completion_Status = (string)config["Completion_Status"],
                        Comments = (string)config["Comments"],

                    }
                ;
                    ValuationProjectBoqsList.Add(ValuationProjectBoq);

                }
            }
            return PartialView("~/Views/Contractor/PartialView/ValuationProjectBoqsLinesPartialView.cshtml", ValuationProjectBoqsList);
        }
        public PartialViewResult PaymentValuationProjectBoqsLinesPartialView(string No)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<ProjectBoqs> ValuationProjectBoqsList = new List<ProjectBoqs>();

            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;


            string page = $"ValuationProjectBoqs?$filter=Project_Code eq '{No}'&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    ProjectBoqs ValuationProjectBoq = new ProjectBoqs
                    {
                        Project_Code = (string)config["Project_Code"],
                        Entry_No = (int)config["Entry_No"],
                        Line_Type = (string)config["Line_Type"],
                        Section = (string)config["Section"],
                        Description = (string)config["Description"],
                        Quantity = (int?)config["Quantity"] ?? 0,
                        UOM = (string)config["UOM"],
                        Unit_Price = (int?)config["Unit_Price"] ?? 0,
                        Line_Amount = (int?)config["Line_Amount"] ?? 0,
                        Total_valued_Qty = (int?)config["Total_valued_Qty"] ?? 0,
                        Total_valued_Amount = (int?)config["Total_valued_Amount"] ?? 0
                    }
                ;
                    ValuationProjectBoqsList.Add(ValuationProjectBoq);

                }
            }
            ViewBag.No = No;
            return PartialView("~/Views/Contractor/PartialView/PaymentValuationProjectBoqsLinesPartialView.cshtml", ValuationProjectBoqsList);
        }
        public JsonResult UpdateValuationBOQs(string Project_Code, int Entry_No, string Completion_Status, string Comments)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                string staffNo = Session["Username"].ToString();
                string msg = "";
                string userId = employeeView.UserID;
                bool result = false;
                /*result = Credentials.ObjNav.FnSubmitExtensionToTeamLead(DocNo, staffNo);*/
                if (result)
                {
                    msg = "Record updated successfully";
                    return Json(new { message = msg, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    msg = "Record Not updated. Try again";
                    return Json(new { message = msg, success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetExcelTemplate(string DocNo)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                string staffNo = Session["Username"].ToString();
                string msg = "";
                string userId = employeeView.UserID;
                string base64String = "";
                base64String = Credentials.ObjNav.ExportpaymentValuationToExcel(DocNo);
                if (base64String != "")
                {

                    return Json(new { message = base64String, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    msg = "No template found";
                    return Json(new { message = msg, success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public PartialViewResult FileUploadForm()
        {
            return PartialView("~/Views/Contractor/PartialView/FileAttachmentForm.cshtml");
        }
        public JsonResult ImportExcelTemplate(string DocNo, string base64Data)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                string staffNo = Session["Username"].ToString();
                string msg = "";
                string userId = employeeView.UserID;
                Credentials.ObjNav.ImportpaymentValuationFromExcel(DocNo, base64Data);

                msg = "Document uploaded successfully";
                return Json(new { message = msg, success = true }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }






        //contractor HOS 
        public ActionResult ContractorHOSRequests()
        {
            try
            {
                if (Session["Username"] == null)
                    return RedirectToAction("Login", "Login");
                return View();
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }
        public PartialViewResult ContractorHOSRequestsListPartialView(string status)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<HOSRequestscard> ExtensionRequestsList = new List<HOSRequestscard>();

            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            var isManager = IsManager(StaffNo);
            string title = status;

            ViewBag.title = title;

            /*string page = $"HOSRequestscard?$filter=Document_Type eq 'Hos Requests' and Status eq '{status}'&$format=json";*/
            string page = $"HOSRequestscard?$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    HOSRequestscard extensionRequest = new HOSRequestscard
                    {
                        No = (string)config["No"],
                        Project_No = (string)config["Project_No"],
                        Project_Name = (string)config["Project_Name"],
                        Contractor_No = (string)config["Contractor_No"],
                        Contractor_Name = (string)config["Contractor_Name"],
                        Request_Description = (string)config["Request_Description"],
                        Key_Comments = (string)config["Key_Comments"],
                        Status = config["Status"]?.ToString() ?? "Open",
                        isManager = isManager,
                        Sent_to_Ast_Directors = false


                    };
                    ExtensionRequestsList.Add(extensionRequest);

                }
            }
            return PartialView("~/Views/Contractor/PartialView/ContractorHOSRequestsListPartialView.cshtml", ExtensionRequestsList);
        }

        [HttpPost]
        public ActionResult ContractorHOSRequestsDocumentView(string No)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            var isManager = IsManager(StaffNo);
            List<HOSRequestscard> ContractorPaymentRequests = new List<HOSRequestscard>();

            string page = $"HOSRequestscard?$filter=No eq '{No}'&$format=json";
            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                var config = details["value"].FirstOrDefault();
                if (config != null)
                {
                    HOSRequestscard contractorPaymentRequest = new HOSRequestscard
                    {
                        No = (string)config["No"],
                        Project_No = (string)config["Project_No"],
                        Project_Name = (string)config["Project_Name"],
                        Contractor_No = (string)config["Contractor_No"],
                        Contractor_Name = (string)config["Contractor_Name"],
                        Request_Description = (string)config["Request_Description"],
                        Key_Comments = (string)config["Key_Comments"],
                        Status = config["Status"]?.ToString() ?? "Open",
                        isManager = isManager,
                        Sent_to_Ast_Directors = false

                    };
                    ContractorPaymentRequests.Add(contractorPaymentRequest);
                }
            }
            ViewBag.loggedInUserID = StaffNo;
            return PartialView(ContractorPaymentRequests.FirstOrDefault());
        }



        //contractor PM Communication
        public ActionResult ContractorPMCommunication()
        {
            try
            {
                if (Session["Username"] == null)
                    return RedirectToAction("Login", "Login");
                return View();
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }
        public PartialViewResult ContractorPMCommunicationListPartialView(string status)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<InstructionRequestCard> ExtensionRequestsList = new List<InstructionRequestCard>();

            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            string title = status;

            ViewBag.title = title;

            string page = $"ProjectManagerInstCard?$filter=Document_Type eq 'Project Manager Instructions' and Status eq '{status}'  and Contractor_No eq '{employeeView.No}'&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    InstructionRequestCard extensionRequest = new InstructionRequestCard
                    {
                        No = (string)config["No"],
                        Project_No = (string)config["Project_No"],
                        Project_Name = (string)config["Project_Name"],
                        Contractor_No = (string)config["Contractor_No"],
                        Contractor_Name = (string)config["Contractor_Name"],
                        Document_Type = (string)config["Document_Type"],
                        Date_of_Submittion = (string)config["Date_of_Submittion"],
                        Date_of_Receipt = (string)config["Date_of_Receipt"],
                        Received_By = (string)config["Received_By"],
                        Status = (string)config["Status"],
                    };
                    ExtensionRequestsList.Add(extensionRequest);

                }
            }
            return PartialView("~/Views/Contractor/PartialView/ContractorPMCommunicationListPartialView.cshtml", ExtensionRequestsList);
        }
        [HttpPost]
        public ActionResult ContractorPMCommunicationDocumentView(string No)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            var isManager = IsManager(UserID);
            List<ContractorExtensionRequests> ContractorPaymentRequests = new List<ContractorExtensionRequests>();

            string page = $"ProjectManagerInstCard?$filter=No eq '{No}'&$format=json";
            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                var config = details["value"].FirstOrDefault();
                if (config != null)
                {
                    ContractorExtensionRequests contractorPaymentRequest = new ContractorExtensionRequests
                    {
                        No = (string)config["No"],
                        Project_No = (string)config["Project_No"],
                        Project_Name = (string)config["Project_Name"],
                        Contractor_No = (string)config["Contractor_No"],
                        Contractor_Name = (string)config["Contractor_Name"],
                        Document_Type = (string)config["Document_Type"],
                        Date_of_Submittion = (DateTime?)config["Date_of_Submittion"] ?? default,
                        Date_of_Receipt = (DateTime?)config["Date_of_Receipt"] ?? default,
                        Received_By = (string)config["Received_By"],
                        Status = (string)config["Status"],
                        Project_Sum = (int?)config["Project_Sum"] ?? 0,
                        Retention_Amount = (int?)config["Retention_Amount"] ?? 0,
                        Project_Manager = (string)config["Project_Manager"],
                        Project_Manager_Name = (string)config["Project_Manager_Name"],
                        Team_Approval_Status = (string)config["Team_Approval_Status"],
                        Invoiced = (bool?)config["Invoiced"] ?? false,
                        Memo_Comments = (string)config["Memo_Comments"],
                        Approved_Amount = (int?)config["Approved_Amount"] ?? 0,
                        Approved_Variation_Addition = (int?)config["Approved_Variation_Addition"] ?? 0,
                        Apprroved_Variation_Omition = (int?)config["Apprroved_Variation_Omition"] ?? 0,
                        Actual_Work_Progress = (int?)config["Actual_Work_Progress"] ?? 0,
                        Approved_Certificates = (int?)config["Approved_Certificates"] ?? 0,
                        Certificate_No = (int?)config["Certificate_No"] ?? 0,
                        Contract_No = (string)config["Contract_No"],
                        Final_Contract_Value = (int?)config["Final_Contract_Value"] ?? 0,
                        Previously_Paid_Amount = (int?)config["Previously_Paid_Amount"] ?? 0,
                        Revised_Completion_Date = (string)config["Revised_Completion_Date"],
                        Valuation_Date = (string)config["Valuation_Date"],
                        Total_Net_Variation = (int?)config["Total_Net_Variation"] ?? 0,
                        Current_Approving_Member = (string)config["Current_Approving_Member"],
                        Approving_Member_Name = (string)config["Approving_Member_Name"],
                        Asssigning_Employee = (string)config["Asssigning_Employee"],
                        Assigning_Emplyee_Name = (string)config["Assigning_Emplyee_Name"],
                        Deadline = (string)config["Deadline"],
                        Team_Lead_General_Comments = (string)config["Team_Lead_General_Comments"],
                        Team_Lead_Rejection_Comments = (string)config["Team_Lead_Rejection_Comments"],
                        Key_Comments = (string)config["Key_Comments"],
                        Selected_SCM_Employee = (string)config["Selected_SCM_Employee"],
                        Selected_Employee_Name = (string)config["Selected_Employee_Name"],
                        Selected_user_Id = (string)config["Selected_user_Id"],
                        Committee_Assesment_Notes = (string)config["Committee_Assesment_Notes"],
                        Initial_Contract_End_Date = (string)config["Initial_Contract_End_Date"],
                        Action_Approved = (string)config["Action_Approved"],
                        Extension_Period = (string)config["Extension_Period"],
                        New_Contract_End_Date = (string)config["New_Contract_End_Date"],
                        Director_SCM_Comments = (string)config["Director_SCM_Comments"],
                        Professional_Opinion_Notes = (string)config["Professional_Opinion_Notes"],
                        SCM_Status = (string)config["SCM_Status"],
                        Procurement_Comments = (string)config["Procurement_Comments"],
                        Committee_Sec_No = (string)config["Committee_Sec_No"],
                        Secretary_Name = (string)config["Secretary_Name"],
                        Crj_Comments = (string)config["Crj_Comments"],
                        CRJ_General_Comments = (string)config["CRJ_General_Comments"],
                        Project_Manger_Communication = (string)config["Project_Manger_Communication"],
                        Contract_Sum_Execution_PercentodatamediaReadLink = (string)config["Contract_Sum_Execution_PercentodatamediaReadLink"],
                        isManager = isManager,
                        /*  Sent_to_Ast_Directors = (bool)config["Sent_to_Ast_Directors"]*/

                    };
                    ContractorPaymentRequests.Add(contractorPaymentRequest);
                }
            }
            ViewBag.loggedInUserID = StaffNo;
            return PartialView(ContractorPaymentRequests.FirstOrDefault());
        }

        //contractor practical
        public ActionResult ContractorPracticalCompletion()
        {
            try
            {
                if (Session["Username"] == null)
                    return RedirectToAction("Login", "Login");
                return View();
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }
        public PartialViewResult ContractorPracticalListPartialView(string status)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<ContractorExtensionRequests> ExtensionRequestsList = new List<ContractorExtensionRequests>();

            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            string title = status;

            ViewBag.title = title;


            string page = $"PracticalCompletionForm?$filter=Document_Type eq 'Practical Completion' and Status eq '{status}' and Contractor_No eq '{employeeView.No}'&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    ContractorExtensionRequests extensionRequest = new ContractorExtensionRequests
                    {
                        No = (string)config["No"],
                        Project_No = (string)config["Project_No"],
                        Project_Name = (string)config["Project_Name"],
                        Contractor_No = (string)config["Contractor_No"],
                        Contractor_Name = (string)config["Contractor_Name"],
                        Document_Type = (string)config["Document_Type"],
                        Date_of_Submittion = (DateTime?)config["Date_of_Submittion"] ?? default,
                        Date_of_Receipt = (DateTime?)config["Date_of_Receipt"] ?? default,
                        Received_By = (string)config["Received_By"],
                        Status = (string)config["Status"],
                        Project_Sum = (int?)config["Project_Sum"] ?? 0,
                        Retention_Amount = (int?)config["Retention_Amount"] ?? 0,
                        Project_Manager = (string)config["Project_Manager"],
                        Project_Manager_Name = (string)config["Project_Manager_Name"],
                        Team_Approval_Status = (string)config["Team_Approval_Status"],
                        Invoiced = (bool?)config["Invoiced"] ?? false,
                        Memo_Comments = (string)config["Memo_Comments"],
                        Approved_Amount = (int?)config["Approved_Amount"] ?? 0,
                        Approved_Variation_Addition = (int?)config["Approved_Variation_Addition"] ?? 0,
                        Apprroved_Variation_Omition = (int?)config["Apprroved_Variation_Omition"] ?? 0,
                        Actual_Work_Progress = (int?)config["Actual_Work_Progress"] ?? 0,
                        Approved_Certificates = (int?)config["Approved_Certificates"] ?? 0,
                        Certificate_No = (int?)config["Certificate_No"] ?? 0,
                        Contract_No = (string)config["Contract_No"],
                        Final_Contract_Value = (int?)config["Final_Contract_Value"] ?? 0,
                        Previously_Paid_Amount = (int?)config["Previously_Paid_Amount"] ?? 0,
                        Revised_Completion_Date = (string)config["Revised_Completion_Date"],
                        Valuation_Date = (string)config["Valuation_Date"],
                        Total_Net_Variation = (int?)config["Total_Net_Variation"] ?? 0,
                        Current_Approving_Member = (string)config["Current_Approving_Member"],
                        Approving_Member_Name = (string)config["Approving_Member_Name"],
                        Asssigning_Employee = (string)config["Asssigning_Employee"],
                        Assigning_Emplyee_Name = (string)config["Assigning_Emplyee_Name"],
                        Deadline = (string)config["Deadline"],
                        Team_Lead_General_Comments = (string)config["Team_Lead_General_Comments"],
                        Team_Lead_Rejection_Comments = (string)config["Team_Lead_Rejection_Comments"],
                        Key_Comments = (string)config["Key_Comments"],
                        Selected_SCM_Employee = (string)config["Selected_SCM_Employee"],
                        Selected_Employee_Name = (string)config["Selected_Employee_Name"],
                        Selected_user_Id = (string)config["Selected_user_Id"],
                        Committee_Assesment_Notes = (string)config["Committee_Assesment_Notes"],
                        Initial_Contract_End_Date = (string)config["Initial_Contract_End_Date"],
                        Action_Approved = (string)config["Action_Approved"],
                        Extension_Period = (string)config["Extension_Period"],
                        New_Contract_End_Date = (string)config["New_Contract_End_Date"],
                        Director_SCM_Comments = (string)config["Director_SCM_Comments"],
                        Professional_Opinion_Notes = (string)config["Professional_Opinion_Notes"],
                        SCM_Status = (string)config["SCM_Status"],
                        Procurement_Comments = (string)config["Procurement_Comments"],
                        Committee_Sec_No = (string)config["Committee_Sec_No"],
                        Secretary_Name = (string)config["Secretary_Name"],
                        Crj_Comments = (string)config["Crj_Comments"],
                        CRJ_General_Comments = (string)config["CRJ_General_Comments"],
                        Project_Manger_Communication = (string)config["Project_Manger_Communication"],
                        Contract_Sum_Execution_PercentodatamediaReadLink = (string)config["Contract_Sum_Execution_PercentodatamediaReadLink"]
                    };
                    ExtensionRequestsList.Add(extensionRequest);

                }
            }
            return PartialView("~/Views/Contractor/PartialView/ContractorPracticalListPartialView.cshtml", ExtensionRequestsList);
        }
        public PartialViewResult ContractorPracticalTeamApprovalListPartialView(string teamApprovalStatus)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<ContractorExtensionRequests> ExtensionRequestsList = new List<ContractorExtensionRequests>();

            string title = "Team Approval";

            ViewBag.title = title;
            ViewBag.teamApprovalStatus = teamApprovalStatus;

            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            string page = ""; // Declare 'page' once at the top.
            /*page = $"Contract?$filter=Created_By eq '{employeeView.UserID}'&$format=json";*/

            if (teamApprovalStatus == "New")
            {
                page = $"PracticalCompletionForm?$filter=Document_Type eq 'Practical Completion' and Status eq 'Team Approval' and Team_Approval_Status eq '{teamApprovalStatus}' and Current_Approving_Member eq '{StaffNo}'  and Contractor_No eq '{employeeView.No}'&$format=json";
            }
            else
            {

                page = $"PracticalCompletionForm?$filter=Document_Type eq 'Practical Completion' and Status eq 'Team Approval' and Team_Approval_Status eq '{teamApprovalStatus}' and Asssigning_Employee eq '{StaffNo}'  and Contractor_No eq '{employeeView.No}'&$format=json";
            }


            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    ContractorExtensionRequests ExtensionRequest = new ContractorExtensionRequests
                    {
                        No = (string)config["No"],
                        Project_No = (string)config["Project_No"],
                        Project_Name = (string)config["Project_Name"],
                        Contractor_No = (string)config["Contractor_No"],
                        Contractor_Name = (string)config["Contractor_Name"],
                        Document_Type = (string)config["Document_Type"],
                        Date_of_Submittion = (DateTime?)config["Date_of_Submittion"] ?? default,
                        Date_of_Receipt = (DateTime?)config["Date_of_Receipt"] ?? default,
                        Received_By = (string)config["Received_By"],
                        Status = (string)config["Status"],
                        Project_Sum = (int?)config["Project_Sum"] ?? 0,
                        Retention_Amount = (int?)config["Retention_Amount"] ?? 0,
                        Project_Manager = (string)config["Project_Manager"],
                        Project_Manager_Name = (string)config["Project_Manager_Name"],
                        Team_Approval_Status = (string)config["Team_Approval_Status"],
                        Invoiced = (bool?)config["Invoiced"] ?? false,
                        Memo_Comments = (string)config["Memo_Comments"],
                        Approved_Amount = (int?)config["Approved_Amount"] ?? 0,
                        Approved_Variation_Addition = (int?)config["Approved_Variation_Addition"] ?? 0,
                        Apprroved_Variation_Omition = (int?)config["Apprroved_Variation_Omition"] ?? 0,
                        Actual_Work_Progress = (int?)config["Actual_Work_Progress"] ?? 0,
                        Approved_Certificates = (int?)config["Approved_Certificates"] ?? 0,
                        Certificate_No = (int?)config["Certificate_No"] ?? 0,
                        Contract_No = (string)config["Contract_No"],
                        Final_Contract_Value = (int?)config["Final_Contract_Value"] ?? 0,
                        Previously_Paid_Amount = (int?)config["Previously_Paid_Amount"] ?? 0,
                        Revised_Completion_Date = (string)config["Revised_Completion_Date"],
                        Valuation_Date = (string)config["Valuation_Date"],
                        Total_Net_Variation = (int?)config["Total_Net_Variation"] ?? 0,
                        Current_Approving_Member = (string)config["Current_Approving_Member"],
                        Approving_Member_Name = (string)config["Approving_Member_Name"],
                        Asssigning_Employee = (string)config["Asssigning_Employee"],
                        Assigning_Emplyee_Name = (string)config["Assigning_Emplyee_Name"],
                        Deadline = (string)config["Deadline"],
                        Team_Lead_General_Comments = (string)config["Team_Lead_General_Comments"],
                        Team_Lead_Rejection_Comments = (string)config["Team_Lead_Rejection_Comments"],
                        Key_Comments = (string)config["Key_Comments"],
                        Selected_SCM_Employee = (string)config["Selected_SCM_Employee"],
                        Selected_Employee_Name = (string)config["Selected_Employee_Name"],
                        Selected_user_Id = (string)config["Selected_user_Id"],
                        Committee_Assesment_Notes = (string)config["Committee_Assesment_Notes"],
                        Initial_Contract_End_Date = (string)config["Initial_Contract_End_Date"],
                        Action_Approved = (string)config["Action_Approved"],
                        Extension_Period = (string)config["Extension_Period"],
                        New_Contract_End_Date = (string)config["New_Contract_End_Date"],
                        Director_SCM_Comments = (string)config["Director_SCM_Comments"],
                        Professional_Opinion_Notes = (string)config["Professional_Opinion_Notes"],
                        SCM_Status = (string)config["SCM_Status"],
                        Procurement_Comments = (string)config["Procurement_Comments"],
                        Committee_Sec_No = (string)config["Committee_Sec_No"],
                        Secretary_Name = (string)config["Secretary_Name"],
                        Crj_Comments = (string)config["Crj_Comments"],
                        CRJ_General_Comments = (string)config["CRJ_General_Comments"],
                        Project_Manger_Communication = (string)config["Project_Manger_Communication"],
                        Contract_Sum_Execution_PercentodatamediaReadLink = (string)config["Contract_Sum_Execution_PercentodatamediaReadLink"]
                    };
                    ExtensionRequestsList.Add(ExtensionRequest);

                }
            }
            return PartialView("~/Views/Contractor/PartialView/ContractorPracticalListPartialView.cshtml", ExtensionRequestsList);
        }
        [HttpPost]
        public ActionResult ContractorPracticalDocumentView(string No)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;

            var isManager = IsManager(UserID);
            List<PracticalCompletionForm> ContractorPaymentRequests = new List<PracticalCompletionForm>();

            string page = $"PracticalCompletionForm?$filter=No eq '{No}'&$format=json";
            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                var config = details["value"].FirstOrDefault();
                if (config != null)
                {
                    PracticalCompletionForm contractorPaymentRequest = new PracticalCompletionForm
                    {
                        No = (string)config["No"],
                        Project_No = (string)config["Project_No"],
                        Project_Name = (string)config["Project_Name"],
                        Contractor_No = (string)config["Contractor_No"],
                        Contractor_Name = (string)config["Contractor_Name"],
                        Document_Type = (string)config["Document_Type"],
                        Date_of_Submittion = (string)config["Date_of_Submittion"],
                        Date_of_Receipt = (string)config["Date_of_Receipt"],
                        Received_By = (string)config["Received_By"],
                        Status = (string)config["Status"],
                        Project_Sum = (int?)config["Project_Sum"] ?? 0,
                        Retention_Amount = (int?)config["Retention_Amount"] ?? 0,
                        Project_Manager = (string)config["Project_Manager"],
                        Project_Manager_Name = (string)config["Project_Manager_Name"],
                        Team_Approval_Status = (string)config["Team_Approval_Status"],
                        Invoiced = (bool?)config["Invoiced"] ?? false,
                        Memo_Comments = (string)config["Memo_Comments"],
                        Approved_Amount = (int?)config["Approved_Amount"] ?? 0,
                        Approved_Variation_Addition = (int?)config["Approved_Variation_Addition"] ?? 0,
                        Apprroved_Variation_Omition = (int?)config["Apprroved_Variation_Omition"] ?? 0,
                        Actual_Work_Progress = (int?)config["Actual_Work_Progress"] ?? 0,
                        Approved_Certificates = (int?)config["Approved_Certificates"] ?? 0,
                        Certificate_No = (int?)config["Certificate_No"] ?? 0,
                        Contract_No = (string)config["Contract_No"],
                        Final_Contract_Value = (int?)config["Final_Contract_Value"] ?? 0,
                        Previously_Paid_Amount = (int?)config["Previously_Paid_Amount"] ?? 0,
                        Revised_Completion_Date = (string)config["Revised_Completion_Date"],
                        Valuation_Date = (string)config["Valuation_Date"],
                        Total_Net_Variation = (int?)config["Total_Net_Variation"] ?? 0,
                        Current_Approving_Member = (string)config["Current_Approving_Member"],
                        Approving_Member_Name = (string)config["Approving_Member_Name"],
                        Asssigning_Employee = (string)config["Asssigning_Employee"],
                        Assigning_Emplyee_Name = (string)config["Assigning_Emplyee_Name"],
                        Deadline = (string)config["Deadline"],
                        Team_Lead_General_Comments = (string)config["Team_Lead_General_Comments"],
                        Team_Lead_Rejection_Comments = (string)config["Team_Lead_Rejection_Comments"],
                        Key_Comments = (string)config["Key_Comments"],
                        Selected_SCM_Employee = (string)config["Selected_SCM_Employee"],
                        Selected_Employee_Name = (string)config["Selected_Employee_Name"],
                        Selected_user_Id = (string)config["Selected_user_Id"],
                        Committee_Assesment_Notes = (string)config["Committee_Assesment_Notes"],
                        Initial_Contract_End_Date = (string)config["Initial_Contract_End_Date"],
                        Consolidated = (bool)config["Consolidated"],
                        New_Contract_End_Date = (string)config["New_Contract_End_Date"],
                        Director_SCM_Comments = (string)config["Director_SCM_Comments"],
                        Professional_Opinion_Notes = (string)config["Professional_Opinion_Notes"],
                        SCM_Status = (string)config["SCM_Status"],
                        Procurement_Comments = (string)config["Procurement_Comments"],
                        Committee_Sec_No = (string)config["Committee_Sec_No"],
                        Secretary_Name = (string)config["Secretary_Name"],
                        Crj_Comments = (string)config["Crj_Comments"],
                        CRJ_General_Comments = (string)config["CRJ_General_Comments"],
                        Project_Manger_Communication = (string)config["Project_Manger_Communication"],



                    }
                ;
                    ContractorPaymentRequests.Add(contractorPaymentRequest);
                }
            }
            ViewBag.isManager = isManager;
            ViewBag.loggedInUserID = StaffNo;
            return PartialView(ContractorPaymentRequests.FirstOrDefault());
        }

        //contractor Good Defects
        public ActionResult ContractorGoodDefects()
        {
            try
            {
                if (Session["Username"] == null)
                    return RedirectToAction("Login", "Login");
                return View();
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }
        public PartialViewResult ContractorGoodDefectsListPartialView(string status)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<ContractorExtensionRequests> ExtensionRequestsList = new List<ContractorExtensionRequests>();

            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            string title = status;

            ViewBag.title = title;


            string page = $"MakingGoodDefectCard?$filter=Document_Type eq 'Making Good' and Status eq '{status}'  and Contractor_No eq '{employeeView.No}'&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    ContractorExtensionRequests extensionRequest = new ContractorExtensionRequests
                    {
                        No = (string)config["No"],
                        Project_No = (string)config["Project_No"],
                        Project_Name = (string)config["Project_Name"],
                        Contractor_No = (string)config["Contractor_No"],
                        Contractor_Name = (string)config["Contractor_Name"],
                        Document_Type = (string)config["Document_Type"],
                        Date_of_Submittion = (DateTime?)config["Date_of_Submittion"] ?? default,
                        Date_of_Receipt = (DateTime?)config["Date_of_Receipt"] ?? default,
                        Received_By = (string)config["Received_By"],
                        Status = (string)config["Status"],
                        Project_Sum = (int?)config["Project_Sum"] ?? 0,
                        Retention_Amount = (int?)config["Retention_Amount"] ?? 0,
                        Project_Manager = (string)config["Project_Manager"],
                        Project_Manager_Name = (string)config["Project_Manager_Name"],
                        Team_Approval_Status = (string)config["Team_Approval_Status"],
                        Invoiced = (bool?)config["Invoiced"] ?? false,
                        Memo_Comments = (string)config["Memo_Comments"],
                        Approved_Amount = (int?)config["Approved_Amount"] ?? 0,
                        Approved_Variation_Addition = (int?)config["Approved_Variation_Addition"] ?? 0,
                        Apprroved_Variation_Omition = (int?)config["Apprroved_Variation_Omition"] ?? 0,
                        Actual_Work_Progress = (int?)config["Actual_Work_Progress"] ?? 0,
                        Approved_Certificates = (int?)config["Approved_Certificates"] ?? 0,
                        Certificate_No = (int?)config["Certificate_No"] ?? 0,
                        Contract_No = (string)config["Contract_No"],
                        Final_Contract_Value = (int?)config["Final_Contract_Value"] ?? 0,
                        Previously_Paid_Amount = (int?)config["Previously_Paid_Amount"] ?? 0,
                        Revised_Completion_Date = (string)config["Revised_Completion_Date"],
                        Valuation_Date = (string)config["Valuation_Date"],
                        Total_Net_Variation = (int?)config["Total_Net_Variation"] ?? 0,
                        Current_Approving_Member = (string)config["Current_Approving_Member"],
                        Approving_Member_Name = (string)config["Approving_Member_Name"],
                        Asssigning_Employee = (string)config["Asssigning_Employee"],
                        Assigning_Emplyee_Name = (string)config["Assigning_Emplyee_Name"],
                        Deadline = (string)config["Deadline"],
                        Team_Lead_General_Comments = (string)config["Team_Lead_General_Comments"],
                        Team_Lead_Rejection_Comments = (string)config["Team_Lead_Rejection_Comments"],
                        Key_Comments = (string)config["Key_Comments"],
                        Selected_SCM_Employee = (string)config["Selected_SCM_Employee"],
                        Selected_Employee_Name = (string)config["Selected_Employee_Name"],
                        Selected_user_Id = (string)config["Selected_user_Id"],
                        Committee_Assesment_Notes = (string)config["Committee_Assesment_Notes"],
                        Initial_Contract_End_Date = (string)config["Initial_Contract_End_Date"],
                        Action_Approved = (string)config["Action_Approved"],
                        Extension_Period = (string)config["Extension_Period"],
                        New_Contract_End_Date = (string)config["New_Contract_End_Date"],
                        Director_SCM_Comments = (string)config["Director_SCM_Comments"],
                        Professional_Opinion_Notes = (string)config["Professional_Opinion_Notes"],
                        SCM_Status = (string)config["SCM_Status"],
                        Procurement_Comments = (string)config["Procurement_Comments"],
                        Committee_Sec_No = (string)config["Committee_Sec_No"],
                        Secretary_Name = (string)config["Secretary_Name"],
                        Crj_Comments = (string)config["Crj_Comments"],
                        CRJ_General_Comments = (string)config["CRJ_General_Comments"],
                        Project_Manger_Communication = (string)config["Project_Manger_Communication"],
                        Contract_Sum_Execution_PercentodatamediaReadLink = (string)config["Contract_Sum_Execution_PercentodatamediaReadLink"]
                    };
                    ExtensionRequestsList.Add(extensionRequest);

                }
            }
            return PartialView("~/Views/Contractor/PartialView/ContractorGoodDefectsListPartialView.cshtml", ExtensionRequestsList);
        }
        public PartialViewResult ContractorGoodDefectsTeamApprovalListPartialView(string teamApprovalStatus)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<ContractorExtensionRequests> ExtensionRequestsList = new List<ContractorExtensionRequests>();

            string title = "Team Approval";

            ViewBag.title = title;
            ViewBag.teamApprovalStatus = teamApprovalStatus;

            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            string page = ""; // Declare 'page' once at the top.
            /*page = $"Contract?$filter=Created_By eq '{employeeView.UserID}'&$format=json";*/
            page = $"MakingGoodDefectCard?$filter=Document_Type eq 'Practical Completion' and Status eq 'Team Approval' and Team_Approval_Status eq '{teamApprovalStatus}'  and Contractor_No eq '{employeeView.No}'&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    ContractorExtensionRequests ExtensionRequest = new ContractorExtensionRequests
                    {
                        No = (string)config["No"],
                        Project_No = (string)config["Project_No"],
                        Project_Name = (string)config["Project_Name"],
                        Contractor_No = (string)config["Contractor_No"],
                        Contractor_Name = (string)config["Contractor_Name"],
                        Document_Type = (string)config["Document_Type"],
                        Date_of_Submittion = (DateTime?)config["Date_of_Submittion"] ?? default,
                        Date_of_Receipt = (DateTime?)config["Date_of_Receipt"] ?? default,
                        Received_By = (string)config["Received_By"],
                        Status = (string)config["Status"],
                        Project_Sum = (int?)config["Project_Sum"] ?? 0,
                        Retention_Amount = (int?)config["Retention_Amount"] ?? 0,
                        Project_Manager = (string)config["Project_Manager"],
                        Project_Manager_Name = (string)config["Project_Manager_Name"],
                        Team_Approval_Status = (string)config["Team_Approval_Status"],
                        Invoiced = (bool?)config["Invoiced"] ?? false,
                        Memo_Comments = (string)config["Memo_Comments"],
                        Approved_Amount = (int?)config["Approved_Amount"] ?? 0,
                        Approved_Variation_Addition = (int?)config["Approved_Variation_Addition"] ?? 0,
                        Apprroved_Variation_Omition = (int?)config["Apprroved_Variation_Omition"] ?? 0,
                        Actual_Work_Progress = (int?)config["Actual_Work_Progress"] ?? 0,
                        Approved_Certificates = (int?)config["Approved_Certificates"] ?? 0,
                        Certificate_No = (int?)config["Certificate_No"] ?? 0,
                        Contract_No = (string)config["Contract_No"],
                        Final_Contract_Value = (int?)config["Final_Contract_Value"] ?? 0,
                        Previously_Paid_Amount = (int?)config["Previously_Paid_Amount"] ?? 0,
                        Revised_Completion_Date = (string)config["Revised_Completion_Date"],
                        Valuation_Date = (string)config["Valuation_Date"],
                        Total_Net_Variation = (int?)config["Total_Net_Variation"] ?? 0,
                        Current_Approving_Member = (string)config["Current_Approving_Member"],
                        Approving_Member_Name = (string)config["Approving_Member_Name"],
                        Asssigning_Employee = (string)config["Asssigning_Employee"],
                        Assigning_Emplyee_Name = (string)config["Assigning_Emplyee_Name"],
                        Deadline = (string)config["Deadline"],
                        Team_Lead_General_Comments = (string)config["Team_Lead_General_Comments"],
                        Team_Lead_Rejection_Comments = (string)config["Team_Lead_Rejection_Comments"],
                        Key_Comments = (string)config["Key_Comments"],
                        Selected_SCM_Employee = (string)config["Selected_SCM_Employee"],
                        Selected_Employee_Name = (string)config["Selected_Employee_Name"],
                        Selected_user_Id = (string)config["Selected_user_Id"],
                        Committee_Assesment_Notes = (string)config["Committee_Assesment_Notes"],
                        Initial_Contract_End_Date = (string)config["Initial_Contract_End_Date"],
                        Action_Approved = (string)config["Action_Approved"],
                        Extension_Period = (string)config["Extension_Period"],
                        New_Contract_End_Date = (string)config["New_Contract_End_Date"],
                        Director_SCM_Comments = (string)config["Director_SCM_Comments"],
                        Professional_Opinion_Notes = (string)config["Professional_Opinion_Notes"],
                        SCM_Status = (string)config["SCM_Status"],
                        Procurement_Comments = (string)config["Procurement_Comments"],
                        Committee_Sec_No = (string)config["Committee_Sec_No"],
                        Secretary_Name = (string)config["Secretary_Name"],
                        Crj_Comments = (string)config["Crj_Comments"],
                        CRJ_General_Comments = (string)config["CRJ_General_Comments"],
                        Project_Manger_Communication = (string)config["Project_Manger_Communication"],
                        Contract_Sum_Execution_PercentodatamediaReadLink = (string)config["Contract_Sum_Execution_PercentodatamediaReadLink"]
                    };
                    ExtensionRequestsList.Add(ExtensionRequest);

                }
            }
            return PartialView("~/Views/Contractor/PartialView/ContractorPracticalListPartialView.cshtml", ExtensionRequestsList);
        }
        [HttpPost]
        public ActionResult ContractorGoodDefectsDocumentView(string No)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            var isManager = IsManager(UserID);
            List<ContractorExtensionRequests> ContractorPaymentRequests = new List<ContractorExtensionRequests>();

            string page = $"ExtensionRequestCard?$filter=No eq '{No}'&$format=json";
            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                var config = details["value"].FirstOrDefault();
                if (config != null)
                {
                    ContractorExtensionRequests contractorPaymentRequest = new ContractorExtensionRequests
                    {
                        No = (string)config["No"],
                        Project_No = (string)config["Project_No"],
                        Project_Name = (string)config["Project_Name"],
                        Contractor_No = (string)config["Contractor_No"],
                        Contractor_Name = (string)config["Contractor_Name"],
                        Document_Type = (string)config["Document_Type"],
                        Date_of_Submittion = (DateTime?)config["Date_of_Submittion"] ?? default,
                        Date_of_Receipt = (DateTime?)config["Date_of_Receipt"] ?? default,
                        Received_By = (string)config["Received_By"],
                        Status = (string)config["Status"],
                        Project_Sum = (int?)config["Project_Sum"] ?? 0,
                        Retention_Amount = (int?)config["Retention_Amount"] ?? 0,
                        Project_Manager = (string)config["Project_Manager"],
                        Project_Manager_Name = (string)config["Project_Manager_Name"],
                        Team_Approval_Status = (string)config["Team_Approval_Status"],
                        Invoiced = (bool?)config["Invoiced"] ?? false,
                        Memo_Comments = (string)config["Memo_Comments"],
                        Approved_Amount = (int?)config["Approved_Amount"] ?? 0,
                        Approved_Variation_Addition = (int?)config["Approved_Variation_Addition"] ?? 0,
                        Apprroved_Variation_Omition = (int?)config["Apprroved_Variation_Omition"] ?? 0,
                        Actual_Work_Progress = (int?)config["Actual_Work_Progress"] ?? 0,
                        Approved_Certificates = (int?)config["Approved_Certificates"] ?? 0,
                        Certificate_No = (int?)config["Certificate_No"] ?? 0,
                        Contract_No = (string)config["Contract_No"],
                        Final_Contract_Value = (int?)config["Final_Contract_Value"] ?? 0,
                        Previously_Paid_Amount = (int?)config["Previously_Paid_Amount"] ?? 0,
                        Revised_Completion_Date = (string)config["Revised_Completion_Date"],
                        Valuation_Date = (string)config["Valuation_Date"],
                        Total_Net_Variation = (int?)config["Total_Net_Variation"] ?? 0,
                        Current_Approving_Member = (string)config["Current_Approving_Member"],
                        Approving_Member_Name = (string)config["Approving_Member_Name"],
                        Asssigning_Employee = (string)config["Asssigning_Employee"],
                        Assigning_Emplyee_Name = (string)config["Assigning_Emplyee_Name"],
                        Deadline = (string)config["Deadline"],
                        Team_Lead_General_Comments = (string)config["Team_Lead_General_Comments"],
                        Team_Lead_Rejection_Comments = (string)config["Team_Lead_Rejection_Comments"],
                        Key_Comments = (string)config["Key_Comments"],
                        Selected_SCM_Employee = (string)config["Selected_SCM_Employee"],
                        Selected_Employee_Name = (string)config["Selected_Employee_Name"],
                        Selected_user_Id = (string)config["Selected_user_Id"],
                        Committee_Assesment_Notes = (string)config["Committee_Assesment_Notes"],
                        Initial_Contract_End_Date = (string)config["Initial_Contract_End_Date"],
                        Action_Approved = (string)config["Action_Approved"],
                        Extension_Period = (string)config["Extension_Period"],
                        New_Contract_End_Date = (string)config["New_Contract_End_Date"],
                        Director_SCM_Comments = (string)config["Director_SCM_Comments"],
                        Professional_Opinion_Notes = (string)config["Professional_Opinion_Notes"],
                        SCM_Status = (string)config["SCM_Status"],
                        Procurement_Comments = (string)config["Procurement_Comments"],
                        Committee_Sec_No = (string)config["Committee_Sec_No"],
                        Secretary_Name = (string)config["Secretary_Name"],
                        Crj_Comments = (string)config["Crj_Comments"],
                        CRJ_General_Comments = (string)config["CRJ_General_Comments"],
                        Project_Manger_Communication = (string)config["Project_Manger_Communication"],
                        Contract_Sum_Execution_PercentodatamediaReadLink = (string)config["Contract_Sum_Execution_PercentodatamediaReadLink"]



                    };
                    ContractorPaymentRequests.Add(contractorPaymentRequest);
                }
            }
            ViewBag.isManager = isManager;
            ViewBag.loggedInUserID = UserID;
            return PartialView(ContractorPaymentRequests.FirstOrDefault());
        }

        //Project Closure
        public ActionResult ProjectClosure()
        {
            try
            {
                if (Session["Username"] == null)
                    return RedirectToAction("Login", "Login");
                return View();
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }
        public PartialViewResult ProjectClosureListPartialView(string status)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<ProjectClosure> ProjectClosureList = new List<ProjectClosure>();

            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            string title = status;
            ViewBag.title = title;

            string page = $"ProjectClosure?$filter= Contractor_No eq '{employeeView.No}'&$format=json";


            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    ProjectClosure closure = new ProjectClosure
                    {
                        No = (string)config["No"],
                        Project_No = (string)config["Project_No"],
                        Project_Name = (string)config["Project_Name"],
                        Contractor_No = (string)config["Contractor_No"],
                        Contractor_Name = (string)config["Contractor_Name"],
                        Completion_Date = (string)config["Completion_Date"],
                        Completion_Comments = (string)config["Completion_Comments"],
                        Portal_Status = (string)config["Portal_Status"],
                        Approval_Status = (string)config["Approval_Status"]
                    };
                    ProjectClosureList.Add(closure);

                }
            }
            return PartialView("~/Views/Contractor/PartialView/ProjectClosureListPartialView.cshtml", ProjectClosureList);
        }
        [HttpPost]
        public ActionResult ProjectClosureDocumentView(string No)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            List<ProjectClosure> closureList = new List<ProjectClosure>();

            string page = $"ProjectClosure?$filter=No eq '{No}'&$format=json";
            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                var config = details["value"].FirstOrDefault();
                if (config != null)
                {
                    ProjectClosure closure = new ProjectClosure
                    {
                        No = (string)config["No"],
                        Project_No = (string)config["Project_No"],
                        Project_Name = (string)config["Project_Name"],
                        Contractor_No = (string)config["Contractor_No"],
                        Contractor_Name = (string)config["Contractor_Name"],
                        Completion_Date = (string)config["Completion_Date"],
                        Completion_Comments = (string)config["Completion_Comments"],
                        Portal_Status = (string)config["Portal_Status"],
                        Approval_Status = (string)config["Approval_Status"]
                    };
                    closureList.Add(closure);
                }
            }
            return PartialView(closureList.FirstOrDefault());
        }


        //Closed Projects
        public ActionResult ClosedProjects()
        {
            try
            {
                if (Session["Username"] == null)
                    return RedirectToAction("Login", "Login");
                return View();
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }
        public PartialViewResult ClosedProjectsListPartialView(string status)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<JobList> ClosedProjectList = new List<JobList>();

            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            string title = status;
            ViewBag.title = title;

            string page = $"JobCard?$filter=Contractor_No eq '{employeeView.No}'&$format=json";
            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    JobList closedProject = new JobList
                    {
                        No = (string)config["No"],
                        Description = (string)config["Description"],
                        Bill_to_Customer_No = (string)config["Bill_to_Customer_No"],
                        Status = (string)config["Status"],
                        Person_Responsible = (string)config["Person_Responsible"],
                        Search_Description = (string)config["Search_Description"],
                        Project_Manager = (string)config["Project_Manager"]



                    };
                    ClosedProjectList.Add(closedProject);

                }
            }
            return PartialView("~/Views/Contractor/PartialView/ClosedProjectsListPartialView.cshtml", ClosedProjectList);
        }
        [HttpPost]
        public ActionResult ClosedProjectsDocumentView(string No)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            List<JobCard> closedProjectsList = new List<JobCard>();

            string page = $"JobCard?$filter=No eq '{No}'&$format=json";
            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                var config = details["value"].FirstOrDefault();
                if (config != null)
                {
                    JobCard closedProject = new JobCard
                    {
                        No = (string)config["No"],
                        Description = (string)config["Description"],
                        Project_Code = (string)config["Project_Code"],
                        Research_Center = (string)config["Research_Center"],
                        County_ID = (string)config["County_ID"],
                        SubCounty = (string)config["SubCounty"],
                        Project_Sum = (int?)config["Project_Sum"] ?? 0,
                        valued_Amount = (int?)config["valued_Amount"] ?? 0,
                        _x0031_st_Moeity_Amount = (int?)config["_x0031_st_Moeity_Amount"] ?? 0,
                        _x0032_nd_Moeity_Amount = (int?)config["_x0032_nd_Moeity_Amount"] ?? 0,
                        Contract_Agreement_Date = (string)config["Contract_Agreement_Date"],
                        Intended_Completion_Date = (string)config["Intended_Completion_Date"],
                        Date_of_Taking_Over = (string)config["Date_of_Taking_Over"],
                        Global_Dimension_2_Code = (string)config["Global_Dimension_2_Code"],
                        Sell_to_Customer_No = (string)config["Sell_to_Customer_No"],
                        Sell_to_Customer_Name = (string)config["Sell_to_Customer_Name"],
                        Sell_to_Address = (string)config["Sell_to_Address"],
                        Sell_to_Address_2 = (string)config["Sell_to_Address_2"],
                        Sell_to_City = (string)config["Sell_to_City"],
                        Sell_to_County = (string)config["Sell_to_County"],
                        Sell_to_Post_Code = (string)config["Sell_to_Post_Code"],
                        Sell_to_Country_Region_Code = (string)config["Sell_to_Country_Region_Code"],
                        Sell_to_Contact_No = (string)config["Sell_to_Contact_No"],
                        SellToPhoneNo = (string)config["SellToPhoneNo"],
                        SellToMobilePhoneNo = (string)config["SellToMobilePhoneNo"],
                        SellToEmail = (string)config["SellToEmail"],
                        Sell_to_Contact = (string)config["Sell_to_Contact"],
                        Search_Description = (string)config["Search_Description"],
                        External_Document_No = (string)config["External_Document_No"],
                        Your_Reference = (string)config["Your_Reference"],
                        Person_Responsible = (string)config["Person_Responsible"],
                        Blocked = (string)config["Blocked"],
                        Last_Date_Modified = (string)config["Last_Date_Modified"],
                        Opportunity_Reference = (string)config["Opportunity_Reference"],
                        Fund_Opportunity_Name = (string)config["Fund_Opportunity_Name"],
                        Grant_Amount = (int?)config["Grant_Amount"] ?? 0,
                        exchequer = (bool?)config["exchequer"] ?? false,
                        Tender_No = (string)config["Tender_No"],
                        Contractor_No = (string)config["Contractor_No"],
                        Contractor_Name = (string)config["Contractor_Name"],
                        Contract_Amount = (double?)config["Contract_Amount"] ?? 0.0,
                        Project_Manager_Name = (string)config["Project_Manager_Name"],
                        Projected_Monthly_Cashflow = (int?)config["Projected_Monthly_Cashflow"] ?? 0,
                        Actual_Monthly_Cashflow = (int?)config["Actual_Monthly_Cashflow"] ?? 0,
                        Cashflow_Variance = (int?)config["Cashflow_Variance"] ?? 0,
                        Audited_Acc_Current_Ratios = (int?)config["Audited_Acc_Current_Ratios"] ?? 0,
                        Valuation_No = (int?)config["Valuation_No"] ?? 0,
                        Valuation_Amount = (int?)config["Valuation_Amount"] ?? 0,
                        Physical_Progress = (int?)config["Physical_Progress"] ?? 0,
                        Revised_Finish_Date = (string)config["Revised_Finish_Date"],
                        Contract_Duration = (string)config["Contract_Duration"],
                        _x0033__Months_Cont_Expiry_Notice = (bool?)config["_x0033__Months_Cont_Expiry_Notice"] ?? false,
                        Perfomace_Bond_Expiry = (string)config["Perfomace_Bond_Expiry"],
                        _x0033__Months_PB_Expiry_Notice = (bool?)config["_x0033__Months_PB_Expiry_Notice"] ?? false,
                        Insurance_Expiry = (string)config["Insurance_Expiry"],
                        Programm_of_Work = (string)config["Programm_of_Work"],
                        Date_of_Site_Meeting = (string)config["Date_of_Site_Meeting"],
                        Date_of_Last_Site_Minutes = (string)config["Date_of_Last_Site_Minutes"],
                        Date_of_Last_MC_Report = (string)config["Date_of_Last_MC_Report"],
                        Expected_Monthly_Progress = (int?)config["Expected_Monthly_Progress"] ?? 0,
                        Actual_Monthly_Progress = (int?)config["Actual_Monthly_Progress"] ?? 0,
                        Progress_Variance = (int?)config["Progress_Variance"] ?? 0,
                        Date_of_Last_Defects_Report = (string)config["Date_of_Last_Defects_Report"],
                        Cum_No_of_Defects_Reports = (int?)config["Cum_No_of_Defects_Reports"] ?? 0,
                        Cum_No_of_Req_for_Amendment = (int?)config["Cum_No_of_Req_for_Amendment"] ?? 0,
                        Current_Workplan = (string)config["Current_Workplan"],
                        Status = (string)config["Status"],
                        Job_Posting_Group = (string)config["Job_Posting_Group"],
                        WIP_Method = (string)config["WIP_Method"],
                        WIP_Posting_Method = (string)config["WIP_Posting_Method"],
                        Allow_Schedule_Contract_Lines = (bool?)config["Allow_Schedule_Contract_Lines"] ?? false,
                        Apply_Usage_Link = (bool?)config["Apply_Usage_Link"] ?? false,
                        Percent_Completed = (int?)config["Percent_Completed"] ?? 0,
                        Percent_Invoiced = (int?)config["Percent_Invoiced"] ?? 0,
                        Percent_of_Overdue_Planning_Lines = (int?)config["Percent_of_Overdue_Planning_Lines"] ?? 0,
                        BillToOptions = (string)config["BillToOptions"],
                        Bill_to_Customer_No = (string)config["Bill_to_Customer_No"],
                        Bill_to_Name = (string)config["Bill_to_Name"],
                        Funding_Source = (string)config["Funding_Source"],
                        Project_Category = (string)config["Project_Category"],
                        Project_Funding_Contract_No = (string)config["Project_Funding_Contract_No"],
                        External_Contract_Reference = (string)config["External_Contract_Reference"],
                        Bill_to_Address = (string)config["Bill_to_Address"],
                        Bill_to_Address_2 = (string)config["Bill_to_Address_2"],
                        Bill_to_City = (string)config["Bill_to_City"],
                        Bill_to_County = (string)config["Bill_to_County"],
                        Bill_to_Post_Code = (string)config["Bill_to_Post_Code"],
                        Bill_to_Country_Region_Code = (string)config["Bill_to_Country_Region_Code"],
                        Bill_to_Contact_No = (string)config["Bill_to_Contact_No"],
                        ContactPhoneNo = (string)config["ContactPhoneNo"],
                        ContactMobilePhoneNo = (string)config["ContactMobilePhoneNo"],
                        ContactEmail = (string)config["ContactEmail"],
                        Bill_to_Contact = (string)config["Bill_to_Contact"],
                        Project_Manager = (string)config["Project_Manager"],
                        Datetime_Sent_to_Inititation = (DateTime?)config["Datetime_Sent_to_Inititation"] ?? default,
                        Initiation_Comments = (string)config["Initiation_Comments"],
                        Datetime_Sent_to_Planning = (DateTime?)config["Datetime_Sent_to_Planning"] ?? default,
                        Planning_Comments = (string)config["Planning_Comments"],
                        Datetime_Sent_to_Monitoring = (DateTime?)config["Datetime_Sent_to_Monitoring"] ?? default,
                        Monitoring_Comments = (string)config["Monitoring_Comments"],
                        Payment_Terms_Code = (string)config["Payment_Terms_Code"],
                        Payment_Method_Code = (string)config["Payment_Method_Code"],
                        ShippingOptions = (string)config["ShippingOptions"],
                        Ship_to_Code = (string)config["Ship_to_Code"],
                        Ship_to_Name = (string)config["Ship_to_Name"],
                        Ship_to_Address = (string)config["Ship_to_Address"],
                        Ship_to_Address_2 = (string)config["Ship_to_Address_2"],
                        Ship_to_City = (string)config["Ship_to_City"],
                        Ship_to_County = (string)config["Ship_to_County"],
                        Ship_to_Post_Code = (string)config["Ship_to_Post_Code"],
                        Ship_to_Country_Region_Code = (string)config["Ship_to_Country_Region_Code"],
                        Ship_to_Contact = (string)config["Ship_to_Contact"],
                        Starting_Date = (string)config["Starting_Date"],
                        Ending_Date = (string)config["Ending_Date"],
                        Creation_Date = (string)config["Creation_Date"],
                        Currency_Code = (string)config["Currency_Code"],
                        Invoice_Currency_Code = (string)config["Invoice_Currency_Code"],
                        Price_Calculation_Method = (string)config["Price_Calculation_Method"],
                        Cost_Calculation_Method = (string)config["Cost_Calculation_Method"],
                        Exch_Calculation_Cost = (string)config["Exch_Calculation_Cost"],
                        Exch_Calculation_Price = (string)config["Exch_Calculation_Price"],
                        WIP_Posting_Date = (string)config["WIP_Posting_Date"],
                        Total_WIP_Sales_Amount = (int?)config["Total_WIP_Sales_Amount"] ?? 0,
                        Applied_Sales_G_L_Amount = (int?)config["Applied_Sales_G_L_Amount"] ?? 0,
                        Total_WIP_Cost_Amount = (int?)config["Total_WIP_Cost_Amount"] ?? 0,
                        Applied_Costs_G_L_Amount = (int?)config["Applied_Costs_G_L_Amount"] ?? 0,
                        Recog_Sales_Amount = (int?)config["Recog_Sales_Amount"] ?? 0,
                        Recog_Costs_Amount = (int?)config["Recog_Costs_Amount"] ?? 0,
                        Recog_Profit_Amount = (int?)config["Recog_Profit_Amount"] ?? 0,
                        Recog_Profit_Percent = (int?)config["Recog_Profit_Percent"] ?? 0,
                        Acc_WIP_Costs_Amount = (int?)config["Acc_WIP_Costs_Amount"] ?? 0,
                        Acc_WIP_Sales_Amount = (int?)config["Acc_WIP_Sales_Amount"] ?? 0,
                        Calc_Recog_Sales_Amount = (int?)config["Calc_Recog_Sales_Amount"] ?? 0,
                        Calc_Recog_Costs_Amount = (int?)config["Calc_Recog_Costs_Amount"] ?? 0,
                        WIP_G_L_Posting_Date = (string)config["WIP_G_L_Posting_Date"],
                        Total_WIP_Sales_G_L_Amount = (int?)config["Total_WIP_Sales_G_L_Amount"] ?? 0,
                        Total_WIP_Cost_G_L_Amount = (int?)config["Total_WIP_Cost_G_L_Amount"] ?? 0,
                        Recog_Sales_G_L_Amount = (int?)config["Recog_Sales_G_L_Amount"] ?? 0,
                        Recog_Costs_G_L_Amount = (int?)config["Recog_Costs_G_L_Amount"] ?? 0,
                        Recog_Profit_G_L_Amount = (int?)config["Recog_Profit_G_L_Amount"] ?? 0,
                        Recog_Profit_G_L_Percent = (int?)config["Recog_Profit_G_L_Percent"] ?? 0,
                        Calc_Recog_Sales_G_L_Amount = (int?)config["Calc_Recog_Sales_G_L_Amount"] ?? 0,
                        Calc_Recog_Costs_G_L_Amount = (int?)config["Calc_Recog_Costs_G_L_Amount"] ?? 0
                    };
                    closedProjectsList.Add(closedProject);
                }
            }
            return PartialView(closedProjectsList.FirstOrDefault());
        }
        public PartialViewResult ClosedProjectBoqsLinesPartialView(string No)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<ProjectBoqs> ValuationProjectBoqsList = new List<ProjectBoqs>();

            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;


            string page = $"ProjectBoqs?$filter=Project_Code eq '{No}'&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    ProjectBoqs ValuationProjectBoq = new ProjectBoqs
                    {
                        Project_Code = (string)config["Project_Code"],
                        Entry_No = (int)config["Entry_No"],
                        Line_Type = (string)config["Line_Type"],
                        Section = (string)config["Section"],
                        Description = (string)config["Description"],
                        Quantity = (int)config["Quantity"],
                        UOM = (string)config["UOM"],
                        Unit_Price = (int)config["Unit_Price"],
                        Line_Amount = (int)config["Line_Amount"],
                        Total_valued_Qty = (int)config["Total_valued_Qty"],
                        Total_valued_Amount = (int)config["Total_valued_Amount"]

                    };
                    ValuationProjectBoqsList.Add(ValuationProjectBoq);

                }
            }
            return PartialView("~/Views/Contractor/PartialView/ClosedProjectBoqsLinesPartialView.cshtml", ValuationProjectBoqsList);
        }
        public PartialViewResult ContractorMilestoneLines(string No)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<MilestonesLines> ValuationProjectBoqsList = new List<MilestonesLines>();

            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;


            string page = $"MilestonesLines?$filter=Project_No eq '{No}'&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    MilestonesLines ValuationProjectBoq = new MilestonesLines
                    {
                        Project_No = (string)config["Project_No"],
                        Task_No = (string)config["Task_No"],
                        Milestone_Code = (string)config["Milestone_Code"],
                        Milestone_Description = (string)config["Milestone_Description"],
                        Milestone_Start_Date = (string)config["Milestone_Start_Date"],
                        Milestone_End_Date = (string)config["Milestone_End_Date"],
                        Notification_Period = (string)config["Notification_Period"],
                        Notfification_Date = (string)config["Notfification_Date"],
                        Actual_Start_Date = (string)config["Actual_Start_Date"],
                        Actual_End_Date = (string)config["Actual_End_Date"],
                        Status = (string)config["Status"]
                    };
                    ValuationProjectBoqsList.Add(ValuationProjectBoq);

                }
            }
            return PartialView("~/Views/Contractor/PartialView/ContractorMilestoneLines.cshtml", ValuationProjectBoqsList);
        }

        //Milestone Update
        public ActionResult MilestoneUpdate()
        {
            try
            {
                if (Session["Username"] == null)
                    return RedirectToAction("Login", "Login");
                return View();
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }
        public PartialViewResult MilestoneUpdateListPartialView(string status)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<ProjectClosure> ProjectClosureList = new List<ProjectClosure>();

            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            string title = status;
            ViewBag.title = title;

            string page = $"ProjectClosure?$filter=Approval_Status ne 'Open'&format=json";


            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    ProjectClosure closure = new ProjectClosure
                    {
                        No = (string)config["No"],
                        Project_No = (string)config["Project_No"],
                        Project_Name = (string)config["Project_Name"],
                        Contractor_No = (string)config["Contractor_No"],
                        Contractor_Name = (string)config["Contractor_Name"],
                        Completion_Date = (string)config["Completion_Date"],
                        Completion_Comments = (string)config["Completion_Comments"],
                        Portal_Status = (string)config["Portal_Status"],
                        Approval_Status = (string)config["Approval_Status"]
                    };
                    ProjectClosureList.Add(closure);

                }
            }
            return PartialView("~/Views/Contractor/PartialView/ProjectClosureListPartialView.cshtml", ProjectClosureList);
        }
        [HttpPost]
        public ActionResult MilestoneUpdateDocumentView(string No)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            List<ProjectClosure> closureList = new List<ProjectClosure>();

            string page = $"ProjectClosure?$filter=No eq '{No}'&$format=json";
            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                var config = details["value"].FirstOrDefault();
                if (config != null)
                {
                    ProjectClosure closure = new ProjectClosure
                    {
                        No = (string)config["No"],
                        Project_No = (string)config["Project_No"],
                        Project_Name = (string)config["Project_Name"],
                        Contractor_No = (string)config["Contractor_No"],
                        Contractor_Name = (string)config["Contractor_Name"],
                        Completion_Date = (string)config["Completion_Date"],
                        Completion_Comments = (string)config["Completion_Comments"],
                        Portal_Status = (string)config["Portal_Status"],
                        Approval_Status = (string)config["Approval_Status"]
                    };
                    closureList.Add(closure);
                }
            }
            return PartialView(closureList.FirstOrDefault());
        }
        private bool IsManager(string userID)
        {
            #region Manager Check
            bool isManager = false; // Default to false
            string pageUser = $"UserSetup?$filter=User_ID eq '{userID}'&$format=json";

            try
            {
                HttpWebResponse httpResponseUser = Credentials.GetOdataData(pageUser);
                using (var streamReader = new StreamReader(httpResponseUser.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    // Check if "value" contains records
                    if (details["value"] != null && details["value"].HasValues)
                    {
                        foreach (JObject config in details["value"])
                        {
                            // If any record indicates the user is a manager, set isManager to true
                            if (config["Project_Manager"] != null && (bool)config["Project_Manager"])
                            {
                                isManager = true;
                                break; // No need to check further
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error (you can replace this with your logging mechanism)
                Console.WriteLine($"Error checking if user is a manager: {ex.Message}");
            }
            #endregion

            return isManager;
        }
        private bool IsCRJ(string StaffNo)
        {
            #region CRJ Check
            bool isCRJ = false;
            string pageUser = $"EmployeeList?$filter=No eq '{StaffNo}'&$format=json";

            try
            {
                HttpWebResponse httpResponseUser = Credentials.GetOdataData(pageUser);
                using (var streamReader = new StreamReader(httpResponseUser.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    if (details["value"] != null && details["value"].HasValues)
                    {
                        foreach (JObject config in details["value"])
                        {
                            if (config["Current_Position_ID"]?.ToString() == "CRJ001" || config["Current_Position_ID"]?.ToString() == "CRJ002")
                            {
                                isCRJ = true;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error checking if user is a Director: {ex.Message}");
            }
            #endregion

            return isCRJ;
        }


        //contractor extension actions
        public JsonResult ReceivePracCompletionRequest(string DocNo)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                string staffNo = Session["Username"].ToString();
                string msg = "";
                string userId = employeeView.UserID;
                bool result = false;
                result = Credentials.ObjNav.FnReceiveContractorRequest(DocNo, staffNo);
                if (result)
                {
                    msg = "Record Successfully Received";
                    return Json(new { message = msg, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    msg = "Record Not received. Try again";
                    return Json(new { message = msg, success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SendPracCompletionToAssistantDirector(string DocNo)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                string staffNo = Session["Username"].ToString();
                string msg = "";
                string userId = employeeView.UserID;
                bool result = false;
                result = Credentials.ObjNav.FnSubmitContractorRequestToAstDirectors(DocNo, staffNo);
                if (result)
                {
                    msg = "Record Successfully Submitted";
                    return Json(new { message = msg, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    msg = "Record Not submitted. Try again";
                    return Json(new { message = msg, success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SubmitPracCompletionToTeamLeadForApproval(string DocNo, string Memo_Comments, string Key_Comments)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                string staffNo = Session["Username"].ToString();
                string msg = "";
                string userId = employeeView.UserID;
                bool result = false;
                Credentials.ObjNav.FnSubmitToTeamleadForApproval(DocNo, staffNo);

                msg = "Record Successfully Submitted";
                return Json(new { message = msg, success = true }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult TeamLeadApprovePracCompletion(string DocNo, string Team_Lead_General_Comments)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                string staffNo = Session["Username"].ToString();
                string msg = "";
                string userId = employeeView.UserID;
                bool result = false;
                Credentials.ObjNav.FnApproveSubmissionPracComp(DocNo, Team_Lead_General_Comments, staffNo);
                /* result = Credentials.ObjNav.FnApproveSubmission(DocNo);*/

                msg = "Record Successfully Submitted";
                return Json(new { message = msg, success = true }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult TeamLeadApprovePayment(string DocNo, string Team_Lead_General_Comments)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                string staffNo = Session["Username"].ToString();
                string msg = "";
                string userId = employeeView.UserID;
                bool result = false;
                Credentials.ObjNav.FnApproveSubmissionPayment(DocNo, Team_Lead_General_Comments, staffNo);
                //Credentials.ObjNav.FnApproveSubmissionPracComp(DocNo, Team_Lead_General_Comments, staffNo);

                msg = "Record Successfully Submitted";
                return Json(new { message = msg, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult RejectPracCompletionSubmission(string DocNo, string Team_Lead_General_Comments)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                string staffNo = Session["Username"].ToString();
                string msg = "";
                string userId = employeeView.UserID;
                bool result = false;
                result = Credentials.ObjNav.FnTeamLeadRejectExtSubmission(DocNo, staffNo, Team_Lead_General_Comments);
                if (result)
                {
                    msg = "Record Rejected";
                    return Json(new { message = msg, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    msg = "Record Not submitted. Try again";
                    return Json(new { message = msg, success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult ApprovePracCompletionAndSubmitToCRJ(string DocNo, string Key_Comments)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                string staffNo = Session["Username"].ToString();
                string msg = "";
                string userId = employeeView.UserID;
                bool result = false;
                result = Credentials.ObjNav.FnPMApproveExtandSubmitToCRJ(DocNo, staffNo, Key_Comments);
                if (result)
                {
                    msg = "Record Successfully Approved";
                    return Json(new { message = msg, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    msg = "Record Not submitted. Try again";
                    return Json(new { message = msg, success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult PMRejectPracCompletionSubmission(string DocNo, string Key_Comments)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                string staffNo = Session["Username"].ToString();
                string msg = "";
                string userId = employeeView.UserID;
                bool result = false;
                result = Credentials.ObjNav.FnPMRejectExtRequest(DocNo, staffNo, Key_Comments);
                if (result)
                {
                    msg = "Record Rejected";
                    return Json(new { message = msg, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    msg = "Record Not submitted. Try again";
                    return Json(new { message = msg, success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SendPracCompletionToSupplyChain(string DocNo, string Key_Comments)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                string staffNo = Session["Username"].ToString();
                string msg = "";
                string userId = employeeView.UserID;
                bool result = false;
                result = Credentials.ObjNav.FnCRJApproveAndSubmitToDSCM(DocNo, staffNo, Key_Comments);
                if (result)
                {
                    msg = "Record Successfully Approved";
                    return Json(new { message = msg, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    msg = "Record Not submitted. Try again";
                    return Json(new { message = msg, success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult CRJRejectPracCompletionSubmission(string DocNo, string Key_Comments)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                string staffNo = Session["Username"].ToString();
                string msg = "";
                string userId = employeeView.UserID;
                bool result = false;
                result = Credentials.ObjNav.FnCRJRejectExtRequest(DocNo, staffNo, Key_Comments);
                if (result)
                {
                    msg = "Record Rejected";
                    return Json(new { message = msg, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    msg = "Request Not submitted. Try again";
                    return Json(new { message = msg, success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult RaiseExpendtureRequisition(string DocNo, string Key_Comments)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                string staffNo = Session["Username"].ToString();
                string msg = "";
                string userId = employeeView.UserID;
                string result = "";
                result = Credentials.ObjNav.FnCreateEpenditureRequisition(DocNo, userId);
                if (result != "")
                {
                    msg = "Record Successfully Approved";
                    return Json(new { message = msg, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    msg = "Record Not submitted. Try again";
                    return Json(new { message = msg, success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SendToContractorForAcknowledgement(string DocNo, string Key_Comments)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                string staffNo = Session["Username"].ToString();
                string msg = "";
                string userId = employeeView.UserID;
                bool result = false;
                /*result = Credentials.ObjNav.FnCRJApproveAndSubmitToDSCM(DocNo, staffNo, Key_Comments);*/
                if (result)
                {
                    msg = "Record Successfully Approved";
                    return Json(new { message = msg, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    msg = "Record Not submitted. Try again";
                    return Json(new { message = msg, success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult NewRequest()
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    ContractorExtensionRequests request = new ContractorExtensionRequests();
                    Session["httpResponse"] = null;
                    EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;


                    #region projects
                    List<DropdownList> projectsList = new List<DropdownList>();
                    string pageProjects = $"ProjectList?$filter=Contractor_No eq '{employeeView.No}'&$format=json";

                    HttpWebResponse httpResponseProjects = Credentials.GetOdataData(pageProjects);
                    using (var streamReader = new StreamReader(httpResponseProjects.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var details = JObject.Parse(result);
                        foreach (JObject config in details["value"])
                        {
                            DropdownList dropdownList3 = new DropdownList();
                            dropdownList3.Text = (string)config["No"];
                            dropdownList3.Value = (string)config["No"];
                            projectsList.Add(dropdownList3);
                        }
                    }
                    #endregion


                    request.ListOfProjects = projectsList.Select(x =>
                        new SelectListItem()
                        {
                            Text = x.Text,
                            Value = x.Value
                        }).ToList();


                    return PartialView("~/Views/Contractor/PartialView/NewRequest.cshtml", request);

                }
            }

            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }

        public JsonResult GetContractorNo(string selectedValue)
        {
            try
            {
                var contractorData = new List<object>();
                string page = $"ProjectList?$filter=No eq '{selectedValue}'&$format=json";

                HttpWebResponse httpResponseOutput = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponseOutput.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    if (details["value"] != null) // Ensure "value" key exists
                    {
                        foreach (JObject config in details["value"])
                        {
                            contractorData.Add(new
                            {
                                Description = (string)config["Description"] ?? "",
                                ContractorNo = (string)config["Contractor_No"] ?? ""
                            });
                        }
                    }
                }

                return Json(new { contractorData, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SubmitRequest(string Project_No, string Contractor_No, int docType)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                string Responsible_Employee_No = employeeView.No;
                string UserID = employeeView.UserID;



                string staffNo = Session["Username"].ToString();
                EmployeeView employee = Session["EmployeeData"] as EmployeeView;
                //string userId = employee.UserID;

                string docNo = Credentials.ObjNav.FnCreateContractorRequestHeader(
                    employeeView.No,
                    Project_No,
                    docType
                );

                //Credentials.ObjNav.fnGetPortalUser()



                if (docNo != "")
                {
                    string Redirect = docNo;
                    //Session["SuccessMsg"] = "Purchase Requisition, Document No: " + DocNo + ", created Successfully. Add line(s) and attachment(s) then sent for approval";
                    return Json(new { message = Redirect, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { message = "Error submitting record. Try again.", success = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SendRequestForProcessing(string RequestNo)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                string Responsible_Employee_No = employeeView.No;
                string UserID = employeeView.UserID;



                string staffNo = Session["Username"].ToString();
                EmployeeView employee = Session["EmployeeData"] as EmployeeView;
                //string userId = employee.UserID;
                bool result = false;
                result = Credentials.ObjNav.FnSubmitContractorRequest(RequestNo);
                if (result)
                {

                    return Json(new { message = "Request successfully sent", success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { message = "Error sending request record. Try again.", success = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SubmitContractorInstructionRequest(string RequestNo, string Contractor_Comment)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                string Responsible_Employee_No = employeeView.No;
                string UserID = employeeView.UserID;

                string staffNo = Session["Username"].ToString();
                EmployeeView employee = Session["EmployeeData"] as EmployeeView;
                //string userId = employee.UserID;
                bool result = false;
                result = Credentials.ObjNav.FnSubmitContractorInstructionRequest(RequestNo, Contractor_Comment);
                if (result)
                {
                    return Json(new { message = "Request successfully sent", success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { message = "Error sending request record. Try again.", success = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SubmitContractorApprovalRequest(string RequestNo, string Contractor_Comment)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                string Responsible_Employee_No = employeeView.No;
                string UserID = employeeView.UserID;

                string staffNo = Session["Username"].ToString();
                EmployeeView employee = Session["EmployeeData"] as EmployeeView;
                //string userId = employee.UserID;
                bool result = false;
                result = Credentials.ObjNav.FnSubmitContractorApprovalRequest(RequestNo, Contractor_Comment);
                if (result)
                {
                    return Json(new { message = "Request successfully sent", success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { message = "Error sending request record. Try again.", success = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
