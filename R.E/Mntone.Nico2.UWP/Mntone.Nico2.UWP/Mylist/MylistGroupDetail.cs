// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Mylist.MylistGroupDetail
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Collections.Generic;
using System.Xml.Serialization;

#nullable disable
namespace Mntone.Nico2.Mylist
{
  [XmlRoot(ElementName = "mylistgroup")]
  public class MylistGroupDetail
  {
    [XmlElement(ElementName = "id")]
    public string Id { get; set; }

    [XmlElement(ElementName = "user_id")]
    public string User_id { get; set; }

    [XmlElement(ElementName = "view_counter")]
    public string View_counter { get; set; }

    [XmlElement(ElementName = "name")]
    public string Name { get; set; }

    [XmlElement(ElementName = "description")]
    public string Description { get; set; }

    [XmlElement(ElementName = "public")]
    public string Public { get; set; }

    [XmlElement(ElementName = "default_sort")]
    public string Default_sort { get; set; }

    [XmlElement(ElementName = "icon_id")]
    public string Icon_id { get; set; }

    [XmlElement(ElementName = "sort_order")]
    public string Sort_order { get; set; }

    [XmlElement(ElementName = "update_time")]
    public string Update_time { get; set; }

    [XmlElement(ElementName = "create_time")]
    public string Create_time { get; set; }

    [XmlElement(ElementName = "count")]
    public string Count { get; set; }

    [XmlElement(ElementName = "default_sort_method")]
    public string Default_sort_method { get; set; }

    [XmlElement(ElementName = "default_sort_order")]
    public string Default_sort_order { get; set; }

    [XmlElement(ElementName = "video_info")]
    public List<Mntone.Nico2.Mylist.Video_info> Video_info { get; set; }
  }
}
