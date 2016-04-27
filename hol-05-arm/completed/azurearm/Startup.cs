using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(azurearm.Startup))]

namespace azurearm {
  public partial class Startup {
    public void Configuration(IAppBuilder app) {
      ConfigureAuth(app);
    }
  }
}
