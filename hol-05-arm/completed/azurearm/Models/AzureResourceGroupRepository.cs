using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using azurearm.Utilities;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Helpers;
using Newtonsoft.Json;

namespace azurearm.Models {
  public class AzureResourceGroupRepository {

    public static async Task<List<AzureResourceGroup>> GetAzureResourceGroups() {
      // get access token
      var accessToken = await AadAuthHelper.GetAadAuthToken(SettingsHelper.AzureManagementApiResourceId);

      // setup authenticated http client
      var httpClient = new HttpClient();
      httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
      httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

      // create request
      var azQueryPath = string.Format("subscriptions/{0}/resourceGroups/?api-version=2014-04-01", SettingsHelper.AzureSubscriptionId);
      var query = string.Format(SettingsHelper.AzureManagementApiEndpoint, azQueryPath);
      var request = new HttpRequestMessage(HttpMethod.Get, query);

      // issue request and convert response to typed object
      var response = await httpClient.SendAsync(request);
      var rawResponseString = await response.Content.ReadAsStringAsync();
      var azrgResponse = JsonConvert.DeserializeObject<AzureResourceGroups>(rawResponseString);

      // convert to normal collection
      List<AzureResourceGroup> resourceGroups = new List<AzureResourceGroup>();
      resourceGroups.AddRange(azrgResponse.Items);

      return resourceGroups;
    }

    public static async Task CreateResourceGroup(string location, string groupName) {
      // get access token
      var accessToken = await AadAuthHelper.GetAadAuthToken(SettingsHelper.AzureManagementApiResourceId);

      // create request
      var azQueryPath = string.Format("subscriptions/{0}/resourcegroups/{1}/?api-version=2016-02-01", SettingsHelper.AzureSubscriptionId, groupName);
      var query = string.Format(SettingsHelper.AzureManagementApiEndpoint, azQueryPath);
      var request = (HttpWebRequest)HttpWebRequest.Create(query);
      request.Method = "PUT";
      request.ContentType = "application/json";
      request.Accept = "application/json";
      request.Headers.Add("Authorization", "Bearer " + accessToken);

      // create payload
      var newAzResourceGroup = new AzureResourceGroup {
        Location = location
      };
      string content = JsonConvert.SerializeObject(newAzResourceGroup,
        Formatting.None,
        new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
      );
      using (var writer = new StreamWriter(request.GetRequestStream())) {
        writer.Write(content);
      }

      // submit request
      await request.GetResponseAsync();
    }

    public static async Task<List<AzureResourceGroupItem>> GetResourceGroupVMs(string groupName) {
      List<AzureResourceGroupItem> contents = new List<AzureResourceGroupItem>();

      // get classic VMs
      contents.AddRange(await GetClassicVms(groupName));
      // get ARM VMs
      contents.AddRange(await GetArmVms(groupName));
      return contents;
    }

    public static async Task CreateTemplateDeployment(string resourceGroup, string vmType, string adminUsername, string adminPassword, string dnsName) {
      // create a new template deployment...
      var armTemplateDeploymentRequest = new ArmTemplateDeploymentRequest {
        Properties = new ArmTemplateDeploymentRequestProperties {
          Mode = "Incremental",
          TemplateLink = new Templatelink {
            ContentVersion = "1.0.0.0"
          },
          Parameters = new ArmTemplateDeploymentRequestParameters {
            AdminUsername = new TemplateParameter { Value = adminUsername },
            AdminPassword = new TemplateParameter { Value = adminPassword },
            DnsLabel = new TemplateParameter { Value = dnsName }
          }
        }
      };

      // add the template & OS version depending on the template
      if (vmType == "windows") {
        armTemplateDeploymentRequest.Properties.TemplateLink.Uri = "https://raw.githubusercontent.com/Azure/azure-quickstart-templates/master/101-vm-simple-windows/azuredeploy.json";
        armTemplateDeploymentRequest.Properties.Parameters.WindowsOsVersion = new TemplateParameter { Value = "2008-R2-SP1" };
      } else {
        armTemplateDeploymentRequest.Properties.TemplateLink.Uri = "https://raw.githubusercontent.com/Azure/azure-quickstart-templates/master/101-vm-simple-linux/azuredeploy.json";
        armTemplateDeploymentRequest.Properties.Parameters.UbuntuOsVersion = new TemplateParameter { Value = "14.04.2-LTS" };
      }

      // get access token
      var accessToken = await AadAuthHelper.GetAadAuthToken(SettingsHelper.AzureManagementApiResourceId);

      // create request
      var azQueryPath = string.Format("subscriptions/{0}/resourcegroups/{1}/providers/microsoft.resources/deployments/{2}/?api-version=2016-02-01",
        SettingsHelper.AzureSubscriptionId,
        resourceGroup,
        Guid.NewGuid().ToString());
      var query = string.Format(SettingsHelper.AzureManagementApiEndpoint, azQueryPath);
      var request = (HttpWebRequest)HttpWebRequest.Create(query);
      request.Method = "PUT";
      request.ContentType = "application/json";
      request.Accept = "application/json";
      request.Headers.Add("Authorization", "Bearer " + accessToken);

      // create payload
      string content = JsonConvert.SerializeObject(armTemplateDeploymentRequest,
        Formatting.None,
        new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
      );
      using (var writer = new StreamWriter(request.GetRequestStream())) {
        writer.Write(content);
      }

      // submit request
      await request.GetResponseAsync();
    }

    private static async Task<IEnumerable<AzureResourceGroupItem>> GetClassicVms(string groupName) {
      // get access token
      var accessToken = await AadAuthHelper.GetAadAuthToken(SettingsHelper.AzureManagementApiResourceId);

      // setup authenticated http client
      var httpClient = new HttpClient();
      httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
      httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

      // create request to get all classic VMs
      var azQueryPath = string.Format("subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.ClassicCompute/virtualMachines/?api-version=2015-06-01", SettingsHelper.AzureSubscriptionId, groupName);
      var query = string.Format(SettingsHelper.AzureManagementApiEndpoint, azQueryPath);
      var request = new HttpRequestMessage(HttpMethod.Get, query);

      // issue request and convert response to typed object
      var response = await httpClient.SendAsync(request);
      var rawResponseString = await response.Content.ReadAsStringAsync();
      var azResponse = JsonConvert.DeserializeObject<AzureVirtualMachines>(rawResponseString);

      // convert to normal collection
      List<AzureResourceGroupItem> contents = new List<AzureResourceGroupItem>();
      foreach (var item in azResponse.Items) {
        contents.Add(new AzureResourceGroupItem {
          Id = item.id,
          Type = "Virtual Machine (Classic)",
          Name = item.name,
          Location = item.location
        });
      }

      return contents;
    }

    private static async Task<IEnumerable<AzureResourceGroupItem>> GetArmVms(string groupName) {
      // get access token
      var accessToken = await AadAuthHelper.GetAadAuthToken(SettingsHelper.AzureManagementApiResourceId);

      // setup authenticated http client
      var httpClient = new HttpClient();
      httpClient.DefaultRequestHeaders.Add("Accept", "appliscation/json");
      httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

      // create request to get all classic VMs
      var azQueryPath = string.Format("subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.Compute/virtualMachines/?api-version=2016-03-30", SettingsHelper.AzureSubscriptionId, groupName);
      var query = string.Format(SettingsHelper.AzureManagementApiEndpoint, azQueryPath);
      var request = new HttpRequestMessage(HttpMethod.Get, query);

      // issue request and convert response to typed object
      var response = await httpClient.SendAsync(request);
      var rawResponseString = await response.Content.ReadAsStringAsync();
      var azResponse = JsonConvert.DeserializeObject<AzureVirtualMachines>(rawResponseString);

      // convert to normal collection
      List<AzureResourceGroupItem> contents = new List<AzureResourceGroupItem>();
      foreach (var item in azResponse.Items) {
        contents.Add(new AzureResourceGroupItem {
          Id = item.id,
          Type = "Virtual Machine",
          Name = item.name,
          Location = item.location
        });
      }

      return contents;
    }
  }
}