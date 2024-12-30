// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Dmc.Owner
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Dmc
{
  [DataContract]
  public class Owner
  {
    [DataMember(Name = "id")]
    public string Id { get; set; }

    [DataMember(Name = "nickname")]
    public string Nickname { get; set; }

    [DataMember(Name = "stampExp")]
    public string StampExp { get; set; }

    [DataMember(Name = "iconURL")]
    public string IconURL { get; set; }

    [DataMember(Name = "nicoliveInfo")]
    public object NicoliveInfo { get; set; }

    [DataMember(Name = "channelInfo")]
    public object ChannelInfo { get; set; }

    [DataMember(Name = "isUserVideoPublic")]
    public bool IsUserVideoPublic { get; set; }

    [DataMember(Name = "isUserMyVideoPublic")]
    public bool IsUserMyVideoPublic { get; set; }

    [DataMember(Name = "isUserOpenListPublic")]
    public bool IsUserOpenListPublic { get; set; }
  }
}
