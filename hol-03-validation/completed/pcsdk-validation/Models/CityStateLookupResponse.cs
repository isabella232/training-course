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
  public partial class CityStateLookupResponse {

    private CityStateLookupResponseZipCode zipCodeField;

    /// <remarks/>
    public CityStateLookupResponseZipCode ZipCode
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
  }

  /// <remarks/>
  [System.SerializableAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class CityStateLookupResponseZipCode {

    private ushort zip5Field;

    private string cityField;

    private string stateField;

    private byte idField;

    /// <remarks/>
    public ushort Zip5
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
    public string City
    {
      get
      {
        return this.cityField;
      }
      set
      {
        this.cityField = value;
      }
    }

    /// <remarks/>
    public string State
    {
      get
      {
        return this.stateField;
      }
      set
      {
        this.stateField = value;
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