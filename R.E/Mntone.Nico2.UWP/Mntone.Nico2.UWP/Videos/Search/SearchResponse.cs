// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Search.SearchResponse
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Search
{
  [DataContract]
  public class SearchResponse
  {
    [DataMember(Name = "list")]
    public IList<ListItem> list { get; set; }

    [DataMember(Name = "count")]
    public int count { get; set; }

    [DataMember(Name = "has_ng_video_for_adsense_on_listing")]
    public bool has_ng_video_for_adsense_on_listing { get; set; }

    [DataMember(Name = "related_tags")]
    public IList<string> related_tags { get; set; }

    [DataMember(Name = "page")]
    public int page { get; set; }

    [DataMember(Name = "status")]
    public string status { get; set; }

    public bool IsStatusOK => this.status == "ok";
  }
}
