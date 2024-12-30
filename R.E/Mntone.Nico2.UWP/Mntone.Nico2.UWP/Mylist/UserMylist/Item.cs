// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Mylist.UserMylist.Item
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Xml.Serialization;

#nullable disable
namespace Mntone.Nico2.Mylist.UserMylist
{
  [XmlRoot(ElementName = "item")]
  public class Item
  {
    [XmlElement(ElementName = "title")]
    public string Title { get; set; }

    [XmlElement(ElementName = "link")]
    public string Link { get; set; }

    [XmlElement(ElementName = "guid")]
    public Guid Guid { get; set; }

    [XmlElement(ElementName = "pubDate")]
    public string PubDate { get; set; }

    [XmlElement(ElementName = "description")]
    public string Description { get; set; }

    [XmlElement(ElementName = "thumbnail", Namespace = "http://search.yahoo.com/mrss/")]
    public Thumbnail Thumbnail { get; set; }
  }
}
