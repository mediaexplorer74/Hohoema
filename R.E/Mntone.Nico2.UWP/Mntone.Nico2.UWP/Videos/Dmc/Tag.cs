// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Dmc.Tag
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Dmc
{
  [DataContract]
  public class Tag
  {
    [DataMember(Name = "id")]
    public string Id { get; set; }

    [DataMember(Name = "name")]
    public string Name { get; set; }

    [DataMember(Name = "isCategory")]
    public bool IsCategory { get; set; }

    [DataMember(Name = "isCategoryCandidate")]
    public object IsCategoryCandidate { get; set; }

    [DataMember(Name = "isDictionaryExists")]
    public bool IsDictionaryExists { get; set; }

    [DataMember(Name = "isLocked")]
    public bool IsLocked { get; set; }
  }
}
