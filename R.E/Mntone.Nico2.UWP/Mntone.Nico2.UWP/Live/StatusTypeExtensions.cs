// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.StatusTypeExtensions
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;

#nullable disable
namespace Mntone.Nico2.Live
{
  internal static class StatusTypeExtensions
  {
    public static StatusType ToStatusType(this string value)
    {
      switch (value)
      {
        case "onair":
          return StatusType.OnAir;
        case "comingsoon":
          return StatusType.ComingSoon;
        case "closed":
          return StatusType.Closed;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public static string ToStatusTypeString(this StatusType value)
    {
      switch (value)
      {
        case StatusType.OnAir:
          return "onair";
        case StatusType.ComingSoon:
          return "comingsoon";
        case StatusType.Closed:
          return "closed";
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}
