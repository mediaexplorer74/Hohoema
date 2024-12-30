// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.NicoRepo.NicoRepoResponse
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.NicoRepo
{
  [DataContract]
  public class NicoRepoResponse
  {
    [DataMember(Name = "meta")]
    public NicoRepoMeta Meta { get; set; }

    [DataMember(Name = "data")]
    [JsonConverter(typeof (SingleOrArrayConverter<NicoRepoTimelineItem>))]
    public IList<NicoRepoTimelineItem> TimelineItems { get; set; }

    [DataMember(Name = "status")]
    public string Status { get; set; }

    public bool IsStatusOK => this.Status == "ok";

    public NicoRepoTimelineItem LastTimelineItem
    {
      get
      {
        IList<NicoRepoTimelineItem> timelineItems = this.TimelineItems;
        return timelineItems == null ? (NicoRepoTimelineItem) null : timelineItems.LastOrDefault<NicoRepoTimelineItem>();
      }
    }

    public static NicoRepoResponse ParseNicoRepoJson(string json)
    {
      try
      {
        return JsonConvert.DeserializeObject<NicoRepoResponse>(json);
      }
      catch (Exception ex)
      {
        return (NicoRepoResponse) null;
      }
    }
  }
}
