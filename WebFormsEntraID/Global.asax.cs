using System;
using System.Web;

namespace WebFormsEntraID
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // Code that runs at the beginning of each request
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            // Code that runs when a user is authenticated
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            Exception exc = Server.GetLastError();
            // Log the exception or handle it appropriately
        }

        protected void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends
        }

        protected void Application_End(object sender, EventArgs e)
        {
            // Code that runs on application shutdown
        }
    }
}
