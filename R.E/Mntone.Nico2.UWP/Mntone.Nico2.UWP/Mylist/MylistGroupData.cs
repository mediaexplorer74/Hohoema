// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Mylist.MylistGroupData
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Mylist
{
  [DataContract]
  public class MylistGroupData
  {
    public const string DeflistGroupId = "0";

    public bool IsDeflist() => MylistGroupData.IsDeflist(this.Id);

    public static bool IsDeflist(string group_id) => group_id == null || group_id == "0";

    [DataMember(Name = "id")]
    public string Id { get; set; }

    [DataMember(Name = "user_id")]
    public string UserId { get; set; }

    [DataMember(Name = "name")]
    public string Name { get; set; }

    [DataMember(Name = "description")]
    public string Description { get; set; }

    [DataMember(Name = "public")]
    public string IsPublic { get; set; }

    [DataMember(Name = "icon_id")]
    public string IconId { get; set; }

    public List<Uri> ThumbnailUrls { get; set; }

    public int Count { get; set; }

    public bool GetIsPublic() => this.IsPublic.ToBooleanFrom1();

    public IconType GetIconType() => (IconType) int.Parse(this.IconId);
  }
}
