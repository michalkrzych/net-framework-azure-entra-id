using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace WebFormsEntraID
{
    public partial class Secure : System.Web.UI.Page
    {
        protected bool IsAuthenticated { get; private set; }
        protected string UserName { get; private set; }
        protected string AuthenticationType { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Require authentication to access this page
            if (!Request.IsAuthenticated)
            {
                // Redirect to sign in if not authenticated
                HttpContext.Current.GetOwinContext().Authentication.Challenge(
                    new AuthenticationProperties { RedirectUri = Request.Url.ToString() },
                    OpenIdConnectAuthenticationDefaults.AuthenticationType);
                Response.End();
                return;
            }

            IsAuthenticated = Request.IsAuthenticated;

            if (!IsPostBack)
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    // Get user information from claims
                    var nameClaim = identity.FindFirst("name") ?? identity.FindFirst(ClaimTypes.Name);
                    UserName = nameClaim != null ? nameClaim.Value : "Unknown User";
                    AuthenticationType = identity.AuthenticationType;

                    // Bind all claims to the GridView
                    var claims = identity.Claims
                        .Select(c => new { Type = c.Type, Value = c.Value })
                        .OrderBy(c => c.Type)
                        .ToList();
                    
                    gvClaims.DataSource = claims;
                    gvClaims.DataBind();
                }
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
