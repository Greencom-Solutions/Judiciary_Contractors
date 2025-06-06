using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Latest_Staff_Portal.ViewModel;

namespace Latest_Staff_Portal.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Dashboard()
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


        public ActionResult ContractorExtension()
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

        public ActionResult ContractorAmmendments()
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

        public ActionResult ContractorInstructions()
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
    }
}