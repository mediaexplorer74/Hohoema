// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Mylist.MylistGroup.Mylistgroup
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Mntone.Nico2.Searches.Video;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Mylist.MylistGroup
{
  [DataContract]
  public class Mylistgroup
  {
    private uint? _ViewCount;
    private string _Description;
    private bool? _IsPublic;
    private uint? _Count;

    [DataMember(Name = "id")]
    public string Id { get; private set; }

    [DataMember(Name = "user_id")]
    public string UserId { get; private set; }

    [DataMember(Name = "view_counter")]
    public string __view_counter { get; private set; }

    public uint ViewCount
    {
      get
      {
        return !this._ViewCount.HasValue ? (this._ViewCount = new uint?(uint.Parse(this.__view_counter))).Value : this._ViewCount.Value;
      }
    }

    [DataMember(Name = "name")]
    public string Name { get; private set; }

    [DataMember(Name = "description")]
    public string __description { get; private set; }

    public string Description => this._Description ?? (this._Description = this.__description);

    [DataMember(Name = "public")]
    public string __isPublic { get; private set; }

    public bool IsPublic
    {
      get
      {
        return !this._IsPublic.HasValue ? (this._IsPublic = new bool?(this.__isPublic.ToBooleanFrom1())).Value : this._IsPublic.Value;
      }
    }

    [DataMember(Name = "default_sort")]
    public string default_sort { get; private set; }

    public MylistDefaultSort GetMylistDefaultSort()
    {
      return (MylistDefaultSort) int.Parse(this.default_sort);
    }

    [DataMember(Name = "icon_id")]
    public string icon_id { get; private set; }

    public IconType GetIconType() => (IconType) int.Parse(this.icon_id);

    [DataMember(Name = "sort_order")]
    public string sort_order { get; private set; }

    public Order GetSortOrder() => (Order) int.Parse(this.sort_order);

    [DataMember(Name = "update_time")]
    public DateTime UpdateTime { get; private set; }

    [DataMember(Name = "create_time")]
    public DateTime CreateTime { get; private set; }

    [DataMember(Name = "count")]
    public string __count { get; private set; }

    public uint Count
    {
      get
      {
        return !this._Count.HasValue ? (this._Count = new uint?(uint.Parse(this.__count))).Value : this._Count.Value;
      }
    }

    [DataMember(Name = "default_sort_method")]
    public string default_sort_method { get; private set; }

    [DataMember(Name = "default_sort_order")]
    public string default_sort_order { get; private set; }

    [DataMember(Name = "video_info")]
    [JsonConverter(typeof (SingleOrArrayConverter<VideoInfo>))]
    public IList<VideoInfo> SampleVideoInfoItems { get; set; }
  }
}
