// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Users.Video.Thumbnail
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Xml.Serialization;

#nullable disable
namespace Mntone.Nico2.Users.Video
{
  [XmlRoot(ElementName = "thumbnail", Namespace = "http://search.yahoo.com/mrss/")]
  public class Thumbnail
  {
    [XmlAttribute(AttributeName = "url")]
    public string Url { get; set; }

    [XmlAttribute(AttributeName = "width")]
    public string Width { get; set; }

    [XmlAttribute(AttributeName = "height")]
    public string Height { get; set; }
  }
}
