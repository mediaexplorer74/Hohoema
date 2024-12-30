// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Dmc.Lead
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Mntone.Nico2.Videos.WatchAPI;
using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Dmc
{
  [DataContract]
  public class Lead
  {
    [DataMember(Name = "tagRelatedMarquee")]
    public TagRelatedMarquee TagRelatedMarquee { get; set; }

    [DataMember(Name = "tagRelatedBanner")]
    public TagRelatedBanner TagRelatedBanner { get; set; }

    [DataMember(Name = "nicosdkApplicationBanner")]
    public object NicosdkApplicationBanner { get; set; }

    [DataMember(Name = "videoEndBannerIn")]
    public object VideoEndBannerIn { get; set; }

    [DataMember(Name = "videoEndOverlay")]
    public object VideoEndOverlay { get; set; }
  }
}
