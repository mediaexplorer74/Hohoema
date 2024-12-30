// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.OnAirStreams.CategoryExtensions
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;

#nullable disable
namespace Mntone.Nico2.Live.OnAirStreams
{
  internal static class CategoryExtensions
  {
    public static string ToCategoryString(this Category category)
    {
      switch (category)
      {
        case Category.General:
          return "common";
        case Category.Challenge:
          return "try";
        case Category.Game:
          return "live";
        case Category.Introduction:
          return "req";
        case Category.FaceOut:
          return "face";
        case Category.Encounter:
          return "totu";
        case Category.Adult:
          return "r18";
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}
