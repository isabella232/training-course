using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.Web.Mvc;

namespace azurearm.Models {
  public class AzureVirtualMachineViewModel {
    public string ResourceGroup { get; set; }
    [DisplayName("VM Type")]
    public string VirtualMachineType { get; set; }
    [DisplayName("Adminstrator Username")]
    public string AdminUsername { get; set; }
    [DisplayName("Administrator Password")]
    public string AdminPassword { get; set; }
    [DisplayName("DNS Name")]
    public string DnsName { get; set; }
    public List<SelectListItem> VirtualMachineOptions { get; set; }

    public AzureVirtualMachineViewModel() {
      VirtualMachineOptions = new List<SelectListItem>();
    }
  }
}