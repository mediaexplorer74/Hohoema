// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Mylist.Options
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Xml.Serialization;

#nullable disable
namespace Mntone.Nico2.Mylist
{
  [XmlRoot(ElementName = "options")]
  public class Options
  {
    [XmlAttribute(AttributeName = "mobile")]
    public string Mobile { get; set; }

    [XmlAttribute(AttributeName = "sun")]
    public string Sun { get; set; }

    [XmlAttribute(AttributeName = "large_thumbnail")]
    public string Large_thumbnail { get; set; }
  }
}
