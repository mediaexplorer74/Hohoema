// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Comment.ViewCounter
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Xml.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Comment
{
  [XmlRoot(ElementName = "view_counter")]
  public class ViewCounter
  {
    [XmlAttribute(AttributeName = "id")]
    public string Id { get; set; }

    [XmlAttribute(AttributeName = "mylist")]
    public string Mylist { get; set; }

    [XmlAttribute(AttributeName = "video")]
    public string Video { get; set; }
  }
}
