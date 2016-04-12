using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using pcsdk_validation.Models;

namespace pcsdk_validation.Controllers {
  public class CustomerController : Controller {
    // GET: Customer
    [Authorize]
    public async Task<ActionResult> Index() {
      CustomerViewModel viewModel = new CustomerViewModel();
      viewModel.Customers = await MyCustomerRepository.GetCustomers();
      return View(viewModel);
    }

    // GET: Customer/Edit/5
    [Authorize]
    public async Task<ActionResult> Edit(string id) {
      if (string.IsNullOrEmpty(id)) {
        return RedirectToAction("Index");
      } else {
        CustomerViewModel viewModel = new CustomerViewModel();

        // fetch customer & add to viewmodel
        var customer = await MyCustomerRepository.GetCustomer(id);
        viewModel.Customer = customer;

        // add lookup to viewmodel
        var countries = await Services.CountryService.List();
        viewModel.Countries = countries.Select(x => new SelectListItem {
          Value = x.Iso2Code,
          Text = x.Name
        });

        return View(viewModel);
      }
    }

    // POST: Customer/Edit/5
    [HttpPost]
    [Authorize]
    public async Task<ActionResult> Edit(CustomerViewModel viewModel) {
      var validator = await Utilities.CustomerValidator.CreateAsync(viewModel.Customer);

      if (!validator.IsValid) {
        // set validation errors on view model
        viewModel.ValidationErrors = validator.ValidationErrors;

        // add lookups to viewmodel
        var countries = await Services.CountryService.List();
        viewModel.Countries = countries.Select(x => new SelectListItem {
          Value = x.Iso2Code,
          Text = x.Name
        });

        return View(viewModel);
      } else {
        // TODO add logic to save customer billing profile
        return RedirectToAction("Index");
      }
    }

  }
}
