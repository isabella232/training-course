using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Threading.Tasks;
using System.Web.Mvc;
using azurearm.Models;
using azurearm.Utilities;

namespace azurearm.Controllers {
  public class ResourceGroupsController : Controller {

    [Authorize]
    public async Task<ActionResult> Index() {
      // build view model
      AzureResourceGroupsViewModel viewModel = new AzureResourceGroupsViewModel {
        AzureSubscriptionId = SettingsHelper.AzureSubscriptionId,
        ResourceGroups = await AzureResourceGroupRepository.GetAzureResourceGroups()
      };

      return View(viewModel);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult> CreateRG() {
      // create instance of viewmodel
      AzureResourceGroupsViewModel viewModel = new AzureResourceGroupsViewModel {
        AzureSubscriptionId = SettingsHelper.AzureSubscriptionId
      };

      // add lookup
      var azureRegions = await Services.AzureRegionService.List();
      viewModel.AzureRegions = azureRegions.OrderBy(x => x.Name).Select(y => new SelectListItem {
        Value = y.Id,
        Text = y.Name
      });

      return View(viewModel);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> CreateRG(AzureResourceGroupsViewModel viewModel) {
      // create resource group
      await AzureResourceGroupRepository.CreateResourceGroup(viewModel.Location, viewModel.NewResourceGroupName);

      return RedirectToAction("Index");
    }

    [Authorize]
    public async Task<ActionResult> Contents(string groupName) {
      AzureResourceGroupContentsViewModel viewModel = new AzureResourceGroupContentsViewModel {
        ResourceGroup = groupName,
        Contents = await AzureResourceGroupRepository.GetResourceGroupVMs(groupName)
      };

      return View(viewModel);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult> CreateVM(string groupName) {
      AzureVirtualMachineViewModel viewModel = new AzureVirtualMachineViewModel {
        ResourceGroup = groupName
      };

      // add VM options
      viewModel.VirtualMachineOptions.Add(new SelectListItem {
        Value = "windows",
        Text = "Simple Windows VM (Standard D1)"
      });
      viewModel.VirtualMachineOptions.Add(new SelectListItem {
        Value = "linux",
        Text = "Simple Linux VM (Standard D1)"
      });

      return View(viewModel);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> CreateVM(AzureVirtualMachineViewModel viewModel) {

      // create the VM
      await AzureResourceGroupRepository.CreateTemplateDeployment(viewModel.ResourceGroup,
        viewModel.VirtualMachineType,
        viewModel.AdminUsername,
        viewModel.AdminPassword,
        viewModel.DnsName);

      return RedirectToAction("Index");
    }
  }
}