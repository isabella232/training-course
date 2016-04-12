using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(pcsdk_validation.Startup))]

namespace pcsdk_validation {
  public partial class Startup {

    public void Configuration(IAppBuilder app) {
      ConfigureAuth(app);
    }
  }
}
