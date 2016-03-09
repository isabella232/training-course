# Sample Web App (Included with SDK)

This demo shows show to configure the sample console application available here: https://github.com/PartnerCenterSamples/Reseller-Web-Application

## Create a Web app Azure AD Application with AppOnly Permissions

Create a Web application in Azure AD with the Azure Management Portal or the Partner Center Dashboard and collect the **Client ID** and **Key**.

## Configure the Sample Web Application's Config Settings

1. Open the **PartnerSdkSampleApp** project in Visual Studio 2015
1. Open the `web.config` file
1. Set the following values in the `App.config` file:
  - **aad.commonDomain**: common
  - **aad.applicationId**: use the value *client ID* for the *app+only* app from above
  - **aad.applicationSecret**: use the value *key* for the *app+only* app from above
  - **aad.applicationDomain**: use the *AAD tenant ID*

## Inspect the Configuration Options Provided

1. Open the `Configuration\WebPortal\WebPortalConfiguration.json` file 
  1. Show how customizations setup
  1. Near line 531 notice how offers are provided
1. Look at the `Resources.resx` file for details on the strings used in the app
1. Launch web application
  1. Click around
  1. Go back to Partner Center and show list of customers
  1. Place an order for an offer
    - remember the username & password that you created
    - use valid address
    - test credit card: 1234123412341234 & CVN 256
  1. After creating, go look at Partner Center and verify customer is shown
  1. Log back into the application, and add / update subscriptions