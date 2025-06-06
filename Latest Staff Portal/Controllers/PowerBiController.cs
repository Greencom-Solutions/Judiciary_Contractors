using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using Latest_Staff_Portal.CustomSecurity;
using Latest_Staff_Portal.Models;
using Latest_Staff_Portal.ViewModel;
using Newtonsoft.Json.Linq;

namespace Latest_Staff_Portal.Controllers
{
    [CustomAuthorization(Role = "ALLUSERS,ACCOUNTANTS,PROCUREMENT")]
    public class PowerBiController : Controller
    {
        public ActionResult Deposits()
        {
            try
            {
                if (Session["Username"] == null)
                    return RedirectToAction("Login", "Login");
                return View("~/Views/PowerBi/Deposits.cshtml"); // Specify the full path to the view
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }

        public ActionResult DailyMonthlyTrends()
        {
            try
            {
                if (Session["Username"] == null)
                    return RedirectToAction("Login", "Login");
                return View("~/Views/PowerBi/DailyMonthlyTrends.cshtml"); // Specify the full path to the view
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }

        public ActionResult AllTimeDeposits()
        {
            try
            {
                if (Session["Username"] == null)
                    return RedirectToAction("Login", "Login");
                return View("~/Views/PowerBi/AllTimeDeposits.cshtml"); // Specify the full path to the view
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }


        public ActionResult AllReports()
        {
            try
            {
                if (Session["Username"] == null)
                    return RedirectToAction("Login", "Login");
                return View("~/Views/PowerBi/AllReports.cshtml"); // Specify the full path to the view
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }

        public ActionResult TodaysDeposit()
        {
            try
            {
                if (Session["Username"] == null)
                    return RedirectToAction("Login", "Login");
                return View("~/Views/PowerBi/TodaysDeposit.cshtml"); // Specify the full path to the view
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }

        // GET: PowerBi
        [HttpGet]
        public ActionResult GetFeesReceipt55()
        {
            var list = new List<DepositReceipt>();
            var todayDate = DateTime.Now.ToString("yyyy-MM-dd");

            // Construct the OData query string with the date filter
            //  string page = $"DepositReceipt?$filter=Date eq {todayDate}&$format=json";

            var page = "DepositReceipt?$filter=Receipt_Type eq 'Fees'&$format=json";

            //string page = "DepositReceipt?$filter=Receipt_Type eq 'Deposit'&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var results = streamReader.ReadToEnd();

                var details = JObject.Parse(results);


                foreach (JObject config in details["value"])
                {
                    var depositReceipt = new DepositReceipt
                    {
                        //  No = (string)config["No"],
                        Date = config["Date"] + "",
                        Receipt_Type = (string)config["Receipt_Type"],
                        Pay_Mode = (string)config["Pay_Mode"],
                        // Cheque_No = (string)config["Cheque_No"],
                        // Cheque_Date =config["Cheque_Date"]+"",
                        Amount = (decimal)config["Amount"],
                        //   Amount_LCY = (decimal)config["Amount_LCY"],
                        //   Bank_Code = (string)config["Bank_Code"],
                        // Currency_Code = (string)config["Currency_Code"],
                        // Received_From = (string)config["Received_From"],
                        // On_Behalf_Of = (string)config["On_Behalf_Of"],
                        // Payment_Reference = (string)config["Payment_Reference"],
                        // Cashier = (string)config["Cashier"],
                        //  Posted_Date = (DateTime)config["Posted_Date"],
                        //  Posted_Time = (string)config["Posted_Time"],
                        //  Posted_By = (string)config["Posted_By"],
                        Status = (string)config["Status"],
                        Global_Dimension_1_Code = (string)config["Global_Dimension_1_Code"],
                        Department_Name = (string)config["Department_Name"],
                        Global_Dimension_2_Code = (string)config["Global_Dimension_2_Code"],
                        Project_Name = (string)config["Project_Name"],
                        PRN_No = (string)config["PRN_No"],
                        Case_No = (string)config["Case_No"],
                        Case_Type = (string)config["Case_Type"],
                        Case_Title = (string)config["Case_Title"],
                        Posted = (bool)config["Posted"]
                    };
                    list.Add(depositReceipt);
                }
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        // GET: PowerBi
        [HttpGet]
        public ActionResult GetDepositReceipt()
        {
            var list = new List<DepositReceipt>();
            var page = "DepositReceipt?$filter=Receipt_Type eq 'Deposit'&$format=json";

            //string page = "DepositReceipt?$filter=Receipt_Type eq 'Deposit'&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var results = streamReader.ReadToEnd();

                var details = JObject.Parse(results);

                foreach (JObject config in details["value"])
                {
                    var depositReceipt = new DepositReceipt
                    {
                        //  No = (string)config["No"],
                        Date = config["Date"] + "",
                        Receipt_Type = (string)config["Receipt_Type"],
                        Pay_Mode = (string)config["Pay_Mode"],
                        //Cheque_No = (string)config["Cheque_No"],
                        // Cheque_Date = config["Cheque_Date"] + "",
                        Amount = (decimal)config["Amount"],
                        // Amount_LCY = (decimal)config["Amount_LCY"],
                        //  Bank_Code = (string)config["Bank_Code"],
                        //   Currency_Code = (string)config["Currency_Code"],
                        //  Received_From = (string)config["Received_From"],
                        //    On_Behalf_Of = (string)config["On_Behalf_Of"],
                        // Payment_Reference = (string)config["Payment_Reference"],
                        // Cashier = (string)config["Cashier"],
                        //  Posted_Date = (DateTime)config["Posted_Date"],
                        // Posted_Time = (string)config["Posted_Time"],
                        //Posted_By = (string)config["Posted_By"],
                        Status = (string)config["Status"],
                        Global_Dimension_1_Code = (string)config["Global_Dimension_1_Code"],
                        Department_Name = (string)config["Department_Name"],
                        Global_Dimension_2_Code = (string)config["Global_Dimension_2_Code"],
                        Project_Name = (string)config["Project_Name"],
                        PRN_No = (string)config["PRN_No"],
                        Case_No = (string)config["Case_No"],
                        Case_Type = (string)config["Case_Type"],
                        Case_Title = (string)config["Case_Title"],
                        Posted = (bool)config["Posted"]
                    };
                    list.Add(depositReceipt);
                }
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        // GET: PowerBi
        [HttpGet]
        public ActionResult GetFeeReceipt()
        {
            var list = new List<DepositReceipt>();
            var page = "DepositReceipt?$filter=Receipt_Type eq 'Fee'&$format=json";

            //string page = "DepositReceipt?$filter=Receipt_Type eq 'Deposit'&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var results = streamReader.ReadToEnd();

                var details = JObject.Parse(results);

                foreach (JObject config in details["value"])
                {
                    var depositReceipt = new DepositReceipt
                    {
                        //  No = (string)config["No"],
                        Date = config["Date"] + "",
                        Receipt_Type = (string)config["Receipt_Type"],
                        Pay_Mode = (string)config["Pay_Mode"],
                        //Cheque_No = (string)config["Cheque_No"],
                        // Cheque_Date = config["Cheque_Date"] + "",
                        Amount = (decimal)config["Amount"],
                        // Amount_LCY = (decimal)config["Amount_LCY"],
                        //  Bank_Code = (string)config["Bank_Code"],
                        //   Currency_Code = (string)config["Currency_Code"],
                        //  Received_From = (string)config["Received_From"],
                        //    On_Behalf_Of = (string)config["On_Behalf_Of"],
                        // Payment_Reference = (string)config["Payment_Reference"],
                        // Cashier = (string)config["Cashier"],
                        //  Posted_Date = (DateTime)config["Posted_Date"],
                        // Posted_Time = (string)config["Posted_Time"],
                        //Posted_By = (string)config["Posted_By"],
                        Status = (string)config["Status"],
                        Global_Dimension_1_Code = (string)config["Global_Dimension_1_Code"],
                        Department_Name = (string)config["Department_Name"],
                        Global_Dimension_2_Code = (string)config["Global_Dimension_2_Code"],
                        Project_Name = (string)config["Project_Name"],
                        PRN_No = (string)config["PRN_No"],
                        Case_No = (string)config["Case_No"],
                        Case_Type = (string)config["Case_Type"],
                        Case_Title = (string)config["Case_Title"],
                        Posted = (bool)config["Posted"]
                    };
                    list.Add(depositReceipt);
                }
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        // GET: PowerBi
        [HttpGet]
        public ActionResult GetFineReceipt()
        {
            var list = new List<DepositReceipt>();
            var page = "DepositReceipt?$filter=Receipt_Type eq 'Fines'&$format=json";

            //string page = "DepositReceipt?$filter=Receipt_Type eq 'Deposit'&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var results = streamReader.ReadToEnd();

                var details = JObject.Parse(results);

                foreach (JObject config in details["value"])
                {
                    var depositReceipt = new DepositReceipt
                    {
                        //  No = (string)config["No"],
                        Date = config["Date"] + "",
                        Receipt_Type = (string)config["Receipt_Type"],
                        Pay_Mode = (string)config["Pay_Mode"],
                        //Cheque_No = (string)config["Cheque_No"],
                        // Cheque_Date = config["Cheque_Date"] + "",
                        Amount = (decimal)config["Amount"],
                        // Amount_LCY = (decimal)config["Amount_LCY"],
                        //  Bank_Code = (string)config["Bank_Code"],
                        //   Currency_Code = (string)config["Currency_Code"],
                        //  Received_From = (string)config["Received_From"],
                        //    On_Behalf_Of = (string)config["On_Behalf_Of"],
                        // Payment_Reference = (string)config["Payment_Reference"],
                        // Cashier = (string)config["Cashier"],
                        //  Posted_Date = (DateTime)config["Posted_Date"],
                        // Posted_Time = (string)config["Posted_Time"],
                        //Posted_By = (string)config["Posted_By"],
                        Status = (string)config["Status"],
                        Global_Dimension_1_Code = (string)config["Global_Dimension_1_Code"],
                        Department_Name = (string)config["Department_Name"],
                        Global_Dimension_2_Code = (string)config["Global_Dimension_2_Code"],
                        Project_Name = (string)config["Project_Name"],
                        PRN_No = (string)config["PRN_No"],
                        Case_No = (string)config["Case_No"],
                        Case_Type = (string)config["Case_Type"],
                        Case_Title = (string)config["Case_Title"],
                        Posted = (bool)config["Posted"]
                    };
                    list.Add(depositReceipt);
                }
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }


        // GET: PowerBi
        [HttpGet]
        public ActionResult GetUtilizationReceipt()
        {
            var list = new List<DepositReceipt>();
            var page = "DepositReceipt?$filter=Receipt_Type eq 'Utilization'&$format=json";

            //string page = "DepositReceipt?$filter=Receipt_Type eq 'Deposit'&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var results = streamReader.ReadToEnd();

                var details = JObject.Parse(results);


                foreach (JObject config in details["value"])
                {
                    var depositReceipt = new DepositReceipt
                    {
                        //No = (string)config["No"],
                        Date = config["Date"] + "",
                        Receipt_Type = (string)config["Receipt_Type"],
                        Pay_Mode = (string)config["Pay_Mode"],
                        // Cheque_No = (string)config["Cheque_No"],
                        //Cheque_Date = config["Cheque_Date"] + "",
                        Amount = (decimal)config["Amount"],
/*                            Amount_LCY = (decimal)config["Amount_LCY"],
                            Bank_Code = (string)config["Bank_Code"],
                            Currency_Code = (string)config["Currency_Code"],
                            Received_From = (string)config["Received_From"],
                            On_Behalf_Of = (string)config["On_Behalf_Of"],
                            Payment_Reference = (string)config["Payment_Reference"],
                            Cashier = (string)config["Cashier"],
                            Posted_Date = (DateTime)config["Posted_Date"],
                            Posted_Time = (string)config["Posted_Time"],
                            Posted_By = (string)config["Posted_By"],*/
                        Status = (string)config["Status"],
                        Global_Dimension_1_Code = (string)config["Global_Dimension_1_Code"],
                        Department_Name = (string)config["Department_Name"],
                        Global_Dimension_2_Code = (string)config["Global_Dimension_2_Code"],
                        Project_Name = (string)config["Project_Name"],
                        PRN_No = (string)config["PRN_No"],
                        Case_No = (string)config["Case_No"],
                        Case_Type = (string)config["Case_Type"],
                        Case_Title = (string)config["Case_Title"],
                        Posted = (bool)config["Posted"]
                    };
                    list.Add(depositReceipt);
                }
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        // GET: PowerBi
        [HttpGet]
        public ActionResult GetNoReceiptType()
        {
            var list = new List<DepositReceipt>();
            var page = "DepositReceipt?$filter=Receipt_Type eq ''&$format=json";

            //string page = "DepositReceipt?$filter=Receipt_Type eq 'Deposit'&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var results = streamReader.ReadToEnd();

                var details = JObject.Parse(results);


                foreach (JObject config in details["value"])
                {
                    var depositReceipt = new DepositReceipt
                    {
                        //No = (string)config["No"],
                        Date = config["Date"] + "",
                        Receipt_Type = (string)config["Receipt_Type"],
                        Pay_Mode = (string)config["Pay_Mode"],
                        // Cheque_No = (string)config["Cheque_No"],
                        // Cheque_Date = config["Cheque_Date"] + "",
                        Amount = (decimal)config["Amount"],
/*                            Amount_LCY = (decimal)config["Amount_LCY"],
                            Bank_Code = (string)config["Bank_Code"],
                            Currency_Code = (string)config["Currency_Code"],
                            Received_From = (string)config["Received_From"],
                            On_Behalf_Of = (string)config["On_Behalf_Of"],
                            Payment_Reference = (string)config["Payment_Reference"],
                            Cashier = (string)config["Cashier"],
                            Posted_Date = (DateTime)config["Posted_Date"],
                            Posted_Time = (string)config["Posted_Time"],
                            Posted_By = (string)config["Posted_By"],*/
                        Status = (string)config["Status"],
                        Global_Dimension_1_Code = (string)config["Global_Dimension_1_Code"],
                        Department_Name = (string)config["Department_Name"],
                        Global_Dimension_2_Code = (string)config["Global_Dimension_2_Code"],
                        Project_Name = (string)config["Project_Name"],
                        PRN_No = (string)config["PRN_No"],
                        Case_No = (string)config["Case_No"],
                        Case_Type = (string)config["Case_Type"],
                        Case_Title = (string)config["Case_Title"],
                        Posted = (bool)config["Posted"]
                    };
                    list.Add(depositReceipt);
                }
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        // GET: PowerBi
        [HttpGet]
        public ActionResult GetAllReceiptType()
        {
            var list = new List<DepositReceipt>();
            var page = "DepositReceipt";

            //string page = "DepositReceipt?$filter=Receipt_Type eq 'Deposit'&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var results = streamReader.ReadToEnd();

                var details = JObject.Parse(results);


                foreach (JObject config in details["value"])
                {
                    var depositReceipt = new DepositReceipt
                    {
                        //No = (string)config["No"],
                        Date = config["Date"] + "",
                        Receipt_Type = (string)config["Receipt_Type"],
                        Pay_Mode = (string)config["Pay_Mode"],
                        // Cheque_No = (string)config["Cheque_No"],
                        // Cheque_Date = config["Cheque_Date"] + "",
                        Amount = (decimal)config["Amount"],
                        /*                            Amount_LCY = (decimal)config["Amount_LCY"],
                                                        Bank_Code = (string)config["Bank_Code"],
                                                        Currency_Code = (string)config["Currency_Code"],
                                                        Received_From = (string)config["Received_From"],
                                                        On_Behalf_Of = (string)config["On_Behalf_Of"],
                                                        Payment_Reference = (string)config["Payment_Reference"],
                                                        Cashier = (string)config["Cashier"],
                                                        Posted_Date = (DateTime)config["Posted_Date"],
                                                        Posted_Time = (string)config["Posted_Time"],
                                                        Posted_By = (string)config["Posted_By"],*/
                        Status = (string)config["Status"],
                        Global_Dimension_1_Code = (string)config["Global_Dimension_1_Code"],
                        Department_Name = (string)config["Department_Name"],
                        Global_Dimension_2_Code = (string)config["Global_Dimension_2_Code"],
                        Project_Name = (string)config["Project_Name"],
                        PRN_No = (string)config["PRN_No"],
                        Case_No = (string)config["Case_No"],
                        Case_Type = (string)config["Case_Type"],
                        Case_Title = (string)config["Case_Title"],
                        Posted = (bool)config["Posted"]
                    };
                    list.Add(depositReceipt);
                }
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}