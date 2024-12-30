// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Mylist.MylistGroup.MylistGroupVideoResponse
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Mylist.MylistGroup
{
  [DataContract]
  public class MylistGroupVideoResponse
  {
    [DataMember(Name = "count")]
    public string __count { get; set; }

    public uint GetCount() => uint.Parse(this.__count);

    [DataMember(Name = "total_count")]
    public string __total_count { get; private set; }

    public uint GetTotalCount() => uint.Parse(this.__total_count);

    [DataMember(Name = "video_info")]
    [JsonConverter(typeof (SingleOrArrayConverter<MylistVideoInfo>))]
    public IList<MylistVideoInfo> MylistVideoInfoItems { get; set; }

    [DataMember(Name = "@status")]
    public string status { get; set; }

    public bool IsOK => this.status == "ok";
  }
}
