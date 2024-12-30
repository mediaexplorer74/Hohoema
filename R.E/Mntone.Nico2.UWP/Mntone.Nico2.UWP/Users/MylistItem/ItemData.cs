// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Users.MylistItem.ItemData
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Users.MylistItem
{
  [DataContract]
  public class ItemData
  {
    [DataMember(Name = "video_id")]
    public string video_id { get; set; }

    [DataMember(Name = "title")]
    public string title { get; set; }

    [DataMember(Name = "thumbnail_url")]
    public string thumbnail_url { get; set; }

    [DataMember(Name = "first_retrieve")]
    public int first_retrieve { get; set; }

    [DataMember(Name = "update_time")]
    public int update_time { get; set; }

    [DataMember(Name = "view_counter")]
    public string view_counter { get; set; }

    [DataMember(Name = "mylist_counter")]
    public string mylist_counter { get; set; }

    [DataMember(Name = "num_res")]
    public string num_res { get; set; }

    [DataMember(Name = "group_type")]
    public string group_type { get; set; }

    [DataMember(Name = "length_seconds")]
    public string length_seconds { get; set; }

    [DataMember(Name = "deleted")]
    public string deleted { get; set; }

    [DataMember(Name = "last_res_body")]
    public string last_res_body { get; set; }

    [DataMember(Name = "watch_id")]
    public string watch_id { get; set; }
  }
}
