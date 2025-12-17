<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebFormsEntraID.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WebForms with Azure Entra ID</title>
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            max-width: 800px;
            margin: 50px auto;
            padding: 20px;
            background-color: #f5f5f5;
        }
        .container {
            background-color: white;
            padding: 30px;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }
        h1 {
            color: #0078d4;
            margin-top: 0;
        }
        .info {
            background-color: #e6f2ff;
            padding: 15px;
            border-left: 4px solid #0078d4;
            margin: 20px 0;
        }
        .button {
            background-color: #0078d4;
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            font-size: 16px;
            text-decoration: none;
            display: inline-block;
            margin: 10px 0;
        }
        .button:hover {
            background-color: #005a9e;
        }
        .status {
            margin: 20px 0;
            padding: 15px;
            border-radius: 4px;
        }
        .authenticated {
            background-color: #d4edda;
            color: #155724;
            border: 1px solid #c3e6cb;
        }
        .unauthenticated {
            background-color: #fff3cd;
            color: #856404;
            border: 1px solid #ffeaa7;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Welcome to WebForms with Azure Entra ID</h1>
            
            <div class="info">
                <strong>About this application:</strong>
                <p>This is an ASP.NET WebForms application targeting .NET Framework 4.5.2 with Azure Entra ID authentication using OWIN middleware and OpenID Connect.</p>
            </div>

            <div class="status <%= IsAuthenticated ? "authenticated" : "unauthenticated" %>">
                <% if (IsAuthenticated) { %>
                    <h3>✓ You are authenticated</h3>
                    <p>Hello, <strong><%= UserName %></strong>!</p>
                    <p>You can now access secured pages.</p>
                    <a href="Secure.aspx" class="button">Go to Secure Page</a>
                    <asp:Button ID="btnSignOut" runat="server" Text="Sign Out" CssClass="button" OnClick="btnSignOut_Click" />
                <% } else { %>
                    <h3>You are not authenticated</h3>
                    <p>Click the button below to sign in with your Azure Entra ID account.</p>
                    <asp:Button ID="btnSignIn" runat="server" Text="Sign In" CssClass="button" OnClick="btnSignIn_Click" />
                <% } %>
            </div>

            <div class="info">
                <strong>Configuration:</strong>
                <p>Make sure to update the following settings in Web.config:</p>
                <ul>
                    <li><code>ida:TenantId</code> - Your Azure tenant ID</li>
                    <li><code>ida:ClientId</code> - Your application (client) ID</li>
                    <li><code>ida:Authority</code> - Your authority URL</li>
                </ul>
            </div>
        </div>
    </form>
</body>
</html>
