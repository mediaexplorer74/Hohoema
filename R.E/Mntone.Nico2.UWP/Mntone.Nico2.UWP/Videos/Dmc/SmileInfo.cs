// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Dmc.SmileInfo
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Dmc
{
  [DataContract]
  public class SmileInfo
  {
    [DataMember(Name = "url")]
    public string Url { get; set; }

    [DataMember(Name = "isSlowLine")]
    public bool IsSlowLine { get; set; }

    [DataMember(Name = "currentQualityId")]
    public string CurrentQualityId { get; set; }

    [DataMember(Name = "qualityIds")]
    public IList<string> QualityIds { get; set; }
  }
}
