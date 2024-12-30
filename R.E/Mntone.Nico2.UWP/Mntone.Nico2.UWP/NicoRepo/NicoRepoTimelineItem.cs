// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.NicoRepo.NicoRepoTimelineItem
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.NicoRepo
{
  [DataContract]
  public class NicoRepoTimelineItem
  {
    [DataMember(Name = "id")]
    public string Id { get; set; }

    [DataMember(Name = "topic")]
    public string Topic { get; set; }

    [DataMember(Name = "createdAt")]
    public DateTime CreatedAt { get; set; }

    [DataMember(Name = "isVisible")]
    public bool IsVisible { get; set; }

    [DataMember(Name = "isMuted")]
    public bool IsMuted { get; set; }

    [DataMember(Name = "isDeletable")]
    public bool? IsDeletable { get; set; }

    [DataMember(Name = "muteContext")]
    public MuteContext MuteContext { get; set; }

    [DataMember(Name = "senderChannel")]
    public SenderChannel SenderChannel { get; set; }

    [DataMember(Name = "senderNiconicoUser")]
    public SenderNiconicoUser SenderNiconicoUser { get; set; }

    [DataMember(Name = "channelArticle")]
    public ChannelArticle ChannelArticle { get; set; }

    [DataMember(Name = "community")]
    public Community Community { get; set; }

    [DataMember(Name = "program")]
    public Program Program { get; set; }

    [DataMember(Name = "video")]
    public Video Video { get; set; }

    [DataMember(Name = "communityForFollower")]
    public CommunityForFollower CommunityForFollower { get; set; }

    [DataMember(Name = "memberOnlyVideo")]
    public MemberOnlyVideo MemberOnlyVideo { get; set; }
  }
}
