using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace azurearm.Models {
  public class AzureResourceGroupItem {
    public string Id { get; set; }

    [DisplayName("Resource Type")]
    public string Type { get; set; }

    public string Name { get; set; }

    public string Location { get; set; }
  }
}