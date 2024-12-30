// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Searches.Live.NicoliveVideoResponse
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Searches.Live
{
  [DataContract]
  public class NicoliveVideoResponse
  {
    [DataMember(Name = "video_info")]
    [JsonConverter(typeof (SingleOrArrayConverter<Mntone.Nico2.Searches.Live.VideoInfo>))]
    public IList<Mntone.Nico2.Searches.Live.VideoInfo> VideoInfo { get; set; }

    [DataMember(Name = "count")]
    public string Count { get; set; }

    [DataMember(Name = "total_count")]
    public TotalCount TotalCount { get; set; }

    [DataMember(Name = "is_terminate")]
    public string __IsTerminate { get; set; }

    public bool IsTerminate => this.__IsTerminate.ToBooleanFrom1();

    [DataMember(Name = "tags")]
    public Tags Tags { get; set; }

    [DataMember(Name = "@status")]
    public string Status { get; set; }

    public bool IsStatusOK => this.Status == "ok";
  }
}
