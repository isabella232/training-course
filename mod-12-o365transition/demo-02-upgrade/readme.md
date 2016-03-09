# Submitting Subscription Transition Upgrade with Partner Center SDK

This demo shows how to submit a subscription upgrade using the Partner Center SDK.

## Partner Center Managed API

1. Obtain the SDK sample from: https://github.com/PartnerCenterSamples/Partner-Center-SDK-Samples
1. Show the following file for the authentication & setup process:
  - Open `\Context\ScenarioContext.cs`
  - `IScenarioContext.AppPartnerOperations()`
  - `IScenarioContext.UserPartnerOperations()`
  - `IScenarioContext.LoginUserToAad()`
1. Show the file for different service request actions:
  - `\Subscriptions\UpgradeSubscription.cs`

## Partner Center REST API

Open the [restapi-trace.saz](restapi-trace.saz) in Fiddler to look at a trace for the REST API call.