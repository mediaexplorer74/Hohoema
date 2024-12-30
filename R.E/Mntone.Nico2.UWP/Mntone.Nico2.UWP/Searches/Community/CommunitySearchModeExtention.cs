// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Searches.Community.CommunitySearchModeExtention
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;

#nullable disable
namespace Mntone.Nico2.Searches.Community
{
  public static class CommunitySearchModeExtention
  {
    public static string ToShortString(this CommunitySearchMode mode)
    {
      if (mode == CommunitySearchMode.Keyword)
        return "s";
      if (mode == CommunitySearchMode.Tag)
        return "t";
      throw new NotSupportedException();
    }
  }
}
