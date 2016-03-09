# Debugging HTTP Requests with Fiddler

1. Explain common scenario:
  - Created application in Azure AD
  - Forgot to grant the application access to the Partner Center API
  - Ran code and got a permission denied like error...
  - Ran code again with Fiddler open and saw much more helpful error message
  - *Refer to trace [login-error_invalid-grant.saz](login-error_invalid-grant.saz)*
1. Show how to share Fiddler traces
  - Add filters
  - Remove sessions
  - Remove passwords & sensitive data from sessions