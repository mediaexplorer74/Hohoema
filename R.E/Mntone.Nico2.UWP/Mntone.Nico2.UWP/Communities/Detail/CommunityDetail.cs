// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Communities.Detail.CommunityDetail
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Mntone.Nico2.Searches.Community;
using System.Collections.Generic;

#nullable disable
namespace Mntone.Nico2.Communities.Detail
{
  public class CommunityDetail : NicoCommynity
  {
    public string OwnerUserName { get; set; }

    public string OwnerUserId { get; set; }

    public uint FollowerMaxCount { get; set; }

    public uint VideoMaxCount { get; set; }

    public string ProfielHtml { get; set; }

    public List<string> Tags { get; set; } = new List<string>();

    public List<CommunityNews> NewsList { get; private set; } = new List<CommunityNews>();

    public List<CommunityLiveInfo> CurrentLiveList { get; private set; } = new List<CommunityLiveInfo>();

    public List<LiveInfo> RecentLiveList { get; private set; } = new List<LiveInfo>();

    public List<LiveInfo> FutureLiveList { get; private set; } = new List<LiveInfo>();

    public List<CommunityVideo> VideoList { get; private set; } = new List<CommunityVideo>();

    public List<CommunityMember> SampleFollwers { get; private set; } = new List<CommunityMember>();

    public string PrivilegeDescription { get; set; }
  }
}
