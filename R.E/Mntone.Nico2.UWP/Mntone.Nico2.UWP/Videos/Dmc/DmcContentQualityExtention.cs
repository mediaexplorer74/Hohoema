// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Dmc.DmcContentQualityExtention
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;

#nullable disable
namespace Mntone.Nico2.Videos.Dmc
{
  public static class DmcContentQualityExtention
  {
    public static bool QualityIdSameVideoQuality(this DmcVideoQuality quality, string qualityId)
    {
      string[] strArray = qualityId.Split('_');
      string str = strArray[2];
      switch (strArray[3])
      {
        case "720p":
          return quality == DmcVideoQuality.High;
        case "540p":
          return quality == DmcVideoQuality.Midium;
        case "480p":
          return quality == DmcVideoQuality.Midium;
        case "360p":
          return quality == DmcVideoQuality.Midium || quality == DmcVideoQuality.Low || quality == DmcVideoQuality.Mobile;
        default:
          throw new NotSupportedException(qualityId + " のDmcサーバーの動画コンテンツはサポートしていません。");
      }
    }
  }
}
