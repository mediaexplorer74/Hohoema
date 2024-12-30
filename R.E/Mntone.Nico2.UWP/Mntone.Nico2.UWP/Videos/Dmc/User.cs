// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Dmc.User
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Dmc
{
  [DataContract]
  public class User
  {
    [DataMember(Name = "user_id")]
    public int UserId { get; set; }

    [DataMember(Name = "is_premium")]
    public bool IsPremium { get; set; }

    [DataMember(Name = "nickname")]
    public string Nickname { get; set; }
  }
}
