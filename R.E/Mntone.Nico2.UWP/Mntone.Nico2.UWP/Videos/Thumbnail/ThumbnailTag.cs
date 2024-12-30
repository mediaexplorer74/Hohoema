// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Thumbnail.ThumbnailTag
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Runtime.Serialization;
using System.Xml.Linq;

#nullable disable
namespace Mntone.Nico2.Videos.Thumbnail
{
  [DataContract]
  public sealed class ThumbnailTag
  {
    internal ThumbnailTag(XElement tagXml)
    {
      this.Category = tagXml.GetNamedAttributeText("category").ToBooleanFrom1();
      this.Lock = tagXml.GetNamedAttributeText("lock").ToBooleanFrom1();
      this.Value = tagXml.GetText();
    }

    public ThumbnailTag()
    {
    }

    [DataMember]
    public bool Category { get; private set; }

    [DataMember]
    public bool Lock { get; private set; }

    [DataMember]
    public string Value { get; private set; }
  }
}
