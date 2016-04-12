using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace Microsoft.Partner.CSP.Api.V1.Samples {
  class OfferCatalogPartnerCenterApi {
    public static dynamic GetOfferCategories(string countryCode, string token) {

      // create endpoint to the Partner Center REST API
      var endpoint = string.Format("{0}/v1/offercategories?country={1}",
                                    ConfigurationManager.AppSettings["PartnerCenterApiEndpoint"],
                                    countryCode);
      
      // create request with common headers
      var request = (HttpWebRequest)HttpWebRequest.Create(endpoint);
      request.Method = "GET";
      request.Accept = "application/json";

      // set HTTP request headers
      request.Headers.Add("MS-Contract-Version", "v1");
      request.Headers.Add("X-Locale", "en-US");
      request.Headers.Add("MS-CorrelationId", Guid.NewGuid().ToString());
      request.Headers.Add("MS-RequestId", Guid.NewGuid().ToString());
      
      // set the authorization header to include the oauth2 access token
      request.Headers.Add("Authorization", "Bearer " + token);

      try {
        Utilities.PrintWebRequest(request, string.Empty);

        // issue request
        var response = request.GetResponse();
        using (var reader = new StreamReader(response.GetResponseStream())) {
          var responseContent = reader.ReadToEnd();
          Utilities.PrintWebResponse((HttpWebResponse)response, responseContent);

          // convert response to strongly typed [dynamic] object
          var offerCategoriesResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);

          // loop through each category and write to to the console
          foreach (var offerCategory in offerCategoriesResponse.items) {
            PrintOfferCategory(offerCategory);
          }

          // return all categories to caller
          return offerCategoriesResponse;
        }
      } catch (WebException webException) {
        using (var reader = new StreamReader(webException.Response.GetResponseStream())) {
          var responseContent = reader.ReadToEnd();
          Utilities.PrintErrorResponse((HttpWebResponse)webException.Response, responseContent);
        }
      }
      return string.Empty;
    }

    public static dynamic GetOffers(string offerCategoryId, string countryCode, string token) {

      // create endpoint to the Partner Center REST API
      var endpoint = string.Format("{0}/v1/offers?country={1}&offer_category_id={2}", 
                                    ConfigurationManager.AppSettings["PartnerCenterApiEndpoint"], 
                                    countryCode, 
                                    offerCategoryId);

      // create request with common headers
      var request = (HttpWebRequest)HttpWebRequest.Create(endpoint);
      request.Method = "GET";
      request.Accept = "application/json";

      // set HTTP request headers
      request.Headers.Add("MS-Contract-Version", "v1");
      request.Headers.Add("X-Locale", "en-US");
      request.Headers.Add("MS-CorrelationId", Guid.NewGuid().ToString());
      request.Headers.Add("MS-RequestId", Guid.NewGuid().ToString());

      // set the authorization header to include the oauth2 access token
      request.Headers.Add("Authorization", "Bearer " + token);

      try {
        Utilities.PrintWebRequest(request, string.Empty);

        // issue request
        var response = request.GetResponse();
        using (var reader = new StreamReader(response.GetResponseStream())) {
          var responseContent = reader.ReadToEnd();
          Utilities.PrintWebResponse((HttpWebResponse)response, responseContent);

          // convert response to strongly typed [dynamic] object
          var offerResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);

          // loop through each category and write to to the console
          foreach (var offer in offerResponse.items) {
            PrintOffer(offer);
          }

          // return all offers to caller
          return offerResponse;
        }
      } catch (WebException webException) {
        using (var reader = new StreamReader(webException.Response.GetResponseStream())) {
          var responseContent = reader.ReadToEnd();
          Utilities.PrintErrorResponse((HttpWebResponse)webException.Response, responseContent);
        }
      }
      return string.Empty;
    }


    public static void PrintOfferCategory(dynamic offerCategory) {
      Console.ForegroundColor = ConsoleColor.DarkGreen;
      Console.WriteLine("=========================================");
      Console.WriteLine("Offer Category Information");
      Console.WriteLine("=========================================");
      Console.WriteLine("Id\t\t: {0}", offerCategory.id);
      Console.WriteLine("Category\t: {0}", offerCategory.category);
      Console.WriteLine("Rank\t\t: {0}", offerCategory.rank);

      Console.WriteLine("=========================================");
      Console.ResetColor();
    }

    public static void PrintOffer(dynamic offer) {
      Console.ForegroundColor = ConsoleColor.DarkGreen;
      Console.WriteLine("=========================================");
      Console.WriteLine("Offer Information");
      Console.WriteLine("=========================================");
      Console.WriteLine("Id\t\t: {0}", offer.id);
      Console.WriteLine("Name\t\t: {0}", offer.name);
      Console.WriteLine("Desc\t\t: {0}", offer.description);
      Console.WriteLine("Range\t\t: {0} - {1}", offer.minimumQuantity, offer.maximumQuantity);
      Console.WriteLine("Uri\t\t: {0}", offer.uri);

      Console.WriteLine("=========================================");
      Console.ResetColor();
    }
  }
}
