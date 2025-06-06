using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Latest_Staff_Portal.Models;
using Latest_Staff_Portal.ViewModel;
using Newtonsoft.Json.Linq;

namespace Latest_Staff_Portal.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            Session.Remove("Username");
            Session.Remove("StaffDetails");
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            var user = new Authedication();
            return View(user);
        }

        [HttpPost]
        public JsonResult LoginUser(Authedication userlogin)
        {
            var msg = "";
            var success = false;
            var UserName = userlogin.UserName.ToUpper();
            var passwrd = userlogin.Password;
            var userID = "";
            var staffNo = "";
            var Redirect = "";


            try
            {
                using var pc = new PrincipalContext(ContextType.Domain, ConfigurationManager.AppSettings["AD_Server"]);
                // validate the credentials
                var isValid = pc.ValidateCredentials(UserName, passwrd) ||
                              ConfigurationManager.AppSettings["IS_PROD"] == "NON-PROD";
                if (isValid)
                {
                    if (UserName.Contains("\\"))
                        userID = UserName;
                    else
                        userID = ConfigurationManager.AppSettings["DOMAIN"] + @"\" + UserName;
                    Session["LoginId"] = userID;
                    Session["LoginUseName"] = UserName;
                    if (ConfigurationManager.AppSettings["IS_PROD"] == "NON-PROD")
                    {
                        Redirect = "/Dashboard/Dashboard";
                        msg = Redirect;
                        try
                        {
                            Redirect = "/Dashboard/Dashboard";
                            var page = "UserSetup?$filter=User_ID eq '" + userID + "'&format=json";

                            var httpResponse = Credentials.GetOdataData(page);
                            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
                            var result = streamReader.ReadToEnd();

                            var details = JObject.Parse(result);

                            if (details["value"].Any())
                            {
                                foreach (var jToken in details["value"])
                                {
                                    var config = (JObject)jToken;
                                    if ((string)config["Employee_No"] != "")
                                    {
                                        userID = (string)config["User_ID"];
                                        staffNo = (string)config["Employee_No"];

                                        var Role = "";
                                        Session["Username"] = (string)config["Employee_No"];
                                        Session["UserID"] = (string)config["User_ID"];
                                        var IDno = (string)config["IDNumber"];
                                        var Email = (string)config["EMail"];
                                        var PhoneNo = (string)config["CellPhoneNumber"];
                                        ESSRoleSetup(userID, UserName, Email);
                                        EmployeeData(staffNo);
                                        if (Credentials.ObjNav.CheckPasswordChanged(
                                                (string)config["Employee_No"]))
                                            Redirect = "/Settings/ChangePassword";

                                        var roleSetup = Session["ESSRoleSetup"] as ESSRoleSetup;

                                        //Role = "ALLUSERS";
                                        //SetUserAuthedication(UserName, Email, Role);


                                        msg = Redirect;
                                        success = true;
                                    }
                                    else
                                    {
                                        msg =
                                            "No Employee Number assigned to the applied username. Contact HR / ICT";
                                        success = false;
                                    }
                                }
                            }
                            else
                            {
                                msg = "No Employee Number assigned to the applied username. Contact HR / ICT";
                                success = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            msg = ex.Message;
                            success = false;
                        }
                    }
                    else
                    {
                        Redirect = "/Settings/RequestOtp";
                        msg = Redirect;
                        success = true;
                        var page = "UserSetup?$filter=User_ID eq '" + userID + "'&format=json";

                        var httpResponse = Credentials.GetOdataData(page);
                        using var streamReader = new StreamReader(httpResponse.GetResponseStream());
                        var result = streamReader.ReadToEnd();

                        var details = JObject.Parse(result);

                        if (details["value"].Any())
                        {
                            foreach (var jToken in details["value"])
                            {
                                var config = (JObject)jToken;
                                if ((string)config["Employee_No"] != "")
                                {
                                    if (ConfigurationManager.AppSettings["IS_PROD"] == "NON-PROD")
                                    {
                                        Session["Username"] = (string)config["Employee_No"];
                                        Session["UserID"] = (string)config["User_ID"];
                                    }

                                    userID = (string)config["User_ID"];
                                    staffNo = (string)config["Employee_No"];
                                    var Role = "";
                                    var IDno = (string)config["IDNumber"];
                                    var Email = "";
                                    var PhoneNo = "";


                                    var pageEmp = "EmployeeList?$filter=No eq '" + staffNo + "'&$format=json";

                                    var httpResponseEmp = Credentials.GetOdataData(pageEmp);
                                    using (var streamReaderEmp =
                                           new StreamReader(httpResponseEmp.GetResponseStream()))
                                    {
                                        var resultEmp = streamReaderEmp.ReadToEnd();

                                        var det = JObject.Parse(resultEmp);

                                        foreach (var data in det["value"])
                                        {
                                            Email = (string)data["E_Mail"];
                                            PhoneNo = (string)data["Phone_No"];
                                        }
                                    }

                                    Session["Phone"] = PhoneNo;
                                    Session["Email"] = Email;
                                    Session["EmpNo"] = staffNo;
                                    msg = Redirect;
                                    success = true;
                                }
                                else
                                {
                                    msg =
                                        "No Employee Number assigned to the applied username. Contact HR / ICT";
                                    success = false;
                                }
                            }
                        }
                        else
                        {
                            msg = "No Employee Number assigned to the applied username. Contact HR / ICT";
                            success = false;
                        }
                    }
                }
                else
                {
                    msg = "Invalid username or password!";
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                success = false;
            }

            return Json(new { message = msg, success }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ConfirmOtp(string otp)
        {
            try
            {
                var msg = "";
                var success = false;
                var staffNo = "";
                var Redirect = "";

                var employee = Session["EmployeeData"] as EmployeeView;
                var upperOtp = otp.ToUpper();
                var confirmOtp = Credentials.ObjNav.ReturnPortalOTPCode(Session["EmpNo"].ToString(), upperOtp);

                if (confirmOtp)
                {
                    try
                    {
                        var userID = Session["LoginId"].ToString();
                        var UserName = Session["LoginUseName"].ToString();
                        Redirect = "/Dashboard/Dashboard";


                        var page = "UserSetup?$filter=User_ID eq '" + userID + "'&format=json";

                        var httpResponse = Credentials.GetOdataData(page);
                        using var streamReader = new StreamReader(httpResponse.GetResponseStream());
                        var result = streamReader.ReadToEnd();

                        var details = JObject.Parse(result);

                        if (details["value"].Any())
                        {
                            foreach (var jToken in details["value"])
                            {
                                var config = (JObject)jToken;
                                if ((string)config["Employee_No"] != "")
                                {
                                    userID = (string)config["User_ID"];
                                    staffNo = (string)config["Employee_No"];

                                    var Role = "";
                                    Session["Username"] = (string)config["Employee_No"];
                                    Session["UserID"] = (string)config["User_ID"];
                                    var IDno = (string)config["IDNumber"];
                                    var Email = (string)config["EMail"];
                                    var PhoneNo = (string)config["CellPhoneNumber"];

                                    ESSRoleSetup(userID, UserName, Email);
                                    EmployeeData(staffNo);
                                    if (Credentials.ObjNav.CheckPasswordChanged((string)config["Employee_No"]))
                                        Redirect = "/Settings/ChangePassword";


                                    msg = Redirect;
                                    success = true;
                                }
                                else
                                {
                                    msg = "No Employee Number assigned to the applied username. Contact HR / ICT";
                                    success = false;
                                }
                            }
                        }
                        else
                        {
                            msg = "No Employee Number assigned to the applied username. Contact HR / ICT";
                            success = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                        success = false;
                    }

                    return Json(new { message = msg, success = true }, JsonRequestBehavior.AllowGet);
                }

                msg = "Incorrect OTP. Please try again!!!";
                return Json(new { message = msg, success = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        private EmployeeView EmployeeData(string staffNo)
        {
            try
            {
                var empView = new EmployeeView();
                var totAssets = 0;
                var userRole = "";
                var page = "EmployeeList?$filter=No eq '" + staffNo + "'&$format=json";
                bool isSupervisor = false;

                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        empView.No = (string)config["No"];
                        empView.Title = (string)config["Title"];
                        empView.FirstName = (string)config["First_Name"];
                        empView.MiddleName = (string)config["Middle_Name"];
                        empView.LastName = (string)config["Last_Name"];
                        empView.FullName = (string)config["First_Name"] + " " + (string)config["Middle_Name"] + " " +(string)config["Last_Name"];
                        empView.RecordType = (string)config["Record_Type"];
                        empView.CurrentPositionID = (string)config["Current_Position_ID"];
                        empView.JobTitle2 = (string)config["Job_Title2"];
                        empView.SearchName = (string)config["Search_Name"];
                        empView.CountyofOrigin = (string)config["County_of_Origin"];
                        empView.CountyofOriginName = (string)config["County_of_Origin_Name"];
                        empView.CountyofResidence = (string)config["County_of_Residence"];
                        empView.CountyofResidenceName = (string)config["County_of_Residence_Name"];
                        empView.Gender = (string)config["Gender"];
                        empView.MaritalStatus = (string)config["Marital_Status"];
                        empView.Religion = (string)config["Religion"];
                        empView.Disabled = (bool)config["Disabled"];
                        empView.DisabilityNo = (string)config["Disability_No"];
                        empView.DisabilityCertExpiry = (DateTime)config["Disability_Cert_Expiry"];
                        empView.InsuranceCertificate = (bool)config["Insurance_Certificate"];
                        empView.EthnicOrigin = (string)config["Ethnic_Origin"];
                        empView.LastDateModified = (DateTime)config["Last_Date_Modified"];
                        empView.GlobalDimension1Code = (string)config["Global_Dimension_1_Code"];
                        empView.GlobalDimension2Code = (string)config["Global_Dimension_2_Code"];
                        empView.DepartmentName = (string)config["Department_Name"];
                        empView.ResponsibilityCenter = (string)config["Responsibility_Center"];
                        empView.HeadofStation = (bool)config["Head_of_Station"];
                        Session["HeadofStation"] = (bool)config["Head_of_Station"];
                        empView.HOD = (bool)config["HOD"];
                        empView.IsChiefJustice = (bool)config["Is_Chief_Justice"];
                        empView.IsPartOfDisciplinaryTeam = (bool)config["Is_Part_Of_Disciplinary_Team"];
                        empView.ICTHelpDeskAdmin = (bool)config["ICT_Help_Desk_Admin"];
                        Session["ICT_Help_Desk_Admin"] = (bool)config["ICT_Help_Desk_Admin"];
                        empView.UserID = (string)config["User_ID"];
                        empView.Supervisor = (string)config["Supervisor"];
                        empView.ReliverCode = (string)config["Reliver_Code"];
                        empView.SalaryScale = (string)config["Salary_Scale"];
                        empView.Present = (string)config["Present"];
                        empView.EmployeePostingGroup1 = (string)config["Employee_Posting_Group1"];
                        empView.IncrementDate = (DateTime)config["Increment_Date"];
                        empView.IncrementalMonth = (string)config["Incremental_Month"];
                        empView.LastIncrementDate = (DateTime)config["Last_Increment_Date"];
                        empView.DirectorateCode = (string)config["Directorate_Code"];
                        empView.ImplementingUnitName = (string)config["Implementing_Unit_Name"];
                        empView.DepartmentCode = (string)config["Department_Code"];
                        empView.DutyStation = (string)config["Duty_Station"];
                        empView.OrgUnit = (string)config["Org_Unit"];
                        empView.OrganisationUnitName = (string)config["Organisation_Unit_Name"];
                        empView.AccountType = (string)config["Account_Type"];
                        empView.Division = (string)config["Division"];
                        empView.EmployeeJobType = (string)config["Employee_Job_Type"];
                        empView.CourtLevel = (string)config["Court_Level"];
                        empView.Address = (string)config["Address"];
                        empView.Address2 = (string)config["Address_2"];
                        empView.PostCode = (string)config["Post_Code"];
                        empView.City = (string)config["City"];
                        empView.CountryRegionCode = (string)config["Country_Region_Code"];
                        empView.CitizenshipType = (string)config["Citizenship_Type"];
                        empView.EmployeeType = (string)config["Employee_Type"];
                        empView.PhoneNo = (string)config["Phone_No"];
                        empView.Extension = (string)config["Extension"];
                        empView.MobilePhoneNo = (string)config["Mobile_Phone_No"];
                        empView.Pager = (string)config["Pager"];
                        empView.PhoneNo2 = (string)config["Phone_No_2"];
                        empView.Email = (string)config["E_Mail"];
                        empView.CompanyEmail = (string)config["Company_E_Mail"];
                        empView.AltAddressCode = (string)config["Alt_Address_Code"];
                        empView.AltAddressStartDate = (DateTime)config["Alt_Address_Start_Date"];
                        empView.AltAddressEndDate = (DateTime)config["Alt_Address_End_Date"];
                        empView.WorkPhoneNumber = (string)config["Work_Phone_Number"];
                        empView.Ext = (string)config["Ext"];
                        empView.DateOfBirth = (DateTime)config["Date_Of_Birth"];
                        empView.Age = (string)config["Age"];
                        empView.EmploymentDate = (DateTime)config["Employment_Date"];
                        empView.EndOfProbationDate = (DateTime)config["End_Of_Probation_Date"];
                        empView.Inducted = (bool)config["Inducted"];
                        empView.RetirementDate = (DateTime)config["Retirement_Date"];
                        empView.FullPartTime = (string)config["Full_Part_Time"];
                        empView.ContractStartDate = (DateTime)config["Contract_Start_Date"];
                        empView.ContractEndDate = (DateTime)config["Contract_End_Date"];
                        empView.ReEmploymentDate = (DateTime)config["Re_Employment_Date"];
                        empView.NoticePeriod = (string)config["Notice_Period"];
                        empView.Status = (string)config["Status"];
                        empView.EmployeeStatus2 = (string)config["Employee_Status_2"];
                        empView.InactiveDate = (DateTime)config["Inactive_Date"];
                        empView.CauseofInactivityCode = (string)config["Cause_of_Inactivity_Code"];
                        empView.EmplymtContractCode = (string)config["Emplymt_Contract_Code"];
                        empView.ResourceNo = (string)config["Resource_No"];
                        empView.SalespersPurchCode = (string)config["Salespers_Purch_Code"];
                        empView.Disciplinarystatus = (string)config["Disciplinary_status"];
                        empView.Reasonfortermination = (string)config["Reason_for_termination"];
                        empView.TerminationDate = (DateTime)config["Termination_Date"];
                        empView.DateOfLeaving = (DateTime)config["Date_Of_Leaving"];
                        empView.ExitInterviewConducted = (string)config["Exit_Interview_Conducted"];
                        empView.ExitInterviewDate = (DateTime)config["Exit_Interview_Date"];
                        empView.ExitInterviewDoneby = (string)config["Exit_Interview_Done_by"];
                        empView.BondingAmount = (int)config["Bonding_Amount"];
                        empView.ExitStatus = (string)config["Exit_Status"];
                        empView.AllowReEmploymentInFuture = (bool)config["Allow_Re_Employment_In_Future"];
                        empView.CurrencyCode = (string)config["Currency_Code"];
                        empView.Paystax_x003F_ = (bool)config["Pays_tax_x003F_"];
                        empView.PayWages = (bool)config["Pay_Wages"];
                        empView.PayMode = (string)config["Pay_Mode"];
                        empView.PIN = (string)config["P_I_N"];
                        empView.NHIFNo = (string)config["N_H_I_F_No"];
                        empView.SocialSecurityNo = (string)config["Social_Security_No"];
                        empView.IDNumber = (string)config["ID_Number"];
                        empView.PostingGroup = (string)config["Posting_Group"];
                        empView.ClaimLimit = (int)config["Claim_Limit"];
                        empView.BankAccountNumber = (string)config["Bank_Account_Number"];
                        empView.Employeex0027sBank = (string)config["Employee_x0027_s_Bank"];
                        empView.BankName = (string)config["Bank_Name"];
                        empView.BankBranch = (string)config["Bank_Branch"];
                        empView.BankBranchName = (string)config["Bank_Branch_Name"];
                        empView.Employeex0027sBank2 = (string)config["Employee_x0027_s_Bank_2"];
                        empView.BankName2 = (string)config["Bank_Name_2"];
                        Session["BankName2"] = (string)config["Bank_Name_2"];
                        empView.BankBranch2 = (string)config["Bank_Branch_2"];
                        Session["BankCode2"] = (string)config["Bank_Branch_2"];
                        empView.BankBranchName2 = (string)config["Bank_Branch_Name_2"];
                        Session["BankBranch2"] = (string)config["Bank_Branch_Name_2"];
                        empView.BankAccountNo2 = (string)config["Bank_Account_No_2"];
                        Session["BankAccountNumber2"] = (string)config["Bank_Account_No_2"];
                        empView.AllowNegativeLeave = (bool)config["Allow_Negative_Leave"];
                        empView.OffDays = (int)config["Off_Days"];
                        empView.LeaveDaysBF = (int)config["Leave_Days_B_F"];
                        empView.AllocatedLeaveDays = (int)config["Allocated_Leave_Days"];
                        empView.TotalLeaveDays = (int)config["Total_Leave_Days"];
                        empView.TotalLeaveTaken = (int)config["Total_Leave_Taken"];
                        empView.LeaveOutstandingBal = (int)config["Leave_Outstanding_Bal"];
                        empView.LeaveBalance = (int)config["Leave_Balance"];
                        empView.AcruedLeaveDays = (int)config["Acrued_Leave_Days"];
                        empView.CashperLeaveDay = (int)config["Cash_per_Leave_Day"];
                        empView.CashLeaveEarned = (int)config["Cash_Leave_Earned"];
                        empView.LeaveStatus = (string)config["Leave_Status"];
                        empView.LeaveTypeFilter = (string)config["Leave_Type_Filter"];
                        empView.LeavePeriodFilter = (string)config["Leave_Period_Filter"];
                        empView.OnLeave = (bool)config["On_Leave"];
                        empView.DateFilter = (string)config["Date_Filter"];
                        empView.AdministrativeUnit = (string)config["Global_Dimension_2_Code"];

                    }
                }

                #region Assigned Assets

                var assets = "FixedAssets?$filter=Responsible_Employee eq '" + staffNo + "'&$format=json";

                var assetsHttpResponse = Credentials.GetOdataData(assets);
                using (var streamReader = new StreamReader(assetsHttpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"]) totAssets++;
                }

                #endregion

                //  string no = $"COURT\\{StaffNo}";

                #region Employee Dependants

                var employeeDependants = new List<EmployeeDependant>();
                var employeeDependantsPage =
                    "EmployeeDependants?$filter=Employee_Code eq '" + staffNo + "'&$format=json";

                var httpResponseCampus = Credentials.GetOdataData(employeeDependantsPage);
                using (var streamReader = new StreamReader(httpResponseCampus.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        var dependant = new EmployeeDependant();
                        dependant.EmployeeCode = (string)config["Employee_Code"];
                        dependant.Type = (string)config["Type"];
                        dependant.Relationship = (string)config["Relationship"];
                        dependant.SurName = (string)config["SurName"];
                        dependant.OtherNames = (string)config["OtherNames"];
                        dependant.LineNo = (int)config["Line_No"];
                        dependant.MemberID = (string)config["MemberID"];
                        dependant.Gender = (string)config["Gender"];
                        dependant.DateOfBirth = DateTime.Parse((string)config["DateOfBirth"]);
                        dependant.Category = (string)config["Category"];
                        dependant.CardNo = (string)config["Card_No"];
                        dependant.NumberInTheFamily = (string)config["Number_In_the_Family"];

                        employeeDependants.Add(dependant);
                    }
                }

                #endregion

                #region Employee Dependants

                var employeeQualifications = new List<EmployeeQualification>();
                var employeeQualificationPage =
                    "EmployeeQualification?$filter=Employee_No eq '" + staffNo + "'&$format=json";

                var httpResponseQual = Credentials.GetOdataData(employeeQualificationPage);
                using (var streamReader = new StreamReader(httpResponseQual.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);


                    foreach (JObject config in details["value"])
                    {
                        var qualification = new EmployeeQualification();
                        qualification.EmployeeNo = (string)config["Employee_No"];
                        qualification.LineNo = (int)config["Line_No"];
                        qualification.QualificationCategory = (string)config["Qualification_Category"];
                        qualification.QualificationCode = (string)config["Qualification_Code"];
                        qualification.Description = (string)config["Description"];
                        qualification.InstitutionCompany = (string)config["Institution_Company"];
                        qualification.FromDate = DateTime.Parse((string)config["From_Date"]);
                        qualification.ToDate = DateTime.Parse((string)config["To_Date"]);
                        qualification.Type = (string)config["Type"];
                        qualification.ExpirationDate = DateTime.Parse((string)config["Expiration_Date"]);
                        qualification.Cost = (decimal)config["Cost"];
                        qualification.Year = (string)config["Year"];
                        qualification.Specialization = (string)config["Specialization"];
                        qualification.CourseGrade = (string)config["Course_Grade"];
                        qualification.Comment = (bool)config["Comment"];

                        employeeQualifications.Add(qualification);
                    }
                }

                #endregion

                #region user Role

                var role = "UserSettings?$filter=User_ID eq '" + empView.UserID + "'&$format=json";

                var httpWebResponseRole = Credentials.GetOdataData(role);
                using (var streamReader = new StreamReader(httpWebResponseRole.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"]) userRole = (string)config["Role"];
                }

                #endregion

                int assignedPreq = 0;
                #region assigned Purchase Requisitions
                string pagePreq = "PurchaseRequisitions?$filter=Purchaser_Code2 eq '" + staffNo + "' and Status eq 'Released' and Ordered_PRN eq false &$format=json";

                HttpWebResponse httpResponsePreq = Credentials.GetOdataData(pagePreq);
                using (var streamReader = new StreamReader(httpResponsePreq.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        assignedPreq++;
                    }
                }
                #endregion
                #region Assigned Dimensions
                var assignedDimensions = new List<AssignedDimensions>();

                string pageDim = "EmployeeMapping?$filter=Employee_No eq '" + staffNo + "'&$format=json";

                HttpWebResponse httpResponseDim = Credentials.GetOdataData(pageDim);
                using (var streamReader = new StreamReader(httpResponseDim.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        var assigned = new AssignedDimensions();
                        assigned.GlobalDim1 = (string)config["Global_Dimension_1_Code"];
                        assigned.GlobalDim2 = (string)config["Global_Dimension_2_Code"];
                        assignedDimensions.Add(assigned);
                    }
                }
                #endregion

                empView.AssignedPurchaseReq = assignedPreq;
                empView.EmployeeQualifications = employeeQualifications;
                empView.EmployeeDependants = employeeDependants;
                empView.AllocatedAssets = totAssets;
                empView.UserRole = userRole;
                empView.EmployeeDimensions = assignedDimensions;


                Session["EmployeeData"] = empView;


                return empView;
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return null;
            }
        }


        
        public ESSRoleSetup ESSRoleSetup(string empNo, string UserName, string Email)
        {
            var roleSetupValue = new ESSRoleSetup();
            var Role = "";

            var rolePage = "ESSRoleSetup?$filter=User_ID eq '" + empNo + "'&$format=json";

            var httpResponseSettings = Credentials.GetOdataData(rolePage);
            using (var streamReader = new StreamReader(httpResponseSettings.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);

                foreach (var jToken in details["value"])
                {
                    var config = (JObject)jToken;
                    roleSetupValue.User_ID = (string)config["User_ID"];
                    roleSetupValue.UserName = (string)config["UserName"];
                    roleSetupValue.Employee_Name = (string)config["Employee_Name"];
                    roleSetupValue.Employee_No = (string)config["Employee_No"];
                    roleSetupValue.Accounts_User = (bool)config["Accounts_User"];
                    roleSetupValue.Accounts_Approver = (bool)config["Accounts_Approver"];
                    roleSetupValue.Audit_Officer = (bool)config["Audit_Officer"];
                    roleSetupValue.HQ_Accountant = (bool)config["HQ_Accountant"];
                    roleSetupValue.HQ_Finance_Officer = (bool)config["HQ_Finance_Officer"];
                    roleSetupValue.HQ_Procurement_Officer = (bool)config["HQ_Procurement_Officer"];
                    roleSetupValue.Station_Accountant = (bool)config["Station_Accountant"];
                    roleSetupValue.Station_Procurement_Office = (bool)config["Station_Procurement_Office"];
                    roleSetupValue.DAAS_Officer = (bool)config["DAAS_Officer"];
                    roleSetupValue.HR_Welfare_Officer = (bool)config["HR_Welfare_Officer"];
                    roleSetupValue.Mobility_Officer = (bool)config["Mobility_Officer"];
                    roleSetupValue.Procurement_officer = (bool)config["Procurement_officer"];
                    roleSetupValue.Recruitment_Officer = (bool)config["Recruitment_Officer"];
                    roleSetupValue.Revenue_Officer = (bool)config["Revenue_Officer"];
                    roleSetupValue.Transport_Officer = (bool)config["Transport_Officer"];
                }
            }

            if (roleSetupValue.HQ_Accountant || roleSetupValue.Station_Accountant)
            {
                Role = "ACCOUNTANTS";
            }
            else if (roleSetupValue.HQ_Procurement_Officer || roleSetupValue.Station_Procurement_Office)
            {
                Role = "PROCUREMENT";
            }
            else
            {
                Role = "ALLUSERS";
            }               
            SetUserAuthedication(UserName, Email, Role);
            Session["ESSRoleSetup"] = roleSetupValue;

            return roleSetupValue;
        }

        private void SetUserAuthedication(string UserName, string email, string role)
        {
            try
            {
                var userModel = new UserViewModel();
                userModel.UserName = UserName;
                userModel.Email = email;
                userModel.RoleName = role;
                var userData = string.Format("{0}|{1}|{2}|{3}", userModel.UserName, userModel.UserID, userModel.Email,
                    userModel.RoleName);
                var ticket = new FormsAuthenticationTicket(1, userModel.UserName, DateTime.Now,
                    DateTime.Now.AddMinutes(1), false, userData);
                var encTicket = FormsAuthentication.Encrypt(ticket);

                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                Response.Cookies.Add(cookie);
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            var user = new Authedication();
            return View(user);
        }

        [HttpPost]
        public ActionResult ForgotPassword(Authedication userlogin)
        {
            var msg = "";
            var email = string.Empty;
            var success = false;
            var UserName = userlogin.UserName;
            try
            {
                var userID = UserName;
                

                var page2 = "EmployeeList?$filter=User_ID eq '" + userID + "'&$format=json";
                var page = $"PortalUsers?$filter=Authentication_Email eq '{userID}'&format=json";

                var httpResponse = Credentials.GetOdataData(page);
                using var streamReader = new StreamReader(httpResponse.GetResponseStream());
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);

                if (details["value"].Any())
                {
                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        var User = (string)config["Authentication_Email"];
                        email = (string)config["Authentication_Email"];
                        if (User != "")
                        {
                            if (email != "")
                            {
                                #region generate random password

                                var rand = new Random();
                                var randAlpha = new Random();
                                var newpassint = rand.Next(10000, 99999);

                                var alphabetPosition = randAlpha.Next(1, 26);
                                var isCap = alphabetPosition % 2 == 0 ? true : false;
                                var theAlphabet = GetTheAlphabet(alphabetPosition, isCap);

                                alphabetPosition = randAlpha.Next(1, 26);
                                isCap = alphabetPosition % 2 == 0 ? true : false;
                                theAlphabet += GetTheAlphabet(alphabetPosition, isCap);

                                alphabetPosition = randAlpha.Next(1, 26);
                                isCap = alphabetPosition % 2 == 0 ? true : false;
                                theAlphabet += GetTheAlphabet(alphabetPosition, isCap);

                                alphabetPosition = randAlpha.Next(1, 26);
                                isCap = alphabetPosition % 2 == 0 ? true : false;
                                theAlphabet += GetTheAlphabet(alphabetPosition, isCap);

                                var newpass = theAlphabet + "#" + newpassint + "@" + alphabetPosition;

                                #endregion generate random password

                                var ok = Credentials.ResetPassword(User, newpass);

                                Credentials.ObjNav.PasswordChanged((string)config["Authentication_Email"], true);
                                if (ok == "CHANGED")
                                {
                                    const string subject = "STAFF PORTAL CREDENTIALS";
                                    var emailmsg =
                                        "Staff portal credentials reset:<br />New password is <b />" + newpass +
                                        "" +
                                        "<br />Remember to change your password after you login";
                                    if (CommonClass.SendEmailAlert(emailmsg, email, subject))
                                    {
                                        msg = "A New password has been send to your Email<b>(" + email +
                                              ")</b>. Use it to login. Remember to change your password after you login";
                                        success = true;
                                    }
                                    else
                                    {
                                        msg =
                                            "An error occured while sending you the credentials.Please contact the ICT office administrator.";
                                        success = false;
                                    }
                                }
                                else
                                {
                                    msg = "Failed to reset password";
                                    success = false;
                                }
                            }
                            else
                            {
                                msg = "Warning!, password reset failed!. E-Mail empty. Contact your administrator!";
                                success = false;
                            }
                        }
                        else
                        {
                            msg = "User Name not found. Confirm if the user name is correct";
                            success = false;
                        }
                    }
                }
                else
                {
                    msg = "User Name not found. Confirm if the user name is correct";
                    success = false;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message.Replace("'", "");
                success = false;
            }

            return Json(new { message = msg, success }, JsonRequestBehavior.AllowGet);
        }

        private string GetTheAlphabet(int alphabetPosition, bool isCap)
        {
            var rval = string.Empty;
            switch (alphabetPosition)
            {
                case 1:
                    rval = "A";
                    break;
                case 2:
                    rval = "B";
                    break;
                case 3:
                    rval = "C";
                    break;
                case 4:
                    rval = "D";
                    break;
                case 5:
                    rval = "E";
                    break;
                case 6:
                    rval = "F";
                    break;
                case 7:
                    rval = "G";
                    break;
                case 8:
                    rval = "H";
                    break;
                case 9:
                    rval = "I";
                    break;
                case 10:
                    rval = "J";
                    break;
                case 11:
                    rval = "K";
                    break;
                case 12:
                    rval = "L";
                    break;
                case 13:
                    rval = "M";
                    break;
                case 14:
                    rval = "N";
                    break;
                case 15:
                    rval = "O";
                    break;
                case 16:
                    rval = "P";
                    break;
                case 17:
                    rval = "Q";
                    break;
                case 18:
                    rval = "R";
                    break;
                case 19:
                    rval = "S";
                    break;
                case 20:
                    rval = "T";
                    break;
                case 21:
                    rval = "U";
                    break;
                case 22:
                    rval = "V";
                    break;
                case 23:
                    rval = "W";
                    break;
                case 24:
                    rval = "X";
                    break;
                case 25:
                    rval = "Y";
                    break;
                default:
                    rval = "Z";
                    break;
            }

            return isCap ? rval : rval.ToLower();
        }

    }
}