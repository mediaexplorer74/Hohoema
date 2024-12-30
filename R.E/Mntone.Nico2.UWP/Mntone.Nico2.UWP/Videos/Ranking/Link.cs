// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Ranking.Link
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Xml.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Ranking
{
  [XmlRoot(ElementName = "link", Namespace = "http://www.w3.org/2005/Atom")]
  public class Link
  {
    [XmlAttribute(AttributeName = "rel")]
    public string Rel { get; set; }

    [XmlAttribute(AttributeName = "type")]
    public string Type { get; set; }

    [XmlAttribute(AttributeName = "href")]
    public string Href { get; set; }
  }
}
