// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Comment.PostComment
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Xml.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Comment
{
  [XmlRoot(ElementName = "chat")]
  public class PostComment
  {
    [XmlAttribute]
    public string thread { get; set; }

    [XmlAttribute]
    public string vpos { get; set; }

    [XmlAttribute]
    public string mail { get; set; }

    [XmlAttribute]
    public string ticket { get; set; }

    [XmlAttribute]
    public string user_id { get; set; }

    [XmlAttribute]
    public string postkey { get; set; }

    [XmlAttribute]
    public string premium { get; set; }

    [XmlText]
    public string comment { get; set; }
  }
}
