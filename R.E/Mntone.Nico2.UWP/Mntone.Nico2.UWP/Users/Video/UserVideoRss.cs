// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Users.Video.UserVideoRss
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Xml.Serialization;

#nullable disable
namespace Mntone.Nico2.Users.Video
{
  [XmlRoot(ElementName = "rss")]
  public class UserVideoRss
  {
    [XmlElement(ElementName = "channel")]
    public Channel Channel { get; set; }

    [XmlAttribute(AttributeName = "version")]
    public string Version { get; set; }

    [XmlAttribute(AttributeName = "dc", Namespace = "http://www.w3.org/2000/xmlns/")]
    public string Dc { get; set; }

    [XmlAttribute(AttributeName = "atom", Namespace = "http://www.w3.org/2000/xmlns/")]
    public string Atom { get; set; }

    [XmlAttribute(AttributeName = "media", Namespace = "http://www.w3.org/2000/xmlns/")]
    public string Media { get; set; }

    [XmlAttribute(AttributeName = "activity", Namespace = "http://www.w3.org/2000/xmlns/")]
    public string Activity { get; set; }
  }
}
