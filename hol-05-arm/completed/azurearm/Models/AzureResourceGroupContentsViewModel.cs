using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace azurearm.Models {
  public class AzureResourceGroupContentsViewModel {
    [DisplayName("Resource Group")]
    public string ResourceGroup { get; set; }
    public IEnumerable<AzureResourceGroupItem> Contents { get; set; }
  }
}