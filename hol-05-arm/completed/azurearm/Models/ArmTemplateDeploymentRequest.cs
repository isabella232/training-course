using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace azurearm.Models {
  public class ArmTemplateDeploymentRequest {
    [JsonProperty(PropertyName = "Properties")]

    public ArmTemplateDeploymentRequestProperties Properties { get; set; }
  }

  public class ArmTemplateDeploymentRequestProperties {
    [JsonProperty(PropertyName = "templateLink")]
    public Templatelink TemplateLink { get; set; }
    [JsonProperty(PropertyName = "mode")]
    public string Mode { get; set; }
    [JsonProperty(PropertyName = "parameters")]
    public ArmTemplateDeploymentRequestParameters Parameters { get; set; }
  }

  public class Templatelink {
    [JsonProperty(PropertyName = "uri")]
    public string Uri { get; set; }
    [JsonProperty(PropertyName = "contentVersion")]
    public string ContentVersion { get; set; }
  }

  public class ArmTemplateDeploymentRequestParameters {
    [Required]
    [JsonProperty(PropertyName = "AdminUsername")]
    public TemplateParameter AdminUsername { get; set; }
    [Required]
    [JsonProperty(PropertyName = "adminPassword")]
    public TemplateParameter AdminPassword { get; set; }
    [Required]
    [JsonProperty(PropertyName = "dnsLabelPrefix")]
    public TemplateParameter DnsLabel{ get; set; }
    [JsonProperty(PropertyName = "ubuntuOSVersion")]
    public TemplateParameter UbuntuOsVersion { get; set; }
    [JsonProperty(PropertyName = "windowsOSVersion")]
    public TemplateParameter WindowsOsVersion { get; set; }
  }

  public class TemplateParameter {
    [JsonProperty(PropertyName = "value")]
    public string Value { get; set; }
  }
}