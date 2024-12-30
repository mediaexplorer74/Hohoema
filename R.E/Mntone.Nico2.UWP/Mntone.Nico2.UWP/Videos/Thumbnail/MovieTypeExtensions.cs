// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Thumbnail.MovieTypeExtensions
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;

#nullable disable
namespace Mntone.Nico2.Videos.Thumbnail
{
  internal static class MovieTypeExtensions
  {
    public static MovieType ToMovieType(this string value)
    {
      switch (value)
      {
        case "flv":
          return MovieType.Flv;
        case "mp4":
          return MovieType.Mp4;
        case "swf":
          return MovieType.Swf;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}
