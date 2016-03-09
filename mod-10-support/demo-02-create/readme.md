# Creating Service Requests with Partner Center SDK

This demo shows how to create service requests using the Partner Center SDK.

## Partner Center Managed API

1. Obtain the SDK sample from: https://github.com/PartnerCenterSamples/Partner-Center-SDK-Samples
1. Show the following file for the authentication & setup process:
  - Open `\Context\ScenarioContext.cs`
  - `IScenarioContext.AppPartnerOperations()`
  - `IScenarioContext.UserPartnerOperations()`
  - `IScenarioContext.LoginUserToAad()`
1. Show the file for different service request actions:
  - `\ServiceRequests\CreatePartnerServiceRequest.cs`
  - `\ServiceRequests\UpdatePartnerServiceRequest.cs`

## Partner Center REST API

Open the [restapi-trace-customer.saz](restapi-trace-customer.saz) & [restapi-trace-partner.saz](restapi-trace-partner.saz) in Fiddler to look at a trace.