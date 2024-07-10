using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using BAL;

namespace Client
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup

        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown
            Session.Abandon();
        }

        void Application_Error(object sender, EventArgs e)
        {
            //Exception Ex = Server.GetLastError();
            //if (Ex != null && Ex.Message != "File does not exist.")
            //{
            //    ExceptionLog_BAL lExceptionLog_BAL = new ExceptionLog_BAL();
            //    lExceptionLog_BAL.LogErrorToDB(Ex);
            //    Server.ClearError();
            //    Server.Transfer("~/Errorpage.html");
            //}

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.
            Session.Abandon();
        }

    }
}
