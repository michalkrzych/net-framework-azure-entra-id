# Copilot Instructions for net-framework-azure-entra-id

## Project Overview

This repository contains a .NET Framework application that integrates with Azure Entra ID (formerly Azure Active Directory) for authentication and authorization.

## Technology Stack

- **.NET Framework**: This project uses the .NET Framework (not .NET Core/.NET 5+)
- **Azure Entra ID**: Microsoft's cloud-based identity and access management service
- **Language**: C#

## Coding Standards

### General Guidelines

- Follow Microsoft C# coding conventions
- Use meaningful variable and method names
- Add XML documentation comments for public APIs
- Keep methods focused and concise (single responsibility principle)

### .NET Framework Specific

- Target the appropriate .NET Framework version consistently
- Be mindful of framework limitations compared to modern .NET
- Use `async`/`await` patterns where supported
- Follow traditional .NET Framework project structure

### Azure Entra ID Integration

- Use Microsoft Authentication Library (MSAL) for modern authentication flows
- Implement proper token caching and refresh mechanisms
- Handle authentication errors gracefully with appropriate user feedback
- Follow the principle of least privilege for API permissions
- Store sensitive configuration (client secrets, tenant IDs) in secure configuration files, not in source code

## Security Best Practices

- Never commit secrets, API keys, or sensitive credentials to the repository
- Use configuration files with placeholders for sensitive values
- Implement proper error handling that doesn't expose sensitive information
- Follow OWASP security guidelines for web applications
- Validate and sanitize all user inputs
- Use HTTPS for all external communications

## Build and Development

- Ensure builds complete without warnings
- Run all tests before committing changes
- Update dependencies carefully, testing thoroughly after updates
- Document any new dependencies or breaking changes

## Documentation

- Update README.md when adding new features or changing setup procedures
- Include setup instructions for Azure Entra ID app registration
- Document required API permissions and scopes
- Provide clear examples for common authentication scenarios

## Testing

- Write unit tests for business logic
- Include integration tests for Azure Entra ID authentication flows (where applicable)
- Mock external dependencies in unit tests
- Test both successful and error scenarios

## Common Patterns

When working with Azure Entra ID authentication:

1. **Token Acquisition**: Use MSAL to acquire tokens, not deprecated ADAL
2. **Token Storage**: Implement secure token caching
3. **Error Handling**: Implement proper retry logic for transient failures
4. **Scope Management**: Request only the minimum required scopes

## Dependencies

- Prefer NuGet packages from Microsoft for Azure/Entra ID integration
- Keep dependencies up-to-date with security patches
- Document the purpose of each major dependency
