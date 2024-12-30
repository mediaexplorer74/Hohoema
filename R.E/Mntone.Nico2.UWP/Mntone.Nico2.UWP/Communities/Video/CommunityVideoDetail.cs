// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Communities.Video.CommunityVideoDetail
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Mntone.Nico2.Communities.Detail;
using System;

#nullable disable
namespace Mntone.Nico2.Communities.Video
{
  public class CommunityVideoDetail : CommunityVideo
  {
    public DateTime PostedAt { get; set; }

    public uint ViewCount { get; set; }

    public uint CommentCount { get; set; }

    public uint MylistCount { get; set; }

    public Uri ThumbnailUri { get; set; }

    public bool IsCommunityOnly { get; set; }
  }
}
