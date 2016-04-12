using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;
using Microsoft.Store.PartnerCenter.Models.CountryValidationRules;
using pcsdk_validation.Models;

namespace pcsdk_validation.Utilities {
  public class CustomerValidator {
    public MyCustomer Customer { get; private set; }
    public List<string> ValidationErrors { get; private set; }
    private CountryValidationRules CountryValidationRules;

    private CustomerValidator(MyCustomer customer) {
      Customer = customer;
      ValidationErrors = new List<string>();
    }

    public static async Task<CustomerValidator> CreateAsync(MyCustomer customer) {
      var validator = new CustomerValidator(customer);

      // get country validation rules
      await validator.InitValidationRules(customer.BillingProfile.Address.Country);

      return validator;
    }

    public bool IsValid
    {
      get
      {
        // check postal code & state for all address...
        var result = ValidatePostalCode() && ValidateState();
        // if US, check city & state combo for zip
        if (this.Customer.BillingProfile.Address.Country == "US") {
          var addressResult = ValidateAddress();
          if (!addressResult) {
            result = false;
          }
        }
        return result;
      }
    }

    public bool ValidatePostalCode() {
      bool result = true;

      // check if required...
      if (CountryValidationRules.IsPostalCodeRequired &&
          string.IsNullOrEmpty(Customer.BillingProfile.Address.PostalCode)) {
        result = false;
        ValidationErrors.Add("Postal code required for the country " + Customer.BillingProfile.Address.Country);
      }
      // if postal code format provided, then check if valid...
      if (!string.IsNullOrEmpty(CountryValidationRules.PostalCodeRegex)) {
        if (!Regex.IsMatch(Customer.BillingProfile.Address.PostalCode,
                           CountryValidationRules.PostalCodeRegex)) {
          result = false;
          ValidationErrors.Add("Postal code invalid for the country " + Customer.BillingProfile.Address.Country);
        }
      }

      return result;
    }

    public bool ValidateState() {
      bool result = true;

      // check if required...
      if (CountryValidationRules.IsStateRequired &&
          string.IsNullOrEmpty(Customer.BillingProfile.Address.State)) {
        result = false;
        ValidationErrors.Add("State is required for the country " + Customer.BillingProfile.Address.Country);
      }
      // if state options provided, check if matching one submitted
      if (CountryValidationRules.SupportedStatesList.Any()) {
        // find state
        var foundState = CountryValidationRules.SupportedStatesList.Any(x => x == Customer.BillingProfile.Address.State);
        if (!foundState) {
          result = false;
          ValidationErrors.Add("State provided is not valid for the country " + Customer.BillingProfile.Address.Country);
        }
      }

      return result;
    }

    public bool ValidateAddress() {
      bool result = true;

      // create request payload
      var zipLookup = new CityStateLookupRequest {
        USERID = "",
        ZipCode = new CityStateLookupRequestZipCode {
          ID = 0,
          Zip5 = this.Customer.BillingProfile.Address.PostalCode
        }
      };

      // build query
      XmlSerializer serializerRequest = new XmlSerializer(typeof(CityStateLookupRequest));
      var writer = new StringWriter();
      serializerRequest.Serialize(writer, zipLookup);
      var query = string.Format("http://production.shippingapis.com/ShippingAPI.dll?API=CityStateLookup&XML={0}", writer.ToString());

      // submit request
      CityStateLookupResponse uspsResponse;
      var httpRequest = WebRequest.Create(query);
      using (var reader = new StreamReader(httpRequest.GetResponse().GetResponseStream())) {
        XmlSerializer serializerResponse = new XmlSerializer(typeof(CityStateLookupResponse));
        uspsResponse = (CityStateLookupResponse)serializerResponse.Deserialize(new StringReader(reader.ReadToEnd()));
      }

      // check if city & state match zip
      if (this.Customer.BillingProfile.Address.City != uspsResponse.ZipCode.City) {
        ValidationErrors.Add("City provided is not valid for the specified postal code " + Customer.BillingProfile.Address.PostalCode);
        result = false;
      }
      if (this.Customer.BillingProfile.Address.State != uspsResponse.ZipCode.State) {
        ValidationErrors.Add("State provided is not valid for the specified postal code " + Customer.BillingProfile.Address.PostalCode);
        result = false;
      }

      return result;
    }

    private async Task InitValidationRules(string countryCode) {
      var partner = await PcAuthHelper.GetPartnerCenterOps();
      CountryValidationRules = await partner.CountryValidationRules.ByCountry(countryCode).GetAsync();
    }
  }
}