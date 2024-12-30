// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.WatchAPI.OwnerChannelInfo
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.WatchAPI
{
  public class OwnerChannelInfo
  {
    [DataMember(Name = "id")]
    public int? id { get; set; }

    [DataMember(Name = "communityId")]
    public int? communityId { get; set; }

    [DataMember(Name = "name")]
    public string name { get; set; }

    [DataMember(Name = "description")]
    public string description { get; set; }

    [DataMember(Name = "isFree")]
    public bool? isFree { get; set; }

    [DataMember(Name = "screenName")]
    public string screenName { get; set; }

    [DataMember(Name = "ownerName")]
    public string ownerName { get; set; }

    [DataMember(Name = "price")]
    public int? price { get; set; }

    [DataMember(Name = "bodyPrice")]
    public int? bodyPrice { get; set; }

    [DataMember(Name = "url")]
    public string url { get; set; }

    [DataMember(Name = "thumbnailUrl")]
    public string thumbnailUrl { get; set; }

    [DataMember(Name = "thumbnailSmallUrl")]
    public string thumbnailSmallUrl { get; set; }

    [DataMember(Name = "canAdmit")]
    public bool? canAdmit { get; set; }
  }
}
