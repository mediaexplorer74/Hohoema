// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Communities.Info.OptionFlagDetails
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Communities.Info
{
  [DataContract]
  public class OptionFlagDetails
  {
    [DataMember(Name = "community_priv_user_auth")]
    public string CommunityPrivUserAuth { get; set; }

    [DataMember(Name = "community_icon_upload")]
    public string CommunityIconUpload { get; set; }
  }
}
