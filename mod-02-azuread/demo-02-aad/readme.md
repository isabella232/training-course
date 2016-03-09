# Creating Azure AD Applications for use with the Partner Center

This demo shows how to create Azure AD applications for use in custom applications calling the Partner Center API.

## Create Native Azure AD Applications with the Azure Management Portal

1. Login to the Azure Management Portal (https://manage.windowsazure.com) using an account configured as the global administrator on your partner's Azure AD tenant.
1. Navigate to the Active Directory module
1. Create a new application by clicking the :heavy_plus_sign: **Add** button in the bottom gutter.
1. Use the following to answer the prompts:
  - Add an application my organization is developing
  - Native client application
  - Reply URL
1. Once the app is created, show different parts of the app:
  - Multi-tenant
  - Client ID
  - Keys
1. Add permissions to Partner Center
  1. Locate **Permissions to other applications**
  1. Update the **Windows Azure Active Directory** role to:
    - Access the directory as the signed-in user
    - Sign in and read user profile
  1. Add a new role:
    - Search for the ID `fa3d9a0c-3fb0-42cc-9193-47c7ecd2edbd` & add the **Microsoft Partner Center** role
    - Select the delegated permission **Access Partner Center**

At this point you now have an Azure AD application that's configured for user+app permissions.

## Create Web Azure AD Applications with the Azure Management Portal

1. Login to the Azure Management Portal (https://manage.windowsazure.com) using an account configured as the global administrator on your partner's Azure AD tenant.
1. Navigate to the Active Directory module
1. Create a new application by clicking the :heavy_plus_sign: **Add** button in the bottom gutter.
1. Use the following to answer the prompts:
  - Add an application my organization is developing
  - Web Application and/or Web Api
  - **Sign-on Url**: http://localhost:4321
  - **App ID URI**: enter a url for an Azure validated domain on your tenant
1. Once the app is created, show different parts of the app:
  - Client ID
1. Add permissions to Azure AD & Partner Center
  1. Locate **Permissions to other applications**
  1. Update the **Windows Azure Active Directory** role to:
    - Access the directory as the signed-in user
    - Sign in and read user profile
  1. Add a new role:
    - Search for the ID `fa3d9a0c-3fb0-42cc-9193-47c7ecd2edbd` & add the **Microsoft Partner Center** role
    - Select the delegated permission **Access Partner Center**

At this point you now have an Azure AD application for a custom web application.

## Create & Onboard AAD Applications with Partner Center's Dashboard

The Partner Center Dashboard can help with creating / onboarding applications.

1. Create an new AAD application with Partner Center: 
  1. Within a browser, navigate to the Partner Center dashboard, https://partnercenter.microsoft.com, and login using your Partner account’s credentials.
  1. In the left-hand navigation, select **Account Settings**.
  1. In the left-hand navigation, select **API**.
  1. If you already have an application selected, unregister it.
  1. Create an application
  1. Go to the Azure Management Portal and locate your application. Notice it already has the permissions setup correctly.
1. Now onboard a new application:
  1. Within the Azure Management Portal, follow the same steps above to create a Native AAD application, but do not configure the permissions.
  1. Navigate to the Partner Center dashboard, https://partnercenter.microsoft.com, and login using your Partner account’s credentials.
  1. In the left-hand navigation, select **Account Settings**.
  1. In the left-hand navigation, select **API**.
  1. If you already have an application selected, unregister it.
  1. On the API page, select the application you just created and click the **Register App** button.
  1. Go back to the Azure Management Portal and find the application... notice it now has the necessary permission to Partner Center.

At this point you have on-boarded the native Azure AD application with the Partner Center SDK. It now has the necessary permissions within your partner account & AAD tenant to use the SDK.
