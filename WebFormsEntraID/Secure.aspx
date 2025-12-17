<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Secure.aspx.cs" Inherits="WebFormsEntraID.Secure" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Secure Page - Azure Entra ID</title>
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            max-width: 900px;
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
        h2 {
            color: #333;
            border-bottom: 2px solid #0078d4;
            padding-bottom: 10px;
        }
        .secure-badge {
            background-color: #d4edda;
            color: #155724;
            padding: 15px;
            border-left: 4px solid #28a745;
            margin: 20px 0;
        }
        .user-info {
            background-color: #e6f2ff;
            padding: 15px;
            border-radius: 4px;
            margin: 20px 0;
        }
        .claims-table {
            width: 100%;
            border-collapse: collapse;
            margin: 20px 0;
        }
        .claims-table th, .claims-table td {
            padding: 12px;
            text-align: left;
            border-bottom: 1px solid #ddd;
        }
        .claims-table th {
            background-color: #0078d4;
            color: white;
            font-weight: bold;
        }
        .claims-table tr:hover {
            background-color: #f5f5f5;
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
            margin: 10px 5px 10px 0;
        }
        .button:hover {
            background-color: #005a9e;
        }
        .button-secondary {
            background-color: #6c757d;
        }
        .button-secondary:hover {
            background-color: #5a6268;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>🔒 Secure Page</h1>
            
            <div class="secure-badge">
                <strong>✓ This page is protected</strong>
                <p>You successfully authenticated with Azure Entra ID!</p>
            </div>

            <div class="user-info">
                <h2>User Information</h2>
                <p><strong>Name:</strong> <%= UserName %></p>
                <p><strong>Authentication Type:</strong> <%= AuthenticationType %></p>
                <p><strong>Is Authenticated:</strong> <%= IsAuthenticated ? "Yes" : "No" %></p>
            </div>

            <h2>User Claims</h2>
            <p>Below are all the claims associated with your authenticated identity:</p>
            
            <asp:GridView ID="gvClaims" runat="server" CssClass="claims-table" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="Type" HeaderText="Claim Type" />
                    <asp:BoundField DataField="Value" HeaderText="Claim Value" />
                </Columns>
            </asp:GridView>

            <div style="margin-top: 30px;">
                <a href="Default.aspx" class="button button-secondary">Back to Home</a>
                <asp:Button ID="btnSignOut" runat="server" Text="Sign Out" CssClass="button" OnClick="btnSignOut_Click" />
            </div>
        </div>
    </form>
</body>
</html>
