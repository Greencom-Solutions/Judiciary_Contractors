
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using Newtonsoft;
using System.Web;


namespace Latest_Staff_Portal.Models
{
    public class NavConnection
    {
       /* public static ClientContext SPClientContext { get; set; }

        public static Web SPWeb { get; set; }*/

        public static string SPErrorMsg { get; set; }


        // remote connection data
      /*  public LiveQueries.eprocurementQueries queries()
        {
            var ws = new LiveQueries.eprocurementQueries();
            try
            {
                var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                   ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                ws.Credentials = credentials;
                ws.PreAuthenticate = true;

            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return ws;
        }*/
        public LiveCodeUnit.procurement ObjNav()
        {
            var ws = new LiveCodeUnit.procurement();
            try
            {
                var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                    ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                ws.Credentials = credentials;
                ws.PreAuthenticate = true;

            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return ws;
        }

      /*  public procurementQueries.eprocurementQueries ProcurementQueries()
        {
            var ws = new procurementQueries.eprocurementQueries();
            try
            {
                var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                    ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                ws.Credentials = credentials;
                ws.PreAuthenticate = true;


            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return ws;
        }
        public ProjectManagement.ProjectManagement projectPlan()
        {
            var ws = new ProjectManagement.ProjectManagement();
            try
            {
                var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                    ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                ws.Credentials = credentials;
                ws.PreAuthenticate = true;


            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return ws;
        }*/
        public static bool Connect(string SPURL, string SPUserName, string SPPassWord, string SPDomainName)
        {

            bool bConnected = false;


            return bConnected;

        }

    }
}
