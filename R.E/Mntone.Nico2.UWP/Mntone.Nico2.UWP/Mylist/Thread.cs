// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Mylist.Thread
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Xml.Serialization;

#nullable disable
namespace Mntone.Nico2.Mylist
{
  [XmlRoot(ElementName = "thread")]
  public class Thread
  {
    [XmlElement(ElementName = "id")]
    public string Id { get; set; }

    [XmlElement(ElementName = "num_res")]
    public string Num_res { get; set; }

    [XmlElement(ElementName = "summary")]
    public string Summary { get; set; }
  }
}
