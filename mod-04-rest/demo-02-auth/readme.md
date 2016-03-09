# Authenticating with the REST APIs

1. Inspect the Fiddler trace in [rest-auth-flow.saz](rest-auth-flow.saz)
1. Open the completed solution from the hands on lab #1 [Introduction to the Partner Center SDK](../../hol-01-intro-pcsdk) & explain the authentication code.

  Don't use, but show how you can use the [EfAdalCache.cs](EfAdalCache.cs). Explain benefit & how ADAL uses the token cache & how `AcquireTokenSilentlyAsync()` works compared to `AcquireTokenAsync()`.

    ```c#
    EfAdalTokenCache sampleCache = new EfAdalTokenCache(userId);
    AuthenticationContext authContext = new AuthenticationContext(aadAuthority, sampleCache);
    
    // AuthenticationResult result = await authContext.AcquireTokenAsync(resourceId, clientId, userCreds);
    
    AuthenticationResult result = await authContext.AcquireTokenSilentlyAsync(resourceId, clientId, userCreds);
    ```

  Sample project: https://github.com/andrewconnell/azadaspnetmvcauth