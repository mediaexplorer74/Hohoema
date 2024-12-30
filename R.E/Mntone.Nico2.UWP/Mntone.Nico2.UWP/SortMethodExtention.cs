// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.SortMethodExtention
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;

#nullable disable
namespace Mntone.Nico2
{
  public static class SortMethodExtention
  {
    public static char ToChar(this Sort method)
    {
      switch (method)
      {
        case Sort.NewComment:
          return 'n';
        case Sort.ViewCount:
          return 'v';
        case Sort.MylistCount:
          return 'm';
        case Sort.CommentCount:
          return 'r';
        case Sort.FirstRetrieve:
          return 'f';
        case Sort.Length:
          return 'l';
        case Sort.Popurarity:
          return 'h';
        case Sort.MylistPopurarity:
          return 'c';
        case Sort.UpdateTime:
          return 'n';
        case Sort.Relation:
          return 's';
        case Sort.VideoCount:
          return 'i';
        default:
          throw new NotSupportedException(string.Format("not support {0}.{1}", (object) "Sort", (object) method.ToString()));
      }
    }

    public static string ToShortString(this Sort method) => method.ToChar().ToString();
  }
}
