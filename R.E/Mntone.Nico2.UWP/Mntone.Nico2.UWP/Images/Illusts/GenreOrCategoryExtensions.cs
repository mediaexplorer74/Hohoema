// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Images.Illusts.GenreOrCategoryExtensions
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;

#nullable disable
namespace Mntone.Nico2.Images.Illusts
{
  internal static class GenreOrCategoryExtensions
  {
    public static string ToGenreAndCategoryString(this GenreOrCategory value)
    {
      switch (value)
      {
        case GenreOrCategory.Rate15:
          return "r15";
        case GenreOrCategory.Adult:
          return "g_adult";
        case GenreOrCategory.Creation:
          return "g_creation";
        case GenreOrCategory.Original:
          return "original";
        case GenreOrCategory.Portrait:
          return "portrait";
        case GenreOrCategory.FanArt:
          return "g_fanart";
        case GenreOrCategory.Anime:
          return "anime";
        case GenreOrCategory.Game:
          return "game";
        case GenreOrCategory.Character:
          return "character";
        case GenreOrCategory.Popular:
          return "g_popular";
        case GenreOrCategory.Toho:
          return "toho";
        case GenreOrCategory.Vocaloid:
          return "vocaloid";
        case GenreOrCategory.KanColle:
          return "kancolle";
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}
