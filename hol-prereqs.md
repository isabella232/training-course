# Hands-On Lab Prerequisites

In order to complete the hands-on labs in this course you must meet the following prerequisites:

1. Completed necessary paperwork & accepted in the [Microsoft Cloud Solution Program (CSP)](https://mspartner.microsoft.com/en/us/pages/solutions/cloud-reseller-overview.aspx).
  - For more information on becoming a partner, refer to the [Partner Center](https://partnercenter.microsoft.com/en-us/partner/programs#nextstep) website.
1. Have access to the [Partner Center](https://partnercenter.microsoft.com) website & your partner ID.
  - *An Integration Sandbox account can be used with the hands-on labs.*
1. Have access to your partner Azure Active Directory (AAD) tenant.
1. Developer environment with Visual Studio 2015 with Update 1 installed & configured.
  - *If you don't have a Windows machine, you can use one of the virtual machine images provided in Microsoft Azure: [Microsoft Windows & Visual Studio 2015 Azure Virtual Machine Image Gallery](https://azure.microsoft.com/en-us/marketplace/virtual-machines/all/?operatingSystem=acom-windows&publisherType=acom-microsoft&term=visual+studio+2015)*

## Collecting Common Partner Center Details

Custom solutions that leverage the Partner Center SDK require different credentials depending on the scenario. Throughout this course and the hands-on labs, references are made to various user & app credentials as well as other IDs. The following sections explain where you can find this information.

You can use your production Partner Center account or your Integration Sandbox account for all of the following values

### Partner's MPN ID

The **MPN ID** is the partner's *Microsoft Partner Number*.

1. Browse to the Partner Center, https://partnercetner.microsoft.com, and login using your credentials.
1. Select **Account Settings** in the left-hand menu.
1. Locate the **MPN ID**, a 7-digit number under the **CSP Program Info** section.

### Partner Account ID (aka: AAD Tenant ID / Microsoft ID)

The **Partner Account ID** is also referred to as the partner's *Microsoft ID*. This is actually the ID, in the form of a GUID, of the partner's *Azure Active Directory (AAD) tenant*.

1. Browse to the Partner Center, https://partnercetner.microsoft.com, and login using your credentials.
1. Select **Account Settings** in the left-hand menu.
1. Select **Organization Profile** in the left-hand menu.
1. Locate the **Microsoft ID** on the **Organization Profile** page.

### Partner Center Registered App Credentials

Your custom applications are registered with your partner's Azure AD (AAD). You can associate one of these applications with Partner Center.

There are two parts to the registered app's credentials:
- **App ID**: The unique ID for the application (*like a user's login or username*).
- **App Key (aka: secret)**: The password for the application (*like a user's password*). This is not used in certain types of apps, such as native apps.

#### Using Partner Center

> NOTE: the Partner Center app registration page supports web applications but not native applications. To app credentials for native apps, use the Azure Management Portal.

1. Browse to the Partner Center, https://partnercetner.microsoft.com, and login using your credentials.
1. Select **Account Settings** in the left-hand menu.
1. Select **API** in the left-hand menu.
1. Locate the **App ID** under the **Registered Azure AD App** section.
1. Locate the **Key Management** section. The value of a key can never be retrieved once it has been created. You can create as many keys as you like, just make sure to copy the key value before leaving the page.

#### Using Azure Management Portal

1. Browse to the Azure Management Portal, https://manage.windowsazure.com, and login using the credentials for an Azure subscription.
1. Using the left-hand navigation, locate and select **Active Directory**.
1. Select the name of the directory for your Partner Center account.

  > You can find the name of your tenant from the **Partner Center &raquo; Accoutn Settings &raquo; Partner Profile**. It is listed as the **Organization Name** under the **Cloud Solution Provider Profile** column.

1. Select **Applications** in the top-navigation menu.
1. Set the filter control to **Show: Applications my company owns** and click the **Check** to update the view.
1. Select the desired application.
1. Locate the **Client ID** GUID, which is also known as the App ID.
1. Locate the **Keys** section. The value of a key can never be retrieved once it has been created. You can create as many keys as you like, just make sure to copy the key value before leaving the page.