// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Communities.Live.Community
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Communities.Live
{
  [DataContract]
  public class Community
  {
    [DataMember(Name = "id")]
    public string Id { get; set; }

    [DataMember(Name = "name")]
    public string Name { get; set; }

    [DataMember(Name = "channel_id")]
    public string ChannelId { get; set; }

    public bool HasChannelId => !string.IsNullOrEmpty(this.ChannelId);

    [DataMember(Name = "global_id")]
    public string GlobalId { get; set; }

    [DataMember(Name = "thumbnail")]
    public string Thumbnail { get; set; }

    [DataMember(Name = "thumbnail_small")]
    public string ThumbnailSmall { get; set; }
  }
}
