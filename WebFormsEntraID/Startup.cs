using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System;
using System.Configuration;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(WebFormsEntraID.Startup))]

namespace WebFormsEntraID
{
    public class Startup
    {
        // Read configuration values from Web.config
        private static string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        private static string tenantId = ConfigurationManager.AppSettings["ida:TenantId"];
        private static string authority = ConfigurationManager.AppSettings["ida:Authority"];
        private static string redirectUri = ConfigurationManager.AppSettings["ida:RedirectUri"];
        private static string postLogoutRedirectUri = ConfigurationManager.AppSettings["ida:PostLogoutRedirectUri"];

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = CookieAuthenticationDefaults.AuthenticationType,
                CookieName = "WebFormsEntraID.Auth",
                ExpireTimeSpan = TimeSpan.FromHours(1),
                SlidingExpiration = true
            });

            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    ClientId = clientId,
                    Authority = authority,
                    RedirectUri = redirectUri,
                    PostLogoutRedirectUri = postLogoutRedirectUri,
                    Scope = "openid profile email",
                    ResponseType = "id_token",
                    
                    // Use cookies to persist the authentication
                    SignInAsAuthenticationType = CookieAuthenticationDefaults.AuthenticationType,
                    
                    TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = $"https://sts.windows.net/{tenantId}/",
                        NameClaimType = "name",
                        SaveSigninToken = true
                    },

                    Notifications = new OpenIdConnectAuthenticationNotifications
                    {
                        AuthenticationFailed = context =>
                        {
                            context.HandleResponse();
                            context.Response.Redirect("/?errormessage=" + context.Exception.Message);
                            return Task.FromResult(0);
                        },
                        RedirectToIdentityProvider = context =>
                        {
                            // Handle sign out
                            if (context.ProtocolMessage.RequestType == OpenIdConnectRequestType.LogoutRequest)
                            {
                                var idTokenHint = context.OwinContext.Authentication.User.FindFirst("id_token");
                                if (idTokenHint != null)
                                {
                                    context.ProtocolMessage.IdTokenHint = idTokenHint.Value;
                                }
                            }
                            return Task.FromResult(0);
                        }
                    }
                });
        }
    }
}
