using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Mvc;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Web.Security;
using System.Web.UI.WebControls;
using Latest_Staff_Portal.Models;
using Latest_Staff_Portal.ViewModel;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using static System.Net.WebRequestMethods;

namespace Contructors_Portal.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }


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
            var UserName = userlogin.UserName; ;
            var passwrd = userlogin.Password;
            var userID = "";
            var staffNo = "";
            var Redirect = "";


            try
            {
                //using var pc = new PrincipalContext(ContextType.Domain, ConfigurationManager.AppSettings["AD_Server"]);
                // validate the credentials
                //var isValid = pc.ValidateCredentials(UserName, passwrd) || ConfigurationManager.AppSettings["IS_PROD"] == "NON-PROD";

                var isValid = true;
                if (isValid)
                {

                    if (ConfigurationManager.AppSettings["IS_PROD"] == "PROD")
                    {
                        Redirect = "/Dashboard/Dashboard";
                        msg = Redirect;
                        try
                        {


                            Redirect = "/Dashboard/Dashboard";
                            var page = $"PortalUsers?$filter=Authentication_Email eq '{UserName}'&format=json";

                            var httpResponse = Credentials.GetOdataData(page);
                            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
                            var result = streamReader.ReadToEnd();

                            var details = JObject.Parse(result);

                            if (details["value"].Any())
                            {
                                foreach (var jToken in details["value"])
                                {
                                    var config = (JObject)jToken;
                                    if ((string)config["Authentication_Email"] != "")
                                    {
                                        userID = (string)config["Authentication_Email"];
                                        var userAuth = AuthenticateUser(UserName, passwrd);


                                        if (userAuth)
                                        {
                                            Session["Username"] = (string)config["Authentication_Email"];
                                            Session["Mobile_Phone_No"] = (string)config["Mobile_Phone_No"];
                                            var IDno = (string)config["Id_Number"];
                                            var Email = (string)config["Authentication_Email"];
                                            var PhoneNo = (string)config["Mobile_Phone_No"];
                                            EmployeeData(UserName);
                                            msg = Redirect;
                                            success = true;
                                        }
                                        else
                                        {
                                            msg =
                                            "Invalid Username or password";
                                            success = false;
                                        }


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
                        Redirect = "/Home/InputOTP";
                        var page = $"PortalUsers?$filter=Authentication_Email eq '{UserName}'&format=json";

                        var httpResponse = Credentials.GetOdataData(page);
                        using var streamReader = new StreamReader(httpResponse.GetResponseStream());
                        var result = streamReader.ReadToEnd();

                        var details = JObject.Parse(result);

                        if (details["value"].Any())
                        {
                            foreach (var jToken in details["value"])
                            {
                                var config = (JObject)jToken;
                                if ((string)config["Authentication_Email"] != "")
                                {
                                    userID = (string)config["Authentication_Email"];
                                    var userAuth = AuthenticateUser(UserName, passwrd);


                                    if (userAuth)
                                    {
                                        Session["Username"] = (string)config["Authentication_Email"];
                                        Session["Mobile_Phone_No"] = (string)config["Mobile_Phone_No"];
                                        var IDno = (string)config["Id_Number"];
                                        var Email = (string)config["Authentication_Email"];
                                        var PhoneNo = (string)config["Mobile_Phone_No"];
                                        EmployeeData(UserName);
                                        msg = Redirect;
                                        success = true;
                                    }
                                    else
                                    {
                                        msg =
                                        "Invalid Username or password";
                                        success = false;
                                    }


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


        private EmployeeView EmployeeData(string staffNo)
        {
            try
            {
                var empView = new EmployeeView();
                var page = "Vendors?$filter=E_Mail eq '" + staffNo + "'&$format=json";
                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        empView.No = (string)config["No"];
                        empView.Name = (string)config["Name"];
                        empView.Search_Name = (string)config["Search_Name"];
                        empView.Name_2 = (string)config["Name_2"];
                        empView.Responsibility_Center = (string)config["Responsibility_Center"];
                        empView.Location_Code = (string)config["Location_Code"];
                        empView.Post_Code = (string)config["Post_Code"];
                        empView.Country_Region_Code = (string)config["Country_Region_Code"];
                        empView.Phone_No = (string)config["Phone_No"];
                        empView.Fax_No = (string)config["Fax_No"];
                        empView.Email = (string)config["Email"];
                        empView.E_Mail = (string)config["E_Mail"];
                        empView.IC_Partner_Code = (string)config["IC_Partner_Code"];
                        empView.Contact = (string)config["Contact"];
                        empView.Purchaser_Code = (string)config["Purchaser_Code"];
                        empView.Vendor_Posting_Group = (string)config["Vendor_Posting_Group"];
                        empView.Gen_Bus_Posting_Group = (string)config["Gen_Bus_Posting_Group"];
                        empView.VAT_Bus_Posting_Group = (string)config["VAT_Bus_Posting_Group"];
                        empView.Payment_Terms_Code = (string)config["Payment_Terms_Code"];
                        empView.Fin_Charge_Terms_Code = (string)config["Fin_Charge_Terms_Code"];
                        empView.Currency_Code = (string)config["Currency_Code"];
                        empView.Language_Code = (string)config["Language_Code"];
                        empView.Blocked = (string)config["Blocked"];
                        empView.Privacy_Blocked = (bool)config["Privacy_Blocked"];
                        empView.Last_Date_Modified = (string)config["Last_Date_Modified"];
                        empView.Application_Method = (string)config["Application_Method"];
                        empView.Shipment_Method_Code = (string)config["Shipment_Method_Code"];
                        empView.Lead_Time_Calculation = (string)config["Lead_Time_Calculation"];
                        empView.Base_Calendar_Code = (string)config["Base_Calendar_Code"];
                        empView.Balance_LCY = (int)config["Balance_LCY"];
                        empView.Balance_Due_LCY = (int)config["Balance_Due_LCY"];
                        empView.Payments_LCY = (int)config["Payments_LCY"];
                        empView.Coupled_to_CRM = (bool)config["Coupled_to_CRM"];
                        empView.Global_Dimension_1_Filter = (string)config["Global_Dimension_1_Filter"];
                        empView.Global_Dimension_2_Filter = (string)config["Global_Dimension_2_Filter"];
                        empView.Currency_Filter = (string)config["Currency_Filter"];
                        empView.Date_Filter = (string)config["Date_Filter"];
                    }
                }

                Session["EmployeeData"] = empView;
                return empView;
            }
            catch (Exception ex)
            {
                var errorMsg = new Error();
                errorMsg.Message = ex.Message;
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

        private bool CheckUser(string userName)
        {

            bool status = false;
            var page = $"PortalUsers?$filter = Authentication_Email eq '{userName}'&format = json";
            var httpResponse = Credentials.GetOdataData(page);

            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);

            if (details["value"].Any())
            {
                foreach (var jToken in details["value"])
                {
                    var config = (JObject)jToken;
                    if ((string)config["Authentication_Email"] != "")
                    {
                        // Set status to true if Employee_No is not empty
                        status = true;
                        break; // No need to continue checking once we find a valid Employee_No
                    }
                    else
                    {
                        // Handle case where Employee_No is empty (optional)
                    }
                }
            }
            else
            {
                status = false;
            }

            return status;
        }
        private bool AuthenticateUser(string userName, string Password)
        {

            bool status = false;
            var page = $"PortalUsers?$filter = Authentication_Email eq '{userName}'&format = json";
            var httpResponse = Credentials.GetOdataData(page);

            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);

            if (details["value"].Any())
            {
                foreach (var jToken in details["value"])
                {
                    var config = (JObject)jToken;
                    if ((string)config["Password_Value"] == Password)
                    {
                        status = true;
                        break;
                    }
                    else
                    {
                        // Handle case where Employee_No is empty (optional)
                    }
                }
            }
            else
            {
                status = false;
            }

            return status;
        }
        public ActionResult CreatePassword()
        {
            return View();
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult submitPasswords(string username, string password)
        {


            try
            {

                if (username == "0")
                {
                    EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                    string Responsible_Employee_No = employeeView.No;
                    string UserID = employeeView.UserID;
                    username = employeeView.UserID;

                }
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    return Json(new { message = "Username and password are required.", success = false });
                }
                var passwordChanged = false;
                /* bool passwordChanged = Credentials.ObjNav.fnChangeUserPassword(username, password);*/

                if (passwordChanged)
                {
                    return Json(new { message = "Password successfully updated. Log in with your new password.", success = true });
                }
                else
                {
                    return Json(new { message = "Failed to update password. Please try again.", success = false });
                }
            }
            catch (Exception ex)
            {
                return Json(new { message = "An error occurred: " + ex.Message, success = false });
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult sendOTP(string username)
        {
            try
            {
                var userID = "";
                bool userExists = CheckUser(username);
                if (userExists)
                {
                    bool res = false;
                    userID = username.Contains("\\") ? username : $"{ConfigurationManager.AppSettings["DOMAIN"]}\\{username}";
                    Credentials.ObjNav.GeneratePortalEmailOTPCode(userID);
                    if (res)
                    {
                        string redirect = "InputOTP?username=" + userID;
                        return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { message = "Record not submitted. Try again", success = false }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { message = "No Employee Number assigned to the applied username. Contact HR / ICT", success = false }, JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult InputOTP()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ConfirmOtp(string OTP)
        {
            try
            {
                bool res = true;
                /*res = Credentials.ObjNav.fnVerifyOTP(username, otp);*/
                if (res)
                {
                    string redirect = "/Dashboard/Dashboard";
                    return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { message = "Invalid OTP. Try again", success = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ForgotPassword(Authedication userlogin)
        {
            var msg = "";
            var email = string.Empty;
            var success = false;
            var UserName = userlogin.UserName.ToUpper();
            try
            {
                var userID = "";
                if (UserName.Contains("\\"))
                    userID = UserName;
                else
                    userID = ConfigurationManager.AppSettings["DOMAIN"] + @"\" + UserName;

                var page = "EmployeeList?$filter=User_ID eq '" + userID + "'&$format=json";

                var httpResponse = Credentials.GetOdataData(page);
                using var streamReader = new StreamReader(httpResponse.GetResponseStream());
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);

                if (details["value"].Any())
                {
                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        var User = (string)config["User_ID"];
                        email = (string)config["Company_E_Mail"];
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
                                Credentials.ObjNav.PasswordChanged((string)config["No"], true);
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

        public ActionResult SignUp()
        {
            Session.Clear();
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult SupplierFirstRegistration(SignupModel signupmodel)
        {
            try
            {
                var status="";
                var nav = new NavConnection().ObjNav();
                status = nav.FnReqforRegistration(signupmodel.VendorName, signupmodel.Phonenumber, signupmodel.Email, signupmodel.KraPin, signupmodel.ContactName);

                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult Logout()
        {
            // Clear the session
            Session.Clear();
            Session.Abandon();

            // Remove authentication cookies (if using Forms Authentication)
            Response.Cookies.Clear();
            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                var sessionCookie = new HttpCookie("ASP.NET_SessionId")
                {
                    Expires = DateTime.Now.AddDays(-1)
                };
                Response.Cookies.Add(sessionCookie);
            }

            if (Request.Cookies[".ASPXAUTH"] != null)
            {
                var authCookie = new HttpCookie(".ASPXAUTH")
                {
                    Expires = DateTime.Now.AddDays(-1)
                };
                Response.Cookies.Add(authCookie);
            }

            // Redirect to login page
            return RedirectToAction("Login", "Home");
        }

    }

}