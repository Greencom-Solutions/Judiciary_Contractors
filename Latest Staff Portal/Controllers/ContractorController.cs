using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Latest_Staff_Portal.Models;
using Latest_Staff_Portal.ViewModel;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

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
            page = $"ExtensionRequestCard?$filter=Document_Type eq 'Extension' and Status eq '{status}'&$format=json";

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
                        Date_of_Submittion = config["Date_of_Submittion"]?.ToObject<DateTime>() ?? default,
                        Date_of_Receipt = config["Date_of_Receipt"]?.ToObject<DateTime>() ?? default,
                        Received_By = (string)config["Received_By"],
                        Status = (string)config["Status"],
                        Memo_Comments = (string)config["Memo_Comments"],
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
                        Project_Manger_Communication = (string)config["Project_Manger_Communication"]
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
                return RedirectToAction("Login", "Home");
            }
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;

            var isManager = IsManager(UserID);
            var isDirector = IsDirector(StaffNo);
            var isCRJ = IsCRJ(StaffNo);
            var SecretaryStaffNo = GetSecretaryStaffNo(No);

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
                        Director_SCM_Advice = (string)config["Director_SCM_Advice"],
                        Professional_Opinion_Notes = (string)config["Professional_Opinion_Notes"],
                        SCM_Status = (string)config["SCM_Status"],
                        Procurement_Comments = (string)config["Procurement_Comments"],
                        Crj_Comments = (string)config["Crj_Comments"],
                        CRJ_General_Comments = (string)config["CRJ_General_Comments"],
                        Project_Manger_Communication = (string)config["Project_Manger_Communication"],
                        isManager = isManager,
                        Sent_to_Ast_Directors = (bool)config["Sent_to_Ast_Directors"],
                        Addendum_Done = (bool)config["Addendum_Done"],


                    };
                    ContractorExtensionRequests.Add(contractorExtensionRequest);
                }
            }

            ViewBag.isDirector = isDirector;
            ViewBag.isCRJ = isCRJ;
            ViewBag.loggedInStaffNo = StaffNo;
            ViewBag.SecretaryStaffNo = SecretaryStaffNo;
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

                    };
                    ExtensionReqLinesList.Add(extensionReqLines);

                }
            }
            return PartialView("~/Views/Contractor/PartialView/ContractorRequestLinesPartialView.cshtml", ExtensionReqLinesList);
        }
        public PartialViewResult ValuationProjectBoqsLinesPartialView(string No)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<ValuationProjectBoqs> ValuationProjectBoqsList = new List<ValuationProjectBoqs>();

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
                    ValuationProjectBoqs ValuationProjectBoq = new ValuationProjectBoqs
                    {
                        Project_Code = (string)config["Project_Code"],
                        Entry_No = (int)config["Entry_No"],
                        Line_Type = (string)config["Line_Type"],
                        Section = (string)config["Section"],
                        Quantity = (int)config["Quantity"],
                        Description = (string)config["Description"],
                        UOM = (string)config["UOM"],
                        Unit_Price = (int)config["Unit_Price"],
                        Line_Amount = (int)config["Line_Amount"],
                        Executed_Quantity = (int)config["Executed_Quantity"],
                        Actualised_Amount = (int)config["Actualised_Amount"],

                    };
                    ValuationProjectBoqsList.Add(ValuationProjectBoq);

                }
            }
            return PartialView("~/Views/Contractor/PartialView/ValuationProjectBoqsLinesPartialView.cshtml", ValuationProjectBoqsList);
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

            ViewBag.DocNo = No;
            return PartialView("~/Views/Contractor/PartialView/TenderEvaluationCommitteePartialView.cshtml", membersList);
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


        //contractor ammendment
        public ActionResult ContractorAmmendedRequests()
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
        public PartialViewResult ContractorAmmendedRequestsListPartialView(string status)
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
            page = $"ExtensionRequestCard?$filter=Document_Type eq 'Amendmend' and Status eq '{status}'&$format=json";

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
                        Date_of_Submittion = config["Date_of_Submittion"]?.ToObject<DateTime>() ?? default,
                        Date_of_Receipt = config["Date_of_Receipt"]?.ToObject<DateTime>() ?? default,
                        Received_By = (string)config["Received_By"],
                        Status = (string)config["Status"],
                        Memo_Comments = (string)config["Memo_Comments"],
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
                        Project_Manger_Communication = (string)config["Project_Manger_Communication"]
                    };
                    ExtensionRequestsList.Add(ExtensionRequest);

                }
            }
            return PartialView("~/Views/Contractor/PartialView/ContractorAmmendedRequestsListPartialView.cshtml", ExtensionRequestsList);
        }
        [HttpPost]
        public ActionResult ContractorAmmendedRequestsDocumentView(string No)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;

            var isManager = IsManager(UserID);
            var isDirector = IsDirector(StaffNo);
            var isCRJ = IsCRJ(StaffNo);
            var SecretaryStaffNo = GetSecretaryStaffNo(No);

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
                        Director_SCM_Advice = (string)config["Director_SCM_Advice"],
                        Professional_Opinion_Notes = (string)config["Professional_Opinion_Notes"],
                        SCM_Status = (string)config["SCM_Status"],
                        Procurement_Comments = (string)config["Procurement_Comments"],
                        Crj_Comments = (string)config["Crj_Comments"],
                        CRJ_General_Comments = (string)config["CRJ_General_Comments"],
                        Project_Manger_Communication = (string)config["Project_Manger_Communication"],
                        isManager = isManager,
                        Sent_to_Ast_Directors = (bool)config["Sent_to_Ast_Directors"],
                        Addendum_Done = (bool)config["Addendum_Done"],


                    };
                    ContractorExtensionRequests.Add(contractorExtensionRequest);
                }
            }

            ViewBag.isDirector = isDirector;
            ViewBag.isCRJ = isCRJ;
            ViewBag.loggedInStaffNo = StaffNo;
            ViewBag.SecretaryStaffNo = SecretaryStaffNo;
            return PartialView(ContractorExtensionRequests.FirstOrDefault());
        }


        //contractor Instructions
        public ActionResult ContractorInstructionsRequests()
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
        public PartialViewResult ContractorInstructionsRequestsListPartialView(string status)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<ProjectManagerInstCard> instructionList = new List<ProjectManagerInstCard>();

            string title = status;
            ViewBag.title = title;

            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            string page = ""; // Declare 'page' once at the top.
            /*page = $"Contract?$filter=Created_By eq '{employeeView.UserID}'&$format=json";*/
            page = $"InstructionRequestCard?$filter=Document_Type eq 'Instructions' and Status eq '{status}'&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    ProjectManagerInstCard instruction = new ProjectManagerInstCard
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
                        Status = (string)config["Status"]
                    };
                    instructionList.Add(instruction);
                }
            }
            return PartialView("~/Views/Contractor/PartialView/ContractorInstructionsRequestsListPartialView.cshtml", instructionList);
        }
        [HttpPost]
        public ActionResult ContractorInstructionsRequestsDocumentView(string No)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;

            var isManager = IsManager(UserID);
            var isDirector = IsDirector(StaffNo);
            var isCRJ = IsCRJ(StaffNo);
            var SecretaryStaffNo = GetSecretaryStaffNo(No);

            List<ProjectManagerInstCard> instructionsList = new List<ProjectManagerInstCard>();

            string page = $"InstructionRequestCard?$filter=No eq '{No}'&$format=json";
            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                var config = details["value"].FirstOrDefault();
                if (config != null)
                {
                    ProjectManagerInstCard instructions = new ProjectManagerInstCard
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
                        Status = (string)config["Status"]


                    };
                    instructionsList.Add(instructions);
                }
            }

            ViewBag.isDirector = isDirector;
            ViewBag.isCRJ = isCRJ;
            ViewBag.loggedInStaffNo = StaffNo;
            ViewBag.SecretaryStaffNo = SecretaryStaffNo;
            return PartialView(instructionsList.FirstOrDefault());
        }



        //contractor Approval request
        public ActionResult ContractorApprovalRequests()
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
        public PartialViewResult ContractorApprovalRequestsListPartialView(string status)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<ProjectManagerInstCard> ApprovalRequestsList = new List<ProjectManagerInstCard>();

            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;

            string title = status;
            ViewBag.title = title;

            string page = $"ApprovalRequestCard?$filter=Document_Type eq 'Approval' and Status eq '{status}'&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    ProjectManagerInstCard approvalRequest = new ProjectManagerInstCard
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
                    ApprovalRequestsList.Add(approvalRequest);

                }
            }
            return PartialView("~/Views/Contractor/PartialView/ContractorApprovalRequestsListPartialView.cshtml", ApprovalRequestsList);
        }
        [HttpPost]
        public ActionResult ContractorApprovalRequestsDocumentView(string No)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            List<ProjectManagerInstCard> ApprovalRequestList = new List<ProjectManagerInstCard>();

            string page = $"ApprovalRequestCard?$filter=No eq '{No}'&$format=json";
            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                var config = details["value"].FirstOrDefault();
                if (config != null)
                {
                    ProjectManagerInstCard approvalRequest = new ProjectManagerInstCard
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
                    ApprovalRequestList.Add(approvalRequest);
                }
            }
            return PartialView(ApprovalRequestList.FirstOrDefault());
        }



        //contractor payment
        public ActionResult ContractorPaymentRequests()
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
        public PartialViewResult ContractorPaymentRequestsListPartialView(string status)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<ContractorPaymentRequests> PaymentRequestsList = new List<ContractorPaymentRequests>();

            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;

            string title = status;

            ViewBag.title = title;


            string page = $"PaymentRequestCard?$filter=Document_Type eq 'Payments' and Status eq '{status}'&$format=json";

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
                return RedirectToAction("Login", "Home");
            }
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            List<ContractorExtensionRequests> ContractorPaymentRequests = new List<ContractorExtensionRequests>();

            string page = $"PaymentRequestCard?$filter=No eq '{No}'&$format=json";
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
            return PartialView(ContractorPaymentRequests.FirstOrDefault());
        }


        //HOS request
        public ActionResult ContractorHOSRequests()
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
        public PartialViewResult ContractorHOSRequestsListPartialView(string status)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<HOSRequestscard> HOSRequestsList = new List<HOSRequestscard>();

            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;

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
                    HOSRequestscard HOSRequest = new HOSRequestscard
                    {
                        No = (string)config["No"],
                        Project_No = (string)config["Project_No"],
                        Project_Name = (string)config["Project_Name"],
                        Contractor_No = (string)config["Contractor_No"],
                        Contractor_Name = (string)config["Contractor_Name"],
                        Request_Description = (string)config["Request_Description"],
                        Key_Comments = (string)config["Key_Comments"]
                    };
                    HOSRequestsList.Add(HOSRequest);

                }
            }
            return PartialView("~/Views/Contractor/PartialView/ContractorHOSRequestsListPartialView.cshtml", HOSRequestsList);
        }
        [HttpPost]
        public ActionResult ContractorHOSRequestsDocumentView(string No)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            List<HOSRequestscard> HOSRequestsList = new List<HOSRequestscard>();

            string page = $"HOSRequestscard?$filter=No eq '{No}'&$format=json";
            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                var config = details["value"].FirstOrDefault();
                if (config != null)
                {
                    HOSRequestscard HOSRequest = new HOSRequestscard
                    {
                        No = (string)config["No"],
                        Project_No = (string)config["Project_No"],
                        Project_Name = (string)config["Project_Name"],
                        Contractor_No = (string)config["Contractor_No"],
                        Contractor_Name = (string)config["Contractor_Name"],
                        Request_Description = (string)config["Request_Description"],
                        Key_Comments = (string)config["Key_Comments"]

                    };
                    HOSRequestsList.Add(HOSRequest);
                }
            }
            return PartialView(HOSRequestsList.FirstOrDefault());
        }



        //Project Manager Communication (Project Manager Instructions)
        public ActionResult ProjectManagerCommunication()
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
        public PartialViewResult ProjectManagerCommunicationListPartialView(string status)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<ProjectManagerInstCard> projectManagerInstList = new List<ProjectManagerInstCard>();

            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;

            string title = status;

            ViewBag.title = title;


            string page = $"ProjectManagerInstCard?$filter=Document_Type eq 'Project Manager Instructions' and Status eq '{status}'&$format=json";


            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    ProjectManagerInstCard projectManagerInst = new ProjectManagerInstCard
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
                    projectManagerInstList.Add(projectManagerInst);
                }
            }
            return PartialView("~/Views/Contractor/PartialView/ProjectManagerCommunicationListPartialView.cshtml", projectManagerInstList);
        }
        [HttpPost]
        public ActionResult ProjectManagerCommunicationDocumentView(string No)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            List<ProjectManagerInstCard> ContractorPaymentRequests = new List<ProjectManagerInstCard>();

            string page = $"PaymentRequestCard?$filter=No eq '{No}'&$format=json";
            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                var config = details["value"].FirstOrDefault();
                if (config != null)
                {
                    ProjectManagerInstCard contractorPaymentRequest = new ProjectManagerInstCard
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
                    ContractorPaymentRequests.Add(contractorPaymentRequest);
                }
            }
            return PartialView(ContractorPaymentRequests.FirstOrDefault());
        }



        //Project Meetings
        public ActionResult ProjectMeetings()
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
        public PartialViewResult ProjectMeetingsListPartialView(string status)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<MeetingCard> projectMeetingsList = new List<MeetingCard>();

            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;

            string title = status;

            ViewBag.title = title;


            string page = $"MeetingCard?$filter=Status eq '{status}'&$format=json";


            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    MeetingCard projectMeeting = new MeetingCard
                    {
                        No = (string)config["No"],
                        Project_No = (string)config["Project_No"],
                        Project_Name = (string)config["Project_Name"],
                        Contractor_No = (string)config["Contractor_No"],
                        Contractor_Name = (string)config["Contractor_Name"],
                        Document_Type = (string)config["Document_Type"],
                        Date_of_meeting = (string)config["Document_Type"],
                        Next_meeting_Date = (string)config["Document_Type"],
                        Received_By = (string)config["Document_Type"],
                        Status = (string)config["Status"],

                    };
                    projectMeetingsList.Add(projectMeeting);
                }
            }
            return PartialView("~/Views/Contractor/PartialView/ProjectMeetingsListPartialView.cshtml", projectMeetingsList);
        }
        [HttpPost]
        public ActionResult ProjectMeetingsDocumentView(string No)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            List<MeetingCard> MeetingList = new List<MeetingCard>();

            string page = $"ProjectMeetings?$filter=No eq '{No}'&$format=json";
            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                var config = details["value"].FirstOrDefault();
                if (config != null)
                {
                    MeetingCard meeting = new MeetingCard
                    {
                        No = (string)config["No"],
                        Project_No = (string)config["Project_No"],
                        Project_Name = (string)config["Project_Name"],
                        Contractor_No = (string)config["Contractor_No"],
                        Contractor_Name = (string)config["Contractor_Name"],
                        Document_Type = (string)config["Document_Type"],
                        Date_of_meeting = (string)config["Document_Type"],
                        Next_meeting_Date = (string)config["Document_Type"],
                        Received_By = (string)config["Document_Type"],
                        Status = (string)config["Status"],
                    };
                    MeetingList.Add(meeting);
                }
            }
            return PartialView(MeetingList.FirstOrDefault());
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



        //PracticalcCompletion Certification
        public ActionResult PracticalCompletionCertification()
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
        public PartialViewResult PracticalCompletionCertificationListPartialView(string status)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<ContractorPaymentRequests> PracticalCompletionList = new List<ContractorPaymentRequests>();

            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            string title = status;
            ViewBag.title = title;

            string page = $"PracticalCompletionForm?$filter=Document_Type eq 'Practical Completion' and Status eq '{status}'&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    ContractorPaymentRequests PracticalCompletion = new ContractorPaymentRequests
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
                    PracticalCompletionList.Add(PracticalCompletion);

                }
            }
            return PartialView("~/Views/Contractor/PartialView/PracticalCompletionCertificationListPartialView.cshtml", PracticalCompletionList);
        }
        [HttpPost]
        public ActionResult PracticalCompletionCertificationDocumentView(string No)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            List<ContractorExtensionRequests> ContractorPaymentRequests = new List<ContractorExtensionRequests>();

            string page = $"PaymentRequestCard?$filter=No eq '{No}'&$format=json";
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
            return PartialView(ContractorPaymentRequests.FirstOrDefault());
        }



        //Practical Completion Certification
        public ActionResult MakingGoodDefectRequests()
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
        public PartialViewResult MakingGoodDefectRequestsListPartialView(string status)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<ContractorPaymentRequests> PracticalCompletionList = new List<ContractorPaymentRequests>();

            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            string title = status;
            ViewBag.title = title;

            string page = $"PracticalCompletionForm?$filter=Document_Type eq 'Making Good' and Status eq '{status}'&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    ContractorPaymentRequests PracticalCompletion = new ContractorPaymentRequests
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
                    PracticalCompletionList.Add(PracticalCompletion);

                }
            }
            return PartialView("~/Views/Contractor/PartialView/MakingGoodDefectRequestsListPartialView.cshtml", PracticalCompletionList);
        }
        [HttpPost]
        public ActionResult MakingGoodDefectRequestsDocumentView(string No)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            List<ContractorExtensionRequests> ContractorPaymentRequests = new List<ContractorExtensionRequests>();

            string page = $"PaymentRequestCard?$filter=No eq '{No}'&$format=json";
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
            return PartialView(ContractorPaymentRequests.FirstOrDefault());
        }


        //Project Closure
        public ActionResult ProjectClosure()
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
        public PartialViewResult ProjectClosureListPartialView(string status)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<ProjectClosure> ProjectClosureList = new List<ProjectClosure>();

            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            string title = status;
            ViewBag.title = title;

            string page = $"ProjectClosure?$format=json";

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
                return RedirectToAction("Login", "Home");
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
                    string pageProjects = "ProjectList?$format=json";

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
                    Contractor_No,
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

        private string GetSecretaryStaffNo(string No)
        {
            #region Fetch Secretary Staff Number
            string SecretaryStaffNo = "";
            string pageUser = $"InspectionCommitteeMembers?$filter=Document_No eq '{No}'&$format=json";

            try
            {
                using (HttpWebResponse httpResponseUser = Credentials.GetOdataData(pageUser))
                using (var streamReader = new StreamReader(httpResponseUser.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    if (details["value"] is JArray members && members.Count > 0)
                    {
                        foreach (JObject member in members)
                        {
                            if (member["Title"]?.ToString() == "Secretary")
                            {
                                SecretaryStaffNo = member["Member_No"]?.ToString() ?? "";
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching Secretary Staff Number: {ex.Message}");
            }
            #endregion

            return SecretaryStaffNo;
        }
        private bool IsManager(string userID)
        {
            #region Manager Check
            bool isManager = false;
            string pageUser = $"UserSetup?$filter=User_ID eq '{userID}'&$format=json";

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
                            if (config["Project_Manager"] != null && (bool)config["Project_Manager"])
                            {
                                isManager = true;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking if user is a manager: {ex.Message}");
            }
            #endregion

            return isManager;
        }
        private bool IsDirector(string StaffNo)
        {
            #region Director Check
            bool isDirector = false;
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
                            if (config["Director_Registrar"] != null && (bool)config["Director_Registrar"])
                            {
                                isDirector = true;
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

            return isDirector;
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




    }
}
