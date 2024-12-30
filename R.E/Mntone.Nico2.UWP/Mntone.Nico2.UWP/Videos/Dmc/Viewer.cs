// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Dmc.Viewer
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Dmc
{
  [DataContract]
  public class Viewer
  {
    [DataMember(Name = "id")]
    public int Id { get; set; }

    [DataMember(Name = "nickname")]
    public string Nickname { get; set; }

    [DataMember(Name = "prefecture")]
    public int? Prefecture { get; set; }

    [DataMember(Name = "sex")]
    public int? Sex { get; set; }

    [DataMember(Name = "age")]
    public int? Age { get; set; }

    [DataMember(Name = "isPremium")]
    public bool IsPremium { get; set; }

    [DataMember(Name = "isPrivileged")]
    public bool? IsPrivileged { get; set; }

    [DataMember(Name = "isPostLocked")]
    public bool? IsPostLocked { get; set; }

    [DataMember(Name = "isHtrzm")]
    public bool? IsHtrzm { get; set; }

    [DataMember(Name = "isTwitterConnection")]
    public bool? IsTwitterConnection { get; set; }
  }
}
