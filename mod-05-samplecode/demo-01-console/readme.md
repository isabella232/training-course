# Sample Console App

This demo shows show to configure the sample console application available here: https://github.com/PartnerCenterSamples/Partner-Center-SDK-Samples

First, collect some information from the Partner Center website and create two Azure AD applications that you will need in configuring the application.

## Collect Information from Partner Center Website

1. Get following values from Partner Center website:
  - Partner Profile / MPN ID
  - Customers / [select a customer] / Microsoft ID

## Create a Web app Azure AD Application with AppOnly Permissions

Create a Web application in Azure AD with the Azure Management Portal or the Partner Center Dashboard and collect the **Client ID** and **Key**.

## Create a Native app Azure AD Application with User+App Permissions

Create a native application in Azure AD with the Azure Management Portal & onboard it with the Partner Center Dashboard and collect the **Client ID**.

## Configure the Sample Application's Config Settings

1. Open the console app project in Visual Studio 2015
1. Open the `App.config` file
1. Set the following values in the `App.config` file:
  - ParterServiceSettings
    - **PartnerServiceApiEndpoint**: https://api.partnercenter.microsoft.com
    - **AuthenticationAuthorityEndpoint**: https://login.microsoftonline.com
    - **GraphEndpoint**: https://graph.windows.net
    - **CommonDomain**: common
  - UserAuthentication
    - **ApplicationId**: use the value *client ID* for the native app from above
    - **ResourceUrl**: https://api.partnercenter.microsoft.com
    - **RedirectUri**: http://localhost
    - **UserName**: use the login name (email) you use to login to the Partner Center website
    - **Password**: use the password you use to login to the Partner Center website
  - AppAuthentication
    - **ApplicationId**: use the value *client ID* for the web app from above
    - **ApplicationSecret**: use the value *key* for the web app from above
    - **Domain**: use the *AAD tenant ID*

Run the application. Open Fiddler along the way and inspect each request being made.