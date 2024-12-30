// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Mylist.MylistGroupResponse
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Xml.Serialization;

#nullable disable
namespace Mntone.Nico2.Mylist
{
  [XmlRoot(ElementName = "nicovideo_mylistgroup_response")]
  public class MylistGroupResponse
  {
    [XmlElement(ElementName = "mylistgroup")]
    public MylistGroupDetail Mylistgroup { get; set; }

    [XmlAttribute(AttributeName = "status")]
    public string Status { get; set; }
  }
}
