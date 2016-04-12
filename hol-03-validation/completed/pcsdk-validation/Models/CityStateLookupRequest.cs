using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pcsdk_validation.Models {
  /// <remarks/>
  [System.SerializableAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
  public partial class CityStateLookupRequest {

    private CityStateLookupRequestZipCode zipCodeField;

    private string uSERIDField;

    /// <remarks/>
    public CityStateLookupRequestZipCode ZipCode
    {
      get
      {
        return this.zipCodeField;
      }
      set
      {
        this.zipCodeField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string USERID
    {
      get
      {
        return this.uSERIDField;
      }
      set
      {
        this.uSERIDField = value;
      }
    }
  }

  /// <remarks/>
  [System.SerializableAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class CityStateLookupRequestZipCode {

    private string zip5Field;

    private byte idField;

    /// <remarks/>
    public string Zip5
    {
      get
      {
        return this.zip5Field;
      }
      set
      {
        this.zip5Field = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte ID
    {
      get
      {
        return this.idField;
      }
      set
      {
        this.idField = value;
      }
    }
  }


}