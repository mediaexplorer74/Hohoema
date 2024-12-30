// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Dmc.TopicItem
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Dmc
{
  [DataContract]
  public class TopicItem
  {
    [DataMember(Name = "id")]
    public string Id { get; set; }

    [DataMember(Name = "title")]
    public string Title { get; set; }

    [DataMember(Name = "thumbnailURL")]
    public string ThumbnailURL { get; set; }

    [DataMember(Name = "point")]
    public int Point { get; set; }

    [DataMember(Name = "isHigh")]
    public bool IsHigh { get; set; }

    [DataMember(Name = "elapsedTimeM")]
    public int ElapsedTimeM { get; set; }

    [DataMember(Name = "communityId")]
    public string CommunityId { get; set; }

    [DataMember(Name = "communityName")]
    public string CommunityName { get; set; }
  }
}
