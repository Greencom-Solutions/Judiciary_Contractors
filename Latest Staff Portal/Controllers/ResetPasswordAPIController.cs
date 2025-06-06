using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Latest_Staff_Portal.CustomSecurity;
using Latest_Staff_Portal.Models;
using Newtonsoft.Json.Linq;

namespace Latest_Staff_Portal.Controllers
{
    [CustomAuthorization(Role = "ALLUSERS,ACCOUNTANTS,PROCUREMENT")]
    public class ResetPasswordAPIController : Controller
    {
        // GET: ResetPasswordAPI
        public ActionResult Index()
        {
            return View();
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

                if (details["value"].Count() > 0)
                {
                    foreach (JObject config in details["value"])
                    {
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


        /*
                public ActionResult ForgotPassword(string username)
                {
                    Authedication user = new Authedication();
                    return View(user);
                }*/
        /*        public ActionResult ForgotPassword(string username)
                {
                    if (string.IsNullOrEmpty(username))
                    {
                        return View(new Authedication());
                    }

                    string msg = "";
                    string email = string.Empty;
                    bool success = false;
                    string UserName = username.ToUpper();
                    try
                    {
                        string userID = "";
                        if (UserName.Contains("\\"))
                        {
                            userID = UserName;
                        }
                        else
                        {
                            userID = ConfigurationManager.AppSettings["DOMAIN"] + @"\" + UserName;
                        }

                        string page = "EmployeeList?$filter=User_ID eq '" + userID + "'&$format=json";

                        HttpWebResponse httpResponse = Credentials.GetOdataData(page);
                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            var result = streamReader.ReadToEnd();

                            var details = JObject.Parse(result);

                            if (details["value"].Count() > 0)
                            {
                                foreach (JObject config in details["value"])
                                {
                                    string User = (string)config["User_ID"];
                                    email = (string)config["Company_E_Mail"];
                                    if (User != "")
                                    {
                                        if (email != "")
                                        {
                                            #region generate random password

                                            Random rand = new Random();
                                            Random randAlpha = new Random();
                                            int newpassint = rand.Next(10000, 99999);

                                            int alphabetPosition = randAlpha.Next(1, 26);
                                            bool isCap = (alphabetPosition % 2 == 0 ? true : false);
                                            string theAlphabet = GetTheAlphabet(alphabetPosition, isCap);

                                            alphabetPosition = randAlpha.Next(1, 26);
                                            isCap = (alphabetPosition % 2 == 0 ? true : false);
                                            theAlphabet += GetTheAlphabet(alphabetPosition, isCap);

                                            alphabetPosition = randAlpha.Next(1, 26);
                                            isCap = (alphabetPosition % 2 == 0 ? true : false);
                                            theAlphabet += GetTheAlphabet(alphabetPosition, isCap);

                                            alphabetPosition = randAlpha.Next(1, 26);
                                            isCap = (alphabetPosition % 2 == 0 ? true : false);
                                            theAlphabet += GetTheAlphabet(alphabetPosition, isCap);

                                            //string newpass = theAlphabet + "#" + newpassint.ToString() + "?" + alphabetPosition.ToString() + "@";
                                            string newpass = theAlphabet + "#" + newpassint.ToString() + "@" + alphabetPosition.ToString();

                                            #endregion generate random password

                                            string ok = Credentials.ResetPassword(User, newpass);
                                            Credentials.ObjNav.PasswordChanged((string)config["No"], true);
                                            //bool ok = Credentials.ObjNav.ResetPassword(UserName, newpass);
                                            if (ok == "CHANGED")
                                            {
                                                const string subject = "STAFF PORTAL CREDENTIALS";
                                                string emailmsg = "Staff portal credentials reset:<br />New password is <b />" + newpass + "" +
                                                    "<br />Remember to change your password after you login";
                                                if (CommonClass.SendEmailAlert(emailmsg, email, subject))
                                                {
                                                    msg = "A New password has been send to your Email<b>(" + email + ")</b>. Use it to login. Remember to change your password after you login";
                                                    success = true;
                                                }
                                                else
                                                {
                                                    msg = "An error occured while sending you the credentials.Please contact the ICT office administrator.";
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
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message.Replace("'", "");
                        success = false;
                    }
                    return Json(new { message = msg, success = success }, JsonRequestBehavior.AllowGet);
                }
        */

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