// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.CommunityTypeExtensions
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;

#nullable disable
namespace Mntone.Nico2.Live
{
  internal static class CommunityTypeExtensions
  {
    public static CommunityType ToCommunityType(this string value)
    {
      switch (value)
      {
        case "official":
          return CommunityType.Official;
        case "community":
          return CommunityType.Community;
        case "channel":
          return CommunityType.Channel;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public static string ToCommunityTypeString(this CommunityType value)
    {
      switch (value)
      {
        case CommunityType.Official:
          return "official";
        case CommunityType.Community:
          return "community";
        case CommunityType.Channel:
          return "channel";
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}
