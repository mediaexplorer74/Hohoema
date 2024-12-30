// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Mylist.Video_info
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Xml.Serialization;

#nullable disable
namespace Mntone.Nico2.Mylist
{
  [XmlRoot(ElementName = "video_info")]
  public class Video_info
  {
    [XmlElement(ElementName = "video")]
    public Video Video { get; set; }

    [XmlElement(ElementName = "thread")]
    public Thread Thread { get; set; }

    [XmlElement(ElementName = "mylist")]
    public Mntone.Nico2.Mylist.Mylist Mylist { get; set; }
  }
}
