using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System;
using System.Security.Claims;
using System.Web;

namespace WebFormsEntraID
{
    public partial class Default : System.Web.UI.Page
    {
        protected bool IsAuthenticated { get; private set; }
        protected string UserName { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if user is authenticated
            IsAuthenticated = Request.IsAuthenticated;
            
            if (IsAuthenticated)
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    // Get the user's name from claims
                    var nameClaim = identity.FindFirst("name") ?? identity.FindFirst(ClaimTypes.Name);
                    UserName = nameClaim != null ? nameClaim.Value : "Unknown User";
                }
            }
        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                HttpContext.Current.GetOwinContext().Authentication.Challenge(
                    new AuthenticationProperties { RedirectUri = "/" },
                    OpenIdConnectAuthenticationDefaults.AuthenticationType);
            }
        }

        protected void btnSignOut_Click(object sender, EventArgs e)
        {
            HttpContext.Current.GetOwinContext().Authentication.SignOut(
                OpenIdConnectAuthenticationDefaults.AuthenticationType,
                CookieAuthenticationDefaults.AuthenticationType);
        }
    }
}
