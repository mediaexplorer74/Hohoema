// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Dictionaries.CategoryExtensions
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;

#nullable disable
namespace Mntone.Nico2.Dictionaries
{
  internal static class CategoryExtensions
  {
    public static char ToCategoryChar(this Category category)
    {
      switch (category)
      {
        case Category.Word:
          return 'a';
        case Category.Video:
          return 'v';
        case Category.Live:
          return 'l';
        case Category.Community:
          return 'c';
        case Category.User:
          return 'u';
        case Category.Goods:
          return 'i';
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public static Category ToCategory(this char categoryString)
    {
      switch (categoryString)
      {
        case 'a':
          return Category.Word;
        case 'c':
          return Category.Community;
        case 'i':
          return Category.Goods;
        case 'l':
          return Category.Live;
        case 'u':
          return Category.User;
        case 'v':
          return Category.Video;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}
