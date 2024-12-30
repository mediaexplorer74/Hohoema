// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.OnAirStreams.SortTypeExtensions
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;

#nullable disable
namespace Mntone.Nico2.Live.OnAirStreams
{
  internal static class SortTypeExtensions
  {
    public static string ToSortTypeString(this SortType type)
    {
      switch (type)
      {
        case SortType.StartTime:
          return "start_time";
        case SortType.ViewCount:
          return "view_counter";
        case SortType.CommentCount:
          return "comment_num";
        case SortType.CommunityLevel:
          return "community_level";
        case SortType.CommunityCreateTime:
          return "community_create_time";
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}
