// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Users.Follow.Watchitem
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Users.Follow
{
  [DataContract]
  public class Watchitem
  {
    [DataMember(Name = "item_type")]
    public string item_type { get; set; }

    [DataMember(Name = "item_id")]
    public string item_id { get; set; }

    [DataMember(Name = "item_data")]
    public ItemData item_data { get; set; }

    [DataMember(Name = "create_time")]
    public int create_time { get; set; }

    [DataMember(Name = "update_time")]
    public int update_time { get; set; }
  }
}
