using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Store.PartnerCenter;
using Microsoft.Store.PartnerCenter.Exceptions;
using Microsoft.Store.PartnerCenter.Extensions;
using Microsoft.Store.PartnerCenter.Models.Query;
using Microsoft.Store.PartnerCenter.Models;
using Microsoft.Store.PartnerCenter.Models.Customers;
using Microsoft.Store.PartnerCenter.Models.Orders;
using System.Globalization;

namespace partnercentersdk {
  class Program {
    static void Main(string[] args) {
      // login to AAD
      var aadAuthenticationResult = Program.LoginToAad().Result;

      // set the partner service to use public API (early SDK builds use internal APIs)
      PartnerService.Instance.ApiRootUrl = SettingsHelper.PartnerCenterApiEndpoint;

      // obtain the partner service credentials
      var authToken = new AuthenticationToken(aadAuthenticationResult.AccessToken, aadAuthenticationResult.ExpiresOn);
      IPartnerCredentials credentials = PartnerCredentials.Instance.GenerateByUserCredentials(SettingsHelper.ClientId, authToken);

      // gain access to the partner APIs
      var partner = PartnerService.Instance.CreatePartnerOperations(credentials);

      // get the first 10 customers for this partner
      int i = 0, customerCount = 10;
      var customers = partner.Customers.Query(QueryFactory.Instance.BuildIndexedQuery(customerCount));

      // create the report and append the header row
      StringBuilder report = new StringBuilder();

      report.AppendFormat("\nCustomer ID, Name & Domain:\n");
      foreach (var customer in customers.Items) {
        try {
          Console.Write(string.Format("Exporting Customer {0} of {1}...", ++i, customerCount));
          report.AppendFormat("{0}, {1}, {2}\n", customer.Id, customer.CompanyProfile.CompanyName, customer.CompanyProfile.Domain);

          // get the current customer subscriptions
          var subscriptions = partner.Customers.ById(customer.Id).Subscriptions.Get();

          report.AppendFormat("  Subscription ID, name & quantity\n");
          foreach (var subscription in subscriptions.Items) {
            // add the joined customer and subscription row to the report
            report.AppendFormat("  {0}, {1}, {2}\n", subscription.Id, subscription.FriendlyName, subscription.Quantity);
          }

          Console.ForegroundColor = ConsoleColor.Green;
          Console.WriteLine("Success!");
        } catch (PartnerException) {
          // there are some 403 error related to Commerce and BEC not being linked in PPE, ignore these
          Console.ForegroundColor = ConsoleColor.Red;
          Console.WriteLine("Error");
        }

        Console.ForegroundColor = ConsoleColor.White;
      }

      // export the report!
      Console.WriteLine(report);

      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("Press any key to continue...");
      Console.ReadLine();

      // create a customer
      //try {
      //  Console.ForegroundColor = ConsoleColor.White;
      //  Console.WriteLine("Creating a new customer...");
      //  var newCustomer = Program.CreateCustomer(partner);
      //  Console.ForegroundColor = ConsoleColor.Green;
      //  Console.WriteLine("Success!");
      //} catch (Exception ex) {
      //  Console.ForegroundColor = ConsoleColor.Red;
      //  Console.WriteLine("Error");
      //}

      // get all office365 offers
      GetOffers(partner);

      PlaceOrder(partner, "98aeda04-cd35-4dff-88ad-2b5be98e8e3a");
    }

    private static Customer CreateCustomer(IAggregatePartner partnerOperations) {
      var domainPrefix = "fabrikam01";

      Customer newCustomer = new Customer() {
        CompanyProfile = new CustomerCompanyProfile() {
          Domain = string.Format(CultureInfo.InvariantCulture, "{0}.onmicrosoft.com", domainPrefix),
          CompanyName = "Fabrikam"
        },
        BillingProfile = new CustomerBillingProfile() {
          Culture = "EN-US",
          Language = "En",
          FirstName = "Janice",
          LastName = "Galvin",
          Email = string.Format("janice.galvin@{0}.onmicrosoft.com", domainPrefix),
          CompanyName = "Fabrikam",

          DefaultAddress = new Address() {
            FirstName = "Janice",
            LastName = "Galvin",
            AddressLine1 = "1 Microsoft Way",
            AddressLine2 = "Building 1",
            City = "Redmond",
            State = "WA",
            Country = "US",
            PostalCode = "98052",
            PhoneNumber = "425-555-1212"
          }
        }
      };

      return partnerOperations.Customers.Create(newCustomer);
    }

    private static void GetOffers(IAggregatePartner partnerOperations) {

      var offers = partnerOperations.Offers.ByCountry("US").Get(0, 25);

      StringBuilder report = new StringBuilder();
      report.Append("OfferID, Name, Category\n");
      foreach (var offer in offers.Items) {
        report.AppendFormat(" {0}, {1}, {2}\n", offer.Id, offer.Category.Name, offer.Name);
      }

      // write report
      Console.ForegroundColor = ConsoleColor.White;
      Console.WriteLine("Getting available offers...");
      Console.WriteLine(report);
    }

    public static void PlaceOrder(IAggregatePartner partnerOperations, string customerId) {

      // create the order line items
      var lineItems = new List<OrderLineItem>();
      lineItems.Add(new OrderLineItem {
        LineItemNumber = 0,
        OfferId = "031C9E47-4802-4248-838E-778FB1D2CC05",
        FriendlyName = "Office 365 Business Premium",
        Quantity = 1
      });

      // create the order
      var order = new Order {
        ReferenceCustomerId = customerId,
        LineItems = lineItems
      };

      // submit the order
      Console.ForegroundColor = ConsoleColor.White;
      Console.WriteLine("Placing order...");

      try {
        partnerOperations.Customers.ById(customerId).Orders.Create(order);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Successully placed order!");
      } catch (Exception ex) {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Error occured", ex);
      }
    }

    private static async Task<AuthenticationResult> LoginToAad() {
      // get the login authority from AAD
      var addAuthority = new UriBuilder(SettingsHelper.AadAuthority);

      // create a user cred object to acquire a token
      UserCredential userCredentials = new UserCredential(SettingsHelper.UserId,
                                                          SettingsHelper.UserPassword);

      // create authcontext from ADAL
      AuthenticationContext authContext = new AuthenticationContext(addAuthority.Uri.AbsoluteUri);

      // request an access token from AAD's token endpoint
      //  for the partner center resourceId (the ID of the Partner Center API endpoint) 
      //  for the specified AAD application & user
      return await authContext.AcquireTokenAsync(SettingsHelper.PartnerCenterApiResourceId,
                                                 SettingsHelper.ClientId,
                                                 userCredentials);
    }

  }
}
