// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Users.Video.VideoData
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;

#nullable disable
namespace Mntone.Nico2.Users.Video
{
  public class VideoData
  {
    public string VideoId { get; set; }

    public string Title { get; set; }

    public DateTime SubmitTime { get; set; }

    public Uri ThumbnailUrl { get; set; }

    public string Description { get; set; }

    public TimeSpan Length { get; set; }
  }
}
