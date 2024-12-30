// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Searches.Community.CommunitySearchSortExtention
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;

#nullable disable
namespace Mntone.Nico2.Searches.Community
{
  public static class CommunitySearchSortExtention
  {
    public static string ToShortString(this CommunitySearchSort sort)
    {
      switch (sort)
      {
        case CommunitySearchSort.CreatedAt:
          return "c";
        case CommunitySearchSort.UpdateAt:
          return "u";
        case CommunitySearchSort.CommunityLevel:
          return "l";
        case CommunitySearchSort.VideoCount:
          return "t";
        case CommunitySearchSort.MemberCount:
          return "m";
        default:
          throw new NotSupportedException();
      }
    }
  }
}
