using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;
using Loginapplication.Models;

namespace Loginapplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer(new EmpInitilizier());

            Application["UsersOnline"] = 0;
        }

        protected void Session_Start(Object sender, EventArgs e)
        {

            Application.Lock();
            Application["UsersOnline"] = (int)Application["UsersOnline"] + 1;
            Application.UnLock();
            SessionIDManager Manager = new SessionIDManager();
            string NewID = Manager.CreateSessionID(Context);
            string OldID = Context.Session.SessionID;
            ////Context.Response.Cookies.Add(new HttpCookie("LID", NewID));
            //bool redirected = false;
            //bool cookieAdded = false;

            //Manager.SaveSessionID(Context, NewID, out redirected, out cookieAdded);
            HttpContext.Current.Session.Add("__MyAppSession", NewID);



            //Response.Write("Old SessionId Is : " + OldID);

            //if (cookieAdded)
            //{
                Session["GUID"] = NewID;
                Response.Write("<br/> New Session ID Is : " + NewID);
                Response.Write("<br/> Old Session ID Is : " + OldID);
                Response.Write("<br/> No of Users : " + Application["UsersOnline"]);
            //}
            //else
            //{
            //    Response.Write("<br/> Session Id did not saved : ");
            //}
        }




        protected void Session_End(Object sender, EventArgs e)
        {
            Application.Lock();
            Application["UsersOnline"] = (int)Application["UsersOnline"] - 1;
            Application.UnLock();
            Session.Clear();
            Session.Abandon();

        }

    }
}
