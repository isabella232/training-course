using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

using Newtonsoft.Json;

namespace azurearm.Models {
  public class AzureResourceGroups {
    [JsonProperty(PropertyName = "value")]
    public AzureResourceGroup[] Items { get; set; }
  }

  public class AzureResourceGroup {
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }

    [DisplayName("Resource Group Name")]
    [JsonProperty(PropertyName = "Name")]
    public string Name { get; set; }

    [DisplayName("Azure Region Location")]
    [JsonProperty(PropertyName = "location")]
    public string Location { get; set; }

    [JsonProperty(PropertyName = "properties")]
    public Properties Properties { get; set; }
  }

  public class Properties {
    [JsonProperty(PropertyName = "provisioningState")]
    public string ProvisioningState { get; set; }
  }
}