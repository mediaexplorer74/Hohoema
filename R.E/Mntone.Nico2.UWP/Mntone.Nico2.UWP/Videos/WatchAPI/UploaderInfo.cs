// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.WatchAPI.UploaderInfo
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.WatchAPI
{
  public class UploaderInfo
  {
    [DataMember(Name = "id")]
    public string id { get; set; }

    [DataMember(Name = "nickname")]
    public string nickname { get; set; }

    [DataMember(Name = "stamp_exp")]
    public string stamp_exp { get; set; }

    [DataMember(Name = "icon_url")]
    public string icon_url { get; set; }

    [DataMember(Name = "is_favorited")]
    public bool is_favorited { get; set; }

    [DataMember(Name = "is_uservideo_public")]
    public bool is_uservideo_public { get; set; }

    [DataMember(Name = "is_user_myvideo_public")]
    public bool is_user_myvideo_public { get; set; }

    [DataMember(Name = "is_user_openlist_public")]
    public bool is_user_openlist_public { get; set; }
  }
}
