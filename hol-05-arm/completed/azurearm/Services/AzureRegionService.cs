using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace azurearm.Services {
  public class AzureRegion {
    public string Id { get; set; }
    public string Name { get; set; }
  }
  public class AzureRegionService {
    public async static Task<IEnumerable<AzureRegion>> List() {
      return new List<AzureRegion> {
        new AzureRegion { Id = "eastasia", Name="East Asia" },
        new AzureRegion { Id = "southeastasia", Name="Southeast Asia" },
        new AzureRegion { Id = "centralus", Name="Central US" },
        new AzureRegion { Id = "eastus", Name="East US" },
        new AzureRegion { Id = "eastus2", Name="East US 2" },
        new AzureRegion { Id = "westus", Name="West US" },
        new AzureRegion { Id = "northcentralus", Name="North Central US" },
        new AzureRegion { Id = "southcentralus", Name="South Central US" },
        new AzureRegion { Id = "northeurope", Name="North Europe" },
        new AzureRegion { Id = "westeurope", Name="West Europe" },
        new AzureRegion { Id = "japanwest", Name="Japan West" },
        new AzureRegion { Id = "japaneast", Name="Japan East" },
        new AzureRegion { Id = "brazilsouth", Name="Brazil South" },
        new AzureRegion { Id = "australiaeast", Name="Australia East" },
        new AzureRegion { Id = "australiasoutheast", Name="Australia Southeast" },
        new AzureRegion { Id = "southindia", Name="South India" },
        new AzureRegion { Id = "centralindia", Name="Central India" },
        new AzureRegion { Id = "westindia", Name="West India" },
        new AzureRegion { Id = "canadacentral", Name="Canada Central" },
        new AzureRegion { Id = "canadaeast", Name="Canada East" },
        new AzureRegion { Id = "uknorth", Name="UK North"  },
        new AzureRegion { Id = "uksouth2", Name="UK South 2" }
      };
    }
  }

}