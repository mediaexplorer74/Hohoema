// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Searches.Mylist.MylistGroup
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Mntone.Nico2.Searches.Video;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Searches.Mylist
{
  [DataContract]
  public class MylistGroup
  {
    private string _Name;
    private string _Description;
    private uint? _ItemCount;
    private DateTime? _UpdateTime;

    [DataMember(Name = "id")]
    public string Id { get; private set; }

    [DataMember(Name = "name")]
    public string __name { get; private set; }

    public string Name => this._Name ?? (this._Name = this.__name);

    [DataMember(Name = "description")]
    public string __description { get; private set; }

    public string Description => this._Description ?? (this._Description = this.__description);

    [DataMember(Name = "thread_ids")]
    public string __thread_ids { get; private set; }

    [DataMember(Name = "item")]
    public string __itemCount { get; private set; }

    public uint ItemCount
    {
      get
      {
        return !this._ItemCount.HasValue ? (this._ItemCount = new uint?(uint.Parse(this.__itemCount))).Value : this._ItemCount.Value;
      }
    }

    [DataMember(Name = "update_time")]
    public string __update_time { get; private set; }

    public DateTime UpdateTime
    {
      get
      {
        return !this._UpdateTime.HasValue ? (this._UpdateTime = new DateTime?(DateTime.Parse(this.__update_time))).Value : this._UpdateTime.Value;
      }
    }

    [DataMember(Name = "video_info")]
    [JsonConverter(typeof (SingleOrArrayConverter<VideoInfo>))]
    public IList<VideoInfo> VideoInfoItems { get; private set; }
  }
}
