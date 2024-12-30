// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.WatchAPI.MorningPremium
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.WatchAPI
{
  public class MorningPremium
  {
    [DataMember(Name = "status")]
    public string status { get; set; }

    [DataMember(Name = "timing")]
    public string timing { get; set; }

    [DataMember(Name = "from_top")]
    public bool from_top { get; set; }
  }
}
