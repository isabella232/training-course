using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

using System.Web.Mvc;

namespace azurearm.Models {
  public class AzureResourceGroupsViewModel {
    public string AzureSubscriptionId { get; set; }
    public AzureResourceGroup ResourceGroup { get; set; }
    public IEnumerable<AzureResourceGroup> ResourceGroups { get; set; }

    [DisplayName("Resource Group Name")]
    public string NewResourceGroupName { get; set; }
    public string Location { get; set; }
    public IEnumerable<SelectListItem> AzureRegions { get; set; }
  }
}