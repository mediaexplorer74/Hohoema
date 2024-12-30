// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Mylist.Video
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Xml.Serialization;

#nullable disable
namespace Mntone.Nico2.Mylist
{
  [XmlRoot(ElementName = "video")]
  public class Video
  {
    [XmlElement(ElementName = "id")]
    public string Id { get; set; }

    [XmlElement(ElementName = "deleted")]
    public string Deleted { get; set; }

    [XmlElement(ElementName = "title")]
    public string Title { get; set; }

    [XmlElement(ElementName = "length_in_seconds")]
    public string Length_in_seconds { get; set; }

    [XmlElement(ElementName = "thumbnail_url")]
    public string Thumbnail_url { get; set; }

    [XmlElement(ElementName = "upload_time")]
    public string Upload_time { get; set; }

    [XmlElement(ElementName = "first_retrieve")]
    public string First_retrieve { get; set; }

    [XmlElement(ElementName = "view_counter")]
    public string View_counter { get; set; }

    [XmlElement(ElementName = "mylist_counter")]
    public string Mylist_counter { get; set; }

    [XmlElement(ElementName = "option_flag_community")]
    public string Option_flag_community { get; set; }

    [XmlElement(ElementName = "option_flag_nicowari")]
    public string Option_flag_nicowari { get; set; }

    [XmlElement(ElementName = "option_flag_middle_thumbnail")]
    public string Option_flag_middle_thumbnail { get; set; }

    [XmlElement(ElementName = "width")]
    public string Width { get; set; }

    [XmlElement(ElementName = "height")]
    public string Height { get; set; }

    [XmlElement(ElementName = "vita_playable")]
    public string Vita_playable { get; set; }

    [XmlElement(ElementName = "ppv_video")]
    public string Ppv_video { get; set; }

    [XmlElement(ElementName = "provider_type")]
    public string Provider_type { get; set; }

    [XmlElement(ElementName = "options")]
    public Options Options { get; set; }
  }
}
