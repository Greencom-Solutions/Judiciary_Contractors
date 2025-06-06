using Latest_Staff_Portal.Models;
using Latest_Staff_Portal.ViewModel;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Latest_Staff_Portal.Controllers
{
    public class ProjectManagementController : Controller
    {
        // user requests
        public ActionResult UserRequests()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();
            }
        }
        public PartialViewResult UserRequestsList(string status)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            List<ProjectProposals> UserRequests = new List<ProjectProposals>();

            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            string UserID = employeeView.UserID;
            string page = ""; // Declare 'page' once at the top.
            string title = "";
            var isManager = IsManager(UserID);
            if (status == "Open")
            {
                page = $"ProjectProposal?$filter=Created_By eq '{employeeView.UserID}' and Request_Status eq 'Open'&$format=json";
                title = "Open User Requests";

            }
            else if (status == "Submitted")
            {
                title = "Submitted User Requests";
                if (isManager)
                {
                    page = $"ProjectProposal?$filter= Request_Status eq 'Submitted'&$format=json";
                }
                else
                {
                    page = $"ProjectProposal?$filter=Created_By eq '{employeeView.UserID}' and Request_Status eq 'Submitted'&$format=json";
                }

            }
            else
            {
                title = "Received User Requests";
                if (isManager)
                {
                    page = $"ProjectProposal?$filter= Request_Status eq 'Received'&$format=json";
                }
                else
                {
                    page = $"ProjectProposal?$filter=Created_By eq '{employeeView.UserID}' and Request_Status eq 'received'&$format=json";
                }
            }

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    ProjectProposals proposal = new ProjectProposals
                    {
                        No = (string)config["No"],
                        Name = (string)config["Name"],
                        Global_Dimension_2_Code = (string)config["Global_Dimension_2_Code"],
                        Administration_Unit_Name = (string)config["Administration_Unit_Name"],
                        County = (string)config["County"],
                        County_Name = (string)config["County_Name"],
                        Sub_County = (string)config["Sub_County"],
                        Sub_County_Name = (string)config["Sub_County_Name"],
                        Request_Description = (string)config["Request_Description"],
                        Justification = (string)config["Justification"],
                        Request_Source = (string)config["Request_Source"],
                        Created_By = staffName,
                        Employee_No = employeeView.No,
                        Employee_Name = staffName,
                        Status = (string)config["Status"],
                        Request_Status = (string)config["Request_Status"],
                        pagetitle=title
                    };
                    UserRequests.Add(proposal);
                    ViewBag.title = title;
                }
            }
            return PartialView("~/Views/ProjectManagement/PartialViews/UserRequestsList.cshtml", UserRequests);
        }
        [HttpPost]
        public ActionResult UserRequestsDocumentView(string No)
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
            List<ProjectProposals> userRequests = new List<ProjectProposals>();

            // Updated API endpoint for ProjectProposals
            string page = $"ProjectProposal?$filter=No eq '{No}'&$format=json";
            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                var config = details["value"].FirstOrDefault();
                if (config != null)
                {
                    ProjectProposals proposal = new ProjectProposals
                    {
                        No = (string)config["No"],
                        Name = (string)config["Name"],
                        /*Name = staffName,*/
                        Global_Dimension_2_Code = (string)config["Global_Dimension_2_Code"],
                        Administration_Unit_Name = (string)config["Administration_Unit_Name"],
                        County = (string)config["County"],
                        County_Name = (string)config["County_Name"],
                        Sub_County = (string)config["Sub_County"],
                        Sub_County_Name = (string)config["Sub_County_Name"],
                        Request_Description = (string)config["Request_Description"],
                        Justification = (string)config["Justification"],
                        Request_Source = (string)config["Request_Source"],
                        Request_Status = (string)config["Request_Status"],
                        Created_By = (string)config["Created_By"],
                        Employee_No = (string)config["Employee_No"],
                        Employee_Name = (string)config["Employee_Name"],
                        Status = (string)config["Status"],
                        IsManager = isManager,
                        Design_Created = (bool)config["Design_Created"],
                        LoggedInUserID = UserID

                    };
                    userRequests.Add(proposal);
                }
            }
            return PartialView("~/Views/ProjectManagement/UserRequestsDocumentView.cshtml", userRequests.FirstOrDefault());
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
                    ProjectProposals request = new ProjectProposals();
                    Session["httpResponse"] = null;
                    EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                    request.No = employeeView.No;
                    request.AdminUnitCode = employeeView.AdministrativeUnit;
                    request.RequesterName = employeeView.FirstName + " " + employeeView.LastName;

                    #region AdminUnits
                    List<DropdownList> AdminUnits = new List<DropdownList>();
                    //string pageAU = "DimensionValueList?$filter=Blocked eq false and Dimension_Code eq 'ADMINISTRATIVE' &$format=json";

                    string pageAU = $"DimensionValueList?$filter=Code eq '{employeeView.AdministrativeUnit}' &$format=json";

                    HttpWebResponse httpResponseAU = Credentials.GetOdataData(pageAU);
                    using (var streamReader = new StreamReader(httpResponseAU.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var details = JObject.Parse(result);
                        foreach (JObject config in details["value"])
                        {
                            DropdownList dropdownList3 = new DropdownList();
                            dropdownList3.Text = (string)config["Name"] + " (" + (string)config["Code"] + ")";
                            dropdownList3.Value = (string)config["Code"];
                            AdminUnits.Add(dropdownList3);
                        }
                    }
                    #endregion

                    #region County
                    List<DropdownList> county = new List<DropdownList>();
                    string pageC = "Counties?$format=json";

                    HttpWebResponse httpResponseC = Credentials.GetOdataData(pageC);
                    using (var streamReader = new StreamReader(httpResponseC.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var details = JObject.Parse(result);
                        foreach (JObject config in details["value"])
                        {
                            DropdownList dropdownList = new DropdownList();
                            dropdownList.Text = (string)config["Description"] + " (" + (string)config["Code"] + ")";
                            dropdownList.Value = (string)config["Code"];
                            county.Add(dropdownList);
                        }
                    }
                    #endregion

                    #region RequestSource
                    List<DropdownList> source = new List<DropdownList>();
                    //string pageSource = "RequestSources?$format=json";
                    string pageSource = "RequestSources?$format=json";


                    HttpWebResponse httpResponseSource = Credentials.GetOdataData(pageSource);
                    using (var streamReader = new StreamReader(httpResponseSource.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var details = JObject.Parse(result);
                        foreach (JObject config in details["value"])
                        {
                            DropdownList dropdownList = new DropdownList();
                            dropdownList.Text = (string)config["Source"];
                            dropdownList.Value = (string)config["Source"];
                            source.Add(dropdownList);
                        }
                    }
                    #endregion

                    request.ListOfAdminUnits = AdminUnits.Select(x =>
                                      new SelectListItem()
                                      {
                                          Text = x.Text,
                                          Value = x.Value
                                      }).ToList();

                    request.ListOfCounties = county.Select(x =>
                                      new SelectListItem()
                                      {
                                          Text = x.Text,
                                          Value = x.Value
                                      }).ToList();

                    request.ListOfRequestSource = source.Select(x =>
                                     new SelectListItem()
                                     {
                                         Text = x.Text,
                                         Value = x.Value
                                     }).ToList();
                    return PartialView("~/Views/ProjectManagement/PartialViews/NewRequest.cshtml", request);
                }
            }

            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult NewRequest(ProjectProposals newRequest)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                string Responsible_Employee_No = employeeView.No;
                string UserID = employeeView.UserID;

                string Name = "";
                if (newRequest.Name != null) { Name = (string)newRequest.Name; }

                string Administration_Unit_Name = "";
                if (newRequest.Administration_Unit_Name != null) { Administration_Unit_Name = (string)newRequest.Administration_Unit_Name; }

                string County = "";
                if (newRequest.County != null) { County = (string)newRequest.County; }

                string Sub_County = "";
                if (newRequest.Sub_County != null) { Sub_County = (string)newRequest.Sub_County; }

                string Request_Description = "";
                if (newRequest.Request_Description != null) { Request_Description = (string)newRequest.Request_Description; }

                string Justification = "";
                if (newRequest.Justification != null) { Justification = (string)newRequest.Justification; }

                string Request_Source = "";
                if (newRequest.Request_Source != null) { Request_Source = (string)newRequest.Request_Source; }

                string staffNo = Session["Username"].ToString();
                EmployeeView employee = Session["EmployeeData"] as EmployeeView;
                string userId = employee.UserID;
                string docNo = "";
               /* docNo = Credentials.ObjNav.FnCreateUserRequest(
                     Name,
                     Administration_Unit_Name,
                     Request_Description,
                     Justification,
                     County,
                     Sub_County,
                     Request_Source,
                     userId
                 );
*/
                if (docNo != "")
                {
                    string Redirect = docNo;
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
        public JsonResult SubmitRequest(String DocNo)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                string Responsible_Employee_No = employeeView.No;
                string UserID = employeeView.UserID;
                bool success = Credentials.ObjNav.FnSubmitUserRequest(
                    DocNo
                 );

                if (success)
                {
                    return Json(new { message = DocNo, success = true }, JsonRequestBehavior.AllowGet);
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
        public JsonResult ReceiveUserRequest(String UserRequestNo)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                string Responsible_Employee_No = employeeView.No;
                string UserID = employeeView.UserID;

                Credentials.ObjNav.FnRecieveUserRequest(
                    UserRequestNo, UserID
                 );
                return Json(new { message = "User request received successfully", success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult CreateProjectLeadSelection(String UserRequestNo)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                string Responsible_Employee_No = employeeView.No;
                string UserID = employeeView.UserID;

                Credentials.ObjNav.FnCreateProjectLeadsAppointment(
                    UserRequestNo
                 );
                return Json(new { message = "Team lead Successfully Created", success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetSubCounties(string county)
        {
            try
            {
                #region SubCountiesLookup
                List<DropdownList> Constituencies = new List<DropdownList>();

                //string pageStratCon = $"Constituencies?$filter=County_Code eq '{county}'&$format=json";
                string pageCon = $"Constituencies?$filter=County_Code eq '{county}'&$format=json";

                HttpWebResponse httpResponseStratCon = Credentials.GetOdataData(pageCon);
                using (var streamReader = new StreamReader(httpResponseStratCon.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        DropdownList dropdownList = new DropdownList();
                        dropdownList.Text = (string)config["Description"] + " (" + (string)config["Code"] + ")";
                        dropdownList.Value = (string)config["Code"];
                        Constituencies.Add(dropdownList);
                    }
                }
                #endregion
                // Create and return the JSON result
                var response = new
                {
                    ListOfSubCounties = Constituencies.Select(x => new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList(),
                    success = true
                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
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


        // Team Lead Selection
        public ActionResult TeamLeadSelection()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();
            }
        }
        public PartialViewResult TeamLeadSelectionList()
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();

            List<TeamLeadSelections> TeamLeadSelections = new List<TeamLeadSelections>();

            //string page = $"ProjectProposal?$orderby=No desc&$filter=Created_By eq '{employeeView.UserID}'&$format=json";
            string page = $"TeamSelectionCard?$filter= Design_Stage eq 'Team Leads Selection'&$orderby=No desc&$&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    TeamLeadSelections selection = new TeamLeadSelections
                    {
                        No = (string)config["No"],
                        User_Request_No = (string)config["User_Request_No"],
                        Administrative_Unit = (string)config["Administrative_Unit"],
                        Committee_Chair = (string)config["Committee_Chair"],
                        Chair_Name = (string)config["Chair_Name"],
                        Date_of_Meeting = (string)config["Date_of_Meeting"],
                        Caseload = (string)config["Caseload"],
                        Caseload_Category = (string)config["Caseload_Category"],
                        Project_Code = (string)config["Project_Code"],
                        Project_Name = (string)config["Project_Name"],
                        Team_Lead = (string)config["Team_Lead"],
                        Section_Lead_Area = (string)config["Section_Lead_Area"],


                    };
                    TeamLeadSelections.Add(selection);
                }
            }
            return PartialView(TeamLeadSelections);
        }
        public ActionResult TeamLeadSelectionDocumentView(string No)
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

            List<TeamLeadSelections> leadSelection = new List<TeamLeadSelections>();

            // Updated API endpoint for ProjectProposals
            string page = $"TeamSelectionCard?$filter=No eq '{No}'&$format=json";
            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                var config = details["value"].FirstOrDefault();
                if (config != null)
                {
                    TeamLeadSelections selection = new TeamLeadSelections
                    {
                        No = (string)config["No"],
                        Project_Code = (string)config["Project_Code"],
                        Project_Name = (string)config["Project_Name"],
                        Date_of_Meeting = (string)config["Date_of_Meeting"],
                        User_Request_No = (string)config["User_Request_No"],
                        Administrative_Unit = (string)config["Administrative_Unit"],
                        Committee_Chair = (string)config["Committee_Chair"],
                        Chair_Name = (string)config["Chair_Name"],
                        Caseload = (string)config["Caseload"],
                        Caseload_Category = (string)config["Caseload_Category"],
                        Team_Lead = (string)config["Team_Lead"],
                        Section_Lead_Area = (string)config["Section_Lead_Area"],
                        Sent_to_team_Leads = (bool)config["Sent_to_team_Leads"],
                        isManager = isManager


                    };
                    leadSelection.Add(selection);
                }
            }
            return PartialView(leadSelection.FirstOrDefault());
        }
        public PartialViewResult TeamLeadsLines(string No, bool isManager, bool Sent_to_team_Leads)
        {
            try
            {
                List<TeamLeads> teamLeads = new List<TeamLeads>();

                string page = "ProjectTeamLeads?$filter=Project_No eq '" + No + "'&$format=json";
                HttpWebResponse httpResponse = Credentials.GetOdataData(page);
                using var streamReader = new StreamReader(httpResponse.GetResponseStream());
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);

                foreach (JObject config in details["value"])
                {

                    // Create the Lines object
                    TeamLeads teamLead = new TeamLeads
                    {
                        Project_No = (string)config["Project_No"],
                        Resource_No = (string)config["Resource_No"],
                        Resource_Name = (string)config["Resource_Name"],
                        Specialty = (string)config["Specialty"],

                    };

                    // Add the object to the list
                    teamLeads.Add(teamLead);
                }
                ViewBag.isManager = isManager;
                ViewBag.Sent_to_team_Leads = Sent_to_team_Leads;
                return PartialView("~/Views/ProjectManagement/PartialViews/TeamLeadsLines.cshtml", teamLeads);
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public ActionResult NewTeamLead(string Project_No)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    TeamLeads lead = new TeamLeads();
                    Session["httpResponse"] = null;
                    EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                    lead.Project_No = Project_No;

                    #region Employees
                    List<DropdownList> outcome = new List<DropdownList>();
                    string pageEmpl = "Employees?$format=json";

                    HttpWebResponse httpResponseEmpl = Credentials.GetOdataData(pageEmpl);
                    using (var streamReader = new StreamReader(httpResponseEmpl.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var details = JObject.Parse(result);

                        foreach (JObject config in details["value"])
                        {
                            DropdownList dropdownList = new DropdownList();
                            dropdownList.Text = (string)config["FullName"] + "-" + (string)config["No"];
                            dropdownList.Value = (string)config["No"];
                            outcome.Add(dropdownList);
                        }
                    }
                    #endregion


                    lead.ListOfEmployees = outcome.Select(x =>
                                       new SelectListItem()
                                       {
                                           Text = x.Text,
                                           Value = x.Value
                                       }).ToList();

                    return PartialView("~/Views/ProjectManagement/PartialViews/NewTeamLead.cshtml", lead);

                }
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SubmitTeamLead(TeamLeads newTeamLead)
        {
            try
            {
                string staffNo = Session["Username"]?.ToString();
                EmployeeView employee = Session["EmployeeData"] as EmployeeView;
                string Project_No = newTeamLead.Project_No;
                string Resource_No = newTeamLead.Resource_No;
                string Resource_Name = newTeamLead.Resource_Name;
                string Specialty = newTeamLead.Specialty;

                Credentials.ObjNav.FnInsertProjectTeamLeadsLines(
                    Project_No,
                    Resource_No,
                    Specialty
                );

                string redirect = Project_No;
                return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UpdateTeamLeadSelectionHeader(string Project_No)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    TeamLeadSelections lead = new TeamLeadSelections();
                    Session["httpResponse"] = null;
                    EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                    lead.No = Project_No;

                    #region Employees
                    List<DropdownList> outcome = new List<DropdownList>();
                    string pageEmpl = "Employees?$format=json";

                    HttpWebResponse httpResponseEmpl = Credentials.GetOdataData(pageEmpl);
                    using (var streamReader = new StreamReader(httpResponseEmpl.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var details = JObject.Parse(result);

                        foreach (JObject config in details["value"])
                        {
                            DropdownList dropdownList = new DropdownList();
                            dropdownList.Text = (string)config["FullName"] + "-" + (string)config["No"];
                            dropdownList.Value = (string)config["No"];
                            outcome.Add(dropdownList);
                        }
                    }
                    #endregion


                    lead.ListOfEmployees = outcome.Select(x =>
                                       new SelectListItem()
                                       {
                                           Text = x.Text,
                                           Value = x.Value
                                       }).ToList();

                    return PartialView("~/Views/ProjectManagement/PartialViews/UpdateTeamLeadSelectionHeader.cshtml", lead);

                }
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SubmitUpdatedTeamLeadHeader(TeamLeadSelections updateTeamLeadDoc)
        {
            try
            {
                string staffNo = Session["Username"]?.ToString();
                EmployeeView employee = Session["EmployeeData"] as EmployeeView;
                string No = updateTeamLeadDoc.No;
                DateTime Date_of_Meeting = DateTime.Parse(updateTeamLeadDoc.Date_of_Meeting);
                string Caseload = updateTeamLeadDoc.Caseload;
                string Caseload_Category = updateTeamLeadDoc.Caseload_Category;
                string Committee_Chair = updateTeamLeadDoc.Committee_Chair;

                bool result = Credentials.ObjNav.FnUpdateProjectLeadsAppointment(
                    No,
                    Date_of_Meeting,
                    Committee_Chair,
                    Caseload_Category,
                    Caseload

                );
                if (result)
                {
                    string redirect = No;
                    return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string redirect = No;
                    return Json(new { message = redirect, success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SendForSelection(string No)
        {
            try
            {
                string staffNo = Session["Username"]?.ToString();
                EmployeeView employee = Session["EmployeeData"] as EmployeeView;

                Credentials.ObjNav.FnCreateProjectTeamCard(
                    No
                );
                string redirect = No;
                return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }



        // project team selection
        public ActionResult ProjectTeamSelection()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();
            }
        }
        public PartialViewResult ProjectTeamSelectionList()
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();


            List<ProjectTeamSelections> IndividualTeamSelections = new List<ProjectTeamSelections>();

            /*string page = $"IndividualTeamSelection?$filter=Design_Stage eq 'Team Selection'&$orderby=No desc&$&$format=json";
*/
            string page = $"IndividualTeamSelection?$filter=Design_Stage eq 'Team Selection' and Team_Lead eq '{StaffNo}'&$orderby=No desc&$&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    ProjectTeamSelections selection = new ProjectTeamSelections
                    {
                        No = (string)config["No"],
                        User_Request_No = (string)config["User_Request_No"],
                        Project_Code = (string)config["Project_Code"],
                        Project_Name = (string)config["Project_Name"],
                        Geographical_Unit_Name = (string)config["Geographical_Unit_Name"],
                        Team_Lead = (string)config["Team_Lead"],
                        Section_Lead_Area = (string)config["Section_Lead_Area"],

                    };
                    IndividualTeamSelections.Add(selection);
                }
            }
            return PartialView(IndividualTeamSelections);
        }
        public ActionResult ProjectTeamSelectionDocumentView(string No)
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

            List<ProjectTeamSelections> leadSelection = new List<ProjectTeamSelections>();

            // Updated API endpoint for ProjectProposals
            string page = $"IndividualTeamSelection?$filter=No eq '{No}'&$format=json";
            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                var config = details["value"].FirstOrDefault();
                if (config != null)
                {
                    ProjectTeamSelections selection = new ProjectTeamSelections
                    {
                        No = (string)config["No"],
                        Project_Code = (string)config["Project_Code"],
                        Project_Name = (string)config["Project_Name"],
                        Team_Lead = (string)config["Team_Lead"],
                        User_Request_No = (string)config["User_Request_No"],
                        Section_Lead_Area = (string)config["Section_Lead_Area"],
                        Appointed = (bool)config["Appointed"],
                        Design_Created = (bool)config["Design_Created"],
                        isManager = isManager

                    };
                    leadSelection.Add(selection);
                }
            }
            ViewBag.isManager = isManager;
            return PartialView(leadSelection.FirstOrDefault());
        }
        public PartialViewResult ProjectTeamLines(string No, bool isManager)
        {
            try
            {
                List<ProjectTeam> projectTeam = new List<ProjectTeam>();

                string page = "ProjectTeam?$filter=Design_No eq '" + No + "'&$format=json";
                HttpWebResponse httpResponse = Credentials.GetOdataData(page);
                using var streamReader = new StreamReader(httpResponse.GetResponseStream());
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);

                foreach (JObject config in details["value"])
                {

                    // Create the Lines object
                    ProjectTeam teamMember = new ProjectTeam
                    {
                        Design_No = (string)config["Design_No"],
                        No = (string)config["No"],
                        Name = (string)config["Name"],
                        Role = (string)config["Role"],
                        Expertise = (string)config["Expertise"],
                        Approval_Sequence = (int)config["Approval_Sequence"],
                        Involvement_Stage = (string)config["Involvement_Stage"],
                        isManager = isManager

                    };

                    // Add the object to the list
                    projectTeam.Add(teamMember);
                    ViewBag.isManager = isManager;
                }

                return PartialView("~/Views/ProjectManagement/PartialViews/ProjectTeamLines.cshtml", projectTeam);
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public ActionResult NewTeamMemberLine(string Project_No)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    ProjectTeam member = new ProjectTeam();
                    Session["httpResponse"] = null;
                    EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                    member.Design_No = Project_No;

                    #region Employees
                    List<DropdownList> employees = new List<DropdownList>();
                    string pageEmpl = "Employees?$format=json";

                    HttpWebResponse httpResponseEmpl = Credentials.GetOdataData(pageEmpl);
                    using (var streamReader = new StreamReader(httpResponseEmpl.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var details = JObject.Parse(result);

                        foreach (JObject config in details["value"])
                        {
                            DropdownList dropdownList = new DropdownList();
                            dropdownList.Text = (string)config["FullName"] + "-" + (string)config["No"];
                            dropdownList.Value = (string)config["No"];
                            employees.Add(dropdownList);
                        }
                    }
                    #endregion


                    member.ListOfEmployees = employees.Select(x =>
                                       new SelectListItem()
                                       {
                                           Text = x.Text,
                                           Value = x.Value
                                       }).ToList();

                    return PartialView("~/Views/ProjectManagement/PartialViews/NewTeamMemberLine.cshtml", member);

                }
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SubmitTeamMember(ProjectTeam newTeamMember)
        {
            try
            {
                string staffNo = Session["Username"]?.ToString();
                EmployeeView employee = Session["EmployeeData"] as EmployeeView;
                string Design_No = newTeamMember.Design_No;
                string Resource = newTeamMember.No;
                string Expertise = newTeamMember.Expertise;
                int Involvement_Stage = int.Parse(newTeamMember.Involvement_Stage);

                Credentials.ObjNav.FnInsertProjectTeamLines(
                    Design_No,
                    Resource,
                    Expertise,
                    Involvement_Stage
                );

                string redirect = Design_No;
                return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateTeamMemberLinesssssssss(string No)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    ProjectTeam member = new ProjectTeam();
                    Session["httpResponse"] = null;
                    EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                    member.Design_No = No;

                    #region Employees
                    List<DropdownList> employees = new List<DropdownList>();
                    string pageEmpl = "Employees?$format=json";

                    HttpWebResponse httpResponseEmpl = Credentials.GetOdataData(pageEmpl);
                    using (var streamReader = new StreamReader(httpResponseEmpl.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var details = JObject.Parse(result);

                        foreach (JObject config in details["value"])
                        {
                            DropdownList dropdownList = new DropdownList();
                            dropdownList.Text = (string)config["FullName"] + "-" + (string)config["No"];
                            dropdownList.Value = (string)config["No"];
                            employees.Add(dropdownList);
                        }
                    }
                    #endregion


                    member.ListOfEmployees = employees.Select(x =>
                                       new SelectListItem()
                                       {
                                           Text = x.Text,
                                           Value = x.Value
                                       }).ToList();

                    return PartialView("~/Views/ProjectManagement/PartialViews/NewTeamMemberLine.cshtml", member);

                }
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public ActionResult UpdateTeamMemberLine(string Design_No, string itemNo)
        {
            try
            {
                // Session validation
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                LoadUsers();

                var employeeView = Session["EmployeeData"] as EmployeeView;
                if (employeeView == null)
                {
                    throw new Exception("Employee data not found in session.");
                }

                // Fetch project team data
                string query = $"ProjectTeam?$filter=Design_No eq '{Design_No}' and No eq '{itemNo}'&$format=json";
                var response = Credentials.GetOdataData(query); // Updated method for async HttpClient
                using var streamReader = new StreamReader(response.GetResponseStream());
                var result = streamReader.ReadToEnd();

                // Parse JSON response
                var details = JObject.Parse(result);
                var projectTeam = details["value"]
                    .Select(config => new ProjectTeam
                    {
                        Design_No = (string)config["Design_No"],
                        No = (string)config["No"],
                        Name = (string)config["Name"],
                        Role = (string)config["Role"],
                        Expertise = (string)config["Expertise"],
                        Approval_Sequence = (int?)config["Approval_Sequence"] ?? 0,
                        Involvement_Stage = (string)config["Involvement_Stage"]
                    })
                    .FirstOrDefault(); // Get only the first matching record

                return PartialView("~/Views/ProjectManagement/PartialViews/UpdateTeamMemberLine.cshtml", projectTeam);
            }
            catch (Exception ex)
            {
                // Log error (e.g., to a file or monitoring system)
                var errorMsg = new Error { Message = ex.Message };
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", errorMsg);
            }
        }
        public JsonResult AppointTeamMember(string No)
        {
            try
            {
                string staffNo = Session["Username"]?.ToString();
                EmployeeView employee = Session["EmployeeData"] as EmployeeView;

                Credentials.ObjNav.FnAppointTeamMember(
                    No

                );

                string redirect = No;
                return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult CreateStakeholderMeeting(string No)
        {
            try
            {
                string staffNo = Session["Username"]?.ToString();
                EmployeeView employee = Session["EmployeeData"] as EmployeeView;

                Credentials.ObjNav.FnCreateStakeHolderFeedback(
                    No
                );

                string redirect = No;
                return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        protected void LoadUsers()
        {
            try
            {
                #region Users

                List<DropdownList> user = new List<DropdownList>();
                string pageUser = "Employees?$&format=json";

                HttpWebResponse httpResponseUser = Credentials.GetOdataData(pageUser);
                using (var streamReader = new StreamReader(httpResponseUser.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        DropdownList LocList = new DropdownList
                        {
                            Text = (string)config["FullName"] + "-" + (string)config["No"],
                            Value = (string)config["No"]
                        };
                        user.Add(LocList);
                    }
                }

                ViewBag.MembersList = user; // Assign to ViewBag

                #endregion
            }
            catch (Exception exception)
            {
                // Handle the exception
                exception.Data.Clear();
            }
        }



        //First stakeholder meeting
        public ActionResult FirstStakeholderMeetingList()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();
            }
        }
        public PartialViewResult FirstStakeholderMeetingListPartialView()
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();

            string UserID = employeeView.UserID;
            var isManager = IsManager(UserID);

            List<StakeholderMeeting> meeting = new List<StakeholderMeeting>();

            //string page = $"StakeholderMeetings?$orderby=No desc&$filter=Created_By eq '{employeeView.UserID}'&$format=json";
            string page = $"StakeholderCard?$filter=Design_Stage eq 'Preliminary Design'&$orderby=No desc&$&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    StakeholderMeeting selection = new StakeholderMeeting
                    {
                        No = (string)config["No"],
                        Project_Code = (string)config["Project_Code"],
                        Project_Name = (string)config["Project_Name"],
                        Date_of_Meeting = (string)config["Date_of_Meeting"],
                        isManager = isManager
                    };
                    meeting.Add(selection);
                }
            }
            ViewBag.isManager = isManager;
            return PartialView(meeting);
        }
        public ActionResult FirstStakeholderMeetingDocumentView(string No)
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


            List<TeamLeadSelections> leadSelection = new List<TeamLeadSelections>();

            // Updated API endpoint for ProjectProposals
            string page = $"StakeholderCard?$filter=No eq '{No}'&$format=json";
            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                var config = details["value"].FirstOrDefault();
                if (config != null)
                {
                    TeamLeadSelections selection = new TeamLeadSelections
                    {
                        No = (string)config["No"],
                        Administrative_Unit = (string)config["Administrative_Unit"],
                        /*User_Request_No = (string)config["User_Request_No"],*/
                        Committee_Chair = (string)config["Committee_Chair"],
                        Chair_Name = (string)config["Chair_Name"],
                        Date_of_Meeting = (string)config["Date_of_Meeting"],
                        Caseload = (string)config["Caseload"],
                        Caseload_Category = (string)config["Caseload_Category"],
                        Project_Code = (string)config["Project_Code"],
                        Project_Name = (string)config["Project_Name"],
                        Objective_of_engagement = (string)config["Objective_of_engagement"],
                        Design_Created = (bool)config["Design_Created"],
                        isManager = isManager

                    };
                    leadSelection.Add(selection);
                }
            }
            return PartialView(leadSelection.FirstOrDefault());
        }
        public PartialViewResult StakeholderFeedbackLines(string No, bool isManager)
        {
            try
            {
                List<StakeholderFeedback> stakeholderFeedback = new List<StakeholderFeedback>();
                string page = "CommitteeSubform?$filter=No eq '" + No + "'&$format=json";
                HttpWebResponse httpResponse = Credentials.GetOdataData(page);
                using var streamReader = new StreamReader(httpResponse.GetResponseStream());
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    // Create the Lines object
                    StakeholderFeedback feedback = new StakeholderFeedback
                    {
                        No = (string)config["No"],
                        Line_No = (int)config["Line_No"],
                        Suggestion = (string)config["Suggestion"],
                        Location = (string)config["Location"],
                        Proponent = (string)config["Proponent"],
                        Status_Of_Suggestion = (string)config["Status_Of_Suggestion"],
                        isManager = isManager
                    };

                    // Add the object to the list
                    stakeholderFeedback.Add(feedback);

                    ViewBag.isManager3 = isManager;
                }

                return PartialView("~/Views/ProjectManagement/PartialViews/StakeholderFeedbackLines.cshtml", stakeholderFeedback);
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public ActionResult NewStakeholderFeedback(string Project_No)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    StakeholderFeedback feedback = new StakeholderFeedback();
                    Session["httpResponse"] = null;
                    EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                    feedback.No = Project_No;

                    #region Employees
                    List<DropdownList> employees = new List<DropdownList>();
                    /* string pageEmpl = "Employees?$format=json";*/
                    string pageEmpl = "ProjectTeam?$filter=Design_No eq '" + Project_No + "'&$format=json";

                    HttpWebResponse httpResponseEmpl = Credentials.GetOdataData(pageEmpl);
                    using (var streamReader = new StreamReader(httpResponseEmpl.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var details = JObject.Parse(result);

                        foreach (JObject config in details["value"])
                        {
                            DropdownList dropdownList = new DropdownList();
                            dropdownList.Text = (string)config["Name"] + " (" + (string)config["No"] + ")";
                            dropdownList.Value = (string)config["No"];
                            employees.Add(dropdownList);
                        }
                    }
                    #endregion

                    #region Status
                    List<DropdownList> status = new List<DropdownList>();
                    string pageStatus = "FeedbackStatus?$format=json";

                    HttpWebResponse httpResponseStatus = Credentials.GetOdataData(pageStatus);
                    using (var streamReader = new StreamReader(httpResponseStatus.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var details = JObject.Parse(result);

                        foreach (JObject config in details["value"])
                        {
                            DropdownList dropdownList = new DropdownList();
                            dropdownList.Text = (string)config["Status"];
                            dropdownList.Value = (string)config["Status"];
                            status.Add(dropdownList);
                        }
                    }
                    #endregion


                    feedback.ListOfEmployees = employees.Select(x =>
                                      new SelectListItem()
                                      {
                                          Text = x.Text,
                                          Value = x.Value
                                      }).ToList();



                    feedback.ListOfStatus = status.Select(x =>
                                       new SelectListItem()
                                       {
                                           Text = x.Text,
                                           Value = x.Value
                                       }).ToList();

                    return PartialView("~/Views/ProjectManagement/PartialViews/NewStakeholderFeedback.cshtml", feedback);

                }
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SubmitStakeholderFeedback(StakeholderFeedback newFeedback)
        {
            try
            {
                string staffNo = Session["Username"]?.ToString();
                EmployeeView employee = Session["EmployeeData"] as EmployeeView;
                string No = newFeedback.No;
                string Suggestion = newFeedback.Suggestion;
                string Location = newFeedback.Location;
                string Proponent = newFeedback.Proponent;
                string Status_Of_Suggestion = newFeedback.Status_Of_Suggestion;


                Credentials.ObjNav.FnUpdateStakeholderFeedBackLines(
                    No,
                    Suggestion,
                    Proponent,
                    Location,
                    Status_Of_Suggestion

                );

                string redirect = No;
                return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult CreatePreliminaryDesign(string No)
        {
            try
            {
                string staffNo = Session["Username"]?.ToString();
                EmployeeView employee = Session["EmployeeData"] as EmployeeView;

                Credentials.ObjNav.FnCreatePreliminaryDesignPortal(
                    No

                );

                string redirect = No;
                return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }


        //preliminary design controls
        public ActionResult PreliminaryDesignControlList()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();
            }
        }
        public PartialViewResult PreliminaryDesignControlListPartialView()
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();

            List<PreliminaryDesignControl> design = new List<PreliminaryDesignControl>();

            string page = $"DesignControlCard?$filter=Document_Type eq 'Design' and Design_Control_Type eq 'Preliminary'&$orderby=No desc&$&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    PreliminaryDesignControl designControl = new PreliminaryDesignControl
                    {
                        No = (string)config["No"],
                        Name = (string)config["Name"],
                        Type = (string)config["Type"],
                        Start_Date = (string)config["Start_Date"],
                        End_Date = (string)config["End_Date"],
                        Committee = (string)config["Committee"],
                        Global_Dimension_2_Code = (string)config["Global_Dimension_2_Code"],
                        Geographic_Location_Name = (string)config["Geographic_Location_Name"],
                        Total_Estimated_Cost = (int)config["Total_Estimated_Cost"],
                        Project_Classifications = (string)config["Project_Classifications"],
                        Created_By = (string)config["Created_By"],
                        Employee_No = (string)config["Employee_No"],
                        Employee_Name = (string)config["Employee_Name"],
                        Rejection_Comments = (string)config["Rejection_Comments"],
                        Deferal_Comments = (string)config["Deferal_Comments"],
                        Design_Stage = (string)config["Design_Stage"],
                        Status = (string)config["Status"],
                        Proposed_Project = (string)config["Proposed_Project"],
                        Proposed_Project_Name = (string)config["Proposed_Project_Name"],
                        Approved_Proposal = (string)config["Approved_Proposal"],
                        Proposal_Name = (string)config["Proposal_Name"],
                        Team_Leader = (string)config["Team_Leader"],
                        Design_Status = (string)config["Design_Status"],
                        Document_Type = (string)config["Document_Type"],
                        Design_Control_Type = (string)config["Design_Control_Type"],
                        Current_Actioning_Member = (string)config["Current_Actioning_Member"],
                        Design_Approval_Stage = (string)config["Design_Approval_Stage"],
                        Situation_Analysis = (string)config["Situation_Analysis"],
                        Relevance_of_proj_Idea = (string)config["Relevance_of_proj_Idea"],
                        Scope_of_the_project = (string)config["Scope_of_the_project"],
                        Logical_Framework = (string)config["Logical_Framework"],
                        Goal = (string)config["Goal"],
                        Project_Objectives_Outcome = (string)config["Project_Objectives_Outcome"],
                        Proposed_Project_Outputs = (string)config["Proposed_Project_Outputs"],
                        Project_Activities_and_Inputs = (string)config["Project_Activities_and_Inputs"],
                        institutional_Mandate = (string)config["institutional_Mandate"],
                        Management_of_the_Project = (string)config["Management_of_the_Project"],
                        Monitoring_and_Evaluation = (string)config["Monitoring_and_Evaluation"],
                        Risk_and_Mitigation_Measures = (string)config["Risk_and_Mitigation_Measures"],
                        Project_Sustainability = (string)config["Project_Sustainability"],
                        Project_Stakeholders_and_Collaborators = (string)config["Project_Stakeholders_and_Collaborators"],
                        Project_Readiness = (string)config["Project_Readiness"]

                    };
                    design.Add(designControl);
                }
            }
            return PartialView(design);
        }
        public ActionResult PreliminaryDesignControlDocumentView(string No)
        {

            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                string staffName = employeeView.FirstName + " " + employeeView.LastName;
                string StaffNo = Session["Username"].ToString();

                List<PreliminaryDesignControl> designControls = new List<PreliminaryDesignControl>();
                string page = $"DesignControlCard?$filter=No eq '{No}'&$format=json";
                HttpWebResponse httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    var config = details["value"].FirstOrDefault();
                    if (config != null)
                    {
                        PreliminaryDesignControl design = new PreliminaryDesignControl
                        {
                            No = (string)config["No"],
                            Project_Code = (string)config["project_Code"],
                            Name = (string)config["Name"],
                            Type = (string)config["Type"],
                            Start_Date = (string)config["Start_Date"],
                            End_Date = (string)config["End_Date"],
                            Committee = (string)config["Committee"],
                            Global_Dimension_2_Code = (string)config["Global_Dimension_2_Code"],
                            Geographic_Location_Name = (string)config["Geographic_Location_Name"],
                            Total_Estimated_Cost = (int)config["Total_Estimated_Cost"],
                            Project_Classifications = (string)config["Project_Classifications"],
                            Created_By = (string)config["Created_By"],
                            Employee_No = (string)config["Employee_No"],
                            Employee_Name = (string)config["Employee_Name"],
                            Rejection_Comments = (string)config["Rejection_Comments"],
                            Deferal_Comments = (string)config["Deferal_Comments"],
                            Design_Stage = (string)config["Design_Stage"],
                            Status = (string)config["Status"],
                            Proposed_Project = (string)config["Proposed_Project"],
                            Proposed_Project_Name = (string)config["Proposed_Project_Name"],
                            Approved_Proposal = (string)config["Approved_Proposal"],
                            Proposal_Name = (string)config["Proposal_Name"],
                            Team_Leader = (string)config["Team_Leader"],
                            Design_Status = (string)config["Design_Status"],
                            Document_Type = (string)config["Document_Type"],
                            Design_Control_Type = (string)config["Design_Control_Type"],
                            Current_Actioning_Member = (string)config["Current_Actioning_Member"],
                            Design_Approval_Stage = (string)config["Design_Approval_Stage"],
                            Situation_Analysis = (string)config["Situation_Analysis"],
                            Relevance_of_proj_Idea = (string)config["Relevance_of_proj_Idea"],
                            Scope_of_the_project = (string)config["Scope_of_the_project"],
                            Logical_Framework = (string)config["Logical_Framework"],
                            Goal = (string)config["Goal"],
                            Project_Objectives_Outcome = (string)config["Project_Objectives_Outcome"],
                            Proposed_Project_Outputs = (string)config["Proposed_Project_Outputs"],
                            Project_Activities_and_Inputs = (string)config["Project_Activities_and_Inputs"],
                            institutional_Mandate = (string)config["institutional_Mandate"],
                            Management_of_the_Project = (string)config["Management_of_the_Project"],
                            Monitoring_and_Evaluation = (string)config["Monitoring_and_Evaluation"],
                            Risk_and_Mitigation_Measures = (string)config["Risk_and_Mitigation_Measures"],
                            Project_Sustainability = (string)config["Project_Sustainability"],
                            Project_Stakeholders_and_Collaborators = (string)config["Project_Stakeholders_and_Collaborators"],
                            Project_Readiness = (string)config["Project_Readiness"]

                        };
                        designControls.Add(design);
                    }
                }
                return PartialView(designControls.FirstOrDefault());

            }
            catch (WebException ex)
            {

                Error erroMsg = new Error
                {
                    Message = ex.Message
                };
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }

        public PartialViewResult DesignControlsLines(string No, string status)
        {
            try
            {
                List<DesignControlsLines> summaryBOQs = new List<DesignControlsLines>();
                string page = $"DesignControlTask?$filter= Header_No eq '{No}'&$format=json";
                HttpWebResponse httpResponse = Credentials.GetOdataData(page);
                using var streamReader = new StreamReader(httpResponse.GetResponseStream());
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);

                foreach (JObject config in details["value"])
                {
                    DesignControlsLines summaryBOQ = new DesignControlsLines
                    {
                        Header_No = (string)config["Header_No"],
                        Entry_No = (int)config["Entry_No"],
                        Attachment_Code = (string)config["Attachment_Code"],
                        Design_Control_No = (int)config["Design_Control_No"],
                        Design_Control_Type = (string)config["Design_Control_Type"],
                        Name = (string)config["Name"],
                        No_of_Items = (int)config["No_of_Items"],
                    };

                    // Add the object to the list
                    summaryBOQs.Add(summaryBOQ);

                }
                ViewBag.Status = status;
                return PartialView("~/Views/ProjectManagement/PartialViews/DesignControlsLines.cshtml", summaryBOQs);
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public PartialViewResult GrandSummaryBOQLines(string No, string status, string Design_Approval_Stage)
        {
            try
            {
                List<GrandSummaryBOQLines> summaryBOQs = new List<GrandSummaryBOQLines>();
                string page = $"ProjectPlanningLines?$filter= Header_No eq '{No}'&$format=json";
                HttpWebResponse httpResponse = Credentials.GetOdataData(page);
                using var streamReader = new StreamReader(httpResponse.GetResponseStream());
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);

                foreach (JObject config in details["value"])
                {
                    GrandSummaryBOQLines summaryBOQ = new GrandSummaryBOQLines
                    {
                        Header_No = (string)config["Header_No"],
                        Line_No = (int)config["Line_No"],
                        Description = (string)config["Description"],
                        Quantity = (int)config["Quantity"],
                        Unit_of_Measure = (string)config["Unit_of_Measure"],
                        Unit_Cost = (int)config["Unit_Cost"],
                        Total_Cost = (int)config["Total_Cost"],
                    };

                    // Add the object to the list
                    summaryBOQs.Add(summaryBOQ);

                }
                ViewBag.Status = status;
                ViewBag.Design_Approval_Stage = Design_Approval_Stage;
                return PartialView("~/Views/ProjectManagement/PartialViews/GrandSummaryBOQLines.cshtml", summaryBOQs);
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public PartialViewResult TeamMembersBOQLines(string Design_Code)
        {
            try
            {
                List<TeamMembersBOQLines> TeamMembersBOQ = new List<TeamMembersBOQLines>();
                string page = $"TeamBoqs?$filter= Design_Code eq '{Design_Code}'&$format=json";
                HttpWebResponse httpResponse = Credentials.GetOdataData(page);
                using var streamReader = new StreamReader(httpResponse.GetResponseStream());
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    TeamMembersBOQLines TeamMemberBOQ = new TeamMembersBOQLines
                    {
                        Design_Code = (string)config["Design_Code"],
                        Member_No = (string)config["Member_No"],
                        Entry_No = (int)config["Entry_No"],
                        Design_Stage = (string)config["Design_Stage"],
                        Description = (string)config["Description"],
                        quantity = (int)config["quantity"],
                        Unit_Price = (int)config["Unit_Price"],
                        Total_Amount = (int)config["Total_Amount"],
                    };

                    TeamMembersBOQ.Add(TeamMemberBOQ);
                }
                return PartialView("~/Views/ProjectManagement/PartialViews/TeamMembersBOQLines.cshtml", TeamMembersBOQ);
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public PartialViewResult ConceiptAnalysisForm(string No)
        {
            try
            {

                PreliminaryDesignControl designControls = new PreliminaryDesignControl();
                string page = $"DesignControlCard?$filter=No eq '{No}'&$format=json";
                HttpWebResponse httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    var config = details["value"].FirstOrDefault();
                    if (config != null)
                    {
                        PreliminaryDesignControl design = new PreliminaryDesignControl
                        {
                            Situation_Analysis = (string)config["Situation_Analysis"],
                            Relevance_of_proj_Idea = (string)config["Relevance_of_proj_Idea"],
                            Scope_of_the_project = (string)config["Scope_of_the_project"],
                            Logical_Framework = (string)config["Logical_Framework"],
                            Goal = (string)config["Goal"],
                            Project_Objectives_Outcome = (string)config["Project_Objectives_Outcome"],
                            Proposed_Project_Outputs = (string)config["Proposed_Project_Outputs"],
                            Project_Activities_and_Inputs = (string)config["Project_Activities_and_Inputs"],
                            institutional_Mandate = (string)config["institutional_Mandate"],
                            Management_of_the_Project = (string)config["Management_of_the_Project"],
                            Monitoring_and_Evaluation = (string)config["Monitoring_and_Evaluation"],
                            Risk_and_Mitigation_Measures = (string)config["Risk_and_Mitigation_Measures"],
                            Project_Sustainability = (string)config["Project_Sustainability"],
                            Project_Stakeholders_and_Collaborators = (string)config["Project_Stakeholders_and_Collaborators"],
                            Project_Readiness = (string)config["Project_Readiness"]
                        };
                        designControls = design;
                    }
                }
                return PartialView("~/Views/ProjectManagement/PartialViews/ConceiptAnalysisForm.cshtml", designControls);
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public ActionResult NewSummaryBOQLine(string Project_No)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    return PartialView("~/Views/ProjectManagement/PartialViews/NewSummaryBOQLine.cshtml");
                }
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SubmitSummaryBOQLine(GrandSummaryBOQLines summaryBOQ)
        {
            try
            {
                string staffNo = Session["Username"]?.ToString();
                EmployeeView employee = Session["EmployeeData"] as EmployeeView;
                string Header_No = summaryBOQ.Header_No;
                int Line_No = summaryBOQ.Line_No;
                string Description = summaryBOQ.Description;
                int Quantity = summaryBOQ.Quantity;
                int Unit_Cost = summaryBOQ.Unit_Cost;
                int Total_Cost = summaryBOQ.Total_Cost;

                /*Credentials.ObjNav.FnUpdateQSBqs(
                    Header_No,
                    Line_No,
                    Quantity,
                    Unit_Cost,
                    Description
                );*/

                string redirect = Header_No;
                return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult NewTeamMemberBQLine(string Project_No)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    TeamMembersBOQLines TeamMemberBOQ = new TeamMembersBOQLines();


                    #region Employees
                    List<DropdownList> employees = new List<DropdownList>();
                    /*    string pageEmpl = "Employees?$format=json";*/
                    string pageEmpl = "ProjectTeam?$filter=Design_No eq '" + Project_No + "'&$format=json";

                    HttpWebResponse httpResponseEmpl = Credentials.GetOdataData(pageEmpl);
                    using (var streamReader = new StreamReader(httpResponseEmpl.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var details = JObject.Parse(result);
                        foreach (JObject config in details["value"])
                        {
                            DropdownList dropdownList = new DropdownList();
                            dropdownList.Text = (string)config["Name"] + " (" + (string)config["No"] + ")";
                            dropdownList.Value = (string)config["No"];
                            employees.Add(dropdownList);
                        }
                    }
                    #endregion

                    TeamMemberBOQ.ListOfEmployees = employees.Select(x =>
                                      new SelectListItem()
                                      {
                                          Text = x.Text,
                                          Value = x.Value
                                      }).ToList();

                    return PartialView("~/Views/ProjectManagement/PartialViews/NewTeamMemberBQLine.cshtml", TeamMemberBOQ);
                }
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SubmitTeamMemberBOQLine(TeamMembersBOQLines teamMember)
        {
            try
            {
                string staffNo = Session["Username"]?.ToString();
                EmployeeView employee = Session["EmployeeData"] as EmployeeView;
                string Design_Code = teamMember.Design_Code;
                string Member_No = teamMember.Member_No;
                int Entry_No = teamMember.Entry_No;
                string Design_Stage = teamMember.Design_Stage;
                string Description = teamMember.Description;
                int quantity = teamMember.quantity;
                int Unit_Price = teamMember.Unit_Price;
                int Total_Amount = teamMember.Total_Amount;



                /*Credentials.ObjNav.FnUpdateQSBqs(
                    Header_No,
                    Line_No,
                    Quantity,
                    Unit_Cost,
                    Description
                );*/

                string redirect = Design_Code;
                return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EditGrandSummaryBOQLine(GrandSummaryBOQLines grandSummaryBOQLine)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    return PartialView("~/Views/ProjectManagement/PartialViews/EditGrandSummaryBOQLine.cshtml", grandSummaryBOQLine);
                }
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SubmitUpdatedSummaryBOQLine(GrandSummaryBOQLines summaryBOQ)
        {
            try
            {
                string staffNo = Session["Username"]?.ToString();
                EmployeeView employee = Session["EmployeeData"] as EmployeeView;
                string Header_No = summaryBOQ.Header_No;
                int Line_No = summaryBOQ.Line_No;
                string Description = summaryBOQ.Description;
                int Quantity = summaryBOQ.Quantity;
                int Unit_Cost = summaryBOQ.Unit_Cost;
                int Total_Cost = summaryBOQ.Total_Cost;

                Credentials.ObjNav.FnUpdateQSBqs(
                    Header_No,
                    Line_No,
                    Quantity,
                    Unit_Cost,
                    Description
                );

                string redirect = Header_No;
                return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult EditTeamMemberBOQLine(TeamMembersBOQLines memberBOQ)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    return PartialView("~/Views/ProjectManagement/PartialViews/EditTeamMemberBOQLine.cshtml");
                }
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SubmitUpdatedTeamMemberBOQLine(TeamMembersBOQLines memberBOQ)
        {
            try
            {
                string staffNo = Session["Username"]?.ToString();
                EmployeeView employee = Session["EmployeeData"] as EmployeeView;

                string Design_Code = memberBOQ.Design_Code;
                int Entry_No = memberBOQ.Entry_No;
                string Design_Stage = memberBOQ.Design_Stage;
                string Description = memberBOQ.Description;
                int quantity = memberBOQ.quantity;
                int Unit_Price = memberBOQ.Unit_Price;
                int Total_Amount = memberBOQ.Total_Amount;


                Credentials.ObjNav.FnUpdateTeamBqs(
                   Design_Code,
                   Entry_No,
                   employee.No, //member Number
                   Design_Stage,
                   quantity,
                   Unit_Price,
                   Description
                );

                string redirect = "";
                return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UpdateConceiptAnalysis(ConceiptAnalysis conceiptAnalysis)
        {
            try
            {
                string staffNo = Session["Username"]?.ToString();
                EmployeeView employee = Session["EmployeeData"] as EmployeeView;

                string situationAnalysis = conceiptAnalysis.situationAnalysis;
                string relevance = conceiptAnalysis.relevance;
                string projectScope = conceiptAnalysis.projectScope;
                string logicalFramework = conceiptAnalysis.logicalFramework;
                string goal = conceiptAnalysis.goal;
                string objectives = conceiptAnalysis.objectives;
                string projectOutput = conceiptAnalysis.projectOutput;
                string activitiesInput = conceiptAnalysis.activitiesInput;

                string InstitutionalMandate = conceiptAnalysis.InstitutionalMandate;
                string ProjectManagement = conceiptAnalysis.ProjectManagement;
                string Monitoring = conceiptAnalysis.Monitoring;
                string Risk = conceiptAnalysis.Risk;
                string ProjectSustainability = conceiptAnalysis.ProjectSustainability;
                string ProjectStakeholders = conceiptAnalysis.ProjectStakeholders;
                string ProjectReadiness = conceiptAnalysis.ProjectReadiness;

                Credentials.ObjNav.FnUpdateDesignCardHeader(
                    conceiptAnalysis.Project_No,
                    situationAnalysis,
                    relevance,
                    projectScope,
                    logicalFramework,
                    goal,
                    objectives,
                    projectOutput,
                    activitiesInput,
                    InstitutionalMandate,
                    ProjectManagement,
                    Monitoring,
                    Risk,
                    ProjectSustainability,
                    ProjectStakeholders,
                    ProjectReadiness
                );

                string redirect = conceiptAnalysis.Project_No;
                return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SubmitDesignToTeamLead(String DocNo)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                string Responsible_Employee_No = employeeView.No;
                string UserID = employeeView.UserID;
                bool success = Credentials.ObjNav.FnSubmitDesignToTeamLead(
                    DocNo
                 );

                if (success)
                {
                    return Json(new { message = DocNo, success = true }, JsonRequestBehavior.AllowGet);
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
        public JsonResult CancelDesignSubmit(String DocNo)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                string Responsible_Employee_No = employeeView.No;
                string UserID = employeeView.UserID;
                bool success = Credentials.ObjNav.FnSCancelDesignApprovalRequest(
                    DocNo
                 );

                if (success)
                {
                    return Json(new { message = DocNo, success = true }, JsonRequestBehavior.AllowGet);
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
        public JsonResult ApproveDesign(String DocNo)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                string Responsible_Employee_No = employeeView.No;
                string UserID = employeeView.UserID;
                bool success = false;
                success = Credentials.ObjNav.FnApproveDesign(DocNo);

                if (success)
                {
                    return Json(new { message = DocNo, success = true }, JsonRequestBehavior.AllowGet);
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

        public JsonResult SendToCommittee(String DocNo)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                string Responsible_Employee_No = employeeView.No;
                string UserID = employeeView.UserID;
                bool success = false;
                success = Credentials.ObjNav.FnSendToCommittee(DocNo);

                if (success)
                {
                    return Json(new { message = DocNo, success = true }, JsonRequestBehavior.AllowGet);
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


        //Annex 7
        public ActionResult AnnexSevenList()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();
            }
        }
        public PartialViewResult AnnexSevenListPartialView()
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();

            List<PreliminaryDesignControl> design = new List<PreliminaryDesignControl>();

            string page = $"DesignControlCard?$filter=Document_Type eq 'Design' and Final_Design_Stage eq 'Ännex 7'&$orderby=No desc&$&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    PreliminaryDesignControl designControl = new PreliminaryDesignControl
                    {
                        No = (string)config["No"],
                        Name = (string)config["Name"],
                        Type = (string)config["Type"],
                        Start_Date = (string)config["Start_Date"],
                        End_Date = (string)config["End_Date"],
                        Committee = (string)config["Committee"],
                        Global_Dimension_2_Code = (string)config["Global_Dimension_2_Code"],
                        Geographic_Location_Name = (string)config["Geographic_Location_Name"],
                        Total_Estimated_Cost = (int)config["Total_Estimated_Cost"],
                        Project_Classifications = (string)config["Project_Classifications"],
                        Created_By = (string)config["Created_By"],
                        Employee_No = (string)config["Employee_No"],
                        Employee_Name = (string)config["Employee_Name"],
                        Rejection_Comments = (string)config["Rejection_Comments"],
                        Deferal_Comments = (string)config["Deferal_Comments"],
                        Design_Stage = (string)config["Design_Stage"],
                        Status = (string)config["Status"],
                        Proposed_Project = (string)config["Proposed_Project"],
                        Proposed_Project_Name = (string)config["Proposed_Project_Name"],
                        Approved_Proposal = (string)config["Approved_Proposal"],
                        Proposal_Name = (string)config["Proposal_Name"],
                        Team_Leader = (string)config["Team_Leader"],
                        Design_Status = (string)config["Design_Status"],
                        Document_Type = (string)config["Document_Type"],
                        Design_Control_Type = (string)config["Design_Control_Type"],
                        Current_Actioning_Member = (string)config["Current_Actioning_Member"],
                        Design_Approval_Stage = (string)config["Design_Approval_Stage"],
                        Situation_Analysis = (string)config["Situation_Analysis"],
                        Relevance_of_proj_Idea = (string)config["Relevance_of_proj_Idea"],
                        Scope_of_the_project = (string)config["Scope_of_the_project"],
                        Logical_Framework = (string)config["Logical_Framework"],
                        Goal = (string)config["Goal"],
                        Project_Objectives_Outcome = (string)config["Project_Objectives_Outcome"],
                        Proposed_Project_Outputs = (string)config["Proposed_Project_Outputs"],
                        Project_Activities_and_Inputs = (string)config["Project_Activities_and_Inputs"],
                        institutional_Mandate = (string)config["institutional_Mandate"],
                        Management_of_the_Project = (string)config["Management_of_the_Project"],
                        Monitoring_and_Evaluation = (string)config["Monitoring_and_Evaluation"],
                        Risk_and_Mitigation_Measures = (string)config["Risk_and_Mitigation_Measures"],
                        Project_Sustainability = (string)config["Project_Sustainability"],
                        Project_Stakeholders_and_Collaborators = (string)config["Project_Stakeholders_and_Collaborators"],
                        Project_Readiness = (string)config["Project_Readiness"]

                    };
                    design.Add(designControl);
                }
            }
            return PartialView(design);
        }
        public ActionResult AnnexSevenDocumentView(string No)
        {

            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                string staffName = employeeView.FirstName + " " + employeeView.LastName;
                string StaffNo = Session["Username"].ToString();

                List<PreliminaryDesignControl> designControls = new List<PreliminaryDesignControl>();
                string page = $"DesignControlCard?$filter=No eq '{No}'&$format=json";
                HttpWebResponse httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    var config = details["value"].FirstOrDefault();
                    if (config != null)
                    {
                        PreliminaryDesignControl design = new PreliminaryDesignControl
                        {
                            No = (string)config["No"],
                            Project_Code = (string)config["project_Code"],
                            Name = (string)config["Name"],
                            Type = (string)config["Type"],
                            Start_Date = (string)config["Start_Date"],
                            End_Date = (string)config["End_Date"],
                            Committee = (string)config["Committee"],
                            Global_Dimension_2_Code = (string)config["Global_Dimension_2_Code"],
                            Geographic_Location_Name = (string)config["Geographic_Location_Name"],
                            Total_Estimated_Cost = (int)config["Total_Estimated_Cost"],
                            Project_Classifications = (string)config["Project_Classifications"],
                            Created_By = (string)config["Created_By"],
                            Employee_No = (string)config["Employee_No"],
                            Employee_Name = (string)config["Employee_Name"],
                            Rejection_Comments = (string)config["Rejection_Comments"],
                            Deferal_Comments = (string)config["Deferal_Comments"],
                            Design_Stage = (string)config["Design_Stage"],
                            Status = (string)config["Status"],
                            Proposed_Project = (string)config["Proposed_Project"],
                            Proposed_Project_Name = (string)config["Proposed_Project_Name"],
                            Approved_Proposal = (string)config["Approved_Proposal"],
                            Proposal_Name = (string)config["Proposal_Name"],
                            Team_Leader = (string)config["Team_Leader"],
                            Design_Status = (string)config["Design_Status"],
                            Document_Type = (string)config["Document_Type"],
                            Design_Control_Type = (string)config["Design_Control_Type"],
                            Current_Actioning_Member = (string)config["Current_Actioning_Member"],
                            Design_Approval_Stage = (string)config["Design_Approval_Stage"],
                            Situation_Analysis = (string)config["Situation_Analysis"],
                            Relevance_of_proj_Idea = (string)config["Relevance_of_proj_Idea"],
                            Scope_of_the_project = (string)config["Scope_of_the_project"],
                            Logical_Framework = (string)config["Logical_Framework"],
                            Goal = (string)config["Goal"],
                            Project_Objectives_Outcome = (string)config["Project_Objectives_Outcome"],
                            Proposed_Project_Outputs = (string)config["Proposed_Project_Outputs"],
                            Project_Activities_and_Inputs = (string)config["Project_Activities_and_Inputs"],
                            institutional_Mandate = (string)config["institutional_Mandate"],
                            Management_of_the_Project = (string)config["Management_of_the_Project"],
                            Monitoring_and_Evaluation = (string)config["Monitoring_and_Evaluation"],
                            Risk_and_Mitigation_Measures = (string)config["Risk_and_Mitigation_Measures"],
                            Project_Sustainability = (string)config["Project_Sustainability"],
                            Project_Stakeholders_and_Collaborators = (string)config["Project_Stakeholders_and_Collaborators"],
                            Project_Readiness = (string)config["Project_Readiness"]

                        };
                        designControls.Add(design);
                    }
                }
                return PartialView(designControls.FirstOrDefault());

            }
            catch (WebException ex)
            {

                Error erroMsg = new Error
                {
                    Message = ex.Message
                };
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }
        public JsonResult ApproveProject(string Project_No)
        {
            try
            {
                string staffNo = Session["Username"]?.ToString();
                EmployeeView employee = Session["EmployeeData"] as EmployeeView;
                Credentials.ObjNav.FnApproveproject(Project_No);
                string redirect = Project_No;
                return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult RejectProject(string Project_No)
        {
            try
            {
                string staffNo = Session["Username"]?.ToString();
                EmployeeView employee = Session["EmployeeData"] as EmployeeView;

                string comments = "";
                Credentials.ObjNav.FnRejectproject(Project_No, comments);
                string redirect = Project_No;
                return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult DeferProject(string Project_No)
        {
            try
            {
                string staffNo = Session["Username"]?.ToString();
                EmployeeView employee = Session["EmployeeData"] as EmployeeView;

                string comments = "";
                Credentials.ObjNav.FnDeferproject(Project_No, comments);
                string redirect = Project_No;
                return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }

        }



        //Approved design controls
        public ActionResult ApprovedDesignControlList()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();
            }
        }
        public PartialViewResult ApprovedDesignControlListPartialView()
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();

            List<PreliminaryDesignControl> design = new List<PreliminaryDesignControl>();

            string page = $"DesignControlCard?$filter=Document_Type eq 'Design' and Final_Design_Stage eq 'Approved'&$orderby=No desc&$&$format=json";
            /*string page = $"DesignControlCard?$filter=Document_Type eq 'Design'&$orderby=No desc&$&$format=json";*/

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    PreliminaryDesignControl designControl = new PreliminaryDesignControl
                    {
                        No = (string)config["No"],
                        Name = (string)config["Name"],
                        Type = (string)config["Type"],
                        Start_Date = (string)config["Start_Date"],
                        End_Date = (string)config["End_Date"],
                        Committee = (string)config["Committee"],
                        Global_Dimension_2_Code = (string)config["Global_Dimension_2_Code"],
                        Geographic_Location_Name = (string)config["Geographic_Location_Name"],
                        Total_Estimated_Cost = (int)config["Total_Estimated_Cost"],
                        Project_Classifications = (string)config["Project_Classifications"],
                        Created_By = (string)config["Created_By"],
                        Employee_No = (string)config["Employee_No"],
                        Employee_Name = (string)config["Employee_Name"],
                        Rejection_Comments = (string)config["Rejection_Comments"],
                        Deferal_Comments = (string)config["Deferal_Comments"],
                        Design_Stage = (string)config["Design_Stage"],
                        Status = (string)config["Status"],
                        Proposed_Project = (string)config["Proposed_Project"],
                        Proposed_Project_Name = (string)config["Proposed_Project_Name"],
                        Approved_Proposal = (string)config["Approved_Proposal"],
                        Proposal_Name = (string)config["Proposal_Name"],
                        Team_Leader = (string)config["Team_Leader"],
                        Design_Status = (string)config["Design_Status"],
                        Document_Type = (string)config["Document_Type"],
                        Design_Control_Type = (string)config["Design_Control_Type"],
                        Current_Actioning_Member = (string)config["Current_Actioning_Member"],
                        Design_Approval_Stage = (string)config["Design_Approval_Stage"],
                        Situation_Analysis = (string)config["Situation_Analysis"],
                        Relevance_of_proj_Idea = (string)config["Relevance_of_proj_Idea"],
                        Scope_of_the_project = (string)config["Scope_of_the_project"],
                        Logical_Framework = (string)config["Logical_Framework"],
                        Goal = (string)config["Goal"],
                        Project_Objectives_Outcome = (string)config["Project_Objectives_Outcome"],
                        Proposed_Project_Outputs = (string)config["Proposed_Project_Outputs"],
                        Project_Activities_and_Inputs = (string)config["Project_Activities_and_Inputs"],
                        institutional_Mandate = (string)config["institutional_Mandate"],
                        Management_of_the_Project = (string)config["Management_of_the_Project"],
                        Monitoring_and_Evaluation = (string)config["Monitoring_and_Evaluation"],
                        Risk_and_Mitigation_Measures = (string)config["Risk_and_Mitigation_Measures"],
                        Project_Sustainability = (string)config["Project_Sustainability"],
                        Project_Stakeholders_and_Collaborators = (string)config["Project_Stakeholders_and_Collaborators"],
                        Project_Readiness = (string)config["Project_Readiness"],
                        Final_Design_Created = (bool)config["Final_Design_Created"],

                    };
                    design.Add(designControl);
                }
            }
            return PartialView(design);
        }
        public ActionResult ApprovedDesignControlDocumentView(string No)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                string staffName = employeeView.FirstName + " " + employeeView.LastName;
                string StaffNo = Session["Username"].ToString();

                List<PreliminaryDesignControl> designControls = new List<PreliminaryDesignControl>();
                string page = $"DesignControlCard?$filter=No eq '{No}'&$format=json";
                HttpWebResponse httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    var config = details["value"].FirstOrDefault();
                    if (config != null)
                    {
                        PreliminaryDesignControl design = new PreliminaryDesignControl
                        {
                            No = (string)config["No"],
                            Project_Code = (string)config["project_Code"],
                            Name = (string)config["Name"],
                            Type = (string)config["Type"],
                            Start_Date = (string)config["Start_Date"],
                            End_Date = (string)config["End_Date"],
                            Committee = (string)config["Committee"],
                            Global_Dimension_2_Code = (string)config["Global_Dimension_2_Code"],
                            Geographic_Location_Name = (string)config["Geographic_Location_Name"],
                            Total_Estimated_Cost = (int)config["Total_Estimated_Cost"],
                            Project_Classifications = (string)config["Project_Classifications"],
                            Created_By = (string)config["Created_By"],
                            Employee_No = (string)config["Employee_No"],
                            Employee_Name = (string)config["Employee_Name"],
                            Rejection_Comments = (string)config["Rejection_Comments"],
                            Deferal_Comments = (string)config["Deferal_Comments"],
                            Design_Stage = (string)config["Design_Stage"],
                            Status = (string)config["Status"],
                            Proposed_Project = (string)config["Proposed_Project"],
                            Proposed_Project_Name = (string)config["Proposed_Project_Name"],
                            Approved_Proposal = (string)config["Approved_Proposal"],
                            Proposal_Name = (string)config["Proposal_Name"],
                            Team_Leader = (string)config["Team_Leader"],
                            Design_Status = (string)config["Design_Status"],
                            Document_Type = (string)config["Document_Type"],
                            Design_Control_Type = (string)config["Design_Control_Type"],
                            Current_Actioning_Member = (string)config["Current_Actioning_Member"],
                            Design_Approval_Stage = (string)config["Design_Approval_Stage"],
                            Situation_Analysis = (string)config["Situation_Analysis"],
                            Relevance_of_proj_Idea = (string)config["Relevance_of_proj_Idea"],
                            Scope_of_the_project = (string)config["Scope_of_the_project"],
                            Logical_Framework = (string)config["Logical_Framework"],
                            Goal = (string)config["Goal"],
                            Project_Objectives_Outcome = (string)config["Project_Objectives_Outcome"],
                            Proposed_Project_Outputs = (string)config["Proposed_Project_Outputs"],
                            Project_Activities_and_Inputs = (string)config["Project_Activities_and_Inputs"],
                            institutional_Mandate = (string)config["institutional_Mandate"],
                            Management_of_the_Project = (string)config["Management_of_the_Project"],
                            Monitoring_and_Evaluation = (string)config["Monitoring_and_Evaluation"],
                            Risk_and_Mitigation_Measures = (string)config["Risk_and_Mitigation_Measures"],
                            Project_Sustainability = (string)config["Project_Sustainability"],
                            Project_Stakeholders_and_Collaborators = (string)config["Project_Stakeholders_and_Collaborators"],
                            Project_Readiness = (string)config["Project_Readiness"],
                            Final_Design_Created = (bool)config["Final_Design_Created"],

                        };
                        designControls.Add(design);
                    }
                }
                return PartialView(designControls.FirstOrDefault());
            }
            catch (WebException ex)
            {

                Error erroMsg = new Error
                {
                    Message = ex.Message
                };
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }

        public JsonResult CreateFinalDesign(string Project_No)
        {
            try
            {
                string staffNo = Session["Username"]?.ToString();
                EmployeeView employee = Session["EmployeeData"] as EmployeeView;
                Credentials.ObjNav.FnCopyPreliminaryControlToFinal(Project_No);
                string redirect = Project_No;
                return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }

        }




        //Submitted design controls
        public ActionResult SubmittedDesignControlList()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();
            }
        }
        public PartialViewResult SubmittedDesignControlListPartialView()
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();

            List<PreliminaryDesignControl> design = new List<PreliminaryDesignControl>();

            string page = $"DesignControlCard?$filter=Document_Type eq 'Design' and Design_Control_Type eq 'Final' and Design_Status eq 'Submitted'&$orderby=No desc&$&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    PreliminaryDesignControl designControl = new PreliminaryDesignControl
                    {
                        No = (string)config["No"],
                        Name = (string)config["Name"],
                        Type = (string)config["Type"],
                        Start_Date = (string)config["Start_Date"],
                        End_Date = (string)config["End_Date"],
                        Committee = (string)config["Committee"],
                        Global_Dimension_2_Code = (string)config["Global_Dimension_2_Code"],
                        Geographic_Location_Name = (string)config["Geographic_Location_Name"],
                        Total_Estimated_Cost = (int)config["Total_Estimated_Cost"],
                        Project_Classifications = (string)config["Project_Classifications"],
                        Created_By = (string)config["Created_By"],
                        Employee_No = (string)config["Employee_No"],
                        Employee_Name = (string)config["Employee_Name"],
                        Rejection_Comments = (string)config["Rejection_Comments"],
                        Deferal_Comments = (string)config["Deferal_Comments"],
                        Design_Stage = (string)config["Design_Stage"],
                        Status = (string)config["Status"],
                        Proposed_Project = (string)config["Proposed_Project"],
                        Proposed_Project_Name = (string)config["Proposed_Project_Name"],
                        Approved_Proposal = (string)config["Approved_Proposal"],
                        Proposal_Name = (string)config["Proposal_Name"],
                        Team_Leader = (string)config["Team_Leader"],
                        Design_Status = (string)config["Design_Status"],
                        Document_Type = (string)config["Document_Type"],
                        Design_Control_Type = (string)config["Design_Control_Type"],
                        Current_Actioning_Member = (string)config["Current_Actioning_Member"],
                        Design_Approval_Stage = (string)config["Design_Approval_Stage"],
                        Situation_Analysis = (string)config["Situation_Analysis"],
                        Relevance_of_proj_Idea = (string)config["Relevance_of_proj_Idea"],
                        Scope_of_the_project = (string)config["Scope_of_the_project"],
                        Logical_Framework = (string)config["Logical_Framework"],
                        Goal = (string)config["Goal"],
                        Project_Objectives_Outcome = (string)config["Project_Objectives_Outcome"],
                        Proposed_Project_Outputs = (string)config["Proposed_Project_Outputs"],
                        Project_Activities_and_Inputs = (string)config["Project_Activities_and_Inputs"],
                        institutional_Mandate = (string)config["institutional_Mandate"],
                        Management_of_the_Project = (string)config["Management_of_the_Project"],
                        Monitoring_and_Evaluation = (string)config["Monitoring_and_Evaluation"],
                        Risk_and_Mitigation_Measures = (string)config["Risk_and_Mitigation_Measures"],
                        Project_Sustainability = (string)config["Project_Sustainability"],
                        Project_Stakeholders_and_Collaborators = (string)config["Project_Stakeholders_and_Collaborators"],
                        Project_Readiness = (string)config["Project_Readiness"]

                    };
                    design.Add(designControl);
                }
            }
            return PartialView(design);
        }
        public ActionResult SubmittedDesignControlDocumentView(string No)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                string staffName = employeeView.FirstName + " " + employeeView.LastName;
                string StaffNo = Session["Username"].ToString();

                List<PreliminaryDesignControl> designControls = new List<PreliminaryDesignControl>();
                string page = $"DesignControlCard?$filter=No eq '{No}'&$format=json";
                HttpWebResponse httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    var config = details["value"].FirstOrDefault();
                    if (config != null)
                    {
                        PreliminaryDesignControl design = new PreliminaryDesignControl
                        {
                            No = (string)config["No"],
                            Project_Code = (string)config["project_Code"],
                            Name = (string)config["Name"],
                            Type = (string)config["Type"],
                            Start_Date = (string)config["Start_Date"],
                            End_Date = (string)config["End_Date"],
                            Committee = (string)config["Committee"],
                            Global_Dimension_2_Code = (string)config["Global_Dimension_2_Code"],
                            Geographic_Location_Name = (string)config["Geographic_Location_Name"],
                            Total_Estimated_Cost = (int)config["Total_Estimated_Cost"],
                            Project_Classifications = (string)config["Project_Classifications"],
                            Created_By = (string)config["Created_By"],
                            Employee_No = (string)config["Employee_No"],
                            Employee_Name = (string)config["Employee_Name"],
                            Rejection_Comments = (string)config["Rejection_Comments"],
                            Deferal_Comments = (string)config["Deferal_Comments"],
                            Design_Stage = (string)config["Design_Stage"],
                            Status = (string)config["Status"],
                            Proposed_Project = (string)config["Proposed_Project"],
                            Proposed_Project_Name = (string)config["Proposed_Project_Name"],
                            Approved_Proposal = (string)config["Approved_Proposal"],
                            Proposal_Name = (string)config["Proposal_Name"],
                            Team_Leader = (string)config["Team_Leader"],
                            Design_Status = (string)config["Design_Status"],
                            Document_Type = (string)config["Document_Type"],
                            Design_Control_Type = (string)config["Design_Control_Type"],
                            Current_Actioning_Member = (string)config["Current_Actioning_Member"],
                            Design_Approval_Stage = (string)config["Design_Approval_Stage"],
                            Situation_Analysis = (string)config["Situation_Analysis"],
                            Relevance_of_proj_Idea = (string)config["Relevance_of_proj_Idea"],
                            Scope_of_the_project = (string)config["Scope_of_the_project"],
                            Logical_Framework = (string)config["Logical_Framework"],
                            Goal = (string)config["Goal"],
                            Project_Objectives_Outcome = (string)config["Project_Objectives_Outcome"],
                            Proposed_Project_Outputs = (string)config["Proposed_Project_Outputs"],
                            Project_Activities_and_Inputs = (string)config["Project_Activities_and_Inputs"],
                            institutional_Mandate = (string)config["institutional_Mandate"],
                            Management_of_the_Project = (string)config["Management_of_the_Project"],
                            Monitoring_and_Evaluation = (string)config["Monitoring_and_Evaluation"],
                            Risk_and_Mitigation_Measures = (string)config["Risk_and_Mitigation_Measures"],
                            Project_Sustainability = (string)config["Project_Sustainability"],
                            Project_Stakeholders_and_Collaborators = (string)config["Project_Stakeholders_and_Collaborators"],
                            Project_Readiness = (string)config["Project_Readiness"]

                        };
                        designControls.Add(design);
                    }
                }
                return PartialView(designControls.FirstOrDefault());

            }
            catch (WebException ex)
            {

                Error erroMsg = new Error
                {
                    Message = ex.Message
                };
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }



        //Final design controls
        public ActionResult FinalDesignControlList()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();
            }
        }
        public PartialViewResult FinalDesignControlListPartialView()
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();

            List<PreliminaryDesignControl> design = new List<PreliminaryDesignControl>();

            string page = $"DesignControlCard?$filter=Document_Type eq 'Design' and Design_Control_Type eq 'Final'&$orderby=No desc&$&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    PreliminaryDesignControl designControl = new PreliminaryDesignControl
                    {
                        No = (string)config["No"],
                        Project_Code = (string)config["Project_Code"],
                        Name = (string)config["Name"],
                        Type = (string)config["Type"],
                        Start_Date = (string)config["Start_Date"],
                        End_Date = (string)config["End_Date"],
                        Committee = (string)config["Committee"],
                        Global_Dimension_2_Code = (string)config["Global_Dimension_2_Code"],
                        Geographic_Location_Name = (string)config["Geographic_Location_Name"],
                        Total_Estimated_Cost = (int)config["Total_Estimated_Cost"],
                        Project_Classifications = (string)config["Project_Classifications"],
                        Created_By = (string)config["Created_By"],
                        Employee_No = (string)config["Employee_No"],
                        Employee_Name = (string)config["Employee_Name"],
                        Rejection_Comments = (string)config["Rejection_Comments"],
                        Deferal_Comments = (string)config["Deferal_Comments"],
                        Design_Stage = (string)config["Design_Stage"],
                        Status = (string)config["Status"],
                        Proposed_Project = (string)config["Proposed_Project"],
                        Proposed_Project_Name = (string)config["Proposed_Project_Name"],
                        Approved_Proposal = (string)config["Approved_Proposal"],
                        Proposal_Name = (string)config["Proposal_Name"],
                        Team_Leader = (string)config["Team_Leader"],
                        Design_Status = (string)config["Design_Status"],
                        Document_Type = (string)config["Document_Type"],
                        Design_Control_Type = (string)config["Design_Control_Type"],
                        Current_Actioning_Member = (string)config["Current_Actioning_Member"],
                        Design_Approval_Stage = (string)config["Design_Approval_Stage"],
                        Situation_Analysis = (string)config["Situation_Analysis"],
                        Relevance_of_proj_Idea = (string)config["Relevance_of_proj_Idea"],
                        Scope_of_the_project = (string)config["Scope_of_the_project"],
                        Logical_Framework = (string)config["Logical_Framework"],
                        Goal = (string)config["Goal"],
                        Project_Objectives_Outcome = (string)config["Project_Objectives_Outcome"],
                        Proposed_Project_Outputs = (string)config["Proposed_Project_Outputs"],
                        Project_Activities_and_Inputs = (string)config["Project_Activities_and_Inputs"],
                        institutional_Mandate = (string)config["institutional_Mandate"],
                        Management_of_the_Project = (string)config["Management_of_the_Project"],
                        Monitoring_and_Evaluation = (string)config["Monitoring_and_Evaluation"],
                        Risk_and_Mitigation_Measures = (string)config["Risk_and_Mitigation_Measures"],
                        Project_Sustainability = (string)config["Project_Sustainability"],
                        Project_Stakeholders_and_Collaborators = (string)config["Project_Stakeholders_and_Collaborators"],
                        Project_Readiness = (string)config["Project_Readiness"]

                    };
                    design.Add(designControl);
                }
            }
            return PartialView(design);
        }
        public ActionResult FinalDesignControlDocumentView(string No)
        {

            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                string staffName = employeeView.FirstName + " " + employeeView.LastName;
                string StaffNo = Session["Username"].ToString();

                List<PreliminaryDesignControl> designControls = new List<PreliminaryDesignControl>();
                string page = $"DesignControlCard?$filter=No eq '{No}'&$format=json";
                HttpWebResponse httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    var config = details["value"].FirstOrDefault();
                    if (config != null)
                    {
                        PreliminaryDesignControl design = new PreliminaryDesignControl
                        {
                            No = (string)config["No"],
                            Project_Code = (string)config["project_Code"],
                            Name = (string)config["Name"],
                            Type = (string)config["Type"],
                            Start_Date = (string)config["Start_Date"],
                            End_Date = (string)config["End_Date"],
                            Committee = (string)config["Committee"],
                            Global_Dimension_2_Code = (string)config["Global_Dimension_2_Code"],
                            Geographic_Location_Name = (string)config["Geographic_Location_Name"],
                            Total_Estimated_Cost = (int)config["Total_Estimated_Cost"],
                            Project_Classifications = (string)config["Project_Classifications"],
                            Created_By = (string)config["Created_By"],
                            Employee_No = (string)config["Employee_No"],
                            Employee_Name = (string)config["Employee_Name"],
                            Rejection_Comments = (string)config["Rejection_Comments"],
                            Deferal_Comments = (string)config["Deferal_Comments"],
                            Design_Stage = (string)config["Design_Stage"],
                            Status = (string)config["Status"],
                            Proposed_Project = (string)config["Proposed_Project"],
                            Proposed_Project_Name = (string)config["Proposed_Project_Name"],
                            Approved_Proposal = (string)config["Approved_Proposal"],
                            Proposal_Name = (string)config["Proposal_Name"],
                            Team_Leader = (string)config["Team_Leader"],
                            Design_Status = (string)config["Design_Status"],
                            Document_Type = (string)config["Document_Type"],
                            Design_Control_Type = (string)config["Design_Control_Type"],
                            Current_Actioning_Member = (string)config["Current_Actioning_Member"],
                            Design_Approval_Stage = (string)config["Design_Approval_Stage"],
                            Situation_Analysis = (string)config["Situation_Analysis"],
                            Relevance_of_proj_Idea = (string)config["Relevance_of_proj_Idea"],
                            Scope_of_the_project = (string)config["Scope_of_the_project"],
                            Logical_Framework = (string)config["Logical_Framework"],
                            Goal = (string)config["Goal"],
                            Project_Objectives_Outcome = (string)config["Project_Objectives_Outcome"],
                            Proposed_Project_Outputs = (string)config["Proposed_Project_Outputs"],
                            Project_Activities_and_Inputs = (string)config["Project_Activities_and_Inputs"],
                            institutional_Mandate = (string)config["institutional_Mandate"],
                            Management_of_the_Project = (string)config["Management_of_the_Project"],
                            Monitoring_and_Evaluation = (string)config["Monitoring_and_Evaluation"],
                            Risk_and_Mitigation_Measures = (string)config["Risk_and_Mitigation_Measures"],
                            Project_Sustainability = (string)config["Project_Sustainability"],
                            Project_Stakeholders_and_Collaborators = (string)config["Project_Stakeholders_and_Collaborators"],
                            Project_Readiness = (string)config["Project_Readiness"]

                        };
                        designControls.Add(design);
                    }
                }
                return PartialView(designControls.FirstOrDefault());

            }
            catch (WebException ex)
            {

                Error erroMsg = new Error
                {
                    Message = ex.Message
                };
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }

        public JsonResult SendDesignForApproval(string Project_No)
        {
            try
            {
                string staffNo = Session["Username"]?.ToString();
                EmployeeView employee = Session["EmployeeData"] as EmployeeView;
                /* Credentials.ObjNav.FnCreatePreliminaryDesignPortal(No);*/
                string redirect = Project_No;
                return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult SubmitFinalDesign(string Project_No)
        {
            try
            {
                string staffNo = Session["Username"]?.ToString();
                EmployeeView employee = Session["EmployeeData"] as EmployeeView;
                /* Credentials.ObjNav.FnCreatePreliminaryDesignPortal(No);*/
                string redirect = Project_No;
                return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }

        }

    }
}