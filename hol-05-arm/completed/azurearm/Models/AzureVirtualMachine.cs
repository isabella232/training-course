using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace azurearm.Models {
  public class AzureVirtualMachines {
    [JsonProperty(PropertyName = "value")]
    public AzureVirtualMachine[] Items { get; set; }
  }

  public class AzureVirtualMachine {
    public AzureVirtualMachineProperties properties { get; set; }
    public string id { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    public string location { get; set; }
  }

  public class AzureVirtualMachineProperties {
    public string provisioningState { get; set; }
    public Instanceview instanceView { get; set; }
    public Domainname domainName { get; set; }
    public Hardwareprofile hardwareProfile { get; set; }
    public Networkprofile networkProfile { get; set; }
    public Storageprofile storageProfile { get; set; }
    public Extension[] extensions { get; set; }
  }

  public class Instanceview {
    public string status { get; set; }
    public string powerState { get; set; }
    public object[] publicIpAddresses { get; set; }
    public string fullyQualifiedDomainName { get; set; }
  }

  public class Domainname {
    public string id { get; set; }
    public string name { get; set; }
    public string type { get; set; }
  }

  public class Hardwareprofile {
    public bool platformGuestAgent { get; set; }
    public string size { get; set; }
    public string deploymentName { get; set; }
    public string deploymentId { get; set; }
    public string deploymentLabel { get; set; }
    public bool deploymentLocked { get; set; }
  }

  public class Networkprofile {
    public Inputendpoint[] inputEndpoints { get; set; }
  }

  public class Inputendpoint {
    public string endpointName { get; set; }
    public int privatePort { get; set; }
    public int publicPort { get; set; }
    public string protocol { get; set; }
    public bool enableDirectServerReturn { get; set; }
  }

  public class Storageprofile {
    public Operatingsystemdisk operatingSystemDisk { get; set; }
  }

  public class Operatingsystemdisk {
    public string diskName { get; set; }
    public string caching { get; set; }
    public string operatingSystem { get; set; }
    public string sourceImageName { get; set; }
    public string vhdUri { get; set; }
    public Storageaccount storageAccount { get; set; }
  }

  public class Storageaccount {
    public string id { get; set; }
    public string name { get; set; }
    public string type { get; set; }
  }

  public class Extension {
    public string extension { get; set; }
    public string publisher { get; set; }
    public string version { get; set; }
    public string state { get; set; }
    public string referenceName { get; set; }
    public Parameters parameters { get; set; }
  }

  public class Parameters {
    public Public _public { get; set; }
  }

  public class Public {
    public string StorageAccount { get; set; }
    public string xmlCfg { get; set; }
  }

}