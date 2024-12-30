// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Communities.Video.CommunityVideoResponse
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Collections.Generic;

#nullable disable
namespace Mntone.Nico2.Communities.Video
{
  public class CommunityVideoResponse
  {
    public uint VideoCount { get; set; }

    public List<CommunityVideoDetail> Videos { get; set; } = new List<CommunityVideoDetail>();
  }
}
