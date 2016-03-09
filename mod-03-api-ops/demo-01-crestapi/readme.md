# Updating CREST API Console App with Partner Center API

This demo shows how to take an existing console application built to use the CREST API and extend it to use the Partner Center API.

> This same example is provided as a hands-on lab that students can do. See [HOL #2: Using the Partner Center SDK with an existing CREST API Based Application](../../hol-02-pcapi-crest). Refer to that lab for the full steps... the steps outlined below are the shortened version

## Update Existing CREST API to Get Access Token for Partner Center

1. Get following values from Partner Center website:
  - Organization Profile / Microsoft ID
  - Organization Profile / Default Domain
  - Account Settings / API / App ID
  - Account Settings / API / Account ID
  - Account Settings / API / Key
  - Customers / Microsoft ID
1. Open the project in the [crest-app](crest-app) folder.
1. Update `App.config` with values from above
1. Add following setting to `App.config`:

  ```xml
  <add key="PartnerCenterApiEndpoint" value="https://api.partnercenter.microsoft.com" />
  ```

1. Add JSON.NET (Newtonsoft.Json) NuGet package to project
1. Add **ASP.NET Web Helpers Library** NuGet package to project
1. Add new file [OfferCatalogPartnerCenterApi.cs](OfferCatalogPartnerCenterApi.cs) to project
1. Within `Reseller.cs`:
  1. Add `using` statements:

    ```c#
    using System.Configuration;
    using Newtonsoft.Json;
    ```

  1. Add the two methods from [Reseller.cs](Reseller.cs) to the existing `Reseller.cs` file
1. Within the `Program.cs` file:
  1. Add the following static field:

    ```c#
    private static AuthorizationToken partnerCenterApiAuthorizationToken { get; set; }
    ```

  1. Add following to the `try-catch` block in `Main()` after obtaining AAD token to get Partner token:

    ```c#
    // Get Partner Center API access token
    partnerCenterApiAuthorizationToken = Reseller.GetPartnerCenterApi_Token(adAuthorizationToken, appId);
    ```

  1. Add following code to get all offers:

    ```c#
    // get all offer categories
    var offerCategoriesResponse = OfferCatalogPartnerCenterApi.GetOfferCategories("US", partnerCenterApiAuthorizationToken.AccessToken);

    // write the number of offers in each category out to the console
    foreach (var offerCategory in offerCategoriesResponse.items) {
    var offersResponse = OfferCatalogPartnerCenterApi.GetOffers(offerCategory.id.ToString(), 
                                                                "US", 
                                                                partnerCenterApiAuthorizationToken.AccessToken);
    Console.WriteLine("Offers in {0} is {1}", offerCategory.name.ToString(), offersResponse.totalCount);
    }
    ```

  1. Add a `Console.ReadLine();` top stop the app at the end

Run & test... this shows how we are now authenticating & getting an access token for Partner Center