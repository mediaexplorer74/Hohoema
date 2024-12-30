// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Mylist.IconTypeExtention
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using Windows.UI;

#nullable disable
namespace Mntone.Nico2.Mylist
{
  public static class IconTypeExtention
  {
    public static Color ToColor(this IconType iconType)
    {
      switch (iconType)
      {
        case IconType.Default:
          return Colors.LightYellow;
        case IconType.Cyan:
          return Colors.LightCyan;
        case IconType.SmokeWhite:
          return Colors.WhiteSmoke;
        case IconType.Dark:
          return Colors.DarkGray;
        case IconType.Red:
          return Colors.Red;
        case IconType.Orenge:
          return Colors.Orange;
        case IconType.Green:
          return Colors.Green;
        case IconType.SkyBlue:
          return Colors.SkyBlue;
        case IconType.Blue:
          return Colors.Blue;
        case IconType.Purple:
          return Colors.Purple;
        default:
          throw new NotSupportedException(string.Format("not support {0}.{1}", (object) "IconType", (object) iconType.ToString()));
      }
    }
  }
}
