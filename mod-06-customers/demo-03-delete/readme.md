# Deleting Customers with Partner Center SDK

This demo shows how to delete customers using the Partner Center SDK.

## Partner Center Managed API

1. Obtain the SDK sample from: https://github.com/PartnerCenterSamples/Partner-Center-SDK-Samples
1. Show the following file for the authentication & setup process:
  - Open `\Context\ScenarioContext.cs`
  - `IScenarioContext.AppPartnerOperations()`
  - `IScenarioContext.UserPartnerOperations()`
  - `IScenarioContext.LoginUserToAad()`
1. Show the file for different service request actions:
  - `Customers\DeleteCustomerFromTipAccount.cs`

## Partner Center REST API

1. Open the [restapi-trace.saz](restapi-trace.saz) in Fiddler to look at a trace for the REST API call.