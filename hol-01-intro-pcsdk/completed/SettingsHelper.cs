using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace partnercentersdk {
  class SettingsHelper {
    #region azure ad tenant details
    public static string AadAuthority
    {
      get { return "https://login.microsoftonline.com/common"; }
    }

    public static string AadTenantId
    {
      get { return ConfigurationManager.AppSettings["aad:tenant-id"]; }
    }
    #endregion

    #region azure ad application details
    public static string ClientId
    {
      get { return ConfigurationManager.AppSettings["aad:client-id"]; }
    }
    public static string ClientSecret
    {
      get { return ConfigurationManager.AppSettings["aad:client-secret"]; }
    }
    #endregion

    #region user details
    public static string UserId
    {
      get { return ConfigurationManager.AppSettings["partner-user"]; }
    }
    public static string UserPassword
    {
      get { return ConfigurationManager.AppSettings["partner-password"]; }
    }
    #endregion

    public static string PartnerCenterApiEndpoint
    {
      get { return ConfigurationManager.AppSettings["partnercenter-endpoint"]; }
    }
    public static string PartnerCenterApiResourceId
    {
      get { return ConfigurationManager.AppSettings["partnercenter-resource"]; }
    }

  }
}
