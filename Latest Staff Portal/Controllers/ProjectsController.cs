using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Latest_Staff_Portal.CustomSecurity;
using Latest_Staff_Portal.Models;
using Latest_Staff_Portal.ViewModel;
using Newtonsoft.Json.Linq;

namespace Latest_Staff_Portal.Controllers
{
/*    [CustomAuthorization(Role = "ALLUSERS,ACCOUNTANTS,PROCUREMENT")]*/
    public class ProjectsController : Controller
    {

        public ActionResult ProjectProposal()
        {
            if (Session["Username"] == null)
                return RedirectToAction("Login", "Home");
            return View();
        }

        public PartialViewResult ProjectProposalList()
        {
            var StaffNo = Session["Username"].ToString();
            var employee = Session["EmployeeData"] as EmployeeView;
            var userId = employee.UserID;


            var projectProposals = new List<ProjectProposal>();

            var pageProject = "ProjectProposal?$filter=Created_By eq '" + userId + "' &$format=json";

            var httpResponse = Credentials.GetOdataData(pageProject);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var project = new ProjectProposal();
                    project.No = (string)config["No"];
                    project.Name = (string)config["Name"];
                    project.GlobalDimension2Code = (string)config["Global_Dimension_2_Code"];
                    project.AdministrationUnitName = (string)config["Administration_Unit_Name"];
                    project.RequestDescription = (string)config["Request_Description"];
                    project.Justification = (string)config["Justification"];
                    project.CreatedBy = (string)config["Created_By"];
                    project.EmployeeNo = (string)config["Employee_No"];
                    project.EmployeeName = (string)config["Employee_Name"];
                    project.Status = (string)config["Status"];
                    project.DeferalComments = (string)config["Deferal_Comments"];
                    projectProposals.Add(project);
                }
            }


            return PartialView("~/Views/Projects/PartialViews/ProjectProposalList.cshtml", projectProposals);
        }

        public ActionResult NewProjectProposal()
        {
            try
            {
                if (Session["Username"] == null) return RedirectToAction("Login", "Home");

                var employeeView = Session["EmployeeData"] as EmployeeView;
                var newProjectProposal = new ProjectProposal();
                Session["httpResponse"] = null;

                #region dim2

                var dim2 = new List<DropdownList>();
                var pageDim2 = "DimensionValues?$filter=Global_Dimension_No eq 2 and Blocked eq false&$format=json";

                var httpResponseDivision = Credentials.GetOdataData(pageDim2);
                using (var streamReader = new StreamReader(httpResponseDivision.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);


                    foreach (JObject config in details["value"])
                    {
                        var dropdownList = new DropdownList();
                        dropdownList.Text = (string)config["Code"] + "-" + (string)config["Name"];
                        dropdownList.Value = (string)config["Code"];
                        dim2.Add(dropdownList);
                    }
                }

                #endregion


                newProjectProposal.CreatedBy = employeeView.UserID;
                newProjectProposal.GlobalDimension2Code = employeeView.DepartmentCode;
                newProjectProposal.ListOfDim2 = dim2.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();


                return View("~/Views/Projects/PartialViews/NewProjectProposal.cshtml", newProjectProposal);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }

        public ActionResult ProjectProposalDocument(string documentNo)
        {
            if (Session["Username"] == null) return RedirectToAction("Login", "Home");

            var dependantChange = new DependantChangeRequests();
            var page = "DependentsChangeRequest?$filter=No eq '" + documentNo + "'&$format=json";
            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    dependantChange.No = (string)config["No"];
                    dependantChange.Description = (string)config["Description"];
                    dependantChange.RequestorID = (string)config["RequestorID"];
                    dependantChange.EmployeeNo = (string)config["EmployeeNo"];
                    dependantChange.EmployeeName = (string)config["EmployeeName"];
                    dependantChange.DocumentDate = (string)config["DocumentDate"];
                    dependantChange.ReasonForChange = (string)config["Reason_For_Change"];
                    dependantChange.TimeCreated = (string)config["TimeCreated"];
                    dependantChange.PostedBy = (string)config["PostedBy"];
                    dependantChange.PostingDate = (string)config["PostingDate"];
                    dependantChange.PostingTime = (string)config["PostingTime"];
                    dependantChange.Status = (string)config["Status"];
                    dependantChange.DocumentStatus = (string)config["DocumentStatus"];
                    dependantChange.Posted = (string)config["Posted"];
                }
            }

            return View(dependantChange);
        }
        //[HttpPost]
        //public JsonResult SubmitProjectProposal(ProjectProposal NewApp, string fileName, string FileType, string FileContent)
        //{
        //    try
        //    {
        //        string userId = Session["Username"].ToString();
        //        string loanProductType = NewApp.LoanProductType;
        //        int  Installment = 12;
        //        string Redirect = "";

        //        if (!string.IsNullOrEmpty(NewApp.Instalment))
        //        {
        //            Installment = Convert.ToInt32(NewApp.Instalment);
        //        }


        //        // DateTime appDate = DateTime.ParseExact(ApplicationDate.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture);

        //        string DocNo = Credentials.ObjNav.createLoansApplication(DateTime.Now, userId, NewApp.LoanProductType, NewApp.AmountRequested, NewApp.Reason,Convert.ToInt32(Installment));
        //        if (FileContent != "" && fileName != "")
        //        {
        //            string filePath = Server.MapPath("~/Uploads/" + fileName);
        //            int tableid = 69011;
        //            bool successVal=false;


        //            bool uploaded = UploadDocuments.UploadEDMSDocumentAttachment(FileContent, fileName, "HRMD", DocNo, "Project Management", "", tableid);
        //            if (uploaded)
        //            {
        //                successVal = true;
        //                 Redirect = "/WelfareManagement/LoanApplicationCardDocument?loanNo=" + DocNo;

        //            }
        //            else
        //            {
        //                successVal = false;
        //            }

        //        string Redirect = "/Projects/ProjectsDocumentView?documentNo=" + DocNo;

        //        return Json(new { message = Redirect, success = true }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
        //    }
        //}
        [HttpPost]
        public JsonResult SendProjectToNextStage(string documentNo)
        {
            try
            {
                var userId = Session["Username"].ToString();


                var Redirect = "/Projects/ProjectProposal";

                return Json(new { message = Redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ProjectsDocumentView(string DocNo)
        {
            try
            {
                if (Session["Username"] == null) return RedirectToAction("Login", "Home");

                var StaffNo = Session["Username"].ToString();
                var projects = new ProjectProposal();

                var page = "ProjectProposal?$filter=No eq '" + DocNo + "'&format=json";
                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        projects.No = (string)config["No"];
                        projects.GlobalDimension2Code = (string)config["Global_Dimension_2_Code"];
                        projects.AdministrationUnitName = (string)config["Administration_Unit_Name"];
                        projects.EmployeeName = (string)config["Employee_Name"];
                        projects.EmployeeNo = (string)config["Employee_No"];
                        projects.RequestDescription = (string)config["Request_Description"];
                        projects.DeferalComments = (string)config["Deferal_Comments"];
                        projects.Status = (string)config["Status"];
                    }
                }

                return View(projects);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }

        public ActionResult DesignControl()
        {
            if (Session["Username"] == null)
                return RedirectToAction("Login", "Home");
            return View();
        }

        public PartialViewResult DesignControlList()
        {
            var StaffNo = Session["Username"].ToString();
            var projectProposals = new List<DesignControlHeader>();

            var pageProject = "DesignControlCard?$filter=EmployeeNo eq '" + StaffNo + "' &$format=json";

            var httpResponse = Credentials.GetOdataData(pageProject);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var project = new DesignControlHeader();
                    project.ProjectNo = (string)config["No"];
                    project.Name = (string)config["Name"];
                    project.CreatedBy = (string)config["Created_By"];
                    project.StartDate = (string)config["Created_By"];
                    project.EndDate = (string)config["Created_By"];
                    project.AdministrativeUnit = (string)config["Created_By"];
                    project.GeographicLocation = (string)config["Created_By"];
                    project.TotalEstimatedCost = (string)config["Created_By"];
                    project.DesignApprovalStage = (string)config["Created_By"];
                    project.EmployeeNo = (string)config["Employee_No"];
                    project.EmployeeName = (string)config["Employee_Name"];
                    project.Status = (string)config["Status"];
                    projectProposals.Add(project);
                }
            }

            return PartialView("~/Views/Projects/PartialViews/DesignControlList.cshtml", projectProposals);
        }

        public ActionResult DesignControlDocumentView(string DocNo)
        {
            try
            {
                if (Session["Username"] == null) return RedirectToAction("Login", "Home");

                var StaffNo = Session["Username"].ToString();
                var project = new DesignControlHeader();
                var pageProject = "DesignControlCard?$filter=Document_No eq '" + DocNo + "' &$format=json";

                var httpResponse = Credentials.GetOdataData(pageProject);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);


                    foreach (JObject config in details["value"])
                    {
                        project.ProjectNo = (string)config["No"];
                        project.Name = (string)config["Name"];
                        project.CreatedBy = (string)config["Created_By"];
                        project.StartDate = (string)config["Created_By"];
                        project.EndDate = (string)config["Created_By"];
                        project.AdministrativeUnit = (string)config["Created_By"];
                        project.GeographicLocation = (string)config["Created_By"];
                        project.TotalEstimatedCost = (string)config["Created_By"];
                        project.DesignApprovalStage = (string)config["Created_By"];
                        project.EmployeeNo = (string)config["Employee_No"];
                        project.EmployeeName = (string)config["Employee_Name"];
                        project.Status = (string)config["Status"];
                    }
                }

                return View(project);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }

        public PartialViewResult DesignControlDocumentLines(string DocNo, string ActioningMember, string ApprovalStage)
        {
            try
            {
                #region Design Control Lines

                var designControlLines = new List<DesignControlTasks>();
                var pageLine = "DesignControlTask?$filter=Header_No eq '" + DocNo + "'&$format=json";
                var httpResponseLine = Credentials.GetOdataData(pageLine);
                using (var streamReader = new StreamReader(httpResponseLine.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        var designControl = new DesignControlTasks();
                        designControl.HeaderNo = (string)config["Header_No"];
                        designControl.EntryNo = (string)config["Entry_No"];
                        designControl.AttachmentCode = (string)config["Attachment_Code"];
                        designControl.DesignControlNo = (string)config["Design_Control_No"];
                        designControl.DesignControlType = (string)config["Design_Control_Type"];
                        designControl.Name = (string)config["Name"];
                        designControl.NoOfItems = (string)config["No_of_Items"];
                        designControlLines.Add(designControl);
                    }
                }

                #endregion

                return PartialView("~/Views/Purchase/Partial Views/DesignControlDocumentLines.cshtml",
                    designControlLines);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }

        public JsonResult SubmitDesignControl(string documentNo, string fileBase64String, string fileName,
            string documentType, string description)
        {
            var successVal = false;
            var msg = "";
            try
            {
                var userId = Session["Username"].ToString();
                var uploaded = UploadDocuments.UploadEDMSDocumentAttachment(fileBase64String, fileName, "PROJECTS",
                    documentNo, "Projects", "", 0, "");
                if (uploaded)
                {
                    msg = "Documents for Design Control, Document Number " + documentNo +
                          " has been submitted successfully and sent to the next stage";
                    successVal = true;
                }
                else
                {
                    msg = "There was an error uploading the Design Control documents, Document Number " + documentNo +
                          ". Please try again.";
                    successVal = false;
                }
                //Credentials.projectWs.FnSubmitUserRequest(documentNo);


                var Redirect = "/Projects/DesignControl";

                return Json(new { message = Redirect, success = successVal }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        //active projects
        public ActionResult ActiveProjectsList()
        {
            if (Session["Username"] == null)
                return RedirectToAction("Login", "Home");
            return View();
        }

        public PartialViewResult ActiveProjectsListPartialView()
        {
            var StaffNo = Session["Username"].ToString();
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            var userId = employeeView.UserID;

            string staffName = employeeView.Name;
  
            string UserID = employeeView.UserID;
            var activeProjects = new List<ActiveProjects>();

          /*  var pageProject = "JobCard?$filter=Created_By eq '" + userId + "' &$format=json";*/
            var pageProject = $"JobCard?$filter=Contractor_No eq '{employeeView.No}'&$format=json";

            var httpResponse = Credentials.GetOdataData(pageProject);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var project = new ActiveProjects();
                    project.No = (string)config["No"];
                    project.Description = (string)config["Description"];
                    project.Project_Code = (string)config["Project_Code"];
                    project.County_ID = (string)config["County_ID"];
                    project.SubCounty = (string)config["SubCounty"];
                    project.Project_Sum = (int)config["Project_Sum"];
                    project.first_Moeity_Amount = (int)config["_x0031_st_Moeity_Amount"];
                    project.second_Moeity_Amount = (int)config["_x0032_nd_Moeity_Amount"];


                    project.Contract_Agreement_Date = (string)config["Contract_Agreement_Date"];
                    project.Intended_Completion_Date = (string)config["Intended_Completion_Date"];
                    project.Date_of_Taking_Over = (string)config["Date_of_Taking_Over"];
                    project.Global_Dimension_2_Code = (string)config["Global_Dimension_2_Code"];
                    project.External_Document_No = (string)config["External_Document_No"];
                    project.Project_Manager_Name = (string)config["Project_Manager_Name"];
                    project.Tender_No = (string)config["Tender_No"];
                    project.Contractor_No = (string)config["Contractor_No"];
                    project.Contractor_Name = (string)config["Contractor_Name"];
                    project.Contract_Amount = (float)config["Contract_Amount"];
                    project.Project_Manager_Name = (string)config["Project_Manager_Name"];


                    project.Projected_Monthly_Cashflow = (int)config["Projected_Monthly_Cashflow"];
                    project.Actual_Monthly_Cashflow = (int)config["Actual_Monthly_Cashflow"];
                    project.Cashflow_Variance = (int)config["Cashflow_Variance"];
                    project.Audited_Acc_Current_Ratios = (int)config["Audited_Acc_Current_Ratios"];
                    project.Valuation_No = (int)config["Valuation_No"];
                    project.Valuation_Amount = (int)config["Valuation_Amount"];
                    project.Physical_Progress = (int)config["Physical_Progress"];

                    activeProjects.Add(project);
                }
            }


            return PartialView("~/Views/Projects/PartialViews/ActiveProjectsListPartialView.cshtml", activeProjects);
        }

        [HttpPost]
        public ActionResult ActiveProjectsDocumentView(string No)
        {
            try
            {
                if (Session["Username"] == null) return RedirectToAction("Login", "Home");

                var StaffNo = Session["Username"].ToString();
                var project = new ActiveProjects();
                var pageProject = "JobCard?$filter=No eq '" + No + "' &$format=json";

                var httpResponse = Credentials.GetOdataData(pageProject);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);


                    foreach (JObject config in details["value"])
                    {
                        project.No = (string)config["No"];
                        project.Description = (string)config["Description"];
                        project.Project_Code = (string)config["Project_Code"];
                        project.County_ID = (string)config["County_ID"];
                        project.SubCounty = (string)config["SubCounty"];
                        project.Project_Sum = (int)config["Project_Sum"];
                        project.first_Moeity_Amount = (int)config["_x0031_st_Moeity_Amount"];
                        project.second_Moeity_Amount = (int)config["_x0032_nd_Moeity_Amount"];


                        project.Contract_Agreement_Date = (string)config["Contract_Agreement_Date"];
                        project.Intended_Completion_Date = (string)config["Intended_Completion_Date"];
                        project.Date_of_Taking_Over = (string)config["Date_of_Taking_Over"];
                        project.Global_Dimension_2_Code = (string)config["Global_Dimension_2_Code"];
                        project.External_Document_No = (string)config["External_Document_No"];
                        project.Project_Manager_Name = (string)config["Project_Manager_Name"];
                        project.Tender_No = (string)config["Tender_No"];
                        project.Contractor_No = (string)config["Contractor_No"];
                        project.Contractor_Name = (string)config["Contractor_Name"];
                        project.Contract_Amount = (float)config["Contract_Amount"];
                        project.Project_Manager_Name = (string)config["Project_Manager_Name"];


                        project.Projected_Monthly_Cashflow = (int)config["Projected_Monthly_Cashflow"];
                        project.Actual_Monthly_Cashflow = (int)config["Actual_Monthly_Cashflow"];
                        project.Cashflow_Variance = (int)config["Cashflow_Variance"];
                        project.Audited_Acc_Current_Ratios = (int)config["Audited_Acc_Current_Ratios"];
                        project.Valuation_No = (int)config["Valuation_No"];
                        project.Valuation_Amount = (int)config["Valuation_Amount"];
                        project.Physical_Progress = (int)config["Physical_Progress"];
                    }
                }

                return View(project);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }

        public PartialViewResult ProjectBQs(string DocNo)
        {
            try
            {
                #region Project BQs Lines

                var BQLines = new List<ProjectBQLines>();
                var pageLine = "ProjectBoqs?$filter=Project_Code eq '" + DocNo + "'&$format=json";
                var httpResponseLine = Credentials.GetOdataData(pageLine);
                using (var streamReader = new StreamReader(httpResponseLine.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        var bq = new ProjectBQLines();
                        bq.Project_Code = (string)config["Project_Code"];
                        bq.Entry_No = (int)config["Entry_No"];
                        bq.Line_Type = (string)config["Line_Type"];
                        bq.Section = (string)config["Section"];
                        bq.Description = (string)config["Description"];
                        bq.Quantity = (int)config["Quantity"];
                        bq.UOM = (string)config["UOM"];        
                        bq.Unit_Price = (decimal)config["Unit_Price"];
                        bq.Line_Amount = (decimal)config["Line_Amount"];
                        bq.Total_valued_Qty = (int)config["Total_valued_Qty"];
                        bq.Total_valued_Amount = (decimal)config["Total_valued_Amount"];
                        
                        BQLines.Add(bq);
                    }
                }

                #endregion

                return PartialView("~/Views/Projects/PartialViews/ProjectBQs.cshtml",
                    BQLines);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }


    }
}