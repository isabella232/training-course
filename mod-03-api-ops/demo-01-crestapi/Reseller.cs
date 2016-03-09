/// <summary>
/// Get the latest partner center api token given the AD Authorization Token
/// </summary>
/// <param name="adAuthorizationToken">AD Authorization Token</param>
/// <param name="clientId">clientID of the application</param>
/// <param name="partnerCenterApiAuthorizationToken">partner center api authorization token, can be null</param>
/// <returns>Latest partner center api authorization token</returns>
public static AuthorizationToken GetPartnerCenterApi_Token(AuthorizationToken adAuthorizationToken, string clientId, AuthorizationToken partnerCenterApiAuthorizationToken = null) {
  if (partnerCenterApiAuthorizationToken == null ||
      (partnerCenterApiAuthorizationToken != null && partnerCenterApiAuthorizationToken.IsNearExpiry())
      ) {
    //// Refresh the token on one of two conditions
    //// 1. If the token has never been retrieved
    //// 2. If the token is near expiry

    var partnerCenterApiToken = GetPartnerCenterApi_Token(adAuthorizationToken.AccessToken, clientId);
    var accessToken = partnerCenterApiToken.access_token.ToString();
    var expiresInOffset = System.Convert.ToInt64(partnerCenterApiToken.expires_in);
    partnerCenterApiAuthorizationToken = new AuthorizationToken(accessToken, expiresInOffset);
  }

  return partnerCenterApiAuthorizationToken;
}

/// <summary>
/// Given the ad token this method retrieves the partner center api token for accessing any of the partner apis
/// </summary>
/// <param name="adToken">this is the access_token we get from AD</param>
/// <param name="clientId">AppId from the azure portal registered for this app</param>
/// <returns>the partner center api token object which contains access_token, expiration duration</returns>
private static dynamic GetPartnerCenterApi_Token(string adToken, string clientId) {
  // create endpoint to the Partner Center REST API to get a token
  var endpoint = string.Format("{0}/GenerateToken", ConfigurationManager.AppSettings["PartnerCenterApiEndpoint"]);

  // create request with common headers
  var request = (HttpWebRequest)WebRequest.Create(endpoint);
  request.Method = "POST";
  request.ContentType = "application/x-www-form-urlencoded";
  request.Accept = "application/json";

  // set HTTP request headers
  request.Headers.Add("MS-Contract-Version", "v1");
  request.Headers.Add("MS-CorrelationId", Guid.NewGuid().ToString());
  request.Headers.Add("MS-RequestId", Guid.NewGuid().ToString());

  // set the authorization header to include the oauth2 access token
  request.Headers.Add("Authorization", "Bearer " + adToken);

  // create the payload for the request
  string content = string.Format("grant_type=jwt_token&client_id={0}", clientId);
  using (var writer = new StreamWriter(request.GetRequestStream())) {
    writer.Write(content);
  }

  try {
    Utilities.PrintWebRequest(request, content);

    // issue request
    var response = request.GetResponse();
    using (var reader = new StreamReader(response.GetResponseStream())) {
      var responseContent = reader.ReadToEnd();
      Utilities.PrintWebResponse((HttpWebResponse)response, responseContent);

      // convert response to strongly typed [dynamic] object
      return JsonConvert.DeserializeObject<dynamic>(responseContent);
    }
  } catch (WebException webException) {
    using (var reader = new StreamReader(webException.Response.GetResponseStream())) {
      var responseContent = reader.ReadToEnd();
      Utilities.PrintErrorResponse((HttpWebResponse)webException.Response, responseContent);
    }
  }
  return string.Empty;
}
