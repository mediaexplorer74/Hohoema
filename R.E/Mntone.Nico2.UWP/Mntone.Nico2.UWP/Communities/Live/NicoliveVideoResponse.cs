// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Communities.Live.NicoliveVideoResponse
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Communities.Live
{
  [DataContract]
  public class NicoliveVideoResponse
  {
    private int? _Count;
    private int? _TotalCount;

    [DataMember(Name = "video_info")]
    [JsonConverter(typeof (SingleOrArrayConverter<LiveVideo>))]
    public List<NicoliveVideoInfo> VideoInfo { get; set; }

    [DataMember(Name = "count")]
    public string __Count { get; set; }

    public int Count
    {
      get
      {
        return !this._Count.HasValue ? (this._Count = new int?(int.Parse(this.__Count))).Value : this._Count.Value;
      }
    }

    [DataMember(Name = "total_count")]
    public string __TotalCount { get; set; }

    public int TotalCount
    {
      get
      {
        return !this._TotalCount.HasValue ? (this._TotalCount = new int?(int.Parse(this.__TotalCount))).Value : this._TotalCount.Value;
      }
    }

    [DataMember(Name = "@status")]
    public string Status { get; set; }

    public bool IsStatusOK => this.Status == "ok";
  }
}
