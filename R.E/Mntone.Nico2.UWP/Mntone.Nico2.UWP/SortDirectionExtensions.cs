// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.SortDirectionExtensions
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;

#nullable disable
namespace Mntone.Nico2
{
  internal static class SortDirectionExtensions
  {
    public static char ToChar(this Order direction)
    {
      if (direction == Order.Ascending)
        return 'a';
      if (direction == Order.Descending)
        return 'd';
      throw new ArgumentOutOfRangeException();
    }

    public static string ToShortString(this Order direction)
    {
      if (direction == Order.Ascending)
        return "asc";
      if (direction == Order.Descending)
        return "desc";
      throw new ArgumentOutOfRangeException();
    }
  }
}
