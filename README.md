# ASP.NET WebForms with Azure Entra ID Authentication

This repository contains a template ASP.NET WebForms application targeting .NET Framework 4.5.2 with Azure Entra ID (formerly Azure AD) authentication using OWIN middleware and OpenID Connect.

## Overview

This project serves as a starting point for migrating existing on-premises WebForms applications to Azure App Service with modern authentication using Azure Entra ID.

## Features

- ✅ ASP.NET WebForms targeting .NET Framework 4.5.2
- ✅ Azure Entra ID authentication via OWIN + OpenID Connect
- ✅ Cookie-based authentication persistence
- ✅ Sample unauthenticated landing page (Default.aspx)
- ✅ Sample secure page requiring authentication (Secure.aspx)
- ✅ User claims display
- ✅ Sign-in and sign-out functionality

## Prerequisites

- Visual Studio 2015 or later
- .NET Framework 4.5.2 or later
- An Azure subscription with an Azure Entra ID tenant
- An App Registration in Azure Entra ID

## Azure Entra ID Configuration

### 1. Register an Application in Azure Entra ID

1. Go to the [Azure Portal](https://portal.azure.com)
2. Navigate to **Azure Active Directory** > **App registrations**
3. Click **New registration**
4. Provide a name (e.g., "WebFormsEntraID")
5. Select **Accounts in this organizational directory only**
6. Set the Redirect URI to `https://localhost:44300/`
7. Click **Register**

### 2. Configure Authentication

1. In your app registration, go to **Authentication**
2. Under **Implicit grant and hybrid flows**, check:
   - **ID tokens**
3. Add a logout URL: `https://localhost:44300/`
4. Click **Save**

### 3. Note Your Configuration Values

From the **Overview** page of your app registration, copy:
- **Application (client) ID**
- **Directory (tenant) ID**

## Application Configuration

Update the following settings in `Web.config`:

```xml
<appSettings>
  <add key="ida:TenantId" value="YOUR_TENANT_ID_HERE" />
  <add key="ida:ClientId" value="YOUR_CLIENT_ID_HERE" />
  <add key="ida:Authority" value="https://login.microsoftonline.com/YOUR_TENANT_ID_HERE" />
  <add key="ida:RedirectUri" value="https://localhost:44300/" />
  <add key="ida:PostLogoutRedirectUri" value="https://localhost:44300/" />
</appSettings>
```

Replace:
- `YOUR_TENANT_ID_HERE` with your Directory (tenant) ID
- `YOUR_CLIENT_ID_HERE` with your Application (client) ID

## Building and Running

### Using Visual Studio

1. Open `WebFormsEntraID.sln` in Visual Studio
2. Restore NuGet packages (right-click solution > Restore NuGet Packages)
3. Update the Web.config with your Azure Entra ID configuration
4. Press F5 to build and run
5. The application will launch at `https://localhost:44300/`

### Using MSBuild

```bash
# Restore NuGet packages
nuget restore WebFormsEntraID.sln

# Build the solution
msbuild WebFormsEntraID.sln /p:Configuration=Release
```

## Project Structure

```
WebFormsEntraID/
├── Properties/
│   └── AssemblyInfo.cs          # Assembly metadata
├── Default.aspx                  # Unauthenticated landing page
├── Default.aspx.cs               # Landing page code-behind
├── Secure.aspx                   # Authenticated secure page
├── Secure.aspx.cs                # Secure page code-behind
├── Global.asax                   # Application events
├── Global.asax.cs                # Application event handlers
├── Startup.cs                    # OWIN startup configuration
├── Web.config                    # Application configuration
├── packages.config               # NuGet package references
└── WebFormsEntraID.csproj       # Project file
```

## Key Components

### Startup.cs

Configures OWIN middleware with:
- Cookie authentication for session persistence
- OpenID Connect authentication for Azure Entra ID integration
- Token validation parameters
- Authentication notifications for error handling

### Default.aspx

The main landing page that:
- Shows authentication status
- Provides sign-in button for unauthenticated users
- Displays user information for authenticated users
- Provides navigation to secure pages

### Secure.aspx

A protected page that:
- Requires authentication to access
- Displays user claims from Azure Entra ID
- Shows user profile information
- Provides sign-out functionality

## NuGet Packages

The application uses the following key packages:

- **Microsoft.Owin** (4.2.2) - OWIN implementation
- **Microsoft.Owin.Host.SystemWeb** (4.2.2) - OWIN host for IIS
- **Microsoft.Owin.Security** (4.2.2) - OWIN security components
- **Microsoft.Owin.Security.Cookies** (4.2.2) - Cookie authentication
- **Microsoft.Owin.Security.OpenIdConnect** (4.2.2) - OpenID Connect authentication
- **System.IdentityModel.Tokens.Jwt** (5.1.2) - JWT token handling
- **Microsoft.IdentityModel.Protocols.OpenIdConnect** (2.1.2) - OpenID Connect protocols

## Authentication Flow

1. User navigates to the application
2. For protected pages, user is redirected to Azure Entra ID login
3. User authenticates with their Azure credentials
4. Azure Entra ID redirects back with an ID token
5. OWIN middleware validates the token and creates a cookie
6. User is authenticated and can access protected resources
7. User can sign out, which clears the cookie and notifies Azure Entra ID

## Deployment to Azure App Service

### App Service Configuration

When deploying to Azure App Service, update the redirect URIs in your Azure Entra ID app registration to include your App Service URL:

1. Go to your app registration in Azure Portal
2. Navigate to **Authentication**
3. Add redirect URIs:
   - `https://your-app-name.azurewebsites.net/`
4. Update logout URL:
   - `https://your-app-name.azurewebsites.net/`

### App Settings

In Azure App Service, configure the application settings:

1. Go to your App Service in Azure Portal
2. Navigate to **Configuration** > **Application settings**
3. Add the following settings:
   - `ida:TenantId` = Your tenant ID
   - `ida:ClientId` = Your client ID
   - `ida:Authority` = `https://login.microsoftonline.com/{YOUR_TENANT_ID}`
   - `ida:RedirectUri` = `https://your-app-name.azurewebsites.net/`
   - `ida:PostLogoutRedirectUri` = `https://your-app-name.azurewebsites.net/`

## Troubleshooting

### Common Issues

1. **HTTPS Required**: The application uses HTTPS for security. Make sure IIS Express is configured to use port 44300 with SSL.

2. **Missing Configuration**: If you see authentication errors, verify that all `ida:*` settings in Web.config are correctly configured.

3. **Token Validation Errors**: Ensure the tenant ID in the configuration matches your Azure Entra ID tenant.

4. **NuGet Package Restore**: If packages are missing, restore them using Visual Studio or `nuget restore`.

### Debugging Tips

- Check the browser's developer console for errors
- Review Application Event logs for OWIN-related errors
- Use Fiddler or browser dev tools to inspect authentication redirects
- Verify the app registration settings in Azure Portal match your configuration

## Security Considerations

- Always use HTTPS in production
- Store sensitive configuration values in Azure App Service configuration or Azure Key Vault
- Regularly update NuGet packages for security patches
- Implement proper error handling and logging
- Consider using Azure App Service authentication as an additional layer

## Migration Guide

When migrating an existing WebForms application:

1. Install the required OWIN NuGet packages
2. Add the `Startup.cs` file with OWIN configuration
3. Update `Web.config` with authentication mode and OWIN settings
4. Add authentication checks to pages requiring security
5. Update navigation and user interface to show authentication status
6. Test locally before deploying to Azure

## License

This project is provided as-is for demonstration and migration purposes.

## Resources

- [Azure Entra ID Documentation](https://docs.microsoft.com/azure/active-directory/)
- [OWIN Documentation](http://owin.org/)
- [ASP.NET WebForms Documentation](https://docs.microsoft.com/aspnet/web-forms/)
- [Azure App Service Documentation](https://docs.microsoft.com/azure/app-service/)
