// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Ranking.Channel
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Collections.Generic;
using System.Xml.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Ranking
{
  [XmlRoot(ElementName = "channel")]
  public class Channel
  {
    [XmlElement(ElementName = "title")]
    public string Title { get; set; }

    [XmlElement(ElementName = "link")]
    public List<string> Link { get; set; }

    [XmlElement(ElementName = "description")]
    public string Description { get; set; }

    [XmlElement(ElementName = "pubDate")]
    public string PubDate { get; set; }

    [XmlElement(ElementName = "lastBuildDate")]
    public string LastBuildDate { get; set; }

    [XmlElement(ElementName = "generator")]
    public string Generator { get; set; }

    [XmlElement(ElementName = "creator", Namespace = "http://purl.org/dc/elements/1.1/")]
    public string Creater { get; set; }

    [XmlElement(ElementName = "language")]
    public string Language { get; set; }

    [XmlElement(ElementName = "copyright")]
    public string Copyright { get; set; }

    [XmlElement(ElementName = "docs")]
    public string Docs { get; set; }

    [XmlElement(ElementName = "item")]
    public List<NiconicoVideoRssItem> Items { get; set; }
  }
}
